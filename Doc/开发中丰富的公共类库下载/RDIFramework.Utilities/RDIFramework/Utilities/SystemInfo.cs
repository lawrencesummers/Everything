namespace RDIFramework.Utilities
{
    using System;

    public class SystemInfo
    {
        public static int AccountMinimumLength = 4;
        public static string AddInDirectory = @"AddIn\";
        public static bool AllowUserToRegister = true;
        public static string AppIco = @"Resource\RDIFrameworkApp.ico";
        public static bool AutoLogOn = false;
        public static string BugFeedback = "mailto:RDIFramework@126.com?subject=On the UMPllatForm feedback&body=Here to enter your valuable feedback";
        public static string BusinessDbConnection = string.Empty;
        public static string BusinessDbConnectionString = string.Empty;
        public static CurrentDbType BusinessDbType = CurrentDbType.SqlServer;
        public static bool CheckOnLine = false;
        public static bool ClientCache = false;
        public static string CompanyName = "";
        public static string CompanyPhone = "15108937790";
        public static ConfigurationCategory ConfigurationFrom = ConfigurationCategory.Configuration;
        public static string Copyright = "Copyright 2009-2012 Edward";
        public static string CurrentLanguage = "zh-CN";
        public static string CurrentPassword = string.Empty;
        public static string CurrentStyle = "VisualStudio2010Blue";
        public static string CurrentThemeColor = string.Empty;
        public static string CurrentUserName = string.Empty;
        public static string CustomerCompanyName = string.Empty;
        public static string CustomerPhone = "";
        public static string DateFormat = "yyyy-MM-dd";
        public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static string DbHelperAssmely = "RDIFramework.Utilities";
        public static string DefaultPassword = "abcd1234";
        public static bool EnableCheckIPAddress = false;
        public static bool EnableCheckPasswordStrength = false;
        public static bool EnableEncryptServerPassword = true;
        public static bool EnableHandWrittenSignature = true;
        public static bool EnableModulePermission = false;
        public static bool EnableOrganizePermission = false;
        public static bool EnablePermissionItem = false;
        public static bool EnableRecordLog = true;
        public static bool EnableRecordLogOnLog = true;
        public static bool EnableTableConstraintPermission = false;
        public static bool EnableTableFieldPermission = true;
        public static bool EnableUserAuthorization = true;
        public static bool EnableUserAuthorizationScope = false;
        public static bool EncryptClientPassword = true;
        public static bool EncryptDbConnection = false;
        public static string ErrorReportFrom = "406590790@qq.com";
        public static string ErrorReportMailPassword = "umplatform2012";
        public static string ErrorReportMailServer = "smtp.126.com";
        public static string ErrorReportMailUserName = "umplatform@126.com";
        public static bool EventLog = false;
        public static string HelpNamespace = string.Empty;
        public static string IEDownloadUrl = "http://download.microsoft.com/download/ie6sp1/finrel/6_sp1/W98NT42KMeXP/CN/ie6setup.exe";
        private int int_0 = 60;
        public static string LastUpdate = "2012.05.08";
        public static bool LoadAllUser = true;
        public static int LockNoWaitCount = 5;
        public static int LockNoWaitTickMilliSeconds = 30;
        public static bool LogException = true;
        public static string LogOnAssembly = "RDIFramework.NET";
        public static bool LogOned = false;
        public static string LogOnForm = "FrmLogOn";
        public static bool LogSQL = false;
        public static string MainAssembly = string.Empty;
        public static string MainForm = "FrmMaiForm";
        public static bool MatchCase = true;
        public static bool MultiLanguage = false;
        public static bool NeedRegister = true;
        public static bool NumericCharacters = true;
        public static int OnLineCheck = 60;
        public static int OnLineLimit = 0;
        public static int OnLineState = 0;
        public static int OnLineTime0ut = 140;
        public static bool OrganizeDynamicLoading = true;
        public static int PageSize = 50;
        public static int PasswordChangeCycle = 3;
        public static int PasswordErrowLockCycle = 30;
        public static int PasswordErrowLockLimit = 5;
        public static int PasswordMiniLength = 6;
        public static string RDIFrameworkBlog = "http://www.cnblogs.com/umplatform/";
        public static string RDIFrameworkDbConection = string.Empty;
        public static string RDIFrameworkDbConectionString = string.Empty;
        public static CurrentDbType RDIFrameworkDbType = CurrentDbType.SqlServer;
        public static string RDIFrameworkWeibo = "http://t.qq.com/yonghu86";
        public static string RegisterException = "请您联系：EricHu QQ:406590790 手机：15108937790 电子邮件：RDIFramework@126.com 对软件进行注册。";
        public static string RegisterKey = string.Empty;
        public static bool RememberPassword = true;
        public static string RootMenuCode = string.Empty;
        public static bool ServerCache = false;
        public static string Service = "RDIFramework.ServiceAdapter";
        public static string ServiceFactory = "ServiceFactory";
        public static string ServicePassword = "RDIFramework654123";
        public static string ServiceUserName = "RDIFramework";
        public static bool ShowExceptionDetail = true;
        public static bool ShowInformation = true;
        public static bool ShowSuccessMsg = true;
        public static string SoftFullName = string.Empty;
        public static string SoftName = string.Empty;
        public static string StartupPath = string.Empty;
        public static string Themes = string.Empty;
        public static string TimeFormat = "HH:mm:ss";
        public static int TopLimit = 200;
        public static bool UpdateVisit = true;
        public static string UploadDirectory = "Document/";
        public static bool UseMessage = false;
        private static RDIFramework.Utilities.UserInfo userInfo_0 = null;
        public static string Version = "1.1";
        public static string WebHostUrl = "WebHostUrl";
        public static string WorkFlowDbConnection = string.Empty;
        public static string WorkFlowDbConnectionString = string.Empty;
        public static CurrentDbType WorkFlowDbType = CurrentDbType.SqlServer;

        public static bool IsAuthorized(RDIFramework.Utilities.UserInfo userInfo)
        {
            if (userInfo == null)
            {
                return false;
            }
            return (string.IsNullOrEmpty(ServiceUserName) || (string.IsNullOrEmpty(ServicePassword) || (ServiceUserName.Equals(userInfo.ServiceUserName) && ServicePassword.Equals(userInfo.ServicePassword))));
        }

        public static void LogOn(RDIFramework.Utilities.UserInfo userInfo)
        {
            UserInfo = userInfo;
        }

        public int LockWaitMinute
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

        public static RDIFramework.Utilities.UserInfo UserInfo
        {
            get
            {
                if (userInfo_0 == null)
                {
                    userInfo_0 = new RDIFramework.Utilities.UserInfo();
                    if (string.IsNullOrEmpty(userInfo_0.String_0))
                    {
                        userInfo_0.String_0 = MachineInfoHelper.GetIPAddress();
                    }
                    if (string.IsNullOrEmpty(userInfo_0.String_1))
                    {
                        userInfo_0.String_1 = MachineInfoHelper.GetMacAddress();
                    }
                    if (string.IsNullOrEmpty(userInfo_0.Id))
                    {
                        userInfo_0.Id = MachineInfoHelper.GetIPAddress();
                    }
                    if (string.IsNullOrEmpty(userInfo_0.UserName))
                    {
                        userInfo_0.UserName = Environment.MachineName;
                    }
                    if (string.IsNullOrEmpty(userInfo_0.RealName))
                    {
                        userInfo_0.RealName = Environment.UserName;
                    }
                    userInfo_0.ServiceUserName = ServiceUserName;
                    userInfo_0.ServicePassword = ServicePassword;
                }
                return userInfo_0;
            }
            set
            {
                userInfo_0 = value;
            }
        }
    }
}

