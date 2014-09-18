namespace WHC.OrderWater.Commons
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CString
    {
        public static string AcquireAssignString(string str, int num)
        {
            string str2 = str;
            return StringToHtml(GetLetter(str2, num, false));
        }

        public static string AddBlankAtForefront(string str)
        {
            return str;
        }

        public static string AddZero(int sheep, int length)
        {
            return AddZero(sheep.ToString(), length);
        }

        public static string AddZero(string sheep, int length)
        {
            StringBuilder builder = new StringBuilder(sheep);
            for (int i = builder.Length; i < length; i++)
            {
                builder.Insert(0, "0");
            }
            return builder.ToString();
        }

        public static bool CheckValidity(string s)
        {
            string str = s;
            if (((((str.IndexOf("'") > 0) || (str.IndexOf("&") > 0)) || ((str.IndexOf("%") > 0) || (str.IndexOf("+") > 0))) || ((str.IndexOf("\"") > 0) || (str.IndexOf("=") > 0))) || (str.IndexOf("!") > 0))
            {
                return false;
            }
            return true;
        }

        public static string ClearTag(string sHtml)
        {
            if (sHtml == "")
            {
                return "";
            }
            Regex regex = new Regex(@"(<[^>\s]*\b(\w)+\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\r|\n|\t", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return regex.Replace(sHtml, "");
        }

        public static string ClearTag(string sHtml, string sRegex)
        {
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return regex.Replace(sHtml, "");
        }

        public static string ConvertToJS(string sHtml)
        {
            StringBuilder builder = new StringBuilder();
            Regex regex = new Regex(@"\r\n", RegexOptions.IgnoreCase);
            foreach (string str2 in regex.Split(sHtml))
            {
                builder.Append("document.writeln(\"" + str2.Replace("\"", "\\\"") + "\");\r\n");
            }
            return builder.ToString();
        }

        public static string DelHtmlString(string str)
        {
            string[] strArray2 = new string[] { "<script[^>]*?>.*?</script>", "<(\\/\\s*)?!?((\\w+:)?\\w+)(\\w+(\\s*=?\\s*(([\"'])(\\\\[\"'tbnr]|[^\\7])*?\\7|\\w+)|.{0})|\\s)*?(\\/\\s*)?>", @"([\r\n])[\s]+", "&(quot|#34);", "&(amp|#38);", "&(lt|#60);", "&(gt|#62);", "&(nbsp|#160);", "&(iexcl|#161);", "&(cent|#162);", "&(pound|#163);", "&(copy|#169);", @"&#(\d+);", "-->", @"<!--.*\n" };
            string[] strArray3 = new string[] { "", "", "", "\"", "&", "<", ">", " ", "\x00a1", "\x00a2", "\x00a3", "\x00a9", "", "\r\n", "" };
            string input = str;
            for (int i = 0; i < strArray2.Length; i++)
            {
                input = new Regex(strArray2[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(input, strArray3[i]);
            }
            input.Replace("<", "");
            input.Replace(">", "");
            input.Replace("\r\n", "");
            return input;
        }

        public static string DelTag(string str, string tag, bool isContent)
        {
            if ((tag == null) || (tag == " "))
            {
                return str;
            }
            if (isContent)
            {
                return Regex.Replace(str, string.Format(@"<({0})[^>]*>([\s\S]*?)<\/\1>", tag), "", RegexOptions.IgnoreCase);
            }
            return Regex.Replace(str, string.Format("(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "", RegexOptions.IgnoreCase);
        }

        public static string DelTagArray(string str, string tagA, bool isContent)
        {
            foreach (string str2 in tagA.Split(new char[] { ',' }))
            {
                str = DelTag(str, str2, isContent);
            }
            return str;
        }

        public static string GetAllLinkText(string html)
        {
            StringBuilder builder = new StringBuilder();
            Match match = Regex.Match(html.ToLower(), "<a href=.*?>(1,100})</a>");
            while (match.Success)
            {
                builder.AppendLine(match.Result("$1"));
                match.NextMatch();
            }
            return builder.ToString();
        }

        public static string GetCleanJsString(string str)
        {
            str = str.Replace("\"", "“");
            str = str.Replace("'", "”");
            str = str.Replace(@"\", @"\\");
            str = new Regex(@"\r|\n|\t", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(str, " ");
            return str;
        }

        public static string GetCleanJsString2(string str)
        {
            str = str.Replace("\"", "\\\"");
            str = new Regex(@"\r|\n|\t", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(str, " ");
            return str;
        }

        public static string GetDateString(DateTime dt)
        {
            return (dt.Year.ToString() + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0'));
        }

        public static int GetLength(string str)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(str);
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

        public static string GetLetter(string str, int iNum, bool bAddDot)
        {
            char[] chArray;
            if ((str == null) || (iNum <= 0))
            {
                return "";
            }
            if ((str.Length < iNum) && ((str.Length * 2) < iNum))
            {
                return str;
            }
            string str2 = str;
            int length = iNum;
            if (str2.Length >= length)
            {
                chArray = str.ToCharArray(0, length);
            }
            else
            {
                chArray = str.ToCharArray(0, str2.Length);
            }
            int num2 = 0;
            int num = 0;
            foreach (char ch in chArray)
            {
                num++;
                int num5 = ch;
                if ((num5 > 0x7f) || (num5 < 0))
                {
                    num2 += 2;
                }
                else
                {
                    num2++;
                }
                if (num2 > length)
                {
                    num--;
                    break;
                }
                if (num2 == length)
                {
                    break;
                }
            }
            if ((num < str.Length) && bAddDot)
            {
                str2 = str2.Substring(0, num - 3) + "...";
            }
            else
            {
                str2 = str2.Substring(0, num);
            }
            return str2;
        }

        public static string GetPreStrByLast(string sOrg, string sLast)
        {
            int length = sOrg.LastIndexOf(sLast);
            if (length > 0)
            {
                return sOrg.Substring(0, length);
            }
            return sOrg;
        }

        public static string GetSQLFildList(string fldList)
        {
            if (fldList == null)
            {
                return "*";
            }
            if (fldList.Trim() == "")
            {
                return "*";
            }
            if (fldList.Trim() == "*")
            {
                return "*";
            }
            string str2 = fldList;
            str2 = str2.Replace(" ", "").Replace("[", "").Replace("]", "");
            return ("[" + str2 + "]").Replace(0xff0c, ',').Replace(",", "],[");
        }

        public static string GetStrByLast(string sOrg, string sLast)
        {
            int num = sOrg.LastIndexOf(sLast);
            if (num > 0)
            {
                return sOrg.Substring(num + 1);
            }
            return sOrg;
        }

        public static string GetTimeDelay(DateTime dtStar, DateTime dtEnd)
        {
            long num = (dtEnd.Ticks - dtStar.Ticks) / 0x989680;
            long num2 = num / 0xe10;
            num2 = (num % 0xe10) / 60;
            num2 = (num % 0xe10) % 60;
            return (((num2.ToString().PadLeft(2, '0') + ":") + num2.ToString().PadLeft(2, '0') + ":") + num2.ToString().PadLeft(2, '0'));
        }

        public static string GetUniqueString()
        {
            Random random = new Random();
            int num = (int) (random.NextDouble() * 10000.0);
            return (num.ToString() + DateTime.Now.Ticks.ToString());
        }

        public static bool IsEmpty(string str)
        {
            return ((str == null) || (str == ""));
        }

        public static string RemoveEndWith(string sOrg, string sEnd)
        {
            if (sOrg.EndsWith(sEnd))
            {
                sOrg = sOrg.Remove(sOrg.IndexOf(sEnd), sEnd.Length);
            }
            return sOrg;
        }

        public static string ReplaceNbsp(string str)
        {
            string str2 = str;
            if (str2.Length > 0)
            {
                str2 = str2.Replace(" ", "").Replace("&nbsp;", "");
                str2 = "&nbsp;&nbsp;&nbsp;&nbsp;" + str2;
            }
            return str2;
        }

        public static string SetVersionFormat(string sVersion)
        {
            // This item is obfuscated and can not be translated.
            if ((sVersion == null) || (sVersion == ""))
            {
                return "";
            }
            int num2 = 0;
            int length = 0;
            string str = "";
            while (num2 >= 4)
            {
                if (0 == 0)
                {
                    if (length > 0)
                    {
                        str = sVersion.Substring(0, length);
                    }
                    else
                    {
                        str = sVersion;
                    }
                    return str;
                }
                length = sVersion.IndexOf(".", (int) (length + 1));
                num2++;
            }
            goto Label_002B;
        }

        public static string smethod_0(string html)
        {
            StringBuilder builder = new StringBuilder();
            Match match = Regex.Match(html.ToLower(), "<a href=(.*?)>.*?</a>");
            while (match.Success)
            {
                builder.AppendLine(match.Result("$1"));
                match.NextMatch();
            }
            return builder.ToString();
        }

        public static string StringToHtml(string str)
        {
            string str2 = str;
            if (str2.Length > 0)
            {
                char ch = '\r';
                str2 = str2.Replace(ch.ToString(), "<br>").Replace(" ", "&nbsp;").Replace("　", "&nbsp;&nbsp;");
            }
            return str2;
        }

        public static string TransformPrice(double dPrice)
        {
            double num = dPrice;
            NumberFormatInfo provider = new NumberFormatInfo {
                NumberNegativePattern = 2
            };
            return num.ToString("N", provider);
        }

        public static string TranslateToHtmlString(string str, int num)
        {
            string str2 = str;
            return StringToHtml(GetLetter(str2, num, false));
        }

        public static string TransToStr(float f, int iNum)
        {
            NumberFormatInfo provider = new NumberFormatInfo {
                NumberNegativePattern = iNum
            };
            return f.ToString("N", provider);
        }
    }
}

