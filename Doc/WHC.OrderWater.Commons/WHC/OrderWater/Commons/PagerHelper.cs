namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;

    public class PagerHelper
    {
        private bool bool_0;
        private int int_0;
        private int int_1;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;

        public PagerHelper()
        {
            this.string_1 = "*";
            this.string_2 = string.Empty;
            this.int_0 = 10;
            this.int_1 = 1;
            this.bool_0 = false;
            this.string_3 = string.Empty;
        }

        public PagerHelper(string tableName, string fieldsToReturn, string fieldNameToSort, int pageSize, int pageIndex, bool isDescending, string strwhere)
        {
            this.string_1 = "*";
            this.string_2 = string.Empty;
            this.int_0 = 10;
            this.int_1 = 1;
            this.bool_0 = false;
            this.string_3 = string.Empty;
            this.string_0 = tableName;
            this.string_1 = fieldsToReturn;
            this.string_2 = fieldNameToSort;
            this.int_0 = pageSize;
            this.int_1 = pageIndex;
            this.bool_0 = isDescending;
            this.string_3 = strwhere;
        }

        public string GetPagingSql(bool isDoCount)
        {
            string str = new AppConfig().AppConfigGet("ComponentDbType");
            DatabaseType dbType = this.method_6(str);
            return this.GetPagingSql(dbType, isDoCount);
        }

        public string GetPagingSql(DatabaseType dbType, bool isDoCount)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    return this.method_2(isDoCount);

                case DatabaseType.Oracle:
                    return this.method_1(isDoCount);

                case DatabaseType.Access:
                    return this.method_3(isDoCount);

                case DatabaseType.MySql:
                    return this.method_4(isDoCount);

                case DatabaseType.SQLite:
                    return this.method_5(isDoCount);
            }
            return "";
        }

        internal string method_0()
        {
            if (this.string_0.ToLower().Contains("from"))
            {
                return string.Format("({0}) AA ", this.string_0);
            }
            return this.string_0;
        }

        private string method_1(bool bool_1)
        {
            if (string.IsNullOrEmpty(this.string_3))
            {
                this.string_3 = " (1=1) ";
            }
            if (bool_1)
            {
                return string.Format("select count(*) as Total from {0} Where {1} ", this.method_0(), this.string_3);
            }
            string str3 = string.Format(" order by {0} {1}", this.string_2, this.bool_0 ? "DESC" : "ASC");
            int num = this.int_0 * (this.int_1 - 1);
            int num2 = this.int_0 * this.int_1;
            string str4 = string.Format("select {0} from {1} Where {2} {3}", new object[] { this.string_1, this.method_0(), this.string_3, str3 });
            return string.Format("select b.* from\r\n                           (select a.*, rownum as rowIndex from({2}) a) b\r\n                           where b.rowIndex > {0} and b.rowIndex <= {1}", num, num2, str4);
        }

        private string method_2(bool bool_1)
        {
            string str = "";
            if (string.IsNullOrEmpty(this.string_3))
            {
                this.string_3 = " (1=1) ";
            }
            if (bool_1)
            {
                return string.Format("select count(*) as Total from {0} Where {1} ", this.method_0(), this.string_3);
            }
            string str3 = string.Empty;
            string str2 = string.Empty;
            if (this.bool_0)
            {
                str3 = "<(select min";
                str2 = string.Format(" order by [{0}] desc", this.string_2);
            }
            else
            {
                str3 = ">(select max";
                str2 = string.Format(" order by [{0}] asc", this.string_2);
            }
            str = string.Format("select top {0} {1} from {2} ", this.int_0, this.string_1, this.method_0());
            if (this.int_1 == 1)
            {
                return (str + string.Format(" Where {0} ", this.string_3) + str2);
            }
            return (str + string.Format(" Where [{0}] {1} ([{0}]) from (select top {2} [{0}] from {3} where {5} {4} ) as tblTmp) and {5} {4}", new object[] { this.string_2, str3, (this.int_1 - 1) * this.int_0, this.method_0(), str2, this.string_3 }));
        }

        private string method_3(bool bool_1)
        {
            return this.method_2(bool_1);
        }

        private string method_4(bool bool_1)
        {
            if (string.IsNullOrEmpty(this.string_3))
            {
                this.string_3 = " (1=1) ";
            }
            if (bool_1)
            {
                return string.Format("select count(*) as Total from {0} Where {1} ", this.method_0(), this.string_3);
            }
            string str3 = string.Format(" order by {0} {1}", this.string_2, this.bool_0 ? "DESC" : "ASC");
            int num = this.int_0 * (this.int_1 - 1);
            int num2 = this.int_0 * this.int_1;
            return string.Format("select {0} from {1} Where {2} {3} LIMIT {4},{5}", new object[] { this.string_1, this.method_0(), this.string_3, str3, num, num2 });
        }

        private string method_5(bool bool_1)
        {
            if (string.IsNullOrEmpty(this.string_3))
            {
                this.string_3 = " (1=1) ";
            }
            if (bool_1)
            {
                return string.Format("select count(*) as Total from {0} Where {1} ", this.method_0(), this.string_3);
            }
            string str2 = string.Format(" order by {0} {1}", this.string_2, this.bool_0 ? "DESC" : "ASC");
            int num = this.int_0 * (this.int_1 - 1);
            int num2 = this.int_0 * this.int_1;
            return string.Format("select {0} from {1} Where {2} {3} LIMIT {4},{5}", new object[] { this.string_1, this.method_0(), this.string_3, str2, num, num2 });
        }

        private DatabaseType method_6(string string_4)
        {
            DatabaseType sqlServer = DatabaseType.SqlServer;
            using (IEnumerator enumerator = Enum.GetValues(typeof(DatabaseType)).GetEnumerator())
            {
                DatabaseType current;
                while (enumerator.MoveNext())
                {
                    current = (DatabaseType) enumerator.Current;
                    if (current.ToString().Equals(string_4, StringComparison.OrdinalIgnoreCase))
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

        public string FieldNameToSort
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string FieldsToReturn
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public bool IsDescending
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public string StrWhere
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

