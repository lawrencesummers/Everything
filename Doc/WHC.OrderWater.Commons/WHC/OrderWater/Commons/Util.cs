namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;

    public sealed class Util
    {
        private static Random random = new Random((int) DateTime.Now.Ticks);

        private Util()
        {
        }

        public static bool AreObjectsEqual(object source, object target)
        {
            int num;
            Type type = source.GetType();
            Type type2 = target.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] infoArray2 = type2.GetProperties();
            for (num = 0; num < properties.Length; num++)
            {
                try
                {
                    PropertyInfo info = properties[num];
                    PropertyInfo inftarget = infoArray2[num];
                    if (!(info.GetValue(source, null).Equals(null) || inftarget.GetValue(target, null).Equals(null)) && !info.GetValue(source, null).Equals(inftarget.GetValue(target, null)))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            FieldInfo[] fields = type.GetFields();
            FieldInfo[] infoArray4 = type2.GetFields();
            for (num = 0; num < fields.Length; num++)
            {
                try
                {
                    FieldInfo info3 = fields[num];
                    FieldInfo info4 = infoArray4[num];
                    if (!(info3.GetValue(source).Equals(null) || info4.GetValue(target).Equals(null)) && !info3.GetValue(source).Equals(info4.GetValue(target)))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return true;
        }

//首字母缩略词
        public static string GetAcronym(string chinese)
        {
            int length = chinese.Length;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                char ch = chinese[i];
                builder.Append(GetChineseCharString(ch.ToString()));
            }
            return builder.ToString();
        }

        public static bool IsDateTime(object inputData)
        {
            return (inputData is DateTime);
        }

        public static bool IsNumeric(object inputData)
        {
            return ((((((inputData is short) || (inputData is int)) || ((inputData is long) || (inputData is decimal))) || (((inputData is double) || (inputData is byte)) || ((inputData is sbyte) || (inputData is float)))) || ((inputData is ushort) || (inputData is uint))) || (inputData is ulong));
        }

        public static string Join(IList<string> list)
        {
            return Join<string>(list, ",");
        }

        public static string Join<T>(IList<T> list)
        {
            return Join<T>(list, ",");
        }

        public static string Join<T>(IList<T> list, string item)
        {
            if ((list != null) && (list.Count > 0))
            {
                StringBuilder builder = new StringBuilder();
                foreach (T local in list)
                {
                    builder.Append(item);
                    builder.Append(local.ToString());
                }
                if (!string.IsNullOrEmpty(item))
                {
                    return builder.ToString().Substring(item.Length);
                }
                return builder.ToString();
            }
            return string.Empty;
        }

        public static string Join(IList<string> list, string item)
        {
            return Join<string>(list, item);
        }

        public static bool RandomAction(int rate)
        {
            return (random.Next(100) < rate);
        }

        private static string GetChineseCharString(string chineseChar)
        {
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(chineseChar);
            if (bytes.Length <= 1)
            {
                return chineseChar;
            }
            int num2 = bytes[0];
            int num3 = bytes[1];
            int num4 = (num2 << 8) + num3;
            int[] numArray = new int[] { 
                0xb0a1, 0xb0c5, 0xb2c1, 0xb4ee, 0xb6ea, 0xb7a2, 0xb8c1, 0xb9fe, 0xbbf7, 0xbbf7, 0xbfa6, 0xc0ac, 0xc2e8, 0xc4c3, 0xc5b6, 0xc5be, 
                0xc6da, 0xc8bb, 0xc8f6, 0xcbfa, 0xcdda, 0xcdda, 0xcdda, 0xcef4, 0xd1b9, 0xd4d1
             };
            for (int i = 0; i < 0x1a; i++)
            {
                int num5 = 0xd7fa;
                if (i != 0x19)
                {
                    num5 = numArray[i + 1];
                }
                if ((numArray[i] <= num4) && (num4 < num5))
                {
                    return Encoding.GetEncoding("GB2312").GetString(new byte[] { (byte) (0x41 + i) });
                }
            }
            return string.Empty;
        }

        public static string WinFormName
        {
            get
            {
                string executablePath = Application.ExecutablePath;
                int num = executablePath.LastIndexOf(@"\");
                int num2 = executablePath.ToLower().LastIndexOf(".exe");
                return executablePath.Substring(num + 1, (num2 - num) - 1);
            }
        }
    }
}

