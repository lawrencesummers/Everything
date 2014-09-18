namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;

    public class DataTableHelper
    {
        public static DataTable AddIdentityColumn(DataTable dt)
        {
            if (!dt.Columns.Contains("identityid"))
            {
                dt.Columns.Add("identityid");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["identityid"] = (i + 1).ToString();
                }
            }
            return dt;
        }

        public static string ConcatColumnValue(DataTable dt, string columnName, string append)
        {
            string str = append;
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                foreach (DataRow row in dt.Rows)
                {
                    str = str + string.Format(",{0}", row[columnName]);
                }
            }
            return str.Trim(new char[] { ',' });
        }

        public static string ConcatColumnValue(DataTable dt, string columnName, string append, char splitChar)
        {
            string str = append;
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                foreach (DataRow row in dt.Rows)
                {
                    str = str + string.Format("{0}{1}", splitChar, row[columnName]);
                }
            }
            return str.Trim(new char[] { splitChar });
        }

        public static DataTable CreateTable(List<string> nameList)
        {
            if (nameList.Count <= 0)
            {
                return null;
            }
            DataTable table2 = new DataTable();
            foreach (string str in nameList)
            {
                table2.Columns.Add(str, typeof(string));
            }
            return table2;
        }

        public static DataTable CreateTable(string nameString)
        {
            string[] strArray = nameString.Split(new char[] { ',', ';' });
            new List<string>();
            DataTable table = new DataTable();
            foreach (string str in strArray)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strArray3 = str.Split(new char[] { '|' });
                    if (strArray3.Length == 2)
                    {
                        table.Columns.Add(strArray3[0], smethod_0(strArray3[1]));
                    }
                    else
                    {
                        table.Columns.Add(strArray3[0]);
                    }
                }
            }
            return table;
        }

        public static IList<T> DataTableToList<T>(DataTable table) where T: class
        {
            if (!IsHaveRows(table))
            {
                return new List<T>();
            }
            IList<T> list2 = new List<T>();
            T local = default(T);
            foreach (DataRow row in table.Rows)
            {
                local = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    object obj2 = row[column.ColumnName];
                    PropertyInfo property = local.GetType().GetProperty(column.ColumnName);
                    if (((property != null) && property.CanWrite) && ((obj2 != null) && !Convert.IsDBNull(obj2)))
                    {
                        property.SetValue(local, obj2, null);
                    }
                }
                list2.Add(local);
            }
            return list2;
        }

        public static DataTable FilterDataTable(DataTable dt, string condition)
        {
            if (condition.Trim() == "")
            {
                return dt;
            }
            DataTable table = new DataTable();
            table = dt.Clone();
            DataRow[] rowArray = dt.Select(condition);
            for (int i = 0; i < rowArray.Length; i++)
            {
                table.ImportRow(rowArray[i]);
            }
            return table;
        }

        public static DataRow[] GetDataRowArray(DataRowCollection drc)
        {
            int count = drc.Count;
            DataRow[] rowArray = new DataRow[count];
            for (int i = 0; i < count; i++)
            {
                rowArray[i] = drc[i];
            }
            return rowArray;
        }

        public static DataTable GetTableFromRows(DataRow[] rows)
        {
            if (rows.Length <= 0)
            {
                return new DataTable();
            }
            DataTable table = rows[0].Table.Clone();
            table.DefaultView.Sort = rows[0].Table.DefaultView.Sort;
            for (int i = 0; i < rows.Length; i++)
            {
                table.LoadDataRow(rows[i].ItemArray, true);
            }
            return table;
        }

        public static bool IsHaveRows(DataTable dt)
        {
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        public static DataTable ListToDataTable<T>(IList<T> list) where T: class
        {
            if ((list == null) || (list.Count <= 0))
            {
                return null;
            }
            DataTable table2 = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int length = properties.Length;
            bool flag = true;
            foreach (T local in list)
            {
                if (local != null)
                {
                    DataRow row = table2.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        PropertyInfo info = properties[i];
                        string name = info.Name;
                        if (flag)
                        {
                            DataColumn column = new DataColumn(name, info.PropertyType);
                            table2.Columns.Add(column);
                        }
                        row[name] = info.GetValue(local, null);
                    }
                    if (flag)
                    {
                        flag = false;
                    }
                    table2.Rows.Add(row);
                }
            }
            return table2;
        }

        private static Type smethod_0(string string_0)
        {
            string_0 = string_0.ToLower().Replace("system.", "");
            Type type = typeof(string);
            switch (string_0)
            {
                case "boolean":
                case "bool":
                    return typeof(bool);

                case "int16":
                case "short":
                    return typeof(short);

                case "int32":
                case "int":
                    return typeof(int);

                case "long":
                case "int64":
                    return typeof(long);

                case "uint16":
                case "ushort":
                    return typeof(ushort);

                case "uint32":
                case "uint":
                    return typeof(uint);

                case "uint64":
                case "ulong":
                    return typeof(ulong);

                case "single":
                case "float":
                    return typeof(float);

                case "string":
                    return typeof(string);

                case "guid":
                    return typeof(Guid);

                case "decimal":
                    return typeof(decimal);

                case "double":
                    return typeof(double);

                case "datetime":
                    return typeof(DateTime);

                case "byte":
                    return typeof(byte);

                case "char":
                    return typeof(char);
            }
            return type;
        }

        public static DataTable SortedTable(DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string str = "";
                for (int i = 0; i < sorts.Length; i++)
                {
                    str = str + sorts[i] + ",";
                }
                dt.DefaultView.Sort = str.TrimEnd(new char[] { ',' });
            }
            return dt;
        }

        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable<T>(list, null);
        }

        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> list2 = new List<string>();
            if (propertyName != null)
            {
                list2.AddRange(propertyName);
            }
            DataTable table = new DataTable();
            if (list.Count > 0)
            {
                T local = list[0];
                PropertyInfo[] properties = local.GetType().GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    if (list2.Count == 0)
                    {
                        table.Columns.Add(info.Name, info.PropertyType);
                    }
                    else if (list2.Contains(info.Name))
                    {
                        table.Columns.Add(info.Name, info.PropertyType);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList list3 = new ArrayList();
                    foreach (PropertyInfo info in properties)
                    {
                        object obj2;
                        if (list2.Count == 0)
                        {
                            obj2 = info.GetValue(list[i], null);
                            list3.Add(obj2);
                        }
                        else if (list2.Contains(info.Name))
                        {
                            obj2 = info.GetValue(list[i], null);
                            list3.Add(obj2);
                        }
                    }
                    object[] values = list3.ToArray();
                    table.LoadDataRow(values, true);
                }
            }
            return table;
        }

        public static DbType TypeToDbType(Type t)
        {
            try
            {
                return (DbType) Enum.Parse(typeof(DbType), t.Name);
            }
            catch
            {
                return DbType.Object;
            }
        }
    }
}

