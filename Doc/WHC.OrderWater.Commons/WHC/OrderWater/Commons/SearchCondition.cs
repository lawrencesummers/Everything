namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class SearchCondition
    {
        private Hashtable hashtable_0 = new Hashtable();

        public SearchCondition AddCondition(string fielName, object fieldValue, SqlOperator sqlOperator)
        {
            this.hashtable_0.Add(Guid.NewGuid(), new SearchInfo(fielName, fieldValue, sqlOperator));
            return this;
        }

        public SearchCondition AddCondition(string fielName, object fieldValue, SqlOperator sqlOperator, bool excludeIfEmpty)
        {
            this.hashtable_0.Add(Guid.NewGuid(), new SearchInfo(fielName, fieldValue, sqlOperator, excludeIfEmpty));
            return this;
        }

        public SearchCondition AddCondition(string fielName, object fieldValue, SqlOperator sqlOperator, bool excludeIfEmpty, string groupName)
        {
            this.hashtable_0.Add(Guid.NewGuid(), new SearchInfo(fielName, fieldValue, sqlOperator, excludeIfEmpty, groupName));
            return this;
        }

        public string BuildConditionSql()
        {
            string str = new AppConfig().AppConfigGet("ComponentDbType");
            DatabaseType dbType = this.method_0(str);
            return this.BuildConditionSql(dbType);
        }

        public string BuildConditionSql(DatabaseType dbType)
        {
            string str = " Where (1=1) ";
            SearchInfo info = null;
            StringBuilder builder = new StringBuilder();
            str = str + this.method_1(dbType);
            foreach (DictionaryEntry entry in this.hashtable_0)
            {
                info = (SearchInfo) entry.Value;
                TypeCode typeCode = Type.GetTypeCode(info.FieldValue.GetType());
                if ((!info.ExcludeIfEmpty || ((info.FieldValue != null) && !string.IsNullOrEmpty(info.FieldValue.ToString()))) && string.IsNullOrEmpty(info.GroupName))
                {
                    if (info.SqlOperator == SqlOperator.Like)
                    {
                        builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("%{0}%", info.FieldValue));
                        continue;
                    }
                    if (info.SqlOperator == SqlOperator.NotLike)
                    {
                        builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("%{0}%", info.FieldValue));
                        continue;
                    }
                    if (info.SqlOperator == SqlOperator.LikeStartAt)
                    {
                        builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("{0}%", info.FieldValue));
                        continue;
                    }
                    if (info.SqlOperator == SqlOperator.In)
                    {
                        builder.AppendFormat(" AND {0} {1} {2}", info.FieldName, this.method_3(info.SqlOperator), string.Format("({0})", info.FieldValue));
                        continue;
                    }
                    if (dbType == DatabaseType.Oracle)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            DateTime time = Convert.ToDateTime(info.FieldValue);
                            if ((time.Hour > 0) || (time.Minute > 0))
                            {
                                builder.AppendFormat(" AND {0} {1} to_date('{2}','YYYY-MM-dd HH:mi')", info.FieldName, this.method_3(info.SqlOperator), time.ToString("yyyy-MM-dd HH:mm"));
                            }
                            else
                            {
                                builder.AppendFormat(" AND {0} {1} to_date('{2}','YYYY-MM-dd')", info.FieldName, this.method_3(info.SqlOperator), time.ToString("yyyy-MM-dd"));
                            }
                        }
                        else if (!info.ExcludeIfEmpty)
                        {
                            if (info.SqlOperator == SqlOperator.Equal)
                            {
                                builder.AppendFormat(" AND ({0} is null or {0}='')", info.FieldName);
                            }
                            else if (info.SqlOperator == SqlOperator.NotEqual)
                            {
                                builder.AppendFormat(" AND {0} is not null", info.FieldName);
                            }
                        }
                        else
                        {
                            builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                        continue;
                    }
                    if (dbType == DatabaseType.Access)
                    {
                        if (((info.SqlOperator == SqlOperator.Equal) && (typeCode == TypeCode.String)) && string.IsNullOrEmpty(info.FieldValue.ToString()))
                        {
                            builder.AppendFormat(" AND ({0} {1} '{2}' OR {0} IS NULL)", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                        else if (typeCode == TypeCode.DateTime)
                        {
                            builder.AppendFormat(" AND {0} {1} #{2}#", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                        else if ((((((typeCode == TypeCode.Byte) || (typeCode == TypeCode.Decimal)) || ((typeCode == TypeCode.Double) || (typeCode == TypeCode.Int16))) || (((typeCode == TypeCode.Int32) || (typeCode == TypeCode.Int64)) || ((typeCode == TypeCode.SByte) || (typeCode == TypeCode.Single)))) || ((typeCode == TypeCode.UInt16) || (typeCode == TypeCode.UInt32))) || (typeCode == TypeCode.UInt64))
                        {
                            builder.AppendFormat(" AND {0} {1} {2}", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                        else
                        {
                            builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                        continue;
                    }
                    if (dbType == DatabaseType.SQLite)
                    {
                        if (typeCode == TypeCode.DateTime)
                        {
                            builder.AppendFormat(" AND {0} {1} '{2}' ", info.FieldName, this.method_3(info.SqlOperator), Convert.ToDateTime(info.FieldValue).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                        continue;
                    }
                    builder.AppendFormat(" AND {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                }
            }
            return (str + builder.ToString());
        }

        private DatabaseType method_0(string string_0)
        {
            DatabaseType sqlServer = DatabaseType.SqlServer;
            using (IEnumerator enumerator = Enum.GetValues(typeof(DatabaseType)).GetEnumerator())
            {
                DatabaseType current;
                while (enumerator.MoveNext())
                {
                    current = (DatabaseType) enumerator.Current;
                    if (current.ToString().Equals(string_0, StringComparison.OrdinalIgnoreCase))
                    {
                        goto Label_0048;
                    }
                }
                return sqlServer;
            Label_0048:
                sqlServer = current;
            }
            return sqlServer;
        }

        private string method_1(DatabaseType databaseType_0)
        {
            Hashtable hashtable = this.method_2();
            SearchInfo info = null;
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            string format = string.Empty;
            foreach (string str3 in hashtable.Keys)
            {
                builder = new StringBuilder();
                format = " AND ({0})";
                foreach (DictionaryEntry entry in this.hashtable_0)
                {
                    info = (SearchInfo) entry.Value;
                    TypeCode typeCode = Type.GetTypeCode(info.FieldValue.GetType());
                    if ((!info.ExcludeIfEmpty || ((info.FieldValue != null) && !string.IsNullOrEmpty(info.FieldValue.ToString()))) && str3.Equals(info.GroupName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (info.SqlOperator == SqlOperator.Like)
                        {
                            builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("%{0}%", info.FieldValue));
                            continue;
                        }
                        if (info.SqlOperator == SqlOperator.NotLike)
                        {
                            builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("%{0}%", info.FieldValue));
                            continue;
                        }
                        if (info.SqlOperator == SqlOperator.LikeStartAt)
                        {
                            builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("{0}%", info.FieldValue));
                            continue;
                        }
                        if (databaseType_0 == DatabaseType.Oracle)
                        {
                            if (typeCode == TypeCode.DateTime)
                            {
                                DateTime time = Convert.ToDateTime(info.FieldValue);
                                if ((time.Hour > 0) || (time.Minute > 0))
                                {
                                    builder.AppendFormat(" OR {0} {1} to_date('{2}','YYYY-MM-dd HH:mi')", info.FieldName, this.method_3(info.SqlOperator), time.ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    builder.AppendFormat(" OR {0} {1} to_date('{2}','YYYY-MM-dd')", info.FieldName, this.method_3(info.SqlOperator), time.ToString("yyyy-MM-dd"));
                                }
                            }
                            else if (!info.ExcludeIfEmpty)
                            {
                                if (info.SqlOperator == SqlOperator.Equal)
                                {
                                    builder.AppendFormat(" OR ({0} is null or {0}='')", info.FieldName);
                                }
                                else if (info.SqlOperator == SqlOperator.NotEqual)
                                {
                                    builder.AppendFormat(" OR {0} is not null", info.FieldName);
                                }
                            }
                            else
                            {
                                builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                            }
                            continue;
                        }
                        if (databaseType_0 == DatabaseType.Access)
                        {
                            if (typeCode == TypeCode.DateTime)
                            {
                                builder.AppendFormat(" OR {0} {1} #{2}#", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                            }
                            else if ((((((typeCode == TypeCode.Byte) || (typeCode == TypeCode.Decimal)) || ((typeCode == TypeCode.Double) || (typeCode == TypeCode.Int16))) || (((typeCode == TypeCode.Int32) || (typeCode == TypeCode.Int64)) || ((typeCode == TypeCode.SByte) || (typeCode == TypeCode.Single)))) || ((typeCode == TypeCode.UInt16) || (typeCode == TypeCode.UInt32))) || (typeCode == TypeCode.UInt64))
                            {
                                builder.AppendFormat(" OR {0} {1} {2}", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                            }
                            else
                            {
                                builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                            }
                            continue;
                        }
                        if (databaseType_0 == DatabaseType.SQLite)
                        {
                            if (typeCode == TypeCode.DateTime)
                            {
                                builder.AppendFormat(" OR {0} {1} '{2}' ", info.FieldName, this.method_3(info.SqlOperator), Convert.ToDateTime(info.FieldValue).ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            else
                            {
                                builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                            }
                            continue;
                        }
                        if (info.SqlOperator == SqlOperator.Like)
                        {
                            builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), string.Format("%{0}%", info.FieldValue));
                        }
                        else
                        {
                            builder.AppendFormat(" OR {0} {1} '{2}'", info.FieldName, this.method_3(info.SqlOperator), info.FieldValue);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(builder.ToString()))
                {
                    format = string.Format(format, builder.ToString().Substring(3));
                    str = str + format;
                }
            }
            return str;
        }

        private Hashtable method_2()
        {
            Hashtable hashtable = new Hashtable();
            SearchInfo info = null;
            foreach (DictionaryEntry entry in this.hashtable_0)
            {
                info = (SearchInfo) entry.Value;
                if (!(string.IsNullOrEmpty(info.GroupName) || hashtable.Contains(info.GroupName)))
                {
                    hashtable.Add(info.GroupName, info.GroupName);
                }
            }
            return hashtable;
        }

        private string method_3(SqlOperator sqlOperator_0)
        {
            switch (sqlOperator_0)
            {
                case SqlOperator.Like:
                    return " Like ";

                case SqlOperator.NotLike:
                    return " NOT Like ";

                case SqlOperator.LikeStartAt:
                    return " Like ";

                case SqlOperator.Equal:
                    return " = ";

                case SqlOperator.NotEqual:
                    return " <> ";

                case SqlOperator.MoreThan:
                    return " > ";

                case SqlOperator.LessThan:
                    return " < ";

                case SqlOperator.MoreThanOrEqual:
                    return " >= ";

                case SqlOperator.LessThanOrEqual:
                    return " <= ";

                case SqlOperator.In:
                    return " in ";
            }
            return " = ";
        }

        private DbType method_4(object object_0)
        {
            DbType type = DbType.String;
            switch (object_0.GetType().ToString())
            {
                case "System.Int16":
                    return DbType.Int16;

                case "System.UInt16":
                    return DbType.UInt16;

                case "System.Single":
                    return DbType.Single;

                case "System.UInt32":
                    return DbType.UInt32;

                case "System.Int32":
                    return DbType.Int32;

                case "System.UInt64":
                    return DbType.UInt64;

                case "System.Int64":
                    return DbType.Int64;

                case "System.String":
                    return DbType.String;

                case "System.Double":
                    return DbType.Double;

                case "System.Decimal":
                    return DbType.Decimal;

                case "System.Byte":
                    return DbType.Byte;

                case "System.Boolean":
                    return DbType.Boolean;

                case "System.DateTime":
                    return DbType.DateTime;

                case "System.Guid":
                    return DbType.Guid;
            }
            return type;
        }

        public Hashtable ConditionTable
        {
            get
            {
                return this.hashtable_0;
            }
        }
    }
}

