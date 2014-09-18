namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        private Serializer()
        {
        }

        public static object Clone(object o)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            object obj2 = null;
            try
            {
                formatter.Serialize(serializationStream, o);
                serializationStream.Seek(0, SeekOrigin.Begin);
                obj2 = formatter.Deserialize(serializationStream);
            }
            catch
            {
            }
            finally
            {
                serializationStream.Close();
            }
            return obj2;
        }

        public static object DeserializeFromBinary(Type type, string path)
        {
            object obj2 = new object();
            FileStream serializationStream = new FileStream(path, FileMode.Open);
            try
            {
                obj2 = new BinaryFormatter().Deserialize(serializationStream);
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                serializationStream.Close();
            }
            return obj2;
        }

        public static object DeserializeFromBinary(Type type, byte[] bytes)
        {
            object obj2 = new object();
            MemoryStream serializationStream = new MemoryStream(bytes);
            try
            {
                try
                {
                    obj2 = new BinaryFormatter().Deserialize(serializationStream);
                }
                catch (SerializationException exception)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + exception.Message);
                    throw;
                }
            }
            finally
            {
            }
            return obj2;
        }

        public static object DeserializeFromSoap(Type type, string s)
        {
            object obj2 = new object();
            MemoryStream serializationStream = new MemoryStream(new UTF8Encoding().GetBytes(s));
            try
            {
                try
                {
                    obj2 = new SoapFormatter().Deserialize(serializationStream);
                }
                catch (SerializationException exception)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + exception.Message);
                    throw;
                }
            }
            finally
            {
            }
            return obj2;
        }

        public static object DeserializeFromXml(Type type, string s)
        {
            object obj2 = new object();
            try
            {
                try
                {
                    obj2 = new XmlSerializer(type).Deserialize(new StringReader(s));
                }
                catch (SerializationException exception)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + exception.Message);
                    throw;
                }
            }
            finally
            {
            }
            return obj2;
        }

        public static object DeserializeFromXmlFile(Type type, string path)
        {
            object obj2 = new object();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                obj2 = new XmlSerializer(type).Deserialize(stream);
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }
            return obj2;
        }

        public static long GetByteSize(object o)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, o);
            return serializationStream.Length;
        }

        public static byte[] SerializeToBinary(object obj)
        {
            byte[] buffer = new byte[0x9c4];
            MemoryStream serializationStream = new MemoryStream();
            try
            {
                new BinaryFormatter().Serialize(serializationStream, obj);
                serializationStream.Seek(0, SeekOrigin.Begin);
                if (serializationStream.Length > buffer.Length)
                {
                    buffer = new byte[serializationStream.Length];
                }
                buffer = serializationStream.ToArray();
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to serialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                serializationStream.Close();
            }
            return buffer;
        }

        public static void SerializeToBinary(object obj, string path)
        {
            SerializeToBinary(obj, path, FileMode.Create);
        }

        public static void SerializeToBinary(object obj, string path, FileMode mode)
        {
            FileStream serializationStream = new FileStream(path, mode);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(serializationStream, obj);
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("序列化对象失败: " + exception.Message);
                throw;
            }
            finally
            {
                serializationStream.Close();
            }
        }

        public static string SerializeToSoap(object obj)
        {
            string str = "";
            MemoryStream serializationStream = new MemoryStream();
            try
            {
                new SoapFormatter().Serialize(serializationStream, obj);
                serializationStream.Seek(0, SeekOrigin.Begin);
                str = Encoding.ASCII.GetString(serializationStream.ToArray());
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to serialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                serializationStream.Close();
            }
            return str;
        }

        public static void SerializeToSoap(object obj, string path)
        {
            SerializeToSoap(obj, path, FileMode.Create);
        }

        public static void SerializeToSoap(object obj, string path, FileMode mode)
        {
            FileStream serializationStream = new FileStream(path, mode);
            SoapFormatter formatter = new SoapFormatter();
            try
            {
                formatter.Serialize(serializationStream, obj);
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to serialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                serializationStream.Close();
            }
        }

        public static string SerializeToXml(object obj)
        {
            string str = "";
            MemoryStream stream = new MemoryStream();
            try
            {
                new XmlSerializer(obj.GetType()).Serialize((Stream) stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                str = Encoding.ASCII.GetString(stream.ToArray());
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to serialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }
            return str;
        }

        public static void SerializeToXmlFile(object obj, string path)
        {
            SerializeToXmlFile(obj, path, FileMode.Create);
        }

        public static void SerializeToXmlFile(object obj, string path, FileMode mode)
        {
            FileStream stream = new FileStream(path, mode);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            try
            {
                serializer.Serialize((Stream) stream, obj);
            }
            catch (SerializationException exception)
            {
                Console.WriteLine("Failed to serialize. Reason: " + exception.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }
        }
    }
}

