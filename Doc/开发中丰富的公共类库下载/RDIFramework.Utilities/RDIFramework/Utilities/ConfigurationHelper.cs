namespace RDIFramework.Utilities
{
    using System;
    using System.Configuration;

    public class ConfigurationHelper
    {
        public static void GetConfig()
        {
            bool flag;
            SystemInfo.CustomerCompanyName = ConfigurationManager.AppSettings["CustomerCompanyName"];
            SystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(ConfigurationManager.AppSettings["ConfigurationFrom"]);
            SystemInfo.SoftName = ConfigurationManager.AppSettings["SoftName"];
            SystemInfo.SoftFullName = ConfigurationManager.AppSettings["SoftFullName"];
            SystemInfo.RootMenuCode = ConfigurationManager.AppSettings["RootMenuCode"];
            SystemInfo.ServiceUserName = ConfigurationManager.AppSettings["ServiceUserName"];
            SystemInfo.ServicePassword = ConfigurationManager.AppSettings["ServicePassword"];
            if (ConfigurationManager.AppSettings["EnableEncryptServerPassword"] != null)
            {
                flag = true;
                SystemInfo.EnableEncryptServerPassword = ConfigurationManager.AppSettings["EnableEncryptServerPassword"].ToUpper().Equals(flag.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["EncryptClientPassword"] != null)
            {
                flag = true;
                SystemInfo.EncryptClientPassword = ConfigurationManager.AppSettings["EncryptClientPassword"].ToUpper().Equals(flag.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["EnableCheckIPAddress"] != null)
            {
                flag = true;
                SystemInfo.EnableCheckIPAddress = ConfigurationManager.AppSettings["EnableCheckIPAddress"].ToUpper().Equals(flag.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["CheckOnLine"] != null)
            {
                flag = true;
                SystemInfo.CheckOnLine = ConfigurationManager.AppSettings["CheckOnLine"].ToUpper().Equals(flag.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["RDIFrameworkDbType"] != null)
            {
                SystemInfo.RDIFrameworkDbType = BaseConfiguration.GetDbType(ConfigurationManager.AppSettings["RDIFrameworkDbType"]);
            }
            if (ConfigurationManager.AppSettings["BusinessDbType"] != null)
            {
                SystemInfo.BusinessDbType = BaseConfiguration.GetDbType(ConfigurationManager.AppSettings["BusinessDbType"]);
            }
            if (ConfigurationManager.AppSettings["WorkFlowDbType"] != null)
            {
                SystemInfo.WorkFlowDbType = BaseConfiguration.GetDbType(ConfigurationManager.AppSettings["WorkFlowDbType"]);
            }
            if (ConfigurationManager.AppSettings["EncryptDbConnection"] != null)
            {
                flag = true;
                SystemInfo.EncryptDbConnection = ConfigurationManager.AppSettings["EncryptDbConnection"].ToUpper().Equals(flag.ToString().ToUpper());
            }
            SystemInfo.BusinessDbConnectionString = ConfigurationManager.AppSettings["BusinessDbConnection"];
            SystemInfo.RDIFrameworkDbConectionString = ConfigurationManager.AppSettings["RDIFrameworkDbConection"];
            SystemInfo.WorkFlowDbConnectionString = ConfigurationManager.AppSettings["WorkFlowDbConnection"];
            if (SystemInfo.EncryptDbConnection)
            {
                SystemInfo.BusinessDbConnection = SecretHelper.smethod_1(SystemInfo.BusinessDbConnectionString);
                SystemInfo.RDIFrameworkDbConection = SecretHelper.smethod_1(SystemInfo.RDIFrameworkDbConectionString);
                SystemInfo.WorkFlowDbConnection = SecretHelper.smethod_1(SystemInfo.WorkFlowDbConnectionString);
            }
            else
            {
                SystemInfo.BusinessDbConnection = ConfigurationManager.AppSettings["BusinessDbConnection"];
                SystemInfo.RDIFrameworkDbConection = ConfigurationManager.AppSettings["RDIFrameworkDbConection"];
                SystemInfo.WorkFlowDbConnection = ConfigurationManager.AppSettings["WorkFlowDbConnection"];
            }
            SystemInfo.RegisterKey = ConfigurationManager.AppSettings["RegisterKey"];
        }
    }
}

