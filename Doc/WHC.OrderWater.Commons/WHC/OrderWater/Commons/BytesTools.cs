namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public static class BytesTools
    {
        public static readonly Encoding ASCII = Encoding.ASCII;
        public static readonly Encoding GB2312 = Encoding.GetEncoding("GB2312");

        public static int BufferLookup(byte[] srcbuff, string subchars)
        {
            return BufferLookup(srcbuff, subchars, 0);
        }

        public static int BufferLookup(byte[] srcbuff, byte[] subbuff)
        {
            return BufferLookup(srcbuff, subbuff, 0);
        }

        public static int BufferLookup(byte[] srcbuff, string subchars, int start)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(subchars);
            return BufferLookup(srcbuff, bytes, start);
        }

        public static int BufferLookup(byte[] srcbuff, byte[] subbuff, int start)
        {
            for (int i = start; i < ((srcbuff.Length - subbuff.Length) + 1); i++)
            {
                for (int j = 0; j < subbuff.Length; j++)
                {
                    if (srcbuff[i + j] != subbuff[j])
                    {
                        break;
                    }
                    if (j == (subbuff.Length - 1))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static string BytesToHex(byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str = str + bytes[i].ToString("X2");
            }
            return str;
        }

        public static byte[] Clone(byte[] byte1)
        {
            if (byte1 == null)
            {
                throw new ArgumentNullException("byte1");
            }
            byte[] dst = new byte[byte1.Length];
            Buffer.BlockCopy(byte1, 0, dst, 0, byte1.Length);
            return dst;
        }

        public static byte[] Combine(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null)
            {
                throw new ArgumentNullException("byte1");
            }
            if (byte2 == null)
            {
                throw new ArgumentNullException("byte2");
            }
            byte[] dst = new byte[byte1.Length + byte2.Length];
            Buffer.BlockCopy(byte1, 0, dst, 0, byte1.Length);
            Buffer.BlockCopy(byte2, 0, dst, byte1.Length, byte2.Length);
            return dst;
        }

        public static bool Compare(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null)
            {
                return false;
            }
            if (byte2 == null)
            {
                return false;
            }
            if (byte1.Length != byte2.Length)
            {
                return false;
            }
            for (int i = 0; i < byte1.Length; i++)
            {
                if (byte1[i] != byte2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] GetBytesFromHexString(string hexadecimalNumber)
        {
            string message = "字符串必须为一个有效的十六进制字符串 (e.g. : 0F99DD)";
            if (string.IsNullOrEmpty(hexadecimalNumber))
            {
                throw new ArgumentNullException("hexadecimalNumber");
            }
            StringBuilder builder = new StringBuilder(hexadecimalNumber.ToUpper(CultureInfo.CurrentCulture));
            char ch = builder[0];
            if (ch.Equals('0') && (ch = builder[1]).Equals('X'))
            {
                builder.Remove(0, 2);
            }
            if ((builder.Length % 2) != 0)
            {
                throw new ArgumentException(message);
            }
            byte[] buffer = new byte[builder.Length / 2];
            try
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    int startIndex = i * 2;
                    buffer[i] = Convert.ToByte(builder.ToString(startIndex, 2), 0x10);
                }
            }
            catch (FormatException exception)
            {
                throw new ArgumentException(message, exception);
            }
            return buffer;
        }

        public static string GetHexStringFromBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                throw new ArgumentOutOfRangeException("bytes", "字节数组的长度必须大于0.");
            }
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("X2", CultureInfo.CurrentCulture));
            }
            return builder.ToString();
        }

        public static byte[] HexToBytes(string input)
        {
            int num = input.Length % 2;
            if (num != 0)
            {
                throw new Exception("字符串的长度必需是偶数");
            }
            List<byte> list = new List<byte>();
            for (int i = 0; i < input.Length; i += 2)
            {
                byte item = Convert.ToByte(input.Substring(i, 2), 0x10);
                list.Add(item);
            }
            return list.ToArray();
        }

        public static int MakeLong(short loValue, short hiValue)
        {
            return ((hiValue << 0x10) | (loValue & 0xffff));
        }

        internal static byte[] smethod_0(object object_0, int int_0, int int_1)
        {
            if (object_0.Length < (int_0 + int_1))
            {
                throw new ArgumentOutOfRangeException("SubBuffer");
            }
            byte[] buffer = new byte[int_1];
            for (int i = 0; i < int_1; i++)
            {
                buffer[i] = (byte) object_0[i + int_0];
            }
            return buffer;
        }

        internal static byte[] smethod_1(ushort ushort_0)
        {
            return SwapBytes(BitConverter.GetBytes(ushort_0));
        }

        internal static byte[] smethod_2(int int_0)
        {
            return SwapBytes(BitConverter.GetBytes(int_0));
        }

        internal static ushort smethod_3(object object_0, int int_0)
        {
            return BitConverter.ToUInt16(SwapBytes(new byte[] { object_0[int_0], object_0[int_0 + 1] }), 0);
        }

        internal static uint smethod_4(object object_0, int int_0)
        {
            return BitConverter.ToUInt32(SwapBytes(smethod_0(object_0, int_0, 4)), 0);
        }

        internal static DateTime smethod_5(object object_0)
        {
            string str = "";
            for (int i = 0; i < 7; i++)
            {
                str = str + ((byte) object_0[i]).ToString("X2");
            }
            str = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2) + " " + str.Substring(8, 2) + ":" + str.Substring(10, 2) + ":" + str.Substring(12, 2);
            try
            {
                return Convert.ToDateTime(str);
            }
            catch
            {
                return new DateTime(0x7d0, 1, 1);
            }
        }

        public static short smethod_6(int value)
        {
            return (short) (value & 0xffff);
        }

        public static short smethod_7(int value)
        {
            return (short) ((value >> 0x10) & 0xffff);
        }

        public static byte[] SpecCharConvert(byte[] srcbuff)
        {
            List<byte> list = new List<byte>();
            foreach (byte num2 in srcbuff)
            {
                if (num2 == 0x7e)
                {
                    list.Add(0x7d);
                    list.Add(2);
                }
                else if (num2 == 0x7d)
                {
                    list.Add(0x7d);
                    list.Add(1);
                }
                else
                {
                    list.Add(num2);
                }
            }
            return list.ToArray();
        }

        public static byte[] SpecCharReverse(byte[] srcbuff)
        {
            List<byte> list = new List<byte>();
            int index = 0;
            while (index < srcbuff.Length)
            {
                if (srcbuff[index] == 0x7d)
                {
                    if (srcbuff[index + 1] == 2)
                    {
                        list.Add(0x7e);
                    }
                    else
                    {
                        if (srcbuff[index + 1] != 1)
                        {
                            throw new ArgumentException("非法数据");
                        }
                        list.Add(0x7d);
                    }
                    index += 2;
                }
                else
                {
                    list.Add(srcbuff[index]);
                    index++;
                }
            }
            return list.ToArray();
        }

        public static byte[] SwapBytes(byte[] bytes)
        {
            int length = bytes.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = bytes[(length - i) - 1];
            }
            return buffer;
        }

        public static byte[] ToDBDate(DateTime dateTime)
        {
            byte[] buffer = new byte[15];
            buffer[0] = (byte) ((dateTime.Year / 100) + 100);
            buffer[1] = (byte) ((dateTime.Year % 100) + 100);
            buffer[2] = (byte) dateTime.Month;
            buffer[3] = (byte) dateTime.Day;
            buffer[4] = (byte) (dateTime.Hour + 1);
            buffer[5] = (byte) (dateTime.Minute + 1);
            buffer[6] = (byte) (dateTime.Second + 1);
            return buffer;
        }

        public static byte[] ToSBC(byte[] srcbuff)
        {
            List<byte> list = new List<byte>();
            int index = 0;
            while (index < srcbuff.Length)
            {
                if (srcbuff[index] == 0x20)
                {
                    list.Add(0xa1);
                    list.Add(0xa1);
                    index++;
                }
                else
                {
                    if (srcbuff[index] == 0x7e)
                    {
                        list.Add(0xa1);
                        list.Add(0xab);
                        index++;
                        continue;
                    }
                    if (srcbuff[index] > 0x80)
                    {
                        list.Add(srcbuff[index]);
                        list.Add(srcbuff[index + 1]);
                        index += 2;
                        continue;
                    }
                    list.Add(0xa3);
                    list.Add((byte) (srcbuff[index] + 0x80));
                    index++;
                }
            }
            return list.ToArray();
        }
    }
}

