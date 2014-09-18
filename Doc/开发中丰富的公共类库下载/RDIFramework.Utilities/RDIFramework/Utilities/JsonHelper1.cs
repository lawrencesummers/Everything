namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;
    using System.Reflection;
    using System.Text;

    public class JsonHelper1
    {
        public static string Convert2Json(object o)
        {
            StringBuilder sb = new StringBuilder();
            WriteValue(sb, o);
            return sb.ToString();
        }

        public static string CreateJsonOne(DataTable dt, bool displayCount)
        {
            StringBuilder builder = new StringBuilder();
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < (dt.Columns.Count - 1))
                        {
                            builder.Append("ipt_" + dt.Columns[j].ColumnName.ToString().ToLower() + ":\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == (dt.Columns.Count - 1))
                        {
                            builder.Append("ipt_" + dt.Columns[j].ColumnName.ToString().ToLower() + ":\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == (dt.Rows.Count - 1))
                    {
                        builder.Append("} ");
                    }
                    else
                    {
                        builder.Append("}, ");
                    }
                }
                return builder.ToString();
            }
            return null;
        }

        public static string CreateJsonParameters(DataTable dt)
        {
            return CreateJsonParameters(dt, true);
        }

        public static string CreateJsonParameters(DataTable dt, bool displayCount)
        {
            return CreateJsonParameters(dt, displayCount, dt.Rows.Count);
        }

        public static string CreateJsonParameters(DataTable dt, bool displayCount, int totalcount)
        {
            StringBuilder builder = new StringBuilder();
            if (dt != null)
            {
                builder.Append("{ ");
                builder.Append("\"Rows\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < (dt.Columns.Count - 1))
                        {
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                builder.Append("\"" + dt.Columns[j].ColumnName + "\":" + dt.Rows[i][j].ToString() + ",");
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                builder.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\",");
                            }
                            else
                            {
                                builder.Append(string.Concat(new object[] { "\"", dt.Columns[j].ColumnName, "\":\"", dt.Rows[i][j], "\"," }));
                            }
                        }
                        else if (j == (dt.Columns.Count - 1))
                        {
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                builder.Append("\"" + dt.Columns[j].ColumnName + "\":" + dt.Rows[i][j].ToString());
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                builder.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\"");
                            }
                            else
                            {
                                builder.Append(string.Concat(new object[] { "\"", dt.Columns[j].ColumnName, "\":\"", dt.Rows[i][j], "\"" }));
                            }
                        }
                    }
                    if (i == (dt.Rows.Count - 1))
                    {
                        builder.Append("} ");
                    }
                    else
                    {
                        builder.Append("}, ");
                    }
                }
                builder.Append("]");
                if (displayCount)
                {
                    builder.Append(",");
                    builder.Append("\"Total\":");
                    builder.Append(totalcount);
                }
                builder.Append("}");
                return builder.ToString().Replace("\n", "");
            }
            return null;
        }

        private static void smethod_0(StringBuilder stringBuilder_0, DataRow dataRow_0)
        {
            stringBuilder_0.Append("{");
            foreach (DataColumn column in dataRow_0.Table.Columns)
            {
                stringBuilder_0.AppendFormat("\"{0}\":", column.ColumnName);
                WriteValue(stringBuilder_0, dataRow_0[column]);
                stringBuilder_0.Append(",");
            }
            if (dataRow_0.Table.Columns.Count > 0)
            {
                stringBuilder_0.Length--;
            }
            stringBuilder_0.Append("}");
        }

        private static void smethod_1(StringBuilder stringBuilder_0, DataSet dataSet_0)
        {
            stringBuilder_0.Append("{\"Tables\":{");
            foreach (DataTable table in dataSet_0.Tables)
            {
                stringBuilder_0.AppendFormat("\"{0}\":", table.TableName);
                smethod_2(stringBuilder_0, table);
                stringBuilder_0.Append(",");
            }
            if (dataSet_0.Tables.Count > 0)
            {
                stringBuilder_0.Length--;
            }
            stringBuilder_0.Append("}}");
        }

        private static void smethod_2(StringBuilder stringBuilder_0, DataTable dataTable_0)
        {
            stringBuilder_0.Append("{\"Rows\":[");
            foreach (DataRow row in dataTable_0.Rows)
            {
                smethod_0(stringBuilder_0, row);
                stringBuilder_0.Append(",");
            }
            if (dataTable_0.Rows.Count > 0)
            {
                stringBuilder_0.Length--;
            }
            stringBuilder_0.Append("]}");
        }

        private static void smethod_3(StringBuilder stringBuilder_0, IEnumerable ienumerable_0)
        {
            bool flag = false;
            stringBuilder_0.Append("[");
            foreach (object obj2 in ienumerable_0)
            {
                WriteValue(stringBuilder_0, obj2);
                stringBuilder_0.Append(",");
                flag = true;
            }
            if (flag)
            {
                stringBuilder_0.Length--;
            }
            stringBuilder_0.Append("]");
        }

        private static void smethod_4(StringBuilder stringBuilder_0, IDictionary idictionary_0)
        {
            bool flag = false;
            stringBuilder_0.Append("{");
            foreach (string str in idictionary_0.Keys)
            {
                stringBuilder_0.AppendFormat("\"{0}\":", str.ToLower());
                WriteValue(stringBuilder_0, idictionary_0[str]);
                stringBuilder_0.Append(",");
                flag = true;
            }
            if (flag)
            {
                stringBuilder_0.Length--;
            }
            stringBuilder_0.Append("}");
        }

        private static void smethod_5(StringBuilder stringBuilder_0, object object_0)
        {
            MemberInfo[] members = object_0.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
            stringBuilder_0.Append("{");
            bool flag = false;
            foreach (MemberInfo info in members)
            {
                bool flag2 = false;
                object val = null;
                if ((info.MemberType & MemberTypes.Field) == MemberTypes.Field)
                {
                    val = ((FieldInfo) info).GetValue(object_0);
                    flag2 = true;
                }
                else if ((info.MemberType & MemberTypes.Property) == MemberTypes.Property)
                {
                    PropertyInfo info3 = (PropertyInfo) info;
                    if (info3.CanRead && (info3.GetIndexParameters().Length == 0))
                    {
                        val = info3.GetValue(object_0, null);
                        flag2 = true;
                    }
                }
                if (flag2)
                {
                    stringBuilder_0.Append("\"");
                    stringBuilder_0.Append(info.Name);
                    stringBuilder_0.Append("\":");
                    WriteValue(stringBuilder_0, val);
                    stringBuilder_0.Append(",");
                    flag = true;
                }
            }
            if (flag)
            {
                stringBuilder_0.Length--;
            }
            stringBuilder_0.Append("}");
        }

        private static void smethod_6(StringBuilder stringBuilder_0, IEnumerable ienumerable_0)
        {
            stringBuilder_0.Append("\"");
            foreach (char ch in ienumerable_0)
            {
                switch (ch)
                {
                    case '\b':
                    {
                        stringBuilder_0.Append(@"\b");
                        continue;
                    }
                    case '\t':
                    {
                        stringBuilder_0.Append(@"\t");
                        continue;
                    }
                    case '\n':
                    {
                        stringBuilder_0.Append(@"\n");
                        continue;
                    }
                    case '\f':
                    {
                        stringBuilder_0.Append(@"\f");
                        continue;
                    }
                    case '\r':
                    {
                        stringBuilder_0.Append(@"\r");
                        continue;
                    }
                    case '"':
                    {
                        stringBuilder_0.Append("\\\"");
                        continue;
                    }
                    case '\\':
                    {
                        stringBuilder_0.Append(@"\\");
                        continue;
                    }
                }
                int num = ch;
                if ((num < 0x20) || (num > 0x7f))
                {
                    stringBuilder_0.AppendFormat(@"\u{0:X04}", num);
                }
                else
                {
                    stringBuilder_0.Append(ch);
                }
            }
            stringBuilder_0.Append("\"");
        }

        public static void WriteValue(StringBuilder sb, object val)
        {
            if ((val == null) || (val == DBNull.Value))
            {
                sb.Append("null");
            }
            else if ((val is string) || (val is Guid))
            {
                smethod_6(sb, val.ToString());
            }
            else if (val is bool)
            {
                sb.Append(val.ToString().ToLower());
            }
            else if (((((val is double) || (val is float)) || ((val is long) || (val is int))) || ((val is short) || (val is byte))) || (val is decimal))
            {
                sb.AppendFormat(CultureInfo.InvariantCulture.NumberFormat, "{0}", new object[] { val });
            }
            else if (val.GetType().IsEnum)
            {
                sb.Append((int) val);
            }
            else if (val is DateTime)
            {
                sb.Append("new Date(\"");
                sb.Append(((DateTime) val).ToString("MMMM, d yyyy HH:mm:ss", new CultureInfo("en-US", false).DateTimeFormat));
                sb.Append("\")");
            }
            else if (val is DataSet)
            {
                smethod_1(sb, val as DataSet);
            }
            else if (val is DataTable)
            {
                smethod_2(sb, val as DataTable);
            }
            else if (val is DataRow)
            {
                smethod_0(sb, val as DataRow);
            }
            else if (val is Hashtable)
            {
                smethod_4(sb, val as Hashtable);
            }
            else if (val is IEnumerable)
            {
                smethod_3(sb, val as IEnumerable);
            }
            else
            {
                smethod_5(sb, val);
            }
        }
    }
}

