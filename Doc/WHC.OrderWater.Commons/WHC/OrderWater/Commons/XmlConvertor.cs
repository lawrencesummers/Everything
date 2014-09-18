namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public sealed class XmlConvertor
    {
        private XmlConvertor()
        {
        }

        public static string ObjectToXml(object obj, bool toBeIndented)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            UTF8Encoding encoding = new UTF8Encoding(false);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream w = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(w, encoding) {
                Formatting = toBeIndented ? Formatting.Indented : Formatting.None
            };
            try
            {
                serializer.Serialize(xmlWriter, obj, namespaces);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                xmlWriter.Close();
            }
            return encoding.GetString(w.ToArray());
        }

        public static object XmlToObject(string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }
            object obj2 = null;
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader input = new StringReader(xml);
            XmlReader xmlReader = new XmlTextReader(input);
            try
            {
                obj2 = serializer.Deserialize(xmlReader);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException("Can not convert xml to object", exception);
            }
            finally
            {
                xmlReader.Close();
            }
            return obj2;
        }
    }
}

