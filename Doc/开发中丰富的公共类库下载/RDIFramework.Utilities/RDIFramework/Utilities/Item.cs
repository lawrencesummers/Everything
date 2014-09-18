namespace RDIFramework.Utilities
{
    using System;
    using System.Xml.Serialization;

    public class Item
    {
        [XmlAttribute("key")]
        public string key = string.Empty;
        [XmlText]
        public string value = string.Empty;
    }
}

