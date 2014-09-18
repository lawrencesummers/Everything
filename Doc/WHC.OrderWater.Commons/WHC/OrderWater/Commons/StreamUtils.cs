namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class StreamUtils
    {
        private const int MAXBUFFER = 0x1000;

        private StreamUtils()
        {
        }

        public static void Copy(string input, Stream output)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(input);
            StreamWriter outputWriter = new StreamWriter(output, Encoding.Default);
            Copy(bytes, outputWriter);
            outputWriter.Flush();
        }

        public static void Copy(byte[] input, Stream output)
        {
            if (input.Length != 0)
            {
                output.Write(input, 0, input.Length);
            }
        }

        public static void Copy(byte[] input, StreamWriter outputWriter)
        {
            MemoryStream inputStream = new MemoryStream(input);
            Copy(inputStream, outputWriter);
        }

        public static int Copy(Stream input, Stream output)
        {
            return Copy(input, output, false);
        }

        public static int Copy(Stream inputStream, StreamWriter outputStreamWriter)
        {
            StreamReader inputStreamReader = new StreamReader(inputStream, Encoding.Default);
            return Copy(inputStreamReader, outputStreamWriter);
        }

        public static void Copy(StreamReader inputReader, Stream output)
        {
            StreamWriter outputStreamWriter = new StreamWriter(output, Encoding.Default);
            Copy(inputReader, outputStreamWriter);
            outputStreamWriter.Flush();
        }

        public static int Copy(StreamReader inputStreamReader, StreamWriter outputStreamWriter)
        {
            char[] buffer = new char[MAXBUFFER];
            int num = 0;
            int count = MAXBUFFER;
            while (count > 0)
            {
                count = inputStreamReader.Read(buffer, 0, MAXBUFFER);
                outputStreamWriter.Write(buffer, 0, count);
                num += count;
            }
            return num;
        }

        public static void Copy(string input, StreamWriter output)
        {
            output.Write(input);
        }

        public static int Copy(Stream inputStream, StreamWriter outputWriter, string encoding)
        {
            Encoding encoding2 = Encoding.Default;
            try
            {
                encoding2 = Encoding.GetEncoding(encoding);
            }
            catch
            {
                encoding2 = Encoding.Default;
            }
            StreamReader inputStreamReader = new StreamReader(inputStream, encoding2);
            return Copy(inputStreamReader, outputWriter);
        }

        public static void Copy(byte[] input, StreamWriter outputWriter, string encoding)
        {
            MemoryStream inputStream = new MemoryStream(input);
            Copy(inputStream, outputWriter, encoding);
        }

        public static void Copy(byte[] input, byte[] output, long outputOffset)
        {
            if (input.Length != 0)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    output[(int) ((IntPtr) (outputOffset + i))] = input[i];
                }
            }
        }

        public static int Copy(Stream input, Stream output, bool copyFromBeginning)
        {
            byte[] buffer = new byte[MAXBUFFER];
            int num = 0;
            int count = MAXBUFFER;
            int offset = 0;
            if (copyFromBeginning)
            {
                input.Seek(0, SeekOrigin.Begin);
            }
            while (count > 0)
            {
                count = input.Read(buffer, offset, MAXBUFFER);
                output.Write(buffer, 0, count);
                num += count;
            }
            return num;
        }

        public static void CopyExact(Stream source, Stream target, int len)
        {
            int num3;
            byte[] buffer = new byte[MAXBUFFER];
            for (int i = 0; i < len; i += num3)
            {
                int count = Math.Min(buffer.Length, len - i);
                num3 = source.Read(buffer, 0, count);
                if (num3 <= 0)
                {
                    throw new IOException(string.Format("Underlying stream does not have enough data. Read {0} bytes, but {1} needed", num3, count));
                }
                target.Write(buffer, 0, num3);
            }
        }

        public static string GetAsciiString(Stream stream)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return GetString(stream, encoding);
        }

        public static byte[] GetBytes(Stream stream)
        {
            if (stream is MemoryStream)
            {
                return ((MemoryStream) stream).ToArray();
            }
            byte[] buffer = new byte[MAXBUFFER];
            using (MemoryStream stream2 = new MemoryStream())
            {
                int num;
                stream.Position = 0;
                goto Label_0040;
            Label_0037:
                stream2.Write(buffer, 0, num);
            Label_0040:
                num = stream.Read(buffer, 0, buffer.Length);
                if (num > 0)
                {
                    goto Label_0037;
                }
                return stream2.ToArray();
            }
        }

        public static byte[] GetBytes(Stream stream, long initialLength)
        {
            int num2;
            if (initialLength < 1)
            {
                initialLength = 0x7fff;
            }
            byte[] buffer = new byte[initialLength];
            int offset = 0;
            while ((num2 = stream.Read(buffer, offset, buffer.Length - offset)) > 0)
            {
                offset += num2;
                if (offset == buffer.Length)
                {
                    int num3 = stream.ReadByte();
                    if (num3 == -1)
                    {
                        return buffer;
                    }
                    byte[] buffer4 = new byte[buffer.Length * 2];
                    Array.Copy(buffer, buffer4, buffer.Length);
                    buffer4[offset] = (byte) num3;
                    buffer = buffer4;
                    offset++;
                }
            }
            byte[] destinationArray = new byte[offset];
            Array.Copy(buffer, destinationArray, offset);
            return destinationArray;
        }

        public static string GetString(Stream stream)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return GetString(stream, encoding);
        }

        public static string GetString(Stream stream, Encoding encoding)
        {
            if (stream == null)
            {
                return string.Empty;
            }
            byte[] byteArray = new byte[stream.Length];
            if (stream is MemoryStream)
            {
                byteArray = ((MemoryStream) stream).GetBuffer();
            }
            else
            {
                ReadIntoByteArray(stream, byteArray);
            }
            return encoding.GetString(byteArray);
        }

        public static void ReadExact(Stream source, byte[] target, int sourceOffset, int targetOffset, int bytesToRead)
        {
            if ((targetOffset + bytesToRead) > target.Length)
            {
                throw new ArgumentException("target array to small");
            }
            int num = 0;
            source.Seek((long) sourceOffset, SeekOrigin.Begin);
            while (num < bytesToRead)
            {
                int count = Math.Min(MAXBUFFER, bytesToRead - num);
                int num3 = source.Read(target, targetOffset + num, count);
                if (num3 <= 0)
                {
                    throw new IOException(string.Format("Underlying stream does not have enough data. Read {0} bytes, but {1} needed", num3, count));
                }
                num += num3;
            }
        }

        public static void ReadIntoByteArray(Stream stream, byte[] byteArray)
        {
            int offset = 0;
            int length = byteArray.Length;
            stream.Position = 0;
            while (length > 0)
            {
                int num3 = stream.Read(byteArray, offset, length);
                if (num3 <= 0)
                {
                    throw new EndOfStreamException(string.Format("End of stream reached with {0} bytes left to read", length));
                }
                length -= num3;
                offset += num3;
            }
        }

        public static byte[] ReadPartial(Stream source, long sourceOffset, long bytesToRead)
        {
            long num = source.Length - sourceOffset;
            if (bytesToRead > num)
            {
                throw new ArgumentException("Bytes required exceeds what is available in stream");
            }
            byte[] buffer = new byte[bytesToRead];
            long num4 = 0;
            source.Seek(sourceOffset, SeekOrigin.Begin);
            while (num4 < bytesToRead)
            {
                int count = (int) Math.Min((long) MAXBUFFER, bytesToRead - num4);
                int num2 = source.Read(buffer, 0, count);
                if (num2 <= 0)
                {
                    throw new IOException(string.Format("Underlying stream does not have enough data. Read {0} bytes, but {1} needed", num2, count));
                }
                num4 += num2;
            }
            return buffer;
        }

        [Obsolete("See StreamUtils.Copy")]
        public static void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            byte[] buffer = new byte[MAXBUFFER];
            for (int i = readStream.Read(buffer, 0, MAXBUFFER); i > 0; i = readStream.Read(buffer, 0, 0x1000))
            {
                writeStream.Write(buffer, 0, i);
            }
            readStream.Close();
            writeStream.Close();
        }

        public static int Skip(Stream stream, int skipBytes)
        {
            long position = stream.Position;
            long num2 = stream.Seek((long) skipBytes, SeekOrigin.Current) - position;
            return (int) num2;
        }

        public static long Skip(StreamReader stream, long number)
        {
            long num = 0;
            for (long i = 0; i < number; i++)
            {
                stream.Read();
                num++;
            }
            return num;
        }

        public static long Skip(StringReader strReader, long number)
        {
            long num = 0;
            for (long i = 0; i < number; i++)
            {
                strReader.Read();
                num++;
            }
            return num;
        }

        public static byte[] ToByteArray(string sourceString)
        {
            return Encoding.UTF8.GetBytes(sourceString);
        }

        public static byte[] ToByteArray(object[] tempObjectArray)
        {
            byte[] buffer = null;
            if (tempObjectArray != null)
            {
                buffer = new byte[tempObjectArray.Length];
                for (int i = 0; i < tempObjectArray.Length; i++)
                {
                    buffer[i] = (byte) tempObjectArray[i];
                }
            }
            return buffer;
        }
    }
}

