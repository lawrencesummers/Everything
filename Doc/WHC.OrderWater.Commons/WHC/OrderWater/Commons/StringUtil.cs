namespace WHC.OrderWater.Commons
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;
    using System.Text;

    public class StringUtil
    {
        private StringUtil()
        {
        }

        public static ArrayList ExtractInnerContent(string content, string start, string end)
        {
            // This item is obfuscated and can not be translated.
            int startIndex = -1;
            int index = -1;
            int num3 = -1;
            int length = 0;
            ArrayList list = new ArrayList();
            startIndex = content.IndexOf(start);
            num3 = startIndex + start.Length;
            index = content.IndexOf(end, num3);
            length = index - num3;
            if ((startIndex >= 0) && (index > startIndex))
            {
                list.Add(content.Substring(num3, length));
            }
            while (startIndex < 0)
            {
                if (0 == 0)
                {
                    return list;
                }
                startIndex = content.IndexOf(start, index);
                if (startIndex > 0)
                {
                    index = content.IndexOf(end, startIndex);
                    num3 = startIndex + start.Length;
                    length = index - num3;
                    if ((num3 > 0) && (index > 0))
                    {
                        list.Add(content.Substring(num3, length));
                    }
                }
            }
            goto Label_0050;
        }

        public static ArrayList ExtractOuterContent(string content, string start, string end)
        {
            // This item is obfuscated and can not be translated.
            int startIndex = -1;
            int index = -1;
            ArrayList list = new ArrayList();
            startIndex = content.IndexOf(start);
            index = content.IndexOf(end);
            if ((startIndex >= 0) && (index > startIndex))
            {
                list.Add(content.Substring(startIndex, (index + end.Length) - startIndex));
            }
            while (startIndex < 0)
            {
                if (0 == 0)
                {
                    return list;
                }
                startIndex = content.IndexOf(start, index);
                if (startIndex > 0)
                {
                    index = content.IndexOf(end, startIndex);
                    if ((startIndex > 0) && (index > 0))
                    {
                        list.Add(content.Substring(startIndex, (index + end.Length) - startIndex));
                    }
                }
            }
            goto Label_0045;
        }

        public static string RemoveFinalChar(string s)
        {
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            return s;
        }

        public static string RemoveFinalComma(string s)
        {
            if (s.Trim().Length > 0)
            {
                int num = s.LastIndexOf(",");
                if (num > 0)
                {
                    s = s.Substring(0, s.Length - (s.Length - num));
                }
            }
            return s;
        }

        public static string RemovePrefix(string content, string prefixString)
        {
            if (string.IsNullOrEmpty(prefixString) || (prefixString.Trim() == string.Empty))
            {
                return content;
            }
            char[] trimChars = new char[] { ',', ';', ' ' };
            string str2 = content;
            prefixString = prefixString.Trim(trimChars);
            foreach (string str in prefixString.Split(trimChars))
            {
                if (str2.IndexOf(str, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return str2.Substring(str.Length);
                }
            }
            return str2;
        }

        public static string RemoveSpaces(string s)
        {
            s = s.Trim();
            s = s.Replace(" ", "");
            return s;
        }

        public static string ToCamel(string name)
        {
            string str = RemoveSpaces(ToProperCase(name.TrimStart(new char[] { '_' })));
            return string.Format("{0}{1}", char.ToLower(str[0]), str.Substring(1, str.Length - 1));
        }

        public static string ToCapit(string name)
        {
            return RemoveSpaces(ToProperCase(name.TrimStart(new char[] { '_' })));
        }

        public static string ToProperCase(string s)
        {
            string str = "";
            if (s.Length <= 0)
            {
                return str;
            }
            if (s.IndexOf(" ") > 0)
            {
                return Strings.StrConv(s, VbStrConv.ProperCase, 0x409);
            }
            return (s.Substring(0, 1).ToUpper(new CultureInfo("en-US")) + s.Substring(1, s.Length - 1));
        }

        public static string ToString(object o)
        {
            Type type = o.GetType();
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("Properties for: " + o.GetType().Name + Environment.NewLine);
            PropertyInfo[] infoArray2 = properties;
            int index = 0;
            while (true)
            {
                if (index >= infoArray2.Length)
                {
                    break;
                }
                PropertyInfo info2 = infoArray2[index];
                try
                {
                    builder.Append("\t" + info2.Name + "(" + info2.PropertyType.ToString() + "): ");
                    if (null != info2.GetValue(o, null))
                    {
                        builder.Append(info2.GetValue(o, null).ToString());
                    }
                }
                catch
                {
                }
                builder.Append(Environment.NewLine);
                index++;
            }
            foreach (FieldInfo info in type.GetFields())
            {
                try
                {
                    builder.Append("\t" + info.Name + "(" + info.FieldType.ToString() + "): ");
                    if (null != info.GetValue(o))
                    {
                        builder.Append(info.GetValue(o).ToString());
                    }
                }
                catch
                {
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        public static string ToTrimmedProperCase(string s)
        {
            return RemoveSpaces(ToProperCase(s));
        }
    }
}

