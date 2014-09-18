namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("dictionary")]
    public class CDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ICloneable<CDictionary<TKey, TValue>>, ICloneable, IXmlSerializable
    {
        public CDictionary()
        {
        }

        public CDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public CDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        public CDictionary(int capacity) : base(capacity)
        {
        }

        public CDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
        {
        }

        public CDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        {
        }

        protected CDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CDictionary<TKey, TValue> Clone()
        {
            return new CDictionary<TKey, TValue>(this);
        }

        public bool Contains(TKey key)
        {
            return base.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            this.CopyTo(array, index);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TKey));
            XmlSerializer serializer2 = new XmlSerializer(typeof(TValue));
            bool isEmptyElement = reader.IsEmptyElement;
            reader.Read();
            if (!isEmptyElement)
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("item");
                    reader.ReadStartElement("key");
                    TKey key = (TKey) serializer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("value");
                    TValue local2 = (TValue) serializer2.Deserialize(reader);
                    reader.ReadEndElement();
                    base.Add(key, local2);
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
            }
        }

        [HostProtection(SecurityAction.LinkDemand, Synchronization=true)]
        public static CDictionary<TKey, TValue> Synchronized(CDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return new SyncDictionary<TKey, TValue>(dictionary);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TKey));
            XmlSerializer serializer2 = new XmlSerializer(typeof(TValue));
            foreach (TKey local in base.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                serializer.Serialize(writer, local);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue o = base[local];
                serializer2.Serialize(writer, o);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }
    }
}

