using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PTC.ERP.Generator.UI.UIAppCode;

namespace SGRSalary.Classes
{
    public class ExcelOprations
    {
        public static DataTable ImportFromExcel()
        {
            DataTable dt = null;
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Excel Files|*.xlsx;";
            opd.FilterIndex = 1;
            opd.ShowDialog();
            if (!string.IsNullOrEmpty(opd.FileName))
            {
                if (!opd.SafeFileName.Contains(".xlsx"))
                {
                    MessageBox.Show("توجه : فایل اکسل انتخاب شده پسوند " + " ( xlsx. ) " + " را ندارد", "اخطار",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                dt = ReadExcel.ReadExcelToDataTable(opd.FileName);
                if (dt == null)
                    return null;
                //حذف ردیفهای خالی از دیتا تیبل
                dt = (dt.Rows.Count > 0)
                   ? dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field =>
                       field is System.DBNull ||
                       string.IsNullOrEmpty(field.ToStringEmpty()))).CopyToDataTable()
                   : dt.Copy();

                //dt.Columns.Add("RowNumber", typeof(int));
                //dt.Columns[dt.Columns.Count - 1].Caption = "شماره ردیف اکسل";
                //for (int i = 1; i <= dt.Rows.Count; i++) dt.Rows[i - 1]["RowNumber"] = i;

            }

            return dt;
        }

        public class ReadExcel
        {
            public static DataTable ReadExcelToDataTable(string filename)//, List<ModuleTableFields> headers)
            {
                DataTable dt = new DataTable();

                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(filename, false))
                {
                    CultureInfo En_Culture = new CultureInfo("en");
                    System.Threading.Thread.CurrentThread.CurrentCulture = En_Culture;
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }
                    int rownumber = 0;
                    foreach (Row r in sheetData.Elements<Row>())
                    {
                        if (rownumber == 0) { rownumber++; continue; }
                        if (r.Elements<Cell>().All(field => field.InnerText == null || field.InnerText is System.DBNull || string.IsNullOrWhiteSpace(field.InnerText.ToStringEmpty()))) continue;
                        DataRow tempRow = dt.NewRow();
                        int i = 0;
                        //var dg = GetRowCells(r);
                        foreach (Cell c in r.Elements<Cell>())
                        {
                            int colIndex = ConvertColumnNameToNumber(GetColumnName(c.CellReference.InnerText));
                            string cellValue = string.Empty;

                            if (c.DataType != null)
                            {
                                if (c.DataType == CellValues.SharedString)
                                {
                                    int id = -1;

                                    if (Int32.TryParse(c.InnerText, out id))
                                    {
                                        SharedStringItem item = GetSharedStringItemById(workbookPart, id);

                                        if (item.Text != null)
                                        {
                                            cellValue = item.Text.Text;
                                        }
                                        else if (item.InnerText != null)
                                        {
                                            cellValue = item.InnerText;
                                        }
                                        else if (item.InnerXml != null)
                                        {
                                            cellValue = item.InnerXml;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string curSep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                                string value = c.CellValue == null ? string.Empty : c.CellValue.InnerText.Replace(".", curSep);
                                if (!string.IsNullOrEmpty(value))
                                {
                                    value = value.Replace(",", curSep);
                                }
                                else
                                    value = c.CellValue == null ? string.Empty : c.CellValue.Text;

                                if (dt.Columns.Count < colIndex)
                                {
                                    cellValue = value;
                                }
                                else
                                {
                                    switch (dt.Columns[colIndex].DataType.Name)
                                    {
                                        case "Decimal":
                                            cellValue = string.IsNullOrEmpty(value) ? "0" : decimal.Parse(value).ToString();
                                            break;
                                        case "Float"://float
                                            cellValue = string.IsNullOrEmpty(value) ? "0" : float.Parse(value).ToString();
                                            break;

                                        default:
                                            cellValue = value;
                                            break;
                                    }

                                }

                            }

                            tempRow[colIndex] = cellValue;
                        }
                        dt.Rows.Add(tempRow);


                        rownumber++;
                    }

                }
                return dt;
            }

            public static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
            {
                return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
            }

            ///<summary>returns an empty cell when a blank cell is encountered
            ///</summary>
            public static IEnumerable<Cell> GetRowCells(Row row)
            {
                int currentCount = 0;

                foreach (DocumentFormat.OpenXml.Spreadsheet.Cell cell in
                    row.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                {
                    string columnName = GetColumnName(cell.CellReference);

                    int currentColumnIndex = ConvertColumnNameToNumber(columnName);

                    for (; currentCount < currentColumnIndex; currentCount++)
                    {
                        yield return new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    }

                    yield return cell;
                    currentCount++;
                }
            }
            /// <summary>
            /// Given a cell name, parses the specified cell to get the column name.
            /// </summary>
            /// <param name="cellReference">Address of the cell (ie. B2)</param>
            /// <returns>Column Name (ie. B)</returns>
            public static string GetColumnName(string cellReference)
            {
                // Match the column name portion of the cell name.
                var regex = new System.Text.RegularExpressions.Regex("[A-Za-z]+");
                var match = regex.Match(cellReference);

                return match.Value;
            }

            /// <summary>
            /// Given just the column name (no row index),
            /// it will return the zero based column index.
            /// </summary>
            /// <param name="columnName">Column Name (ie. A or AB)</param>
            /// <returns>Zero based index if the conversion was successful</returns>
            /// <exception cref="ArgumentException">thrown if the given string
            /// contains characters other than uppercase letters</exception>
            public static int ConvertColumnNameToNumber(string columnName)
            {
                var alpha = new System.Text.RegularExpressions.Regex("^[A-Z]+$");
                if (!alpha.IsMatch(columnName)) throw new ArgumentException();

                char[] colLetters = columnName.ToCharArray();
                Array.Reverse(colLetters);

                int convertedValue = 0;
                for (int i = 0; i < colLetters.Length; i++)
                {
                    char letter = colLetters[i];
                    int current = i == 0 ? letter - 65 : letter - 64; // ASCII 'A' = 65
                    convertedValue += current * (int)Math.Pow(26, i);
                }

                return convertedValue;
            }

            private static string GetCellValue(SpreadsheetDocument document, Cell cell)
            {
                SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
                string value = cell.CellValue == null ? "" : cell.CellValue.InnerXml;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
                else
                {
                    return value;
                }
            }
        }

    }
}
