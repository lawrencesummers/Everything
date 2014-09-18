namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("resources")]
    public class Resources
    {
        [XmlElement("author")]
        public string author = string.Empty;
        [XmlElement("description")]
        public string description = string.Empty;
        [XmlElement("displayName")]
        public string displayName = string.Empty;
        [XmlElement("items", typeof(Items))]
        public Items items;
        [XmlElement("language")]
        public string language = string.Empty;
        private SortedList<string, string> sortedList_0 = new SortedList<string, string>();
        [XmlElement("version")]
        public string version = string.Empty;

        public void createIndex()
        {
            this.sortedList_0.Clear();
            if (this.items != null)
            {
                this.sortedList_0 = new SortedList<string, string>(this.items.items.Length);
                for (int i = 0; i < this.items.items.Length; i++)
                {
                    try
                    {
                        this.sortedList_0.Add(this.items.items[i].key, this.items.items[i].value);
                    }
                    catch
                    {
                        throw new Exception(this.items.items[i].key + this.items.items[i].value);
                    }
                }
            }
        }

        public string Get(string key)
        {
            if (!this.sortedList_0.ContainsKey(key))
            {
                return string.Empty;
            }
            return this.sortedList_0[key];
        }

        public bool Set(string key, string value)
        {
            if (!this.sortedList_0.ContainsKey(key))
            {
                return false;
            }
            this.sortedList_0[key] = value;
            for (int i = 0; i < this.items.items.Length; i++)
            {
                if (this.items.items[i].key == key)
                {
                    this.items.items[i].value = value;
                    break;
                }
            }
            return true;
        }
    }
}

