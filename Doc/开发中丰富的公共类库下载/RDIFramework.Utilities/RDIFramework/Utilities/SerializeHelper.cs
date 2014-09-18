namespace RDIFramework.Utilities
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class SerializeHelper
    {
        public static object Clone(object obj)
        {
            object obj2 = null;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            try
            {
                formatter.Serialize(serializationStream, obj);
                serializationStream.Seek(0L, SeekOrigin.Begin);
                obj2 = formatter.Deserialize(serializationStream);
            }
            catch
            {
            }
            finally
            {
                if (serializationStream != null)
                {
                    serializationStream.Dispose();
                }
            }
            return obj2;
        }

        public static object DeSerialize(byte[] bytes, SerializedType type)
        {
            switch (type)
            {
                case SerializedType.ByteArray:
                case (SerializedType.Object | SerializedType.Bool):
                    return bytes;

                case SerializedType.Object:
                {
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        return new BinaryFormatter().Deserialize(stream);
                    }
                }
                case SerializedType.String:
                    return Encoding.UTF8.GetString(bytes);

                case SerializedType.Datetime:
                    return new DateTime(BitConverter.ToInt64(bytes, 0));

                case SerializedType.Bool:
                    return (bytes[0] == 1);

                case SerializedType.Byte:
                    return bytes[0];

                case SerializedType.Short:
                    return BitConverter.ToInt16(bytes, 0);

                case SerializedType.UShort:
                    return BitConverter.ToUInt16(bytes, 0);

                case SerializedType.Int:
                    return BitConverter.ToInt32(bytes, 0);

                case SerializedType.UInt:
                    return BitConverter.ToUInt32(bytes, 0);

                case SerializedType.Long:
                    return BitConverter.ToInt64(bytes, 0);

                case SerializedType.ULong:
                    return BitConverter.ToUInt64(bytes, 0);

                case SerializedType.Float:
                    return BitConverter.ToSingle(bytes, 0);

                case SerializedType.Double:
                    return BitConverter.ToDouble(bytes, 0);

                case SerializedType.CompressedByteArray:
                    return DeSerialize(smethod_1(bytes), SerializedType.ByteArray);

                case SerializedType.CompressedObject:
                    return DeSerialize(smethod_1(bytes), SerializedType.Object);

                case SerializedType.CompressedString:
                    return DeSerialize(smethod_1(bytes), SerializedType.String);
            }
            return bytes;
        }

        public static object DeserializeFromBinary(byte[] bytes)
        {
            object obj2 = new object();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }

        public static object DeserializeFromBinary(string path)
        {
            object obj2 = new object();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }

        public static T FromBinary<T>(string str)
        {
            int num = str.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                int num3 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num3;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                return (T) formatter.Deserialize(stream);
            }
        }

        public static T FromSoap<T>(string str)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(str);
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                stream.Position = 0L;
                return (T) formatter.Deserialize(stream);
            }
        }

        public T FromXml<T>(string str)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static long GetByteSize(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.Length;
            }
        }

        public static object Load(Type type, string filename)
        {
            FileStream stream = null;
            object obj2;
            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                obj2 = new XmlSerializer(type).Deserialize(stream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return obj2;
        }

        public static string ReadFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        public static void Save(object obj, string filename)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                new XmlSerializer(obj.GetType()).Serialize((Stream) stream, obj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static byte[] Serialize(object value, out SerializedType type, uint compressionThreshold)
        {
            byte[] bytes;
            if (value is byte[])
            {
                bytes = (byte[]) value;
                type = SerializedType.ByteArray;
                if (bytes.Length > compressionThreshold)
                {
                    bytes = smethod_0(bytes);
                    type = SerializedType.CompressedByteArray;
                }
                return bytes;
            }
            if (value is string)
            {
                bytes = Encoding.UTF8.GetBytes((string) value);
                type = SerializedType.String;
                if (bytes.Length > compressionThreshold)
                {
                    bytes = smethod_0(bytes);
                    type = SerializedType.CompressedString;
                }
                return bytes;
            }
            if (value is DateTime)
            {
                DateTime time = (DateTime) value;
                bytes = BitConverter.GetBytes(time.Ticks);
                type = SerializedType.Datetime;
                return bytes;
            }
            if (value is bool)
            {
                bytes = new byte[] { ((bool) value) ? ((byte) 1) : ((byte) 0) };
                type = SerializedType.Bool;
                return bytes;
            }
            if (value is byte)
            {
                bytes = new byte[] { (byte) value };
                type = SerializedType.Byte;
                return bytes;
            }
            if (value is short)
            {
                bytes = BitConverter.GetBytes((short) value);
                type = SerializedType.Short;
                return bytes;
            }
            if (value is ushort)
            {
                bytes = BitConverter.GetBytes((ushort) value);
                type = SerializedType.UShort;
                return bytes;
            }
            if (value is int)
            {
                bytes = BitConverter.GetBytes((int) value);
                type = SerializedType.Int;
                return bytes;
            }
            if (value is uint)
            {
                bytes = BitConverter.GetBytes((uint) value);
                type = SerializedType.UInt;
                return bytes;
            }
            if (value is long)
            {
                bytes = BitConverter.GetBytes((long) value);
                type = SerializedType.Long;
                return bytes;
            }
            if (value is ulong)
            {
                bytes = BitConverter.GetBytes((ulong) value);
                type = SerializedType.ULong;
                return bytes;
            }
            if (value is float)
            {
                bytes = BitConverter.GetBytes((float) value);
                type = SerializedType.Float;
                return bytes;
            }
            if (value is double)
            {
                bytes = BitConverter.GetBytes((double) value);
                type = SerializedType.Double;
                return bytes;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, value);
                bytes = stream.GetBuffer();
                type = SerializedType.Object;
                if (bytes.Length > compressionThreshold)
                {
                    bytes = smethod_0(bytes);
                    type = SerializedType.CompressedObject;
                }
            }
            return bytes;
        }

        public static byte[] SerializeToBinary(object obj)
        {
            byte[] buffer = new byte[0x9c4];
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                stream.Seek(0L, SeekOrigin.Begin);
                if (stream.Length > buffer.Length)
                {
                    buffer = new byte[stream.Length];
                }
                return stream.ToArray();
            }
        }

        public static void SerializeToBinary(object obj, string path)
        {
            SerializeToBinary(obj, path, FileMode.Create);
        }

        public static void SerializeToBinary(object obj, string path, FileMode fileMode)
        {
            using (FileStream stream = new FileStream(path, fileMode))
            {
                new BinaryFormatter().Serialize(stream, obj);
            }
        }

        private static byte[] smethod_0(byte[] byte_0)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Compress, false))
                {
                    stream2.Write(byte_0, 0, byte_0.Length);
                }
                stream.Close();
                return stream.GetBuffer();
            }
        }

        private static byte[] smethod_1(byte[] byte_0)
        {
            byte[] buffer2;
            using (MemoryStream stream = new MemoryStream(byte_0, false))
            {
                using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress, false))
                {
                    using (MemoryStream stream3 = new MemoryStream())
                    {
                        int num;
                        byte[] buffer = new byte[byte_0.Length];
                        while ((num = stream2.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            stream3.Write(buffer, 0, num);
                        }
                        stream3.Close();
                        buffer2 = stream3.GetBuffer();
                    }
                }
            }
            return buffer2;
        }

        public static string ToBinary<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, item);
                stream.Position = 0L;
                byte[] buffer = stream.ToArray();
                StringBuilder builder = new StringBuilder();
                foreach (byte num2 in buffer)
                {
                    builder.Append(string.Format("{0:X2}", num2));
                }
                return builder.ToString();
            }
        }

        public static string ToSoap<T>(T item)
        {
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, item);
                stream.Position = 0L;
                XmlDocument document = new XmlDocument();
                document.Load(stream);
                return document.InnerXml;
            }
        }

        public string ToXml<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder output = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                serializer.Serialize(writer, item);
                return output.ToString();
            }
        }
    }
}

