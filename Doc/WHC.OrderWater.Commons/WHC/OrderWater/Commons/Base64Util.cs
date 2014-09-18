namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Base64Util
    {
        protected string m_codeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";
        protected string m_pad = "v";
        protected Dictionary<int, char> m_t1 = new Dictionary<int, char>();
        protected Dictionary<char, int> m_t2 = new Dictionary<char, int>();
        protected static Base64Util s_b64 = new Base64Util();

        public Base64Util()
        {
            this.InitDict();
        }

        public string Decode(string source)
        {
            int num2;
            if ((source == null) || (source == ""))
            {
                return "";
            }
            List<byte> byteArr = new List<byte>();
            char[] array = source.ToCharArray();
            int num4 = array.Length % 4;
            if (num4 != 0)
            {
                Array.Resize<char>(ref array, array.Length - num4);
            }
            int index = source.IndexOf(this.m_pad);
            if (index != -1)
            {
                index = source.Length - index;
            }
            int num3 = array.Length / 4;
            for (num2 = 0; num2 < num3; num2++)
            {
                this.DecodeUnit(byteArr, new char[] { array[num2 * 4], array[(num2 * 4) + 1], array[(num2 * 4) + 2], array[(num2 * 4) + 3] });
            }
            for (num2 = 0; num2 < index; num2++)
            {
                byteArr.RemoveAt(byteArr.Count - 1);
            }
            return Encoding.UTF8.GetString(byteArr.ToArray());
        }

        protected void DecodeUnit(List<byte> byteArr, params char[] chArray)
        {
            int num;
            int[] numArray = new int[3];
            byte[] buffer = new byte[chArray.Length];
            for (num = 0; num < chArray.Length; num++)
            {
                buffer[num] = this.FindChar(chArray[num]);
            }
            numArray[0] = (buffer[0] << 2) + ((buffer[1] & 0x30) >> 4);
            numArray[1] = ((buffer[1] & 15) << 4) + ((buffer[2] & 60) >> 2);
            numArray[2] = ((buffer[2] & 3) << 6) + buffer[3];
            for (num = 0; num < numArray.Length; num++)
            {
                byteArr.Add((byte) numArray[num]);
            }
        }

        public static string Decrypt(string input)
        {
            return s_b64.Decode(input);
        }

        public string Encode(string source)
        {
            int num2;
            if ((source == null) || (source == ""))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            int num4 = bytes.Length % 3;
            int length = 3 - num4;
            if (num4 != 0)
            {
                Array.Resize<byte>(ref bytes, bytes.Length + length);
            }
            int num3 = (int) Math.Ceiling((double) ((bytes.Length * 1.0) / 3.0));
            for (num2 = 0; num2 < num3; num2++)
            {
                builder.Append(this.EncodeUnit(new byte[] { bytes[num2 * 3], bytes[(num2 * 3) + 1], bytes[(num2 * 3) + 2] }));
            }
            if (num4 != 0)
            {
                builder.Remove(builder.Length - length, length);
                for (num2 = 0; num2 < length; num2++)
                {
                    builder.Append(this.m_pad);
                }
            }
            return builder.ToString();
        }

        protected string EncodeUnit(params byte[] unit)
        {
            int[] numArray = new int[] { (unit[0] & 0xfc) >> 2, ((unit[0] & 3) << 4) + ((unit[1] & 240) >> 4), ((unit[1] & 15) << 2) + ((unit[2] & 0xc0) >> 6), unit[2] & 0x3f };
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < numArray.Length; i++)
            {
                builder.Append(this.GetEC(numArray[i]));
            }
            return builder.ToString();
        }

        public static string Encrypt(string input)
        {
            return s_b64.Encode(input);
        }

        protected byte FindChar(char ch)
        {
            int num = this.m_t2[ch];
            return (byte) num;
        }

        protected char GetEC(int code)
        {
            return this.m_t1[code];
        }

        public static Base64Util GetStandardBase64()
        {
            return new Base64Util { Pad = "=", CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/" };
        }

        protected void InitDict()
        {
            this.m_t1.Clear();
            this.m_t2.Clear();
            this.m_t2.Add(this.m_pad[0], -1);
            for (int i = 0; i < this.m_codeTable.Length; i++)
            {
                this.m_t1.Add(i, this.m_codeTable[i]);
                this.m_t2.Add(this.m_codeTable[i], i);
            }
        }

        protected void Test()
        {
            this.InitDict();
            string str = "abc ABC 你好！◎＃￥％……!@#$%^";
            string source = this.Encode("false");
            string str3 = this.Decode(source);
            Console.WriteLine(source);
            Console.WriteLine(str == str3);
        }

        protected void ValidateEqualPad(string input, string pad)
        {
            if (input.IndexOf(pad) > -1)
            {
                throw new Exception("密码表中包含了补码字符：" + pad);
            }
        }

        protected void ValidateRepeat(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.LastIndexOf(input[i]) > i)
                {
                    throw new Exception("密码表中含有重复字符：" + input[i]);
                }
            }
        }

        public string CodeTable
        {
            get
            {
                return this.m_codeTable;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表不能为null");
                }
                if (value.Length < 0x40)
                {
                    throw new Exception("密码表长度必须至少为64");
                }
                this.ValidateRepeat(value);
                this.ValidateEqualPad(value, this.m_pad);
                this.m_codeTable = value;
                this.InitDict();
            }
        }

        public string Pad
        {
            get
            {
                return this.m_pad;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表的补码不能为null");
                }
                if (value.Length != 1)
                {
                    throw new Exception("密码表的补码长度必须为1");
                }
                this.ValidateEqualPad(this.m_codeTable, value);
                this.m_pad = value;
                this.InitDict();
            }
        }
    }
}

