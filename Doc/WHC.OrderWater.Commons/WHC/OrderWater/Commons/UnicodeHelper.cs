namespace WHC.OrderWater.Commons
{
    using System;
    using System.Globalization;
    using System.Text;

    public class UnicodeHelper
    {
        public static string ConvertSingle(string unicodeSingle)
        {
            if (unicodeSingle.Length != 4)
            {
                return null;
            }
            Encoding unicode = Encoding.Unicode;
            byte[] bytes = new byte[2];
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        bytes[1] = (byte) (bytes[1] + ((byte) (smethod_1(unicodeSingle[i]) * 0x10)));
                        break;

                    case 1:
                        bytes[1] = (byte) (bytes[1] + ((byte) smethod_1(unicodeSingle[i])));
                        break;

                    case 2:
                        bytes[0] = (byte) (bytes[0] + ((byte) (smethod_1(unicodeSingle[i]) * 0x10)));
                        break;

                    case 3:
                        bytes[0] = (byte) (bytes[0] + ((byte) smethod_1(unicodeSingle[i])));
                        break;
                }
            }
            char[] chars = new char[unicode.GetCharCount(bytes, 0, bytes.Length)];
            unicode.GetChars(bytes, 0, bytes.Length, chars, 0);
            return new string(chars);
        }

        public static string smethod_0(string str)
        {
            string str2 = "";
            Encoding encoding = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;
            byte[] bytes = encoding.GetBytes(str);
            for (int i = 0; i < bytes.Length; i++)
            {
                string str4 = "%" + bytes[i].ToString("X");
                str2 = str2 + str4;
            }
            return str2;
        }

        private static ushort smethod_1(char char_0)
        {
            switch (char_0)
            {
                case 'A':
                case 'a':
                    return 10;

                case 'B':
                case 'b':
                    return 11;

                case 'C':
                case 'c':
                    return 12;

                case 'D':
                case 'd':
                    return 13;

                case 'E':
                case 'e':
                    return 14;

                case 'F':
                case 'f':
                    return 15;
            }
            return ushort.Parse(char_0.ToString());
        }

        private static string smethod_2(string string_0)
        {
            if (string_0.Length == 2)
            {
                string_0 = string_0.ToUpper();
                string str = "0123456789ABCDEF";
                int num = (str.IndexOf(string_0.Substring(0, 1)) * 0x10) + str.IndexOf(string_0.Substring(1, 1));
                return num.ToString();
            }
            return "";
        }

        public static string StringToUnicode(string str)
        {
            string str2 = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    str2 = str2 + @"\u" + ((int) str[i]).ToString("x");
                }
            }
            return str2;
        }

        public static string UnicodeToGB(string str)
        {
            int num;
            string[] strArray = str.Replace(@"\", "").Split(new char[] { 'u' });
            byte[] bytes = new byte[strArray.Length - 1];
            for (num = 1; num < strArray.Length; num++)
            {
                bytes[num - 1] = Convert.ToByte(smethod_2(strArray[num]));
            }
            char[] chars = Encoding.GetEncoding("GB2312").GetChars(bytes);
            string str2 = "";
            for (num = 0; num < chars.Length; num++)
            {
                str2 = str2 + chars[num].ToString();
            }
            return str2;
        }

        public static string UnicodeToString(string str)
        {
            string str2 = "";
            str = CRegex.Replace(str, "[\r\n]", "", 0);
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Replace(@"\u", "㊣").Split(new char[] { '㊣' });
                try
                {
                    str2 = str2 + strArray[0];
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        string str3 = strArray[i];
                        if (!(string.IsNullOrEmpty(str3) || (str3.Length < 4)))
                        {
                            str3 = strArray[i].Substring(0, 4);
                            str2 = str2 + ((char) int.Parse(str3, NumberStyles.HexNumber));
                            str2 = str2 + strArray[i].Substring(4);
                        }
                    }
                }
                catch (FormatException)
                {
                    str2 = str2 + "Erorr";
                }
            }
            return str2;
        }
    }
}

