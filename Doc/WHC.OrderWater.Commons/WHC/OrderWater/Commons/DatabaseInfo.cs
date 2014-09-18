namespace WHC.OrderWater.Commons
{
    using System;
    using System.Text;

    public class DatabaseInfo
    {
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;

        public DatabaseInfo()
        {
        }

        public DatabaseInfo(string connectionString)
        {
            this.string_0 = this.method_0(connectionString, "服务名称");
            if (this.string_0 == null)
            {
                this.string_0 = this.method_0(connectionString, "Data Source");
            }
            if (this.string_0 == null)
            {
                this.string_0 = this.method_0(connectionString, "server");
            }
            this.string_1 = this.method_0(connectionString, "数据库名称");
            if (this.string_1 == null)
            {
                this.string_1 = this.method_0(connectionString, "Initial Catalog");
            }
            if (this.string_1 == null)
            {
                this.string_1 = this.method_0(connectionString, "database");
            }
            this.string_2 = this.method_0(connectionString, "用户名称");
            if (this.string_2 == null)
            {
                this.string_2 = this.method_0(connectionString, "User ID");
            }
            if (this.string_2 == null)
            {
                this.string_2 = this.method_0(connectionString, "uid");
            }
            this.string_3 = this.method_0(connectionString, "用户密码");
            if (this.string_3 == null)
            {
                this.string_3 = this.method_0(connectionString, "Password");
            }
            if (this.string_3 == null)
            {
                this.string_3 = this.method_0(connectionString, "pwd");
            }
        }

        private string method_0(string string_4, string string_5)
        {
            string[] strArray = string_4.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToLower().IndexOf(string_5.ToLower()) >= 0)
                {
                    int index = strArray[i].IndexOf("=");
                    return strArray[i].Substring(index + 1);
                }
            }
            return null;
        }

        private string method_1(string string_4)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(string_4);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        public string ConnectionString
        {
            get
            {
                if (!(string.IsNullOrEmpty(this.UserId) || string.IsNullOrEmpty(this.Password)))
                {
                    return string.Format("Persist Security Info=False;Data Source={0};Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;Pooling=true;Max Pool Size=100;Min Pool Size=1", new object[] { this.string_0, this.string_1, this.string_2, this.string_3 });
                }
                return string.Format("Persist Security Info=False;Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Packet Size=4096;Pooling=true;Max Pool Size=100;Min Pool Size=1", this.string_0, this.string_1);
            }
        }

        public string Database
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

        public string EncryptConnectionString
        {
            get
            {
                return this.method_1(this.ConnectionString);
            }
        }

        public string OleDbConnectionString
        {
            get
            {
                string str = "Driver={SQL Server};";
                return (str + this.ConnectionString);
            }
        }

        public string Password
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

        public string Server
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

        public string UserId
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
    }
}

