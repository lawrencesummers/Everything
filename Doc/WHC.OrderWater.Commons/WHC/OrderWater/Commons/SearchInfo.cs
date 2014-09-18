namespace WHC.OrderWater.Commons
{
    using System;

    [Serializable]
    public class SearchInfo
    {
        private bool bool_0;
        private object object_0;
        private WHC.OrderWater.Commons.SqlOperator sqlOperator_0;
        private string string_0;
        private string string_1;

        public SearchInfo()
        {
            this.bool_0 = true;
        }

        public SearchInfo(string fieldName, object fieldValue, WHC.OrderWater.Commons.SqlOperator sqlOperator) : this(fieldName, fieldValue, sqlOperator, true)
        {
        }

        public SearchInfo(string fieldName, object fieldValue, WHC.OrderWater.Commons.SqlOperator sqlOperator, bool excludeIfEmpty) : this(fieldName, fieldValue, sqlOperator, excludeIfEmpty, null)
        {
        }

        public SearchInfo(string fieldName, object fieldValue, WHC.OrderWater.Commons.SqlOperator sqlOperator, bool excludeIfEmpty, string groupName)
        {
            this.bool_0 = true;
            this.string_0 = fieldName;
            this.object_0 = fieldValue;
            this.sqlOperator_0 = sqlOperator;
            this.bool_0 = excludeIfEmpty;
            this.string_1 = groupName;
        }

        public bool ExcludeIfEmpty
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

        public string FieldName
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

        public object FieldValue
        {
            get
            {
                return this.object_0;
            }
            set
            {
                this.object_0 = value;
            }
        }

        public string GroupName
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

        public WHC.OrderWater.Commons.SqlOperator SqlOperator
        {
            get
            {
                return this.sqlOperator_0;
            }
            set
            {
                this.sqlOperator_0 = value;
            }
        }
    }
}

