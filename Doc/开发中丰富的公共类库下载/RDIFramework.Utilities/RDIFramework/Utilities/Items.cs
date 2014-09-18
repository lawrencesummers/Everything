namespace RDIFramework.Utilities
{
    using System;
    using System.Xml.Serialization;

    public class Items
    {
        [XmlElement("item", typeof(Item))]
        public Item[] items;
    }
}

