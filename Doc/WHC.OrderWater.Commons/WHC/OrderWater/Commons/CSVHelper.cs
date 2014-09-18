namespace WHC.OrderWater.Commons
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;

    public class CSVHelper
    {
        public static DataTable CSVToDataTableByOledb(string csvPath)
        {
            DataTable dataTable = new DataTable("csv");
            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException("csv文件路径不存在!");
            }
            FileInfo info = new FileInfo(csvPath);
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + info.DirectoryName + ";Extended Properties='Text;'"))
            {
                new OleDbDataAdapter("SELECT * FROM [" + info.Name + "]", connection).Fill(dataTable);
            }
            return dataTable;
        }

        public static DataTable CSVToDataTableByStreamReader(string csvPath)
        {
            DataTable table = new DataTable("csv");
            int length = 0;
            bool flag = true;
            string str = null;
            using (StreamReader reader = new StreamReader(csvPath, FileUtil.GetEncoding(csvPath)))
            {
                while (!string.IsNullOrEmpty(str = reader.ReadLine()))
                {
                    int num2;
                    string[] strArray = str.Split(new char[] { ',' });
                    if (flag)
                    {
                        flag = false;
                        length = strArray.Length;
                        num2 = 0;
                        while (num2 < strArray.Length)
                        {
                            DataColumn column = new DataColumn(strArray[num2]);
                            table.Columns.Add(column);
                            num2++;
                        }
                    }
                    else
                    {
                        DataRow row = table.NewRow();
                        for (num2 = 0; num2 < length; num2++)
                        {
                            row[num2] = strArray[num2];
                        }
                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }

        public static void DataTableToCSV(DataTable dt, string csvPath)
        {
            if (null != dt)
            {
                StringBuilder builder2 = new StringBuilder();
                StringBuilder builder = new StringBuilder();
                foreach (DataColumn column in dt.Columns)
                {
                    builder.Append(",");
                    builder.Append(column.ColumnName);
                }
                builder2.AppendLine(builder.ToString().Substring(1));
                foreach (DataRow row in dt.Rows)
                {
                    builder = new StringBuilder();
                    foreach (DataColumn column in dt.Columns)
                    {
                        builder.Append(",");
                        builder.Append(row[column.ColumnName].ToString().Replace(',', ' '));
                    }
                    builder2.AppendLine(builder.ToString().Substring(1));
                }
                File.WriteAllText(csvPath, builder2.ToString(), Encoding.Default);
            }
        }
    }
}

