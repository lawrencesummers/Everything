namespace RDIFramework.Utilities
{
    using System;

    [Serializable]
    public class UserInfo
    {
        private bool bool_0 = false;
        private int int_0 = 0;
        private int? nullable_0 = null;
        private int? nullable_1 = null;
        private int? nullable_2 = null;
        private string string_0 = "RDIFramework";
        private string string_1 = "RDIFramework";
        private string string_10 = string.Empty;
        private string string_11 = string.Empty;
        private string string_12 = string.Empty;
        private string string_13 = string.Empty;
        private string string_14 = string.Empty;
        private string string_15 = string.Empty;
        private string string_16 = string.Empty;
        private string string_17 = string.Empty;
        private string string_18 = string.Empty;
        private string string_19 = string.Empty;
        private string string_2 = string.Empty;
        private string string_20 = string.Empty;
        private string string_21 = string.Empty;
        private string string_22 = string.Empty;
        private string string_23 = string.Empty;
        private string string_3 = string.Empty;
        private string string_4 = string.Empty;
        private string string_5 = string.Empty;
        private string string_6 = string.Empty;
        private string string_7 = string.Empty;
        private string string_8 = string.Empty;
        private string string_9 = string.Empty;
        private int? tkfqFgVak = null;

        public UserInfo()
        {
            this.GetUserInfo();
        }

        public void GetUserInfo()
        {
            this.ServiceUserName = SystemInfo.ServiceUserName;
            this.ServicePassword = SystemInfo.ServicePassword;
            this.CurrentLanguage = SystemInfo.CurrentLanguage;
            this.Themes = SystemInfo.Themes;
        }

        public string Code
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public string CompanyCode
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public int? CompanyId
        {
            get
            {
                return this.nullable_0;
            }
            set
            {
                this.nullable_0 = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = value;
            }
        }

        public string CurrentLanguage
        {
            get
            {
                return this.string_19;
            }
            set
            {
                this.string_19 = value;
            }
        }

        public string DepartmentCode
        {
            get
            {
                return this.string_11;
            }
            set
            {
                this.string_11 = value;
            }
        }

        public int? DepartmentId
        {
            get
            {
                return this.nullable_1;
            }
            set
            {
                this.nullable_1 = value;
            }
        }

        public string DepartmentName
        {
            get
            {
                return this.string_12;
            }
            set
            {
                this.string_12 = value;
            }
        }

        public string Id
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public bool IsAdministrator
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

        public string LastVisit
        {
            get
            {
                return this.string_23;
            }
            set
            {
                this.string_23 = value;
            }
        }

        public string OpenId
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

        public string Password
        {
            get
            {
                return this.string_16;
            }
            set
            {
                this.string_16 = value;
            }
        }

        public string ProcessId
        {
            get
            {
                return this.string_22;
            }
            set
            {
                this.string_22 = value;
            }
        }

        public string ProcessName
        {
            get
            {
                return this.string_21;
            }
            set
            {
                this.string_21 = value;
            }
        }

        public string RealName
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public int? RoleId
        {
            get
            {
                return this.tkfqFgVak;
            }
            set
            {
                this.tkfqFgVak = value;
            }
        }

        public string RoleName
        {
            get
            {
                return this.string_15;
            }
            set
            {
                this.string_15 = value;
            }
        }

        public int SecurityLevel
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

        public string ServicePassword
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

        public string ServiceUserName
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

        public string StaffId
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public string String_0
        {
            get
            {
                return this.string_17;
            }
            set
            {
                this.string_17 = value;
            }
        }

        public string String_1
        {
            get
            {
                return this.string_18;
            }
            set
            {
                this.string_18 = value;
            }
        }

        public string TargetUserId
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

        public string Themes
        {
            get
            {
                return this.string_20;
            }
            set
            {
                this.string_20 = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string WorkgroupCode
        {
            get
            {
                return this.string_13;
            }
            set
            {
                this.string_13 = value;
            }
        }

        public int? WorkgroupId
        {
            get
            {
                return this.nullable_2;
            }
            set
            {
                this.nullable_2 = value;
            }
        }

        public string WorkgroupName
        {
            get
            {
                return this.string_14;
            }
            set
            {
                this.string_14 = value;
            }
        }
    }
}

