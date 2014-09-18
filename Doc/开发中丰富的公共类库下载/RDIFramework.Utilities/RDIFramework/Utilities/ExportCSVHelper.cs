namespace RDIFramework.Utilities
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;

    public class ExportCSVHelper
    {
        public static StringBuilder GetCSVFormatData(DataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataTable table in dataSet.Tables)
            {
                builder.Append(GetCSVFormatData(table));
            }
            return builder;
        }

        public static StringBuilder GetCSVFormatData(DataTable dataTable)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataColumn column in dataTable.Columns)
            {
                builder.Append(column.ColumnName.ToString() + ",");
            }
            builder.Append("\n");
            foreach (DataRowView view in dataTable.DefaultView)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    builder.Append(view[column.ColumnName].ToString() + ",");
                }
                builder.Append("\n");
            }
            return builder;
        }

        public static void GetResponseCSV(DataSet dataSet, string fileName)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(GetCSVFormatData(dataSet).ToString());
            HttpContext.Current.Response.End();
        }

        public static void GetResponseCSV(DataTable dataTable, string fileName)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(GetCSVFormatData(dataTable).ToString());
            HttpContext.Current.Response.End();
        }

        public static void smethod_0(DataTable dataTable, string fileName)
        {
            StreamWriter writer = null;
            if (SystemInfo.CurrentLanguage.Equals("zh-CN"))
            {
                writer = new StreamWriter(fileName, false, Encoding.GetEncoding("gb2312"));
            }
            else
            {
                writer = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8"));
            }
            writer.WriteLine(GetCSVFormatData(dataTable).ToString());
            writer.Flush();
            writer.Close();
        }

        public static void smethod_1(DataSet dataSet, string fileName)
        {
            StreamWriter writer = null;
            if (SystemInfo.CurrentLanguage.Equals("zh-CN"))
            {
                writer = new StreamWriter(fileName, false, Encoding.GetEncoding("gb2312"));
            }
            else
            {
                writer = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8"));
            }
            writer.WriteLine(GetCSVFormatData(dataSet).ToString());
            writer.Flush();
            writer.Close();
        }
    }
}

