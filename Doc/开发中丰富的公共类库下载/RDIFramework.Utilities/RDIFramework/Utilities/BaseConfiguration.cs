namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;

    public class BaseConfiguration
    {
        public BaseConfiguration()
        {
        }

        public BaseConfiguration(string softName)
        {
            SystemInfo.SoftName = softName;
        }

        public static ConfigurationCategory GetConfiguration(string configuration)
        {
            ConfigurationCategory category = ConfigurationCategory.Configuration;
            using (IEnumerator enumerator = Enum.GetValues(typeof(ConfigurationCategory)).GetEnumerator())
            {
                ConfigurationCategory current;
                while (enumerator.MoveNext())
                {
                    current = (ConfigurationCategory) enumerator.Current;
                    if (current.ToString().Equals(configuration))
                    {
                        goto Label_0046;
                    }
                }
                return category;
            Label_0046:
                category = current;
            }
            return category;
        }

        public static CurrentDbType GetDbType(string dbType)
        {
            CurrentDbType sqlServer = CurrentDbType.SqlServer;
            using (IEnumerator enumerator = Enum.GetValues(typeof(CurrentDbType)).GetEnumerator())
            {
                CurrentDbType current;
                while (enumerator.MoveNext())
                {
                    current = (CurrentDbType) enumerator.Current;
                    if (current.ToString().Equals(dbType))
                    {
                        goto Label_0046;
                    }
                }
                return sqlServer;
            Label_0046:
                sqlServer = current;
            }
            return sqlServer;
        }

        public static void GetSetting()
        {
            if (SystemInfo.ConfigurationFrom == ConfigurationCategory.RegistryKey)
            {
                RegistryHelper.GetConfig();
            }
            if (SystemInfo.ConfigurationFrom == ConfigurationCategory.Configuration)
            {
                ConfigurationHelper.GetConfig();
            }
            if (SystemInfo.ConfigurationFrom == ConfigurationCategory.UserConfig)
            {
                UserConfigHelper.GetConfig();
            }
        }
    }
}

