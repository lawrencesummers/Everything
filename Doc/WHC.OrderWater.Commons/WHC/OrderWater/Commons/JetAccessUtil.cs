namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class JetAccessUtil
    {
        public static Dictionary<string, string> ListColumns(string mdbFilePath, string password, string tableName)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;";
            if ((password == null) || (password.Trim() == ""))
            {
                connectionString = connectionString + "Data Source=" + mdbFilePath;
            }
            else
            {
                string str2 = connectionString;
                connectionString = str2 + "Jet OLEDB:Database Password=" + password + ";Data Source=" + mdbFilePath;
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                foreach (DataRow row in smethod_4(tableName, connection).Rows)
                {
                    string key = row["ColumnName"].ToString();
                    ((OleDbType) row["ProviderType"]).ToString();
                    string str4 = row["DataType"].ToString();
                    dictionary.Add(key, str4);
                }
            }
            return dictionary;
        }

        public static List<string> ListTables(string mdbFilePath, string password)
        {
            List<string> list = new List<string>();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;";
            if ((password == null) || (password.Trim() == ""))
            {
                connectionString = connectionString + "Data Source=" + mdbFilePath;
            }
            else
            {
                string str2 = connectionString;
                connectionString = str2 + "Jet OLEDB:Database Password=" + password + ";Data Source=" + mdbFilePath;
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                object[] restrictions = new object[4];
                restrictions[3] = "TABLE";
                DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
                for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
                {
                    string item = oleDbSchemaTable.Rows[i]["TABLE_NAME"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public static string SetMDBPassword(string mdbFilePath, string oldPwd, string newPwd)
        {
            string message;
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" + "Mode=Share Deny Read|Share Deny Write;" + ("Jet OLEDB:Database Password=" + oldPwd + ";Data Source=" + mdbFilePath));
            try
            {
                connection.Open();
                string str2 = ((oldPwd == null) || (oldPwd.Trim() == "")) ? "null" : ("[" + oldPwd + "]");
                string str3 = ((newPwd == null) || (newPwd.Trim() == "")) ? "null" : ("[" + newPwd + "]");
                new OleDbCommand("ALTER DATABASE PASSWORD " + str3 + " " + str2, connection).ExecuteNonQuery();
                connection.Close();
                message = "0";
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return message;
        }

        public static string smethod_0(string mdbFilePath, string password)
        {
            try
            {
                string str = "Provider=Microsoft.Jet.OLEDB.4.0;";
                if ((password == null) || (password.Trim() == ""))
                {
                    str = str + "Data Source=" + mdbFilePath;
                }
                else
                {
                    string str2 = str;
                    str = str2 + "Jet OLEDB:Database Password=" + password + ";Data Source=" + mdbFilePath;
                }
                object target = Activator.CreateInstance(Type.GetTypeFromProgID("ADOX.Catalog"));
                object[] args = new object[] { str };
                target.GetType().InvokeMember("Create", BindingFlags.InvokeMethod, null, target, args);
                Marshal.ReleaseComObject(target);
                target = null;
                return "0";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string smethod_1(string mdbFilePath)
        {
            return smethod_0(mdbFilePath, null);
        }

        public static string smethod_2(string mdbFilePath, string password)
        {
            string str = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Engine Type=5;";
            string str2 = str;
            string sourceFileName = mdbFilePath + ".tmp";
            if ((password == null) || (password.Trim() == ""))
            {
                str = str + "Data Source=" + mdbFilePath;
                str2 = str2 + "Data Source=" + sourceFileName;
            }
            else
            {
                string str5 = str;
                str = str5 + "Jet OLEDB:Database Password=" + password + ";Data Source=" + mdbFilePath;
                str5 = str2;
                str2 = str5 + "Jet OLEDB:Database Password=" + password + ";Data Source=" + mdbFilePath + ".tmp";
            }
            string message = "";
            try
            {
                object target = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
                object[] args = new object[] { str, str2 };
                target.GetType().InvokeMember("CompactDatabase", BindingFlags.InvokeMethod, null, target, args);
                Marshal.ReleaseComObject(target);
                target = null;
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            try
            {
                File.Delete(mdbFilePath);
                File.Move(sourceFileName, mdbFilePath);
            }
            catch (Exception exception2)
            {
                message = message + exception2.Message;
            }
            return ((message == "") ? "0" : message);
        }

        public static string smethod_3(string mdbFilePath)
        {
            return smethod_2(mdbFilePath, null);
        }

        private static DataTable smethod_4(object object_0, IDbConnection idbConnection_0)
        {
            IDbCommand command = new OleDbCommand {
                CommandText = string.Format("select * from [{0}]", object_0),
                Connection = idbConnection_0
            };
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
            {
                return reader.GetSchemaTable();
            }
        }
    }
}

