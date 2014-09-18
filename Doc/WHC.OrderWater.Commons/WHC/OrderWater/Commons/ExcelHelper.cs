namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Windows.Forms;

    public class ExcelHelper
    {
        public static void DataSetToExcel(DataSet source, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            int num = 1;
            writer.Write("<xml version>\r\n<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n xmlns:x=\"urn:schemas-    microsoft-com:office:excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\r\n <Styles>\r\n <Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n <Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>\r\n <Protection/>\r\n </Style>\r\n <Style ss:ID=\"BoldColumn\">\r\n <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n <Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat ss:Format=\"@\"/>\r\n </Style>\r\n <Style ss:ID=\"Decimal\">\r\n <NumberFormat ss:Format=\"#,##0.###\"/>\r\n </Style>\r\n <Style ss:ID=\"Integer\">\r\n <NumberFormat ss:Format=\"0\"/>\r\n </Style>\r\n <Style ss:ID=\"DateLiteral\">\r\n <NumberFormat ss:Format=\"yyyy-mm-dd;@\"/>\r\n </Style>\r\n </Styles>\r\n ");
            for (int i = 0; i < source.Tables.Count; i++)
            {
                int num4 = 0;
                DataTable table = source.Tables[i];
                writer.Write("<Worksheet ss:Name=\"Sheet" + num + "\">");
                writer.Write("<Table>");
                writer.Write("<Row>");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    writer.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    writer.Write(source.Tables[0].Columns[j].ColumnName);
                    writer.Write("</Data></Cell>");
                }
                writer.Write("</Row>");
                foreach (DataRow row in table.Rows)
                {
                    num4++;
                    if (num4 == 0xfa00)
                    {
                        num4 = 0;
                        num++;
                        writer.Write("</Table>");
                        writer.Write(" </Worksheet>");
                        writer.Write("<Worksheet ss:Name=\"Sheet" + num + "\">");
                        writer.Write("<Table>");
                    }
                    writer.Write("<Row>");
                    for (int k = 0; k < source.Tables[0].Columns.Count; k++)
                    {
                        Type type = row[k].GetType();
                        switch (type.ToString())
                        {
                            case "System.String":
                            {
                                string str = row[k].ToString().Trim().Replace("&", "&").Replace(">", ">").Replace("<", "<");
                                writer.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                                writer.Write(str);
                                writer.Write("</Data></Cell>");
                                break;
                            }
                            case "System.DateTime":
                            {
                                int num6;
                                DateTime time = (DateTime) row[k];
                                string str2 = string.Concat(new object[] { time.Year, "-", (time.Month < 10) ? ("0" + time.Month) : (num6 = time.Month).ToString(), "-", (time.Day < 10) ? ("0" + time.Day) : (num6 = time.Day).ToString(), "T", (time.Hour < 10) ? ("0" + time.Hour) : (num6 = time.Hour).ToString(), ":", (time.Minute < 10) ? ("0" + time.Minute) : (num6 = time.Minute).ToString(), ":", (time.Second < 10) ? ("0" + time.Second) : (num6 = time.Second).ToString(), ".000" });
                                writer.Write("<Cell ss:StyleID=\"DateLiteral\"><Data ss:Type=\"DateTime\">");
                                writer.Write(str2);
                                writer.Write("</Data></Cell>");
                                break;
                            }
                            case "System.Boolean":
                                writer.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                                writer.Write(row[k].ToString());
                                writer.Write("</Data></Cell>");
                                break;

                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                writer.Write("<Cell ss:StyleID=\"Integer\"><Data ss:Type=\"Number\">");
                                writer.Write(row[k].ToString());
                                writer.Write("</Data></Cell>");
                                break;

                            case "System.Decimal":
                            case "System.Double":
                                writer.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"Number\">");
                                writer.Write(row[k].ToString());
                                writer.Write("</Data></Cell>");
                                break;

                            case "System.DBNull":
                                writer.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                                writer.Write("");
                                writer.Write("</Data></Cell>");
                                break;

                            default:
                                throw new Exception(type.ToString() + " not handled.");
                        }
                    }
                    writer.Write("</Row>");
                }
                writer.Write("</Table>");
                writer.Write(" </Worksheet>");
                num++;
            }
            writer.Write("</Workbook>");
            writer.Close();
        }

        public static void DataSetToExcel(DataTable dataTable, string fileName)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "xls files (*.xls)|*.xls",
                FileName = fileName
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch
                    {
                        MessageBox.Show("该文件正在使用中,关闭文件或重新命名导出文件再试!");
                        return;
                    }
                }
                OleDbConnection connection = new OleDbConnection();
                OleDbCommand command = new OleDbCommand();
                string str = "";
                try
                {
                    connection.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + fileName + ";Extended ProPerties=\"Excel 8.0;HDR=Yes;\"";
                    connection.Open();
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    str = "CREATE TABLE sheet1 (";
                    int num = 0;
                    while (num < dataTable.Columns.Count)
                    {
                        if (num < (dataTable.Columns.Count - 1))
                        {
                            str = str + "[" + dataTable.Columns[num].Caption + "] TEXT(100) ,";
                        }
                        else
                        {
                            str = str + "[" + dataTable.Columns[num].Caption + "] TEXT(200) )";
                        }
                        num++;
                    }
                    command.CommandText = str;
                    command.ExecuteNonQuery();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        str = "INSERT INTO sheet1 VALUES('";
                        for (num = 0; num < dataTable.Columns.Count; num++)
                        {
                            if (num < (dataTable.Columns.Count - 1))
                            {
                                str = str + dataTable.Rows[i][num].ToString() + " ','";
                            }
                            else
                            {
                                str = str + dataTable.Rows[i][num].ToString() + " ')";
                            }
                        }
                        command.CommandText = str;
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("导出EXCEL成功");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("导出EXCEL失败:" + exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public static DataSet ExcelToDataSet(string connectstring)
        {
            using (OleDbConnection connection = new OleDbConnection(connectstring))
            {
                DataSet dataSet = new DataSet();
                foreach (string str in GetExcelTablesName(connection))
                {
                    new OleDbDataAdapter("SELECT * FROM [" + str + "]", connection).Fill(dataSet, str);
                }
                return dataSet;
            }
        }

        public static DataSet ExcelToDataSet(string connectstring, string table)
        {
            using (OleDbConnection connection = new OleDbConnection(connectstring))
            {
                DataSet dataSet = new DataSet();
                if (smethod_1(connection, table))
                {
                    new OleDbDataAdapter("SELECT * FROM [" + table + "]", connection).Fill(dataSet, table);
                }
                return dataSet;
            }
        }

        public static DataSet ExcelToDataSet(string excelPath, bool header, ExcelType eType)
        {
            return ExcelToDataSet(GetExcelConnectstring(excelPath, header, eType));
        }

        public static DataSet ExcelToDataSet(string excelPath, string table, bool header, ExcelType eType)
        {
            return ExcelToDataSet(GetExcelConnectstring(excelPath, header, eType), table);
        }

        public static List<string> GetColumnsList(string excelPath, ExcelType eType, string table)
        {
            List<string> list = new List<string>();
            DataTable table2 = null;
            using (OleDbConnection connection = new OleDbConnection(GetExcelConnectstring(excelPath, true, eType)))
            {
                connection.Open();
                table2 = smethod_0(table, connection);
            }
            foreach (DataRow row in table2.Rows)
            {
                string item = row["ColumnName"].ToString();
                ((OleDbType) row["ProviderType"]).ToString();
                row["DataType"].ToString();
                list.Add(item);
            }
            return list;
        }

        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType)
        {
            return GetExcelConnectstring(excelPath, header, eType, IMEXType.ImportMode);
        }

        public static string GetExcelConnectstring(string excelPath, bool header, ExcelType eType, IMEXType imex)
        {
            if (!FileUtil.IsExistFile(excelPath))
            {
                throw new FileNotFoundException("Excel路径不存在!");
            }
            string str = "NO";
            if (header)
            {
                str = "YES";
            }
            if (eType == ExcelType.const_0)
            {
                return string.Concat(new object[] { "Provider=Microsoft.Jet.OleDb.4.0; data source=", excelPath, ";Extended Properties='Excel 8.0; HDR=", str, "; IMEX=", imex.GetHashCode(), "'" });
            }
            return string.Concat(new object[] { "Provider=Microsoft.ACE.OLEDB.12.0; data source=", excelPath, ";Extended Properties='Excel 12.0 Xml; HDR=", str, "; IMEX=", imex.GetHashCode(), "'" });
        }

        public static string GetExcelFirstTableName(OleDbConnection connection)
        {
            string str = string.Empty;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if ((oleDbSchemaTable != null) && (oleDbSchemaTable.Rows.Count > 0))
            {
                str = ConvertHelper.ConvertTo<string>(oleDbSchemaTable.Rows[0][2]);
            }
            return str;
        }

        public static string GetExcelFirstTableName(string connectstring)
        {
            using (OleDbConnection connection = new OleDbConnection(connectstring))
            {
                return GetExcelFirstTableName(connection);
            }
        }

        public static string GetExcelFirstTableName(string excelPath, ExcelType eType)
        {
            return GetExcelFirstTableName(GetExcelConnectstring(excelPath, true, eType));
        }

        public static List<string> GetExcelTablesName(OleDbConnection connection)
        {
            List<string> list = new List<string>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if ((oleDbSchemaTable != null) && (oleDbSchemaTable.Rows.Count > 0))
            {
                for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
                {
                    list.Add(ConvertHelper.ConvertTo<string>(oleDbSchemaTable.Rows[i][2]));
                }
            }
            return list;
        }

        public static List<string> GetExcelTablesName(string connectstring)
        {
            using (OleDbConnection connection = new OleDbConnection(connectstring))
            {
                return GetExcelTablesName(connection);
            }
        }

        public static List<string> GetExcelTablesName(string excelPath, ExcelType eType)
        {
            return GetExcelTablesName(GetExcelConnectstring(excelPath, true, eType));
        }

        private static DataTable smethod_0(object object_0, IDbConnection idbConnection_0)
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

        private static bool smethod_1(OleDbConnection oleDbConnection_0, string string_0)
        {
            foreach (string str in GetExcelTablesName(oleDbConnection_0))
            {
                if (str.ToLower() == string_0.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public enum ExcelType
        {
            const_0,
            const_1
        }

        public enum IMEXType
        {
            ExportMode,
            ImportMode,
            LinkedMode
        }
    }
}

