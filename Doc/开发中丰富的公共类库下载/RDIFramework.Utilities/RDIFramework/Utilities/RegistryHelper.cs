namespace RDIFramework.Utilities
{
    using Microsoft.Win32;
    using System;

    public class RegistryHelper
    {
        public static string SubKey = @"Software\RDIFramework.NET";

        public static bool Exists(string key)
        {
            return Exists(SubKey, key);
        }

        public static bool Exists(string subKey, string key)
        {
            string[] subKeyNames = Registry.LocalMachine.OpenSubKey(subKey, false).GetSubKeyNames();
            for (int i = 0; i < subKeyNames.Length; i++)
            {
                if (key.Equals(subKeyNames[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static void GetConfig()
        {
            if (!Exists("Software", "RDIFramework.NET"))
            {
                Registry.LocalMachine.OpenSubKey("SOFTWARE", true).CreateSubKey("RDIFramework.NET");
                smethod_1();
            }
            else if (!Exists(SystemInfo.SoftName))
            {
                smethod_1();
            }
            if (SystemInfo.SoftName.Length == 0)
            {
                smethod_0();
            }
        }

        public static string GetValue(string key)
        {
            return (string) Registry.LocalMachine.OpenSubKey(SubKey, false).GetValue(key);
        }

        public static void SetValue(string key, string keyValue)
        {
            Registry.LocalMachine.OpenSubKey(SubKey, true).SetValue(key, keyValue);
        }

        private static void smethod_0()
        {
            SubKey = @"Software\RDIFramework.NET\" + SystemInfo.SoftName;
            SystemInfo.CustomerCompanyName = GetValue("CustomerCompanyName");
            SystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(GetValue("ConfigurationFrom"));
            SystemInfo.SoftName = GetValue("SoftName");
            SystemInfo.SoftFullName = GetValue("SoftFullName");
            SystemInfo.RootMenuCode = GetValue("RootMenuCode");
            SystemInfo.CurrentLanguage = GetValue("CurrentLanguage");
            SystemInfo.Version = GetValue("Version");
            SystemInfo.BusinessDbConnection = GetValue("BusinessDbConnection");
            SystemInfo.RDIFrameworkDbConection = GetValue("RDIFrameworkDbConection");
            SystemInfo.BusinessDbType = BaseConfiguration.GetDbType(GetValue("DbType"));
            SystemInfo.RegisterKey = GetValue("RegisterKey");
        }

        private static void smethod_1()
        {
            SubKey = @"Software\RDIFramework.NET";
            SetValue("CustomerCompanyName", SystemInfo.CustomerCompanyName);
            SetValue("ConfigurationFrom", "RDIFramework.NET");
            SetValue("SoftName", SystemInfo.SoftName);
            SetValue("SoftFullName", SystemInfo.SoftFullName);
            SetValue("RootMenuCode", SystemInfo.RootMenuCode);
            SetValue("CurrentLanguage", SystemInfo.CurrentLanguage);
            SetValue("DbType", CurrentDbType.SqlServer.ToString());
            SetValue("RegisterKey", SystemInfo.RegisterKey.ToString());
        }
    }
}

