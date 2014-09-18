namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Xml;

    public sealed class AppConfig
    {
        private string string_0;

        public AppConfig()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.Config");
            string str2 = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", "");
            if (File.Exists(path))
            {
                this.string_0 = path;
            }
            else
            {
                if (!File.Exists(str2))
                {
                    throw new ArgumentNullException("没有找到Web.Config文件或者应用程序配置文件, 请指定配置文件");
                }
                this.string_0 = str2;
            }
        }

        public AppConfig(string configFilePath)
        {
            this.string_0 = configFilePath;
        }

        public string AppConfigGet(string keyName)
        {
            string str = string.Empty;
            try
            {
                XmlAttribute attribute;
                XmlDocument document = new XmlDocument();
                document.Load(this.string_0);
                XmlNodeList elementsByTagName = document.GetElementsByTagName("add");
                for (int i = 0; i < elementsByTagName.Count; i++)
                {
                    attribute = elementsByTagName[i].Attributes["key"];
                    if ((attribute != null) && (attribute.Value == keyName))
                    {
                        attribute = elementsByTagName[i].Attributes["value"];
                        if (attribute != null)
                        {
                            goto Label_0089;
                        }
                    }
                }
                return str;
            Label_0089:
                str = attribute.Value;
            }
            catch
            {
            }
            return str;
        }

        public void AppConfigSet(string keyName, string keyValue)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.string_0);
            XmlNodeList elementsByTagName = document.GetElementsByTagName("add");
            for (int i = 0; i < elementsByTagName.Count; i++)
            {
                XmlAttribute attribute = elementsByTagName[i].Attributes["key"];
                if ((attribute != null) && (attribute.Value == keyName))
                {
                    attribute = elementsByTagName[i].Attributes["value"];
                    if (attribute != null)
                    {
                        attribute.Value = keyValue;
                        break;
                    }
                }
            }
            document.Save(this.string_0);
        }

        public string GetConnectionString(string keyName)
        {
            string str = string.Empty;
            try
            {
                XmlAttribute attribute;
                XmlDocument document = new XmlDocument();
                document.Load(this.string_0);
                XmlNodeList elementsByTagName = document.GetElementsByTagName("add");
                for (int i = 0; i < elementsByTagName.Count; i++)
                {
                    attribute = elementsByTagName[i].Attributes["name"];
                    if ((attribute != null) && (attribute.Value == keyName))
                    {
                        attribute = elementsByTagName[i].Attributes["connectionString"];
                        if (attribute != null)
                        {
                            goto Label_0089;
                        }
                    }
                }
                return str;
            Label_0089:
                str = attribute.Value;
            }
            catch
            {
            }
            return str;
        }

        public DatabaseInfo GetDatabaseInfo(string keyName)
        {
            return new DatabaseInfo(this.GetConnectionString(keyName));
        }

        public string GetSubValue(string keyName, string subKeyName)
        {
            string[] strArray = this.AppConfigGet(keyName).ToLower().Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToLower().IndexOf(subKeyName.ToLower()) >= 0)
                {
                    int index = strArray[i].IndexOf("=");
                    return strArray[i].Substring(index + 1);
                }
            }
            return string.Empty;
        }

        public void SetConnectionString(string keyName, string keyValue)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.string_0);
            XmlNodeList elementsByTagName = document.GetElementsByTagName("add");
            for (int i = 0; i < elementsByTagName.Count; i++)
            {
                XmlAttribute attribute = elementsByTagName[i].Attributes["name"];
                if ((attribute != null) && (attribute.Value == keyName))
                {
                    attribute = elementsByTagName[i].Attributes["connectionString"];
                    if (attribute != null)
                    {
                        attribute.Value = keyValue;
                        break;
                    }
                }
            }
            document.Save(this.string_0);
        }

        public void SetDatabaseInfo(string keyName, DatabaseInfo databaseInfo)
        {
            this.SetConnectionString(keyName, databaseInfo.ConnectionString);
        }

        public string AppName
        {
            get
            {
                return this.AppConfigGet("ApplicationName");
            }
        }

        public string Manufacturer
        {
            get
            {
                return this.AppConfigGet("Manufacturer");
            }
        }

        public string String_0
        {
            get
            {
                return this.AppConfigGet("HWSecurity");
            }
        }

        public string System_ID
        {
            get
            {
                return this.AppConfigGet("System_ID");
            }
        }
    }
}

