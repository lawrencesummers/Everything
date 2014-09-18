namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Specialized;
    using System.Data;

    public class SqlBuilder
    {
        private DataView dataView_0;
        private string string_0;
        private string string_1;

        public SqlBuilder(string tableName)
        {
            this.string_0 = tableName;
            this.string_1 = "";
            this.dataView_0 = this.method_0();
            if ((this.dataView_0 == null) || (this.dataView_0.Table.Rows.Count == 0))
            {
                throw new Exception("要操作的表不存在！");
            }
        }

        public SqlBuilder(string tableName, string connname)
        {
            this.string_0 = tableName;
            if (connname == null)
            {
                this.string_1 = "";
            }
            else
            {
                this.string_1 = connname;
            }
            this.dataView_0 = this.method_0();
            if ((this.dataView_0 == null) || (this.dataView_0.Table.Rows.Count == 0))
            {
                throw new Exception("要操作的表不存在！");
            }
        }

        public string BuildInsertSql(NameValueCollection v)
        {
            return this.BuildInsertSql(v, 0);
        }

        public string BuildInsertSql(NameValueCollection v, int type)
        {
            if ((this.dataView_0 == null) || (this.dataView_0.Table.Rows.Count == 0))
            {
                throw new Exception("要操作的表或字段不存在！");
            }
            NameValueCollection values = new NameValueCollection();
            for (int i = 0; i < v.Count; i++)
            {
                string name = v.GetKey(i).ToLower();
                string str2 = "";
                if (v[i] != null)
                {
                    str2 = v[i].ToString();
                }
                values.Add(name, str2);
            }
            v = values;
            int num2 = 0;
            string str3 = "";
            string str4 = "";
            string str5 = this.method_1(v).ToLower();
            for (int j = 0; j < this.dataView_0.Table.Rows.Count; j++)
            {
                string str6 = this.dataView_0.Table.Rows[j]["name"].ToString();
                if ((v != null) && (str5.IndexOf("," + str6.ToLower() + ",") != -1))
                {
                    num2++;
                    string str7 = v[str6.ToLower()].ToString();
                    int.Parse(this.dataView_0.Table.Rows[j]["length"].ToString());
                    int num4 = int.Parse(this.dataView_0.Table.Rows[j]["xtype"].ToString());
                    if (num4 == -1)
                    {
                        throw new Exception("出现不支持的数据类型！");
                    }
                    switch (str7)
                    {
                        case null:
                        case "":
                        {
                            if (type == 0)
                            {
                                str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str6 + "] ") : (", [" + str6 + "] "));
                                str4 = str4 + (str4.Equals(string.Empty) ? " null " : ", null ");
                            }
                            else if (type == 1)
                            {
                                str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str6 + "] ") : (", [" + str6 + "] "));
                                switch (num4)
                                {
                                    case 0:
                                    {
                                        str4 = str4 + (str4.Equals(string.Empty) ? " 0 " : ", 0 ");
                                        continue;
                                    }
                                    case 1:
                                    {
                                        str4 = str4 + (str4.Equals(string.Empty) ? " '' " : ", '' ");
                                        continue;
                                    }
                                    case 2:
                                        str4 = str4 + (str4.Equals(string.Empty) ? " N'' " : ", N'' ");
                                        break;
                                }
                                if (num4 == 2)
                                {
                                    str4 = str4 + (str4.Equals(string.Empty) ? " N'' " : ", N'' ");
                                }
                                else
                                {
                                    str4 = str4 + (str4.Equals(string.Empty) ? " null " : ", null ");
                                }
                            }
                            continue;
                        }
                    }
                    if ((num4 == 0) || (num4 == 4))
                    {
                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str6 + "] ") : (", [" + str6 + "] "));
                        str4 = str4 + (str4.Equals(string.Empty) ? (" " + str7 + " ") : (", " + str7 + " "));
                    }
                    else if ((num4 == 1) || (num4 == 3))
                    {
                        str7 = str7.Replace("'", "''");
                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str6 + "] ") : (", [" + str6 + "] "));
                        str4 = str4 + (str4.Equals(string.Empty) ? (" '" + str7 + "' ") : (", '" + str7 + "' "));
                    }
                    else if (num4 == 2)
                    {
                        str7 = str7.Replace("'", "''");
                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str6 + "] ") : (", [" + str6 + "] "));
                        str4 = str4 + (str4.Equals(string.Empty) ? (" N'" + str7 + "' ") : (", N'" + str7 + "' "));
                    }
                }
            }
            if ((v != null) && (num2 != v.Count))
            {
                throw new Exception("指定的字段列表中某些字段不存在！");
            }
            if (!(str3.Equals(string.Empty) || str4.Equals(string.Empty)))
            {
                string format = "Insert into {0} ({1}) values ({2})";
                return string.Format(format, this.string_0, str3, str4);
            }
            return string.Empty;
        }

        public string BuildUpdateSql(NameValueCollection v)
        {
            return this.BuildUpdateSql(v, null, 0);
        }

        public string BuildUpdateSql(NameValueCollection v, NameValueCollection identity)
        {
            return this.BuildUpdateSql(v, identity, 0);
        }

        public string BuildUpdateSql(NameValueCollection v, int type)
        {
            return this.BuildUpdateSql(v, null, type);
        }

        public string BuildUpdateSql(NameValueCollection v, NameValueCollection identity, int type)
        {
            int num;
            string str;
            string str2;
            if ((this.dataView_0 == null) || (this.dataView_0.Table.Rows.Count == 0))
            {
                throw new Exception("要操作的表或字段不存在！");
            }
            NameValueCollection values = new NameValueCollection();
            for (num = 0; num < v.Count; num++)
            {
                str = v.GetKey(num).ToLower();
                str2 = "";
                if (v[num] != null)
                {
                    str2 = v[num].ToString();
                }
                values.Add(str, str2);
            }
            v = values;
            NameValueCollection values2 = new NameValueCollection();
            for (num = 0; num < identity.Count; num++)
            {
                str = identity.GetKey(num).ToLower();
                str2 = "";
                if (identity[num] != null)
                {
                    str2 = identity[num].ToString();
                }
                values2.Add(str, str2);
            }
            identity = values2;
            string str3 = "";
            string str4 = "";
            int num2 = 0;
            int num3 = 0;
            string str5 = this.method_1(v).ToLower();
            string str6 = this.method_1(identity).ToLower();
            for (int i = 0; i < this.dataView_0.Table.Rows.Count; i++)
            {
                string str8;
                int num5;
                string str7 = this.dataView_0.Table.Rows[i]["name"].ToString();
                if ((identity != null) && (str6.IndexOf("," + str7.ToLower() + ",") != -1))
                {
                    num2++;
                    str8 = identity[str7.ToLower()].ToString();
                    num5 = int.Parse(this.dataView_0.Table.Rows[i]["xtype"].ToString());
                    if (str8 == null)
                    {
                        throw new Exception("指定的条件没有给出匹配值！");
                    }
                    if ((num5 == 0) || (num5 == 4))
                    {
                        str4 = str4 + (str4.Equals(string.Empty) ? (" [" + str7 + "]=" + str8 + " ") : ("and [" + str7 + "]=" + str8 + " "));
                    }
                    else if ((num5 == 1) || (num5 == 3))
                    {
                        str8 = str8.Replace("'", "''");
                        str4 = str4 + (str4.Equals(string.Empty) ? (" [" + str7 + "]='" + str8 + "' ") : ("and [" + str7 + "]='" + str8 + "' "));
                    }
                    else if (num5 == 2)
                    {
                        str8 = str8.Replace("'", "''");
                        str4 = str4 + (str4.Equals(string.Empty) ? (" [" + str7 + "]=N'" + str8 + "' ") : ("and [" + str7 + "]=N'" + str8 + "' "));
                    }
                }
                if ((v != null) && (str5.IndexOf("," + str7.ToLower() + ",") != -1))
                {
                    num3++;
                    str8 = v[str7.ToLower()].ToString();
                    int.Parse(this.dataView_0.Table.Rows[i]["length"].ToString());
                    num5 = int.Parse(this.dataView_0.Table.Rows[i]["xtype"].ToString());
                    if (num5 == -1)
                    {
                        throw new Exception("出现不支持的数据类型！");
                    }
                    switch (str8)
                    {
                        case null:
                        case "":
                        {
                            if (type == 0)
                            {
                                str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=null ") : (", [" + str7 + "]=null "));
                            }
                            else if (type == 1)
                            {
                                switch (num5)
                                {
                                    case 0:
                                    {
                                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=0 ") : (", [" + str7 + "]=0 "));
                                        continue;
                                    }
                                    case 1:
                                    {
                                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]='' ") : (", [" + str7 + "]='' "));
                                        continue;
                                    }
                                    case 2:
                                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=N'' ") : (", [" + str7 + "]=N'' "));
                                        break;
                                }
                                if (num5 == 2)
                                {
                                    str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=N'' ") : (", [" + str7 + "]=N'' "));
                                }
                                else
                                {
                                    str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=null ") : (", [" + str7 + "]=null "));
                                }
                            }
                            continue;
                        }
                    }
                    if ((num5 == 0) || (num5 == 4))
                    {
                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=" + str8 + " ") : (", [" + str7 + "]=" + str8 + " "));
                    }
                    else if ((num5 == 1) || (num5 == 3))
                    {
                        str8 = str8.Replace("'", "''");
                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]='" + str8 + "' ") : (", [" + str7 + "]='" + str8 + "' "));
                    }
                    else if (num5 == 2)
                    {
                        str8 = str8.Replace("'", "''");
                        str3 = str3 + (str3.Equals(string.Empty) ? (" [" + str7 + "]=N'" + str8 + "' ") : (", [" + str7 + "]=N'" + str8 + "' "));
                    }
                }
            }
            if ((identity != null) && (num2 != identity.Count))
            {
                throw new Exception("指定的更新条件中某些字段不存在！");
            }
            if ((v != null) && (num3 != v.Count))
            {
                throw new Exception("指定的字段列表中某些字段不存在！");
            }
            if (!str3.Equals(string.Empty))
            {
                string format = "";
                if ((identity != null) && (identity.Count > 0))
                {
                    format = "update {0} set {1} where {2}";
                    return string.Format(format, this.string_0, str3, str4);
                }
                format = "update {0} set {1}";
                return string.Format(format, this.string_0, str3);
            }
            return string.Empty;
        }

        public string BuildUpdateSql(NameValueCollection v, string idcolumn, string idvalue)
        {
            return this.BuildUpdateSql(v, idcolumn, idvalue, 0);
        }

        public string BuildUpdateSql(NameValueCollection v, string idcolumn, string idvalue, int type)
        {
            NameValueCollection identity = new NameValueCollection();
            identity.Add(idcolumn, idvalue);
            return this.BuildUpdateSql(v, identity, type);
        }

        private DataView method_0()
        {
            string commandText = "\r\n                     select a.[name],a.[length]\r\n                     ,case \r\n                     when a.xusertype in (127,104,106,62,56,60,108,59,52,122,48) then 0 \r\n                     when a.xusertype in (175,35,167) then 1 \r\n                     when a.xusertype in (239,99,231) then 2\r\n                     when a.xusertype in (61,58) then 3 \r\n                     when a.xusertype in (165,173,36) then 4\r\n                     when a.xusertype in (34,98,241,189,256) then -1 else -1 end [xtype]\r\n                     from syscolumns a \r\n                     where object_id('" + this.string_0 + "')=a.[id] order by a.colid\r\n                    ";
            return DbHelperFactory.GetHelper(this.string_1).Fill(commandText).DefaultView;
        }

        private string method_1(NameValueCollection nameValueCollection_0)
        {
            // This item is obfuscated and can not be translated.
            string str = ",";
            for (int i = 0; nameValueCollection_0 == null; i++)
            {
                if (0 == 0)
                {
                    return str;
                }
                str = str + nameValueCollection_0.GetKey(i) + ',';
            }
            goto Label_000B;
        }

        private void method_2()
        {
            NameValueCollection v = new NameValueCollection();
            v.Add("col1", "value1");
            v.Add("col2", "value2");
            SqlBuilder builder = new SqlBuilder("testTable");
            builder.BuildInsertSql(v);
            builder.BuildUpdateSql(v, "idCol", "value");
        }
    }
}

