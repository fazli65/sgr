using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;
using System.Data;
using System.Security.Permissions;
using System.Reflection;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PTC.ERP.Generator.UI.UIAppCode
{
	enum ShowFromStatus
	{
		RunTime, Design
	}
	public static class Extention
	{
		public static List<Control> GetControls(Control form)
		{
			var controlList = new List<Control>();
			foreach (Control childControl in form.Controls)
			{
				// Recurse child controls.
				controlList.AddRange(GetControls(childControl));
				if (childControl is DataGridView || childControl is Label || childControl is CheckBox)
					controlList.Add(childControl);
			}
			return controlList;
		}
		public static bool BtnSaveEdit_Click(this Form aForm)
		{
		    return MessageBox.Show("اطمينان داريد؟", "ذخيره ميشود", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK;
		}
		public static bool BtnDelete_Click(this Form aForm)
		{
			return MessageBox.Show("اطمينان داريد؟", "حذف ميشود", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK;
		}
		public static void BtnClose_Click(this Form aForm)
		{
			if (MessageBox.Show("اطمينان داريد؟", "خروج", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				aForm.DialogResult = DialogResult.Cancel;
				aForm.Close();
			}
		}
		public static bool BtnConfirm_Click(this Form aForm)
		{
			if (MessageBox.Show("اطمينان داريد؟", "انجام عملیات", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
			{
				return true;
			}
			else return false;
		}
        public static bool IsNumericValue(this object o)
        {
            try
            {

                double num;
                if (!double.TryParse(o.ToString(), out num))
                    return false;
                else
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
		public static double ToDouble(this object obj)
		{
			double retVal;
			try
			{
				retVal = Convert.ToDouble(obj.ToString().Replace(",", ""));
			}
			catch
			{
				retVal = 0;
			}
			return retVal;
		}
        public static long ToLong(this object obj)
        {
            long retVal = 0;
            try
            {
                retVal = Convert.ToInt64(obj.ToString().Replace(",", ""));
            }
            catch
            {
                retVal = 0;
            }
            return retVal;
        }

        public static long GetNumbersOnly(this object obj)
        {
            try
            {
                string[] numbers = Regex.Split(obj.ToStringEmpty().Trim(), @"\D+");
                if(numbers == null || numbers.Count() == 0)
                {
                    throw new Exception();
                }
                numbers = numbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                return numbers[0].ToLong();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

		public static decimal ToDecimal(this object obj)
		{
			decimal retVal = 0;
			if (obj != null)
				decimal.TryParse(obj.ToString().Replace(",", ""), out retVal);

			return retVal;
		}
		public static float ToFloat(this object obj)
		{
			float retVal = 0;
			if (obj != null)
				float.TryParse(obj.ToString().Replace(",", ""), out retVal);

			return retVal;
		}
		public static string ToStringEmpty(this object obj)
		{
			string retVal;
			try
			{
				retVal = Convert.ToString(obj);

			}
			catch
			{
				retVal = "";
			}
			return retVal;
		}
		public static int ToInt(this object obj)
		{
			int retVal = 0;
			try
			{
                //retVal = Convert.ToInt32(obj.ToString().Replace(",", ""));
                if (!int.TryParse(obj.ToString().Replace(",", ""), out retVal))
                    return 0;
			}
			catch
			{
				retVal = 0;
			}
			return retVal;
		}
		public static int? ToIntNullable(this object obj)
		{
			if (obj == null)
				return null;
			if (string.IsNullOrEmpty(obj.ToString()))
				return null;
			int retVal = 0;
			try
			{
				retVal = Convert.ToInt32(obj.ToString().Replace(",", ""));
			}
			catch
			{
				retVal = 0;
			}
			return retVal;
		}
		public static long? ToLongNullable(this object obj)
		{
			if (obj == null)
				return null;

			if (string.IsNullOrEmpty(obj.ToString()))
				return null;

			long retVal = 0;
			try
			{
				retVal = Convert.ToInt64(obj.ToString().Replace(",", ""));
			}
			catch
			{
				retVal = 0;
			}
			return retVal;
		}
		public static bool ToBool(this object obj)
		{
			bool retVal = false;
			try
			{
				retVal = Convert.ToBoolean(obj);
			}
			catch
			{
				retVal = false;
			}
			return retVal;
		}
		public static short? ToShortNullable(this object obj)
		{
			if (obj == null)
				return null;
			short retVal = 0;
			try
			{
				retVal = Convert.ToInt16(obj.ToString().Replace(",", ""));
			}
			catch
			{
				retVal = 0;
			}
			return retVal;
		}
		public static short ToShort(this object obj)
		{
			short retVal = 0;
			try
			{
				retVal = Convert.ToInt16(obj.ToString().Replace(",", ""));
			}
			catch
			{
				retVal = 0;
			}
			return retVal;
		}
		public static void InitEscFormClose(this Form aForm, bool ImplementEnterAsTab = false)
		{
			aForm.KeyPreview = true;
			if (ImplementEnterAsTab)
			{
				aForm.Tag = ImplementEnterAsTab;
			}
			aForm.KeyDown += new KeyEventHandler(aForm_KeyDown);
		}
		static void aForm_KeyDown(object sender, KeyEventArgs e)
		{
			// Exit The Forms
			if (e.KeyCode == Keys.Escape)
			{

				if (sender != null)
				{
					if (sender is Form)
					{
						((Form)sender).Close();

					}
				}
			}
		}
		
        public static void ShowPropertyGrid(this Control parent, object selectedObject)
		{
			var splitter = new Splitter();
			splitter.Dock = System.Windows.Forms.DockStyle.Right;
			splitter.Location = new System.Drawing.Point(518, 0);
			splitter.Name = "splitter";
			splitter.Size = new System.Drawing.Size(3, 435);
			splitter.TabIndex = 4;
			splitter.TabStop = false;

			var pg = new PropertyGrid();
			pg.Dock = System.Windows.Forms.DockStyle.Right;
			pg.Location = new System.Drawing.Point(521, 0);
			pg.Name = "pg";
			pg.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			pg.Size = new System.Drawing.Size(233, 435);
			pg.TabIndex = 3;
			pg.SelectedObject = selectedObject;

			parent.Controls.Add(splitter);
			parent.Controls.Add(pg);
		}
		public static T FromXMLString<T>(string xml) where T : class
		{
			try
			{
				var sr = new StringReader(xml);
				var serializer = new XmlSerializer(typeof(T));
				return serializer.Deserialize(sr) as T;
			}
			catch
			{
				return null;
			}
		}
		public static string ToXMLString<T>(T o)
		{
			using (var sw = new StringWriter())
			{
				using (var xtw = new XmlTextWriter(sw))
				{
					var serializer = new XmlSerializer(typeof(T));
					serializer.Serialize(xtw, o);
					return sw.ToString();
				}
			}
		}
		
		private static char[] LatinNameKeys = null;
		public static void HandleLatinName(this TextBox tb, ErrorProvider errorProvider)
		{
			tb.KeyPress += GetHandleLatinNameKeyDown(tb, errorProvider);
		}
		private static KeyPressEventHandler GetHandleLatinNameKeyDown(Control ctrl, ErrorProvider errorProvider)
		{
			if (LatinNameKeys == null)
			{
				var allKeys = Enum.GetValues(typeof(Keys)).OfType<Keys>().ToArray();
				var latinNameKeys = new List<char>();
				latinNameKeys.AddRange(Enumerable.Range((int)'A', ((int)'Z') - ((int)'A')+1).Select(o => (char)o));
				latinNameKeys.AddRange(Enumerable.Range((int)'a', ((int)'z') - ((int)'a')+1).Select(o => (char)o));
				latinNameKeys.AddRange(Enumerable.Range((int)'0', ((int)'9') - ((int)'0')+1).Select(o => (char)o));
				//latinNameKeys.Add('_');
				//latinNameKeys.Add(' ');
				latinNameKeys.Add('\b');
				LatinNameKeys = latinNameKeys.ToArray();
			}

			return (s, e) =>
			{
				e.Handled = false;
				errorProvider.SetError(ctrl, string.Empty);

				if (
					(Control.ModifierKeys & Keys.Shift) != 0 ||
					(Control.ModifierKeys & Keys.Control) != 0 ||
					(Control.ModifierKeys & Keys.Alt) != 0
					)
					return;

				if (!LatinNameKeys.Contains((char)e.KeyChar))
				{
					e.Handled = true;
					//errorProvider.SetError(ctrl, ServiceLocator.GetTranslate("General.ControlJustLatinKeyPress", "فقط حروف لاتین مجاز می باشند"));
				}
			};
		}
		public static bool IsPDA(this Screen screen)
		{
			try
			{
				var IsPDA= Screen.PrimaryScreen.WorkingArea.Width < 800;

                return IsPDA;
            }
			catch
			{
			}

			return false;
		}
        //public static DataTable FilterColumn(DataTable result, string ColKey, ConditionOperator filter, string SearchText, Type ColType,string Format)
        //{
        //    bool NeedReplaceAlphabet = SearchText.Contains("ک") || SearchText.Contains("ی") || SearchText.Contains("ك") || SearchText.Contains("ي");
        //    if (result == null || result.Rows.Count == 0)
        //        return result;
        //    EnumerableRowCollection<DataRow> r = null;
        //    switch (filter)
        //    {
        //        case ConditionOperator.BeginsWith:
        //            if (NeedReplaceAlphabet)
        //                r = result.AsEnumerable().Where(dr =>
        //                   dr.Field<string>(ColKey).StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)
        //                || dr.Field<string>(ColKey).StartsWith(SearchText.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase)
        //                || dr.Field<string>(ColKey).StartsWith(SearchText.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase)
        //                || dr.Field<string>(ColKey).StartsWith(SearchText.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase));
        //            else r = result.AsEnumerable().Where(dr => dr.Field<string>(ColKey).StartsWith(SearchText, StringComparison.OrdinalIgnoreCase));
        //            break;

        //        case ConditionOperator.EndsWith:
        //            if (NeedReplaceAlphabet)
        //                r = result.AsEnumerable().Where(dr =>
        //                   dr.Field<string>(ColKey).EndsWith(SearchText, StringComparison.OrdinalIgnoreCase)
        //                || dr.Field<string>(ColKey).EndsWith(SearchText.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase)
        //                || dr.Field<string>(ColKey).EndsWith(SearchText.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase)
        //                || dr.Field<string>(ColKey).EndsWith(SearchText.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase));
        //            else r = result.AsEnumerable().Where(dr => dr.Field<string>(ColKey).EndsWith(SearchText, StringComparison.OrdinalIgnoreCase));
        //            break;

        //        case ConditionOperator.Contains:
        //            if (result.Columns[ColKey].DataType == typeof(string))
        //            {
        //                string[] searchparts = SearchText.Split('*');
        //                r = result.AsEnumerable();
        //                foreach (string eachSearch in searchparts)
        //                {

        //                    if (NeedReplaceAlphabet)
        //                        r = r.Where(dr =>
        //                           dr.Field<string>(ColKey).Contains(eachSearch, StringComparison.OrdinalIgnoreCase)
        //                        || dr.Field<string>(ColKey).Contains(eachSearch.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase)
        //                        || dr.Field<string>(ColKey).Contains(eachSearch.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase)
        //                        || dr.Field<string>(ColKey).Contains(eachSearch.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase));
        //                    else
        //                    {
        //                        r = r.Where(dr => dr.Field<string>(ColKey).Contains(eachSearch, StringComparison.OrdinalIgnoreCase));
        //                    }

        //                }
                            
        //            }
        //            else if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                //r = result.AsEnumerable().Where(x => x[ColKey].ToDecimal() == SearchText.ToDecimal());
        //                r = result.AsEnumerable().Where(x => !string.IsNullOrEmpty(x[ColKey].ToStringEmpty()) && Convert.ToDecimal(x[ColKey]).ToString(Format).Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        //            else
        //            {
        //                if (NeedReplaceAlphabet)
        //                    r = result.AsEnumerable().Where(dr =>
        //                       dr[ColKey].ToStringEmpty().Contains(SearchText, StringComparison.OrdinalIgnoreCase)
        //                    || dr[ColKey].ToStringEmpty().Contains(SearchText.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase)
        //                    || dr[ColKey].ToStringEmpty().Contains(SearchText.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase)
        //                    || dr[ColKey].ToStringEmpty().Contains(SearchText.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase));
        //                else r = result.AsEnumerable().Where(dr => dr[ColKey].ToStringEmpty().Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        //            }
        //            break;

        //        case ConditionOperator.NotContains:
        //            if (NeedReplaceAlphabet)
        //                r = result.AsEnumerable().Where(dr =>
        //                   !(dr.Field<string>(ColKey).Contains(SearchText, StringComparison.OrdinalIgnoreCase))
        //                || !(dr.Field<string>(ColKey).Contains(SearchText.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase))
        //                || !(dr.Field<string>(ColKey).Contains(SearchText.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase))
        //                || !(dr.Field<string>(ColKey).Contains(SearchText.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase)));
        //            else r = result.AsEnumerable().Where(dr => !(dr.Field<string>(ColKey).Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
        //            break;

        //        case ConditionOperator.Equal:
        //            if (result.Columns[ColKey].DataType == typeof(string))
        //            {
        //                if (NeedReplaceAlphabet)
        //                    r = result.AsEnumerable().Where(dr =>
        //                       string.Equals(dr.Field<string>(ColKey), SearchText, StringComparison.OrdinalIgnoreCase)
        //                    || string.Equals(dr.Field<string>(ColKey), SearchText.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase)
        //                    || string.Equals(dr.Field<string>(ColKey), SearchText.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase)
        //                    || string.Equals(dr.Field<string>(ColKey), SearchText.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase));
        //                else r = result.AsEnumerable().Where(dr => string.Equals(dr.Field<string>(ColKey), SearchText, StringComparison.OrdinalIgnoreCase));
        //            }
        //            else if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                //r = result.AsEnumerable().Where(x => x[ColKey].ToDecimal() == SearchText.ToDecimal());
        //            r = result.AsEnumerable().Where(x => !string.IsNullOrEmpty(x[ColKey].ToStringEmpty()) && Convert.ToDecimal(x[ColKey]).ToString(Format) == SearchText);
        //            else r = result.AsEnumerable().Where(x => string.Equals(x[ColKey].ToStringEmpty(), SearchText, StringComparison.OrdinalIgnoreCase));
        //            break;

        //        case ConditionOperator.NotEqual:
        //            if (result.Columns[ColKey].DataType == typeof(string))
        //            {
        //                if (NeedReplaceAlphabet)
        //                    r = result.AsEnumerable().Where(dr =>
        //                       !string.Equals(dr.Field<string>(ColKey), SearchText, StringComparison.OrdinalIgnoreCase)
        //                    || !string.Equals(dr.Field<string>(ColKey), SearchText.Replace("ک", "ك"), StringComparison.OrdinalIgnoreCase)
        //                    || !string.Equals(dr.Field<string>(ColKey), SearchText.Replace("ی", "ي"), StringComparison.OrdinalIgnoreCase)
        //                    || !string.Equals(dr.Field<string>(ColKey), SearchText.Replace("ي", "ی"), StringComparison.OrdinalIgnoreCase));
        //                else r = result.AsEnumerable().Where(dr => !string.Equals(dr.Field<string>(ColKey), SearchText, StringComparison.OrdinalIgnoreCase));
        //            }
        //            else if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                //r = result.AsEnumerable().Where(x => x[ColKey].ToDecimal() != SearchText.ToDecimal());
        //                r = result.AsEnumerable().Where(x => !string.IsNullOrEmpty(x[ColKey].ToStringEmpty()) && Convert.ToDecimal(x[ColKey]).ToString(Format) != SearchText);
        //            else r = result.AsEnumerable().Where(x => !string.Equals(x[ColKey].ToStringEmpty(), SearchText, StringComparison.OrdinalIgnoreCase));
        //            break;

        //        case ConditionOperator.IsNull:
        //        case ConditionOperator.IsEmpty:
        //            r = result.AsEnumerable().Where(dr => string.IsNullOrWhiteSpace(dr.Field<object>(ColKey).ToStringEmpty()));
        //            break;
        //        //case ConditionOperator.NotIn:
        //        //    term = ComparisonTerms.NotOfAny;
        //        //break;
        //        case ConditionOperator.NotIsNull:
        //        case ConditionOperator.NotIsEmpty:
        //            r = result.AsEnumerable().Where(dr => !string.IsNullOrWhiteSpace(dr.Field<object>(ColKey).ToStringEmpty()));
        //            break;

        //        case ConditionOperator.GreaterThan:
        //            if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                r = result.AsEnumerable().Where(dr => dr.Field<decimal>(ColKey) > SearchText.ToDecimal());
        //            break;

        //        case ConditionOperator.GreaterThanOrEqualTo:
        //            if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                r = result.AsEnumerable().Where(dr => dr.Field<decimal>(ColKey) >= SearchText.ToDecimal());
        //            break;

        //        case ConditionOperator.LessThan:
        //            if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                r = result.AsEnumerable().Where(dr => dr.Field<decimal>(ColKey) < SearchText.ToDecimal());
        //            break;

        //        case ConditionOperator.LessThanOrEqualTo:
        //            if (ColType == typeof(int) || ColType == typeof(decimal) || ColType == typeof(float) || ColType == typeof(Int64) || ColType == typeof(double))
        //                r = result.AsEnumerable().Where(dr => dr.Field<decimal>(ColKey) <= SearchText.ToDecimal());
        //            break;

        //            //default:
        //            //    return r = result.AsEnumerable();
        //    }
        //    if (r != null && r.Any())
        //        result = r.CopyToDataTable();
        //    else result.Clear();
        //    return result;
        //}
        //public static EditType GetEditType(this GridEXColumn column, bool isEditable = true)
        //{
           
        //    if (isEditable)
        //    {
        //        switch (column.ColumnType)
        //        {
        //            case ColumnType.CheckBox:
        //                return EditType.CheckBox;
        //            default:
        //                return EditType.TextBox;
        //        }
        //    }
           

        //    return EditType.NoEdit;
        //}
        [ReflectionPermission(SecurityAction.Demand, MemberAccess = true)]
        public static void ResetExceptionState(this Control control)
        {
            typeof(Control).InvokeMember("SetState", BindingFlags.NonPublic |
              BindingFlags.InvokeMethod | BindingFlags.Instance, null,
              control, new object[] { 0x400000, false });
        }
        public static Color? ToColor(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var colorValues = value.Split(new char[] { ',', ')', '(' }, StringSplitOptions.RemoveEmptyEntries).Select(o => o.ToInt()).Where(o => o >= 0).ToArray();
                if (colorValues.Length >= 3)
                {
                    return Color.FromArgb(colorValues[0], colorValues[1], colorValues[2]);
                }
            }

            return null;
        }
       

    }
}

