namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;

    public class StringHelper
    {
        public static string ArrayToList(string[] ids)
        {
            return ArrayToList(ids, string.Empty);
        }

        public static string ArrayToList(string[] ids, string separativeSign)
        {
            int num = 0;
            string str = string.Empty;
            foreach (string str2 in ids)
            {
                num++;
                string str3 = str;
                str = str3 + separativeSign + str2 + separativeSign + ",";
            }
            if (num == 0)
            {
                return "";
            }
            return str.TrimEnd(new char[] { ',' });
        }

        public static string[] Concat(params string[][] ids)
        {
            Hashtable hashtable = new Hashtable();
            if (ids != null)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (ids[j] != null)
                    {
                        for (int k = 0; k < ids[j].Length; k++)
                        {
                            if ((ids[j][k] != null) && !hashtable.ContainsKey(ids[j][k]))
                            {
                                hashtable.Add(ids[j][k], ids[j][k]);
                            }
                        }
                    }
                }
            }
            string[] strArray = new string[hashtable.Count];
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                strArray[i] = enumerator.Key.ToString();
            }
            return strArray;
        }

        public static string[] Concat(string[] ids, string id)
        {
            return Concat(new string[][] { ids, new string[] { id } });
        }

        public static string DeleteUnVisibleChar(string sourceString)
        {
            StringBuilder builder = new StringBuilder(0x83);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int num2 = sourceString[i];
                if (num2 >= 0x10)
                {
                    builder.Append(sourceString[i].ToString());
                }
            }
            return builder.ToString();
        }

        public static bool Exists(string[] ids, string targetString)
        {
            if ((ids != null) && !string.IsNullOrEmpty(targetString))
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Equals(targetString))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string GetEqual(string field, string search)
        {
            string str = field + " = '" + search + "' ";
            return ("(" + str + ")");
        }

        public static string GetLike(string field, string search)
        {
            string str = string.Empty;
            for (int i = 0; i < search.Length; i++)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, field, " LIKE '%", search[i], "%' AND " });
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.Length - 5);
            }
            return ("(" + str + ")");
        }

        public static string GetSearchString(string searchValue, bool allLike = false)
        {
            searchValue = searchValue.Trim();
            searchValue = SqlSafe(searchValue);
            if (searchValue.Length > 0)
            {
                searchValue = searchValue.Replace('[', '_');
                searchValue = searchValue.Replace(']', '_');
            }
            if (searchValue == "%")
            {
                searchValue = "[%]";
            }
            if (((searchValue.Length > 0) && (searchValue.IndexOf('%') < 0)) && (searchValue.IndexOf('_') < 0))
            {
                if (allLike)
                {
                    string str = string.Empty;
                    for (int i = 0; i < searchValue.Length; i++)
                    {
                        str = str + "%" + searchValue[i];
                    }
                    searchValue = str + "%";
                    return searchValue;
                }
                searchValue = "%" + searchValue + "%";
            }
            return searchValue;
        }

        public static string GetSubString(string Str, int Num)
        {
            if ((Str == null) || (Str == ""))
            {
                return "";
            }
            string str2 = "";
            int num = 0;
            foreach (char ch in Str)
            {
                num += Encoding.Default.GetByteCount(ch.ToString());
                if (num > Num)
                {
                    return str2;
                }
                str2 = str2 + ch;
            }
            return str2;
        }

        public static string GetSubString(string Str, int Num, string LastStr)
        {
            return ((Str.Length > Num) ? (Str.Substring(0, Num) + LastStr) : Str);
        }

        public static bool HasDangerousWord(string word)
        {
            string[] strArray2 = new string[] { 
                "delete", "truncate", "drop", "insert", "update", "exec", "select", "truncate", "dbcc", "@", "alter", "drop", "create", "if", "else", "and", 
                "add", "open", "return", "exists", "declare", "go", "use"
             };
            word = word.ToLower();
            int length = strArray2.Length;
            for (int i = 0; i < length; i++)
            {
                if (word.Contains(strArray2[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsImgString(string Input)
        {
            return IsImgString(Input, "/{@dirfile}/");
        }

        public static bool IsImgString(string Input, string checkStr)
        {
            bool flag = false;
            if (Input != string.Empty)
            {
                string str = Input.ToLower();
                if ((str.IndexOf(checkStr.ToLower()) != -1) && (str.IndexOf(".") != -1))
                {
                    switch (str.Substring(str.LastIndexOf(".") + 1).ToString().ToLower())
                    {
                        case "jpg":
                        case "gif":
                        case "bmp":
                        case "png":
                            flag = true;
                            break;
                    }
                }
            }
            return flag;
        }

        public static bool IsNullOrEmpty(string value)
        {
            return ((value == null) || (value == string.Empty));
        }

        public static string Lost(string chr)
        {
            if ((chr == null) || (chr == string.Empty))
            {
                return "";
            }
            chr = chr.Remove(chr.LastIndexOf(","));
            return chr;
        }

        public static int NumChar(string Input)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(Input);
            int num = 0;
            for (int i = 0; i <= (bytes.Length - 1); i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num++;
                }
                num++;
            }
            return num;
        }

        public static string[] Remove(string[] ids, string id)
        {
            Hashtable hashtable = new Hashtable();
            if (ids != null)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (!((ids[j] == null) || ids[j].Equals(id)) && !hashtable.ContainsKey(ids[j]))
                    {
                        hashtable.Add(ids[j], ids[j]);
                    }
                }
            }
            string[] strArray = new string[hashtable.Count];
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                strArray[i] = enumerator.Key.ToString();
            }
            return strArray;
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

        public static string RemoveSpaces(string s)
        {
            s = s.Trim();
            s = s.Replace(" ", "");
            return s;
        }

        public static string RepeatString(string targetString, int repeatCount)
        {
            string str = string.Empty;
            for (int i = 0; i < repeatCount; i++)
            {
                str = str + targetString;
            }
            return str;
        }

        public static string smethod_0(string Input)
        {
            string str = "";
            foreach (char ch in Input)
            {
                if ((ch >= '!') && (ch <= '~'))
                {
                    str = str + ch.ToString();
                }
                else
                {
                    str = str + smethod_1(ch.ToString());
                }
            }
            return str;
        }

        private static string smethod_1(string string_0)
        {
            byte[] bytes = new byte[2];
            bytes = Encoding.Default.GetBytes(string_0);
            int num = (bytes[0] * 0x100) + bytes[1];
            if (num >= 0xb0a1)
            {
                if (num < 0xb0c5)
                {
                    return "A";
                }
                if (num < 0xb2c1)
                {
                    return "B";
                }
                if (num < 0xb4ee)
                {
                    return "C";
                }
                if (num < 0xb6ea)
                {
                    return "D";
                }
                if (num < 0xb7a2)
                {
                    return "E";
                }
                if (num < 0xb8c1)
                {
                    return "F";
                }
                if (num < 0xb9fe)
                {
                    return "G";
                }
                if (num < 0xbbf7)
                {
                    return "H";
                }
                if (num < 0xbfa6)
                {
                    return "G";
                }
                if (num < 0xc0ac)
                {
                    return "K";
                }
                if (num < 0xc2e8)
                {
                    return "L";
                }
                if (num < 0xc4c3)
                {
                    return "M";
                }
                if (num < 0xc5b6)
                {
                    return "N";
                }
                if (num < 0xc5be)
                {
                    return "O";
                }
                if (num < 0xc6da)
                {
                    return "P";
                }
                if (num < 0xc8bb)
                {
                    return "Q";
                }
                if (num < 0xc8f6)
                {
                    return "R";
                }
                if (num < 0xcbfa)
                {
                    return "S";
                }
                if (num < 0xcdda)
                {
                    return "T";
                }
                if (num < 0xcef4)
                {
                    return "W";
                }
                if (num < 0xd1b9)
                {
                    return "X";
                }
                if (num < 0xd4d1)
                {
                    return "Y";
                }
                if (num < 0xd7fa)
                {
                    return "Z";
                }
            }
            return "*";
        }

        internal static string smethod_2(string string_0)
        {
            string[] strArray2 = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string[] strArray3 = new string[] { "", "拾", "佰", "仟" };
            string[] strArray4 = new string[] { "", "[万]", "[亿]", "[万亿]" };
            string str = "";
            int startIndex = 0;
            int num2 = string_0.Length % 4;
            int num3 = (num2 > 0) ? ((string_0.Length / 4) + 1) : (string_0.Length / 4);
            for (int i = num3; i > 0; i--)
            {
                int length = 4;
                if ((i == num3) && (num2 != 0))
                {
                    length = num2;
                }
                string str2 = string_0.Substring(startIndex, length);
                int num6 = str2.Length;
                for (int j = 0; j < num6; j++)
                {
                    int index = Convert.ToInt32(str2.Substring(j, 1));
                    if (index == 0)
                    {
                        if (!(((j >= (num6 - 1)) || (Convert.ToInt32(str2.Substring(j + 1, 1)) <= 0)) || str.EndsWith(strArray2[index])))
                        {
                            str = str + strArray2[index];
                        }
                    }
                    else
                    {
                        if (((index != 1) || !(str.EndsWith(strArray2[0]) | (str.Length == 0))) || (j != (num6 - 2)))
                        {
                            str = str + strArray2[index];
                        }
                        str = str + strArray3[(num6 - j) - 1];
                    }
                }
                startIndex += length;
                if (i < num3)
                {
                    if (Convert.ToInt32(str2) != 0)
                    {
                        str = str + strArray4[i - 1];
                    }
                }
                else
                {
                    str = str + strArray4[i - 1];
                }
            }
            return str;
        }

        internal static string smethod_3(string string_0, string[] string_1, string[] string_2, string[] string_3)
        {
            string str = "";
            int startIndex = 0;
            int num2 = string_0.Length % 4;
            int num3 = (num2 > 0) ? ((string_0.Length / 4) + 1) : (string_0.Length / 4);
            for (int i = num3; i > 0; i--)
            {
                int length = 4;
                if ((i == num3) && (num2 != 0))
                {
                    length = num2;
                }
                string str2 = string_0.Substring(startIndex, length);
                int num6 = str2.Length;
                for (int j = 0; j < num6; j++)
                {
                    int index = Convert.ToInt32(str2.Substring(j, 1));
                    if (index == 0)
                    {
                        if (!(((j >= (num6 - 1)) || (Convert.ToInt32(str2.Substring(j + 1, 1)) <= 0)) || str.EndsWith(string_1[index])))
                        {
                            str = str + string_1[index];
                        }
                    }
                    else
                    {
                        if (((index != 1) || !(str.EndsWith(string_1[0]) | (str.Length == 0))) || (j != (num6 - 2)))
                        {
                            str = str + string_1[index];
                        }
                        str = str + string_2[(num6 - j) - 1];
                    }
                }
                startIndex += length;
                if (i < num3)
                {
                    if (Convert.ToInt32(str2) != 0)
                    {
                        str = str + string_3[i - 1];
                    }
                }
                else
                {
                    str = str + string_3[i - 1];
                }
            }
            return str;
        }

        public static string SqlSafe(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }
    }
}

