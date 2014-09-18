namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Xml;

    public class TableConfigHelper
    {
        public string SelectPath = "//resultMaps/resultMap/result";
        private string string_0 = string.Empty;

        public void GetConfig(string fileName, object table)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            Type type = table.GetType();
            string key = string.Empty;
            string str2 = string.Empty;
            foreach (FieldInfo info in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (info.Name.StartsWith("Field"))
                {
                    key = info.Name.Substring("Field".Length);
                    str2 = this.GetValue(xmlDocument, key);
                    info.SetValue(table, str2);
                }
            }
        }

        public string GetValue(string key)
        {
            return this.GetValue(this.FielName, this.SelectPath, key);
        }

        public string GetValue(string fileName, string key)
        {
            return this.GetValue(fileName, this.SelectPath, key);
        }

        public string GetValue(XmlDocument xmlDocument, string key)
        {
            return this.GetValue(xmlDocument, this.SelectPath, key);
        }

        public string GetValue(string fileName, string selectPath, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return this.GetValue(xmlDocument, selectPath, key);
        }

        public string GetValue(XmlDocument xmlDocument, string selectPath, string key)
        {
            string str = string.Empty;
            using (IEnumerator enumerator = xmlDocument.SelectNodes(selectPath).GetEnumerator())
            {
                XmlNode current;
                while (enumerator.MoveNext())
                {
                    current = (XmlNode) enumerator.Current;
                    if (current.Attributes["property"].Value.ToUpper().Equals(key.ToUpper()))
                    {
                        goto Label_0058;
                    }
                }
                return str;
            Label_0058:
                str = current.Attributes["column"].Value;
            }
            return str;
        }

        public string FielName
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

