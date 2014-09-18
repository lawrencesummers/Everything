namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Xml;

    public class XMLHelper
    {
        public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName, string value)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node == null)
                {
                    goto Label_00AD;
                }
                using (IEnumerator enumerator = node.Attributes.GetEnumerator())
                {
                    XmlAttribute current;
                    while (enumerator.MoveNext())
                    {
                        current = (XmlAttribute) enumerator.Current;
                        if (current.Name.ToLower() == xmlAttributeName.ToLower())
                        {
                            goto Label_0067;
                        }
                    }
                    goto Label_008B;
                Label_0067:
                    current.Value = value;
                    flag2 = true;
                }
            Label_008B:
                if (!flag2)
                {
                    XmlAttribute attribute2 = document.CreateAttribute(xmlAttributeName);
                    attribute2.Value = value;
                    node.Attributes.Append(attribute2);
                }
            Label_00AD:
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node == null)
                {
                    goto Label_00A8;
                }
                using (IEnumerator enumerator = node.ChildNodes.GetEnumerator())
                {
                    XmlNode current;
                    while (enumerator.MoveNext())
                    {
                        current = (XmlNode) enumerator.Current;
                        if (current.Name.ToLower() == xmlNodeName.ToLower())
                        {
                            goto Label_0067;
                        }
                    }
                    goto Label_008B;
                Label_0067:
                    current.InnerXml = innerText;
                    flag2 = true;
                }
            Label_008B:
                if (!flag2)
                {
                    XmlElement newChild = document.CreateElement(xmlNodeName);
                    newChild.InnerXml = innerText;
                    node.AppendChild(newChild);
                }
            Label_00A8:
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding, string standalone)
        {
            bool flag = false;
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration(version, encoding, standalone);
                XmlNode node = document.CreateElement(rootNodeName);
                document.AppendChild(newChild);
                document.AppendChild(node);
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText, string xmlAttributeName, string value)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    XmlElement newChild = document.CreateElement(xmlNodeName);
                    newChild.InnerXml = innerText;
                    if (!(string.IsNullOrEmpty(xmlAttributeName) || string.IsNullOrEmpty(value)))
                    {
                        XmlAttribute attribute = document.CreateAttribute(xmlAttributeName);
                        attribute.Value = value;
                        newChild.Attributes.Append(attribute);
                    }
                    node.AppendChild(newChild);
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode oldChild = document.SelectSingleNode(node);
                XmlElement element = (XmlElement) oldChild;
                if (attribute.Equals(""))
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                else
                {
                    element.RemoveAttribute(attribute);
                }
                document.Save(path);
            }
            catch
            {
            }
        }

        public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    node.Attributes.RemoveAll();
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                XmlAttribute attribute = null;
                if (node == null)
                {
                    goto Label_009B;
                }
                using (IEnumerator enumerator = node.Attributes.GetEnumerator())
                {
                    XmlAttribute current;
                    while (enumerator.MoveNext())
                    {
                        current = (XmlAttribute) enumerator.Current;
                        if (current.Name.ToLower() == xmlAttributeName.ToLower())
                        {
                            goto Label_0067;
                        }
                    }
                    goto Label_0087;
                Label_0067:
                    attribute = current;
                    flag2 = true;
                }
            Label_0087:
                if (flag2)
                {
                    node.Attributes.Remove(attribute);
                }
            Label_009B:
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode oldChild = document.SelectSingleNode(xpath);
                if (oldChild != null)
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
        {
            XmlDocument document = new XmlDocument();
            XmlAttribute attribute = null;
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if ((node != null) && (node.Attributes.Count > 0))
                {
                    attribute = node.Attributes[xmlAttributeName];
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return attribute;
        }

        public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                return document.SelectSingleNode(xpath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                return document.SelectNodes(xpath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlElement element2;
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node2 = document.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        element2 = (XmlElement) node2;
                        element2.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    element2 = document.CreateElement(element);
                    if (attribute.Equals(""))
                    {
                        element2.InnerText = value;
                    }
                    else
                    {
                        element2.SetAttribute(attribute, value);
                    }
                    node2.AppendChild(element2);
                }
                document.Save(path);
            }
            catch
            {
            }
        }

        public static string Read(string path, string node, string attribute)
        {
            string str = "";
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node2 = document.SelectSingleNode(node);
                str = attribute.Equals("") ? node2.InnerText : node2.Attributes[attribute].Value;
            }
            catch
            {
            }
            return str;
        }

        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlElement element = (XmlElement) document.SelectSingleNode(node);
                if (attribute.Equals(""))
                {
                    element.InnerText = value;
                }
                else
                {
                    element.SetAttribute(attribute, value);
                }
                document.Save(path);
            }
            catch
            {
            }
        }
    }
}

