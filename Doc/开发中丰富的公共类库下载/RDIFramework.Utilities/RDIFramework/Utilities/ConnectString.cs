namespace RDIFramework.Utilities
{
    using System;

    [Serializable]
    public class ConnectString
    {
        private CurrentDbType currentDbType_0 = CurrentDbType.SqlServer;
        private string string_0 = string.Empty;
        private string string_1 = string.Empty;

        public string DbLink
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

        public CurrentDbType DbType
        {
            get
            {
                return this.currentDbType_0;
            }
            set
            {
                this.currentDbType_0 = value;
            }
        }

        public string GetSqlServerDBLink
        {
            get
            {
                return SecretHelper.smethod_1(this.DbLink);
            }
        }

        public string LinkName
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

