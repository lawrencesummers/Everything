using RDIFramework.Utilities;
using System;
using System.IO;
using System.Xml.Serialization;

internal class Class1
{
    public static Resources smethod_0(string string_0)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Resources));
        FileStream stream = new FileStream(string_0, FileMode.Open);
        Resources resources = serializer.Deserialize(stream) as Resources;
        stream.Close();
        return resources;
    }

    public static void smethod_1(string string_0, Resources resources_0)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Resources));
        FileStream stream = new FileStream(string_0, FileMode.Create);
        serializer.Serialize((Stream) stream, resources_0);
        stream.Close();
    }
}

