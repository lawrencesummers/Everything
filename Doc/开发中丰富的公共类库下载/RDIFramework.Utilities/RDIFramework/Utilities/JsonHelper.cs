namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Script.Serialization;

    public class JsonHelper
    {
        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, object>>(jsonText);
        }

        public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
        {
            Dictionary<string, List<Dictionary<string, object>>> dictionary = new Dictionary<string, List<Dictionary<string, object>>>();
            foreach (DataTable table in ds.Tables)
            {
                dictionary.Add(table.TableName, DataTableToList(table));
            }
            return dictionary;
        }

        public static string DataTableToJSON(DataTable dt)
        {
            return ObjectToJSON(DataTableToList(dt));
        }

        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                foreach (DataColumn column in dt.Columns)
                {
                    item.Add(column.ColumnName, row[column.ColumnName]);
                }
                list.Add(item);
            }
            return list;
        }

        public static T JSONToObject<T>(string jsonText)
        {
            T local;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                local = serializer.Deserialize<T>(jsonText);
            }
            catch (Exception exception)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + exception.Message);
            }
            return local;
        }

        public static string ObjectToJSON(object obj)
        {
            string str;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                str = serializer.Serialize(obj);
            }
            catch (Exception exception)
            {
                throw new Exception("JSONHelper.ObjectToJSON(): " + exception.Message);
            }
            return str;
        }

        public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
        }
    }
}

