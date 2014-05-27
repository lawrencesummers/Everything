namespace LawrenceUtils
{
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlHelper
    {
        protected XmlDocument objXmlDoc = new XmlDocument();
        protected string strXmlFile;

        public XmlHelper(string XmlFile)
        {
            try
            {
                this.objXmlDoc.Load(XmlFile);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            this.strXmlFile = XmlFile;
        }

        public void Delete(string Node)
        {
            string xpath = Node.Substring(0, Node.LastIndexOf("/"));
            this.objXmlDoc.SelectSingleNode(xpath).RemoveChild(this.objXmlDoc.SelectSingleNode(Node));
        }

        public static object Deserialize(string path)
        {
            object obj3;
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj2 = formatter.Deserialize(stream);
                    stream.Close();
                    obj3 = obj2;
                }
            }
            catch
            {
                obj3 = null;
            }
            return obj3;
        }

        public DataSet GetData(string XmlPathNode)
        {
            DataSet set = new DataSet();
            StringReader reader = new StringReader(this.objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            set.ReadXml(reader);
            return set;
        }

        public XmlElement GetElement(string XmlPathNode, string elementName)
        {
            XmlElement element = null;
            IEnumerator enumerator = this.objXmlDoc.SelectSingleNode(XmlPathNode).GetEnumerator();
            {
                XmlElement element2;
                while (enumerator.MoveNext())
                {
                    XmlNode current = (XmlNode) enumerator.Current;
                    element2 = (XmlElement) current;
                    if (element2.Name == elementName)
                    {
                        element = element2;
                    }
                }
                return element;
            }
            return element;
        }

        public string GetElementData(string XmlPathNode, string elementName)
        {
            string str = null;
            IEnumerator enumerator = this.objXmlDoc.SelectSingleNode(XmlPathNode).GetEnumerator();
            {
                XmlElement element;
                while (enumerator.MoveNext())
                {
                    XmlNode current = (XmlNode) enumerator.Current;
                    element = (XmlElement) current;
                    if (element.Name == elementName)
                    {
                        str = element.InnerText;
                    }
                }
                return str;
                
            }
            return str;
        }

        public void InsertElement(string MainNode, string Element, string Content)
        {
            XmlNode node = this.objXmlDoc.SelectSingleNode(MainNode);
            XmlElement newChild = this.objXmlDoc.CreateElement(Element);
            newChild.InnerText = Content;
            node.AppendChild(newChild);
        }

        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode node = this.objXmlDoc.SelectSingleNode(MainNode);
            XmlElement newChild = this.objXmlDoc.CreateElement(Element);
            newChild.SetAttribute(Attrib, AttribContent);
            newChild.InnerText = Content;
            node.AppendChild(newChild);
        }

        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            XmlNode node = this.objXmlDoc.SelectSingleNode(MainNode);
            XmlElement newChild = this.objXmlDoc.CreateElement(ChildNode);
            node.AppendChild(newChild);
            XmlElement element2 = this.objXmlDoc.CreateElement(Element);
            element2.InnerText = Content;
            newChild.AppendChild(element2);
        }

        public XmlNodeList Read(string XmlPathNode)
        {
            XmlNodeList childNodes;
            try
            {
                childNodes = this.objXmlDoc.SelectSingleNode(XmlPathNode).ChildNodes;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return childNodes;
        }

        public string Read(string XmlPathNode, string Attrib)
        {
            string str = "";
            try
            {
                XmlNode node = this.objXmlDoc.SelectSingleNode(XmlPathNode);
                str = Attrib.Equals("") ? node.InnerText : node.Attributes[Attrib].Value;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public void Replace(string XmlPathNode, string Content)
        {
            this.objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        public void Save()
        {
            try
            {
                this.objXmlDoc.Save(this.strXmlFile);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            this.objXmlDoc = null;
        }

        public static bool Serialize(string path, object obj)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    stream.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object XmlDeserialize(string path, Type type)
        {
            object obj3;
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj2 = serializer.Deserialize(stream);
                    stream.Close();
                    obj3 = obj2;
                }
            }
            catch
            {
                obj3 = null;
            }
            return obj3;
        }

        //public object XmlDeserializeDecrypt(string path, Type type)
        //{
        //    object obj3;
        //    try
        //    {
        //        string s = EncodeHelper.DecryptString(File.ReadAllText(path, Encoding.UTF8), true);
        //        using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(s)))
        //        {
        //            XmlSerializer serializer = new XmlSerializer(type);
        //            stream.Seek(0, SeekOrigin.Begin);
        //            object obj2 = serializer.Deserialize(stream);
        //            stream.Close();
        //            obj3 = obj2;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //Console.WriteLine(exception.Message);
        //        obj3 = null;
        //    }
        //    return obj3;
        //}

        public static bool XmlSerialize(string path, object obj, Type type)
        {
            try
            {
                if (!File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    if (!info.Directory.Exists)
                    {
                        Directory.CreateDirectory(info.Directory.FullName);
                    }
                }
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    new XmlSerializer(type).Serialize(stream, obj, namespaces);
                    stream.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
               // Console.WriteLine(exception.Message);
                return false;
            }
        }

        public bool XmlSerializeEncrypt(string path, object obj, Type type)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            try
            {
                if (!File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    if (!info.Directory.Exists)
                    {
                        Directory.CreateDirectory(info.Directory.FullName);
                    }
                }
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    string input = "";
                    using (MemoryStream stream2 = new MemoryStream())
                    {
                        new XmlSerializer(type).Serialize(stream2, obj, namespaces);
                        stream2.Seek(0, SeekOrigin.Begin);
                        input = Encoding.ASCII.GetString(stream2.ToArray());
                    }
                    string s = EncodeHelper.EncryptString(input);
                    byte[] bytes = Encoding.Default.GetBytes(s);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}

