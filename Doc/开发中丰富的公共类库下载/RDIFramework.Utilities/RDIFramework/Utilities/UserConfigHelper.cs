namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Forms;
    using System.Xml;

    public class UserConfigHelper
    {
        public static string LogOnTo = "Config";
        public static string SelectPath = "//appSettings/add";

        public static bool Exists(string key)
        {
            return !string.IsNullOrEmpty(GetValue(key));
        }

        public static void GetConfig()
        {
            GetConfig(ConfigFielName);
        }

        public static void GetConfig(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            if (Exists("CurrentUserName"))
            {
                SystemInfo.CurrentUserName = GetValue(xmlDocument, "CurrentUserName");
            }
            if (Exists("CurrentPassword"))
            {
                SystemInfo.CurrentPassword = GetValue(xmlDocument, "CurrentPassword");
            }
            if (Exists("MultiLanguage"))
            {
                SystemInfo.MultiLanguage = string.Compare(GetValue(xmlDocument, "MultiLanguage"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("CurrentLanguage"))
            {
                SystemInfo.CurrentLanguage = GetValue(xmlDocument, "CurrentLanguage");
            }
            if (Exists("RememberPassword"))
            {
                SystemInfo.RememberPassword = string.Compare(GetValue(xmlDocument, "RememberPassword"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("AutoLogOn"))
            {
                SystemInfo.AutoLogOn = string.Compare(GetValue(xmlDocument, "AutoLogOn"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableCheckPasswordStrength"))
            {
                SystemInfo.EnableCheckPasswordStrength = string.Compare(GetValue(xmlDocument, "EnableCheckPasswordStrength"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EncryptClientPassword"))
            {
                SystemInfo.EncryptClientPassword = string.Compare(GetValue(xmlDocument, "EncryptClientPassword"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableEncryptServerPassword"))
            {
                SystemInfo.EnableEncryptServerPassword = string.Compare(GetValue(xmlDocument, "EnableEncryptServerPassword"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("CurrentStyle"))
            {
                SystemInfo.CurrentStyle = GetValue(xmlDocument, "CurrentStyle");
            }
            if (Exists("CurrentThemeColor"))
            {
                SystemInfo.CurrentThemeColor = GetValue(xmlDocument, "CurrentThemeColor");
            }
            if (Exists("OnLineTime0ut"))
            {
                string str = GetValue(xmlDocument, "OnLineTime0ut");
                if (!(string.IsNullOrEmpty(str) || !MathHelper.IsInteger(str)))
                {
                    SystemInfo.OnLineTime0ut = Convert.ToInt16(str);
                }
            }
            if (Exists("EnableCheckIPAddress"))
            {
                SystemInfo.EnableCheckIPAddress = string.Compare(GetValue(xmlDocument, "EnableCheckIPAddress"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("CheckOnLine"))
            {
                SystemInfo.CheckOnLine = string.Compare(GetValue(xmlDocument, "CheckOnLine"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("UseMessage"))
            {
                SystemInfo.UseMessage = string.Compare(GetValue(xmlDocument, "UseMessage"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("AllowUserToRegister"))
            {
                SystemInfo.AllowUserToRegister = string.Compare(GetValue(xmlDocument, "AllowUserToRegister"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableRecordLog"))
            {
                SystemInfo.EnableRecordLog = string.Compare(GetValue(xmlDocument, "EnableRecordLog"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            SystemInfo.CustomerCompanyName = GetValue(xmlDocument, "CustomerCompanyName");
            SystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(GetValue(xmlDocument, "ConfigurationFrom"));
            SystemInfo.SoftName = GetValue(xmlDocument, "SoftName");
            SystemInfo.SoftFullName = GetValue(xmlDocument, "SoftFullName");
            SystemInfo.RootMenuCode = GetValue(xmlDocument, "RootMenuCode");
            SystemInfo.Version = GetValue(xmlDocument, "Version");
            if (Exists("EnableUserAuthorization"))
            {
                SystemInfo.EnableUserAuthorization = string.Compare(GetValue(xmlDocument, "EnableUserAuthorization"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableModulePermission"))
            {
                SystemInfo.EnableModulePermission = string.Compare(GetValue(xmlDocument, "EnableModulePermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnablePermissionItem"))
            {
                SystemInfo.EnablePermissionItem = string.Compare(GetValue(xmlDocument, "EnablePermissionItem"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableTableFieldPermission"))
            {
                SystemInfo.EnableTableFieldPermission = string.Compare(GetValue(xmlDocument, "EnableTableFieldPermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableTableConstraintPermission"))
            {
                SystemInfo.EnableTableConstraintPermission = string.Compare(GetValue(xmlDocument, "EnableTableConstraintPermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableUserAuthorizationScope"))
            {
                SystemInfo.EnableUserAuthorizationScope = string.Compare(GetValue(xmlDocument, "EnableUserAuthorizationScope"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("EnableHandWrittenSignature"))
            {
                SystemInfo.EnableHandWrittenSignature = string.Compare(GetValue(xmlDocument, "EnableHandWrittenSignature"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            if (Exists("DefaultPassword"))
            {
                SystemInfo.DefaultPassword = GetValue(xmlDocument, "DefaultPassword");
                SystemInfo.DefaultPassword = SecretHelper.smethod_1(SystemInfo.DefaultPassword);
            }
            if (Exists("LoadAllUser"))
            {
                SystemInfo.LoadAllUser = string.Compare(GetValue(xmlDocument, "LoadAllUser"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            }
            SystemInfo.Service = GetValue(xmlDocument, "Service");
            if (Exists("LogOnAssembly"))
            {
                SystemInfo.LogOnAssembly = GetValue(xmlDocument, "LogOnAssembly");
            }
            if (Exists("LogOnForm"))
            {
                SystemInfo.LogOnForm = GetValue(xmlDocument, "LogOnForm");
            }
            if (Exists("MainForm"))
            {
                SystemInfo.MainForm = GetValue(xmlDocument, "MainForm");
            }
            int.TryParse(GetValue(xmlDocument, "OnLineLimit"), out SystemInfo.OnLineLimit);
            if (Exists("RDIFrameworkDbType"))
            {
                SystemInfo.RDIFrameworkDbType = BaseConfiguration.GetDbType(GetValue(xmlDocument, "RDIFrameworkDbType"));
            }
            if (Exists("BusinessDbType"))
            {
                SystemInfo.BusinessDbType = BaseConfiguration.GetDbType(GetValue(xmlDocument, "BusinessDbType"));
            }
            if (Exists("WorkFlowDbType"))
            {
                SystemInfo.WorkFlowDbType = BaseConfiguration.GetDbType(GetValue(xmlDocument, "WorkFlowDbType"));
            }
            SystemInfo.EncryptDbConnection = string.Compare(GetValue(xmlDocument, "EncryptDbConnection"), "TRUE", true, CultureInfo.CurrentCulture) == 0;
            SystemInfo.BusinessDbConnectionString = GetValue(xmlDocument, "BusinessDbConnection");
            SystemInfo.WorkFlowDbConnectionString = GetValue(xmlDocument, "WorkFlowDbConnection");
            SystemInfo.RDIFrameworkDbConectionString = GetValue(xmlDocument, "RDIFrameworkDbConection");
            if (SystemInfo.EncryptDbConnection)
            {
                SystemInfo.WorkFlowDbConnection = SecretHelper.smethod_1(SystemInfo.WorkFlowDbConnectionString);
                SystemInfo.BusinessDbConnection = SecretHelper.smethod_1(SystemInfo.BusinessDbConnectionString);
                SystemInfo.RDIFrameworkDbConection = SecretHelper.smethod_1(SystemInfo.RDIFrameworkDbConectionString);
            }
            else
            {
                SystemInfo.WorkFlowDbConnection = SystemInfo.WorkFlowDbConnectionString;
                SystemInfo.BusinessDbConnection = SystemInfo.BusinessDbConnectionString;
                SystemInfo.RDIFrameworkDbConection = SystemInfo.RDIFrameworkDbConectionString;
            }
            SystemInfo.ServiceUserName = GetValue(xmlDocument, "ServiceUserName");
            SystemInfo.ServicePassword = GetValue(xmlDocument, "ServicePassword");
            SystemInfo.ServicePassword = SecretHelper.smethod_1(SystemInfo.ServicePassword);
            SystemInfo.RegisterKey = GetValue(xmlDocument, "RegisterKey");
            SystemInfo.ErrorReportFrom = GetValue(xmlDocument, "ErrorReportFrom");
            SystemInfo.ErrorReportMailUserName = GetValue(xmlDocument, "ErrorReportMailUserName");
            SystemInfo.ErrorReportMailPassword = GetValue(xmlDocument, "ErrorReportMailPassword");
            SystemInfo.ErrorReportMailServer = GetValue(xmlDocument, "ErrorReportMailServer");
            SystemInfo.ErrorReportMailPassword = SecretHelper.smethod_1(SystemInfo.ErrorReportMailPassword);
            string str2 = GetValue(xmlDocument, "RDIFrameworkBlog");
            if (!string.IsNullOrEmpty(str2))
            {
                SystemInfo.RDIFrameworkBlog = str2;
            }
            string str3 = GetValue(xmlDocument, "RDIFrameworkWeibo");
            if (!string.IsNullOrEmpty(str3))
            {
                SystemInfo.RDIFrameworkWeibo = str3;
            }
        }

        public static Dictionary<string, string> GetLogOnTo()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XmlDocument document = new XmlDocument();
            document.Load(ConfigFielName);
            foreach (XmlNode node in document.SelectNodes(SelectPath))
            {
                if (node.Attributes["key"].Value.ToUpper().Equals("LogOnTo".ToUpper()))
                {
                    dictionary.Add(node.Attributes["value"].Value, node.Attributes["dispaly"].Value);
                }
            }
            return dictionary;
        }

        public static string GetOption(XmlDocument xmlDocument, string selectPath, string key)
        {
            string str = string.Empty;
            using (IEnumerator enumerator = xmlDocument.SelectNodes(selectPath).GetEnumerator())
            {
                XmlNode current;
                while (enumerator.MoveNext())
                {
                    current = (XmlNode) enumerator.Current;
                    if (current.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()) && (current.Attributes["Options"] != null))
                    {
                        goto Label_006D;
                    }
                }
                return str;
            Label_006D:
                str = current.Attributes["Options"].Value;
            }
            return str;
        }

        public static string[] GetOptions(string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigFielName);
            return GetOption(xmlDocument, SelectPath, key).Split(new char[] { ',' });
        }

        public static string GetValue(string key)
        {
            return GetValue(ConfigFielName, SelectPath, key);
        }

        public static string GetValue(string fileName, string key)
        {
            return GetValue(fileName, SelectPath, key);
        }

        public static string GetValue(XmlDocument xmlDocument, string key)
        {
            return GetValue(xmlDocument, SelectPath, key);
        }

        public static string GetValue(string fileName, string selectPath, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return GetValue(xmlDocument, selectPath, key);
        }

        public static string GetValue(XmlDocument xmlDocument, string selectPath, string key)
        {
            string str = string.Empty;
            using (IEnumerator enumerator = xmlDocument.SelectNodes(selectPath).GetEnumerator())
            {
                XmlNode current;
                while (enumerator.MoveNext())
                {
                    current = (XmlNode) enumerator.Current;
                    if (current.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                    {
                        goto Label_0058;
                    }
                }
                return str;
            Label_0058:
                str = current.Attributes["value"].Value;
            }
            return str;
        }

        public static void SaveConfig()
        {
            SaveConfig(ConfigFielName);
        }

        public static void SaveConfig(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            SetValue(xmlDocument, "CurrentUserName", SystemInfo.CurrentUserName);
            SetValue(xmlDocument, "CurrentPassword", SystemInfo.CurrentPassword);
            SetValue(xmlDocument, "MultiLanguage", SystemInfo.MultiLanguage.ToString());
            SetValue(xmlDocument, "CurrentLanguage", SystemInfo.CurrentLanguage);
            SetValue(xmlDocument, "RememberPassword", SystemInfo.RememberPassword.ToString());
            SetValue(xmlDocument, "EncryptClientPassword", SystemInfo.EncryptClientPassword.ToString());
            SetValue(xmlDocument, "EnableCheckIPAddress", SystemInfo.EnableCheckIPAddress.ToString());
            SetValue(xmlDocument, "CurrentStyle", SystemInfo.CurrentStyle.ToString());
            SetValue(xmlDocument, "CurrentThemeColor", SystemInfo.CurrentThemeColor.ToString());
            SetValue(xmlDocument, "EnableCheckPasswordStrength", SystemInfo.EnableCheckPasswordStrength.ToString());
            SetValue(xmlDocument, "EnableEncryptServerPassword", SystemInfo.EnableEncryptServerPassword.ToString());
            SetValue(xmlDocument, "PasswordMiniLength", SystemInfo.PasswordMiniLength.ToString());
            SetValue(xmlDocument, "NumericCharacters", SystemInfo.NumericCharacters.ToString());
            SetValue(xmlDocument, "PasswordChangeCycle", SystemInfo.PasswordChangeCycle.ToString());
            SetValue(xmlDocument, "CheckOnLine", SystemInfo.CheckOnLine.ToString());
            SetValue(xmlDocument, "OnLineTime0ut", SystemInfo.OnLineTime0ut.ToString());
            SetValue(xmlDocument, "AccountMinimumLength", SystemInfo.AccountMinimumLength.ToString());
            SetValue(xmlDocument, "PasswordErrowLockLimit", SystemInfo.PasswordErrowLockLimit.ToString());
            SetValue(xmlDocument, "PasswordErrowLockCycle", SystemInfo.PasswordErrowLockCycle.ToString());
            SetValue(xmlDocument, "DefaultPassword", SecretHelper.smethod_0(SystemInfo.DefaultPassword.ToString()));
            SetValue(xmlDocument, "UseMessage", SystemInfo.UseMessage.ToString());
            SetValue(xmlDocument, "AutoLogOn", SystemInfo.AutoLogOn.ToString());
            SetValue(xmlDocument, "AllowUserToRegister", SystemInfo.AllowUserToRegister.ToString());
            SetValue(xmlDocument, "EnableRecordLog", SystemInfo.EnableRecordLog.ToString());
            SetValue(xmlDocument, "CustomerCompanyName", SystemInfo.CustomerCompanyName);
            SetValue(xmlDocument, "ConfigurationFrom", SystemInfo.ConfigurationFrom.ToString());
            SetValue(xmlDocument, "SoftName", SystemInfo.SoftName);
            SetValue(xmlDocument, "SoftFullName", SystemInfo.SoftFullName);
            SetValue(xmlDocument, "RootMenuCode", SystemInfo.RootMenuCode);
            SetValue(xmlDocument, "Version", SystemInfo.Version);
            SetValue(xmlDocument, "EnableUserAuthorization", SystemInfo.EnableUserAuthorization.ToString());
            SetValue(xmlDocument, "EnableModulePermission", SystemInfo.EnableModulePermission.ToString());
            SetValue(xmlDocument, "EnableTableFieldPermission", SystemInfo.EnableTableFieldPermission.ToString());
            SetValue(xmlDocument, "EnableTableConstraintPermission", SystemInfo.EnableTableConstraintPermission.ToString());
            SetValue(xmlDocument, "LoadAllUser", SystemInfo.LoadAllUser.ToString());
            SetValue(xmlDocument, "Service", SystemInfo.Service);
            SetValue(xmlDocument, "ServiceUserName", SystemInfo.ServiceUserName);
            SetValue(xmlDocument, "ServicePassword", SecretHelper.smethod_0(SystemInfo.ServicePassword));
            SetValue(xmlDocument, "LogOnAssembly", SystemInfo.LogOnAssembly);
            SetValue(xmlDocument, "LogOnForm", SystemInfo.LogOnForm);
            SetValue(xmlDocument, "MainForm", SystemInfo.MainForm);
            SetValue(xmlDocument, "OnLineLimit", SystemInfo.OnLineLimit.ToString());
            SetValue(xmlDocument, "DbType", SystemInfo.BusinessDbType.ToString());
            SetValue(xmlDocument, "BusinessDbType", SystemInfo.BusinessDbType.ToString());
            SetValue(xmlDocument, "RDIFrameworkDbType", SystemInfo.RDIFrameworkDbType.ToString());
            SetValue(xmlDocument, "WorkFlowDbType", SystemInfo.WorkFlowDbType.ToString());
            SetValue(xmlDocument, "EncryptDbConnection", SystemInfo.EncryptDbConnection.ToString());
            SetValue(xmlDocument, "BusinessDbConnection", SystemInfo.BusinessDbConnectionString);
            SetValue(xmlDocument, "RDIFrameworkDbConection", SystemInfo.RDIFrameworkDbConectionString);
            if (SystemInfo.EncryptDbConnection)
            {
                SetValue(xmlDocument, "RDIFrameworkDbConection", SecretHelper.smethod_0(SystemInfo.RDIFrameworkDbConectionString));
            }
            SetValue(xmlDocument, "WorkFlowDbConnection", SystemInfo.WorkFlowDbConnectionString);
            SetValue(xmlDocument, "RegisterKey", SystemInfo.RegisterKey);
            SetValue(xmlDocument, "ErrorReportFrom", SystemInfo.ErrorReportFrom);
            SetValue(xmlDocument, "ErrorReportMailServer", SystemInfo.ErrorReportMailServer);
            SetValue(xmlDocument, "ErrorReportMailUserName", SystemInfo.ErrorReportMailUserName);
            SetValue(xmlDocument, "ErrorReportMailPassword", SecretHelper.smethod_0(SystemInfo.ErrorReportMailPassword));
            SetValue(xmlDocument, "RDIFrameworkBlog", SystemInfo.RDIFrameworkBlog);
            SetValue(xmlDocument, "RDIFrameworkWeibo", SystemInfo.RDIFrameworkWeibo);
            xmlDocument.Save(fileName);
        }

        public static bool SetValue(XmlDocument xmlDocument, string key, string keyValue)
        {
            return SetValue(xmlDocument, SelectPath, key, keyValue);
        }

        public static bool SetValue(XmlDocument xmlDocument, string selectPath, string key, string keyValue)
        {
            bool flag = false;
            using (IEnumerator enumerator = xmlDocument.SelectNodes(selectPath).GetEnumerator())
            {
                XmlNode current;
                while (enumerator.MoveNext())
                {
                    current = (XmlNode) enumerator.Current;
                    if (current.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                    {
                        goto Label_0054;
                    }
                }
                return flag;
            Label_0054:
                current.Attributes["value"].Value = keyValue;
                return true;
            }
        }

        public static string ConfigFielName
        {
            get
            {
                return (Application.StartupPath + @"\" + FileName);
            }
        }

        public static string FileName
        {
            get
            {
                return (LogOnTo + ".xml");
            }
        }
    }
}

