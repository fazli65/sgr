using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraBars.Docking2010.Base;
using PTC.ERP.Generator.UI.UIAppCode;
using SGRSalary.Model;

namespace SGRSalary.Classes
{
    public class BaseRepository
    {
        
        protected const int SqlCommandTimeout = 220;
        private static string[][] ColDataTypeGroups;

        public static SGR_SALARYEntities SGR_SALARYEntitiesBase { get; set; }

        public BaseRepository()
        {
            if (SGR_SALARYEntitiesBase == null)
            {
                //CurrentConnection = Cipher.GetConnectionString();
                //CurrentConnection += " Connection Timeout = 0 ;";
                //var dbname = CurrentConnection.Split(';')[1].Split('=')[1];//گرفتن نام دیتابیس جاری
                //CurrentConnectionAttachment = CurrentConnection.Replace(dbname, dbname);
                //var con = Cipher.GetConnectionString();
                //SGR_SALARYEntitiesBase = new SGR_SALARYEntities(con);
                //SGR_SALARYEntitiesBase.CommandTimeout = SqlCommandTimeout;
                //SGR_SALARYEntitiesBase.CurrentUserID = CurrentUserID;
            }
        }

        //public static void ChangeDB(string DB_Name)
        //{
        //    string ConnectionStr = Cipher.GetConnectionString();
        //    var dbname = CurrentConnection.Split(';')[1].Split('=')[1];
        //    CurrentConnection = CurrentConnection.Replace(dbname, DB_Name);
        //    CurrentConnectionAttachment = CurrentConnection;
        //    ConnectionStr = ConnectionStr.Replace(dbname, DB_Name);
        //    SGR_SALARYEntitiesBase = new SGR_SALARYEntities(ConnectionStr);
        //}

        #region Sql Functions

        private static string CurrentConnection;
        private static string CurrentConnectionAttachment;
        static SqlConnection conLocal;

        public static string ExecuteScalar(string command, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                comm.Connection.Open();
                object retVal = comm.ExecuteScalar();
                return retVal.ToString();
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return string.Empty;
            }
            finally
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
            }
        }

        public static string ExecuteScalar(string command, List<SqlParameter> InsertParams, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                comm.Parameters.Clear();

                foreach (var item in InsertParams)
                {
                    comm.Parameters.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                comm.Connection.Open();

                object retVal = comm.ExecuteScalar();

                return retVal.ToString();
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return string.Empty;
            }
            finally
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
            }
        }

        public static object ExecuteScalarObject(string command, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                comm.Connection.Open();

                return comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return null;
            }
            finally
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
            }
        }

        public static bool ExecuteNonQuery(string command, List<SqlParameter> InsertParams, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                comm.Parameters.Clear();

                foreach (var item in InsertParams)
                {
                    SqlParameter sqlParameter = new SqlParameter(item.ParameterName, item.Value)
                    {
                        SqlDbType = item.SqlDbType,
                        Size = item.Size
                    };

                    comm.Parameters.Add(sqlParameter);
                    //TODO تایپ پارامتر تنظیم بشه در پارامتر حدید
                }

                comm.Connection.Open();
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return false;
            }
            finally
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
            }
        }

        public static object ExecuteScalarQuery(string command, List<SqlParameter> InsertParams,
            bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            using (SqlCommand comm = new SqlCommand(command, conLocal))
            {
                comm.CommandTimeout = SqlCommandTimeout;
                comm.Parameters.Clear();

                comm.Parameters.AddRange(InsertParams.ToArray());

                try
                {
                    comm.Connection.Open();
                    return comm.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //var result = ShowExceptionMessage(ex, command);
                    return ex.InnerException.ToString();
                }

                finally
                {
                    if (comm.Connection.State == ConnectionState.Open)
                    {
                        comm.Connection.Close();
                    }
                }
            }
        }

        public static bool ExecuteNonQuery(string command, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                comm.Connection.Open();
                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command, ExecCommand: command);
                return false;
            }

            finally
            {
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
            }
        }

        public partial class MenuConfigGroups
        {
            public MenuConfigGroups()
            {
            }

            public int ModuleId { get; set; }
            public string GroupTitle { get; set; }
        }

        public static DataTable Fill(string command, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            string FinalCommand =
                string.Format(@"Declare @Text Nvarchar(MAX) select @Text = '{0}' Exec Sp_executeSql @Text",
                    command.Replace("'", "''"));

            SqlCommand comm = new SqlCommand(FinalCommand, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                //ShowExceptionMessage(ex, command, FinalCommand);
                return new DataTable();
            }
        }

        public static DataSet FillDataSet(string command, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            string FinalCommand =
                string.Format(@"Declare @Text Nvarchar(MAX) select @Text = '{0}' Exec Sp_executeSql @Text",
                    command.Replace("'", "''"));

            SqlCommand comm = new SqlCommand(FinalCommand, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return new DataSet();
            }
        }

        public static DataTable Fill(string command, List<SqlParameter> InsertParams, bool FromAttach = false,
            EntityObject entityObject = null)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };

            String SqlStr = "";
            try
            {
                foreach (var item in InsertParams)
                {
                    if (item.SqlDbType == SqlDbType.NVarChar || item.SqlDbType == SqlDbType.VarChar)
                        item.Size = 8000;
                    comm.Parameters.Add(item);
                    var val = string.IsNullOrEmpty(item.Value.ToString()) ? "null" : item.Value.ToString();
                    SqlStr = SqlStr + "\n  Declare @" + item.ParameterName + "  " + item.SqlDbType.ToString() +
                             ((item.SqlDbType == SqlDbType.NVarChar || item.SqlDbType == SqlDbType.VarChar)
                                 ? "(Max)"
                                 : "") + "  =  " + val;
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                //SqlStr = SqlStr + command;
                //if (entityObject != null && entityObject.EntityState == EntityState.Modified)
                //    ServiceLocator<>.ExecutionService.RetrieveObjectState(entityObject.EntityKey,
                //        EntityState.Unchanged);

                //ShowExceptionMessage(ex, SqlStr, ExecCommand: SqlStr);
                return new DataTable();
            }
        }

        public static DataSet FillDataSet(string command, List<SqlParameter> InsertParams, bool FromAttach = false)
        {
            //CheckSqlInjection(command);

            conLocal = FromAttach.Equals(false)
                ? new SqlConnection(CurrentConnection)
                : new SqlConnection(CurrentConnectionAttachment);

            SqlCommand comm = new SqlCommand(command, conLocal)
            {
                CommandTimeout = SqlCommandTimeout
            };
            String SqlStr = "";
            String val = "";
            foreach (var item in InsertParams)
            {
                if (item.SqlDbType == SqlDbType.NVarChar || item.SqlDbType == SqlDbType.VarChar)
                    item.Size = 8000;
                comm.Parameters.Add(item);
                val = string.IsNullOrEmpty(item.Value.ToString()) ? "null" : item.Value.ToString();
                SqlStr = SqlStr + "\n  Declare @" + item.ParameterName + "  " + item.SqlDbType.ToString() +
                         ((item.SqlDbType == SqlDbType.NVarChar || item.SqlDbType == SqlDbType.VarChar) ? "(Max)" : "")
                         + "  =  " + val;
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                SqlStr = SqlStr + command;

                //ShowExceptionMessage(ex, SqlStr, ExecCommand: SqlStr);
                return new DataSet();
            }
        }

        public static string NormalizeSqlScripts(string command)
        {
            //حذف کامنت های وسط خط با /**/
            while (command.Contains("/*") || command.Contains("*/"))
            {
                var fromIndex = command.IndexOf("/*");
                var SecondIndex = command.IndexOf("*/");
                if (SecondIndex > fromIndex)
                    command = command.Remove(fromIndex, (SecondIndex + 2) - fromIndex).Insert(fromIndex, " ");
            }

            //حذف خطوطی که کلا کامنت شدن
            string[] lines = command.Split(new[] { Environment.NewLine, "\r\n", "\r", "\n" }, StringSplitOptions.None);
            lines = lines.Where(x => !x.StartsWith("--")).ToArray();

            //از وسط خط به بعد کامنت شده 
            if (lines.Any(x => x.Contains("--")))
            {
                List<string> lines2 = lines.Where(x => !x.Contains("--")).ToList();
                List<string> lines3 = lines.Where(x => x.Contains("--")).ToList();
                foreach (var s in lines3)
                    lines[lines.ToList().IndexOf(s)] = s.Remove(s.IndexOf("--")); //lines2.Add();
                lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            }

            //حذف اسپیس ها و تب های اضافی
            command = string.Join(Environment.NewLine, lines.Where(s => !String.IsNullOrEmpty(s)));
            command = command.Replace("\t", " ");
            command = command.Replace("\r", " ");
            command = command.Replace("\n", " ");
            command = command.Replace("\r\n", " ");
            var values = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            command = string.Join(" ", values.Where(s => !String.IsNullOrEmpty(s)));

            return command;
        }

        public static DataSet FillDataSet(string command, params string[] tableNames)
        {
            conLocal = new SqlConnection(CurrentConnection);
            var ds = new DataSet();

            var cmd = new SqlCommand(command, conLocal) { CommandTimeout = SqlCommandTimeout };
            try
            {
                conLocal.Open();
                using (var sdr = cmd.ExecuteReader())
                {
                    ds.Load(sdr, LoadOption.OverwriteChanges, tableNames);
                }

                return ds;
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return new DataSet();
            }
            finally
            {
                conLocal.Close();
            }
        }

        public static DataSet FillDataSet(string command)
        {
            ////CheckSqlInjection(command);

            conLocal = new SqlConnection(CurrentConnection);
            var ds = new DataSet();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, conLocal);
                sqlDataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //ShowExceptionMessage(ex, command);
                return new DataSet();
            }
        }


        #region [ FillSchema ]

        /// <summary>
        /// این تابع دو جدول در اختیار قرار میدهد از شمای ایجاد شده از اجرای دستور
        /// </summary>
        /// <param name="command">دستور مورد نظر</param>
        /// <param name="table">جدول اجرای دستور بدون رکورد</param>
        /// <param name="schemaTable">جدول اطلاعات فیلدها</param>
        public static void FillSchema(string command, out DataTable table, out DataTable schemaTable)
        {
            FillSchema(command, new List<SqlParameter>(), out table, out schemaTable);
        }

        public static void FillSchema(string commandText, List<SqlParameter> parameters, out DataTable table,
            out DataTable schemaTable)
        {
            table = null;
            schemaTable = null;
            ////CheckSqlInjection(commandText);
            using (var connection = new SqlConnection(CurrentConnection))
            {
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandTimeout = SqlCommandTimeout;
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);

                    try
                    {
                        IDataReader dr = null;
                        try
                        {
                            connection.Open();
                            dr = command.ExecuteReader();

                            //	dr.FieldCount					4				int
                            //	dr.GetName(0)					"TotalPrice"	string
                            //	dr.GetDataTypeName(0)			"nvarchar"		string
                            //	dr.GetFieldType(0)				{Name = "String" FullName = "System.String"}
                            //	dr.GetOrdinal("TotalPrice")		0				int

                            table = new DataTable();
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                var dc = new DataColumn();
                                dc.ColumnName = dr.GetName(i);
                                dc.DataType = dr.GetFieldType(i);
                                table.Columns.Add(dc);
                            }

                            schemaTable = dr.GetSchemaTable();
                        }
                        finally
                        {
                            if (dr != null)
                            {
                                dr.Close();
                                dr.Dispose();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        //ShowExceptionMessage(ex,
                        // /*
                        // "FillSchema(commandText" +
                        // ", parameters: " + (parameters != null ? string.Join(", ", parameters.Select(o => "[" + o.ParameterName + ":" + o.Value + "]")) : "empty") +
                        // ", out table" +
                        // ", out schemaTable)",
                        // */
                        // commandText);
                    }
                    finally
                    {
                        if (command != null)
                        {
                            command.Dispose();
                        }

                        if (connection != null)
                        {
                            if (connection.State == ConnectionState.Open)
                                connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        #endregion



        //    protected bool ExistsTable(string schemaName, string tableName)
        //    {

        //        return SGR_SALARYEntities.ExecuteStoreQuery<int?>(
        //            @"SELECT 1 FROM sys.tables AS T
        //INNER JOIN sys.schemas AS S ON T.schema_id = S.schema_id
        //WHERE S.Name = {0} AND T.Name = {1}", schemaName, tableName).FirstOrDefault() != null;
        //    }

        #endregion

        public static string[][] GetColDataTypeGroups()
        {
            if (ColDataTypeGroups != null && ColDataTypeGroups.Length > 0)
                return ColDataTypeGroups;

            var groups = new string[]
            {
                "String,StringMax,StringMultiSelect,StringListSelect,Text,URL,HTML",
                "DateTime,Date"
            };

            ColDataTypeGroups = groups.Select(o =>
                    o.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(oo => oo.Trim()).ToArray())
                .ToArray();

            return ColDataTypeGroups;
        }

        public SqlDbType GetSqlType(string p)
        {
            p = p.ToLower();
            if (p.StartsWith("decimal"))
                return SqlDbType.Decimal;
            switch (p)
            {
                case "bigint":
                    return SqlDbType.BigInt;
                case "binary":
                    return SqlDbType.Binary;
                case "bit":
                    return SqlDbType.Bit;
                case "char":
                    return SqlDbType.Char;
                case "date":
                    return SqlDbType.Date;
                case "datetime":
                    return SqlDbType.DateTime;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
                case "decimal":
                    return SqlDbType.Decimal;
                case "float":
                    return SqlDbType.Float;
                case "image":
                    return SqlDbType.Image;
                case "int":
                    return SqlDbType.Int;
                case "money":
                    return SqlDbType.Money;
                case "nchar":
                    return SqlDbType.NChar;
                case "ntext":
                    return SqlDbType.NText;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "real":
                    return SqlDbType.Real;
                case "smallsatetime":
                    return SqlDbType.SmallDateTime;
                case "smallInt":
                    return SqlDbType.SmallInt;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "structured":
                    return SqlDbType.Structured;
                case "text":
                    return SqlDbType.Text;
                case "time":
                    return SqlDbType.Time;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "udt":
                    return SqlDbType.Udt;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                case "varbinary":
                case "varbinary(max)":
                case "file":
                    return SqlDbType.VarBinary;
                case "varchar":
                    return SqlDbType.NVarChar;
                case "variant":
                    return SqlDbType.BigInt;
                case "xml":
                    return SqlDbType.Xml;
                default:
                    return SqlDbType.NVarChar;
            }
        }

        public int GetParameterSize(int Datatype)
        {
            switch (Datatype)
            {
                case 106:
                    return 18;
                default:
                    return 0;
            }
        }

    }
}
