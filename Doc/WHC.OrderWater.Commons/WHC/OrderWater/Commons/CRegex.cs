namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.RegularExpressions;

    public class CRegex
    {
        public static string GetAuthor(string sInput, string sRegex)
        {
            string str = CString.ClearTag(GetText(sInput, sRegex, "Author"));
            if (str.Length > 0x63)
            {
                str = str.Substring(0, 0x63);
            }
            return str;
        }

        public static string GetBody(string sInput)
        {
            return GetText(sInput, @"<Body[^>]*>(?<Body>[\s\S]{10,})</body>", "Body");
        }

        public static string GetBody(string sInput, string sRegex)
        {
            return GetText(sInput, sRegex, "Body");
        }

        public static string GetContent(string sOriContent, string sOtherRemoveReg, string sPageUrl, DataTable dtAntiLink)
        {
            string input = sOriContent;
            input = Regex.Replace(Regex.Replace(input, @"<script[\s\S]*?</script>", "", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase), @"<iframe[^>]*>[\s\S]*?</iframe>", "", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            input = new Regex(@"<input[\s\S]+?>|<form[\s\S]+?>|</form[\s\S]*?>|<select[\s\S]+?>?</select>|<textarea[\s\S]*?>?</textarea>|<file[\s\S]*?>|<noscript>|</noscript>", RegexOptions.IgnoreCase).Replace(input, "");
            foreach (string str2 in sOtherRemoveReg.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                input = Replace(input, str2, "", 0);
            }
            input = smethod_1("<img[\\s\\S]+?src\\s*=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", "src", input, sPageUrl);
            DataRow[] rowArray = dtAntiLink.Select("Domain='" + GetDomain(sPageUrl) + "'");
            if (rowArray.Length > 0)
            {
                foreach (DataRow row in rowArray)
                {
                    if (Convert.ToInt32(row["Type"]) != 1)
                    {
                        input = input.Replace(row["imgUrl"].ToString(), "http://stat.580k.com/t.asp?url=" + row["imgUrl"].ToString());
                    }
                    else
                    {
                        input = input.Replace(row["imgUrl"].ToString(), "http://stat.580k.com/t.asp?url=");
                    }
                }
            }
            input = smethod_1("<a[^>]+href\\s*=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>", "href", input, sPageUrl);
            input = smethod_1("<link[^>]+href\\s*=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>", "href", input, sPageUrl);
            input = smethod_1("background\\s*=\\s*(?:'(?<img>[^']+)'|\"(?<img>[^\"]+)\"|(?<img>[^>\\s]+))", "img", input, sPageUrl);
            input = smethod_1(@"background-image\s*:\s*url\s*\x28(?<img>[^\x29]+)\x29", "img", input, sPageUrl);
            input = smethod_1("<param\\s[^>]+\"movie\"[^>]+value\\s*=\\s*\"(?<flash>[^\">]+\\x2eswf)\"[^>]*>", "flash", input, sPageUrl);
            if (IsXml(input))
            {
                input = smethod_1("<\\x3fxml-stylesheet\\s+[^\\x3f>]+href=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\")\\s*[^\\x3f>]*\\x3f>", "href", input, sPageUrl);
            }
            return input;
        }

        public static DateTime GetCreateDate(string sInput, string sRegex)
        {
            DateTime now = DateTime.Now;
            Match match = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sInput);
            if (match.Success)
            {
                try
                {
                    int year = int.Parse(match.Groups["Year"].Value);
                    int month = int.Parse(match.Groups["Month"].Value);
                    int day = int.Parse(match.Groups["Day"].Value);
                    int hour = now.Hour;
                    int minute = now.Minute;
                    string s = match.Groups["Hour"].Value;
                    string str2 = match.Groups["Mintue"].Value;
                    if (s != "")
                    {
                        hour = int.Parse(s);
                    }
                    if (str2 != "")
                    {
                        minute = int.Parse(str2);
                    }
                    now = new DateTime(year, month, day, hour, minute, 0);
                }
                catch
                {
                }
            }
            return now;
        }

        public static string GetDomain(string sInput)
        {
            return GetText(sInput, @"http(s)?://([\w-]+\.)+(\w){2,}", 0);
        }

        public static string GetHtml(string sInput)
        {
            return Replace(sInput, "(?<Head>[^<]+)<", "", "Head");
        }

        public static string GetImgSrc(string sInput)
        {
            return GetText(sInput, "<img[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", "src");
        }

        public static List<string> GetImgTag(string sInput)
        {
            return GetList(sInput, "<img[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", "");
        }

        public static string GetKeyWord(string sInput)
        {
            List<string> list = Split(sInput, @"(,|，|\+|＋|。|;|；|：|:|“)|”|、|_|\(|（|\)|）", 2);
            List<string> list2 = new List<string>();
            foreach (string str2 in list)
            {
                Regex regex = new Regex("[a-zA-z]+", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                MatchCollection matchs = regex.Matches(str2);
                string input = str2;
                foreach (Match match in matchs)
                {
                    if (match.Value.ToString().Length > 2)
                    {
                        list2.Add(match.Value.ToString());
                    }
                    input = input.Replace(match.Value.ToString(), ",");
                }
                regex = new Regex(",{1}", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                matchs = regex.Matches(input);
                foreach (string str4 in regex.Split(input))
                {
                    if (str4.Trim().Length > 2)
                    {
                        list2.Add(str4);
                    }
                }
            }
            string str = "";
            for (int i = 0; i < (list2.Count - 1); i++)
            {
                for (int j = i + 1; j < list2.Count; j++)
                {
                    if (list2[i] == list2[j])
                    {
                        list2[j] = "";
                    }
                }
            }
            foreach (string str2 in list2)
            {
                if (str2.Length > 2)
                {
                    str = str + str2 + ",";
                }
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            else
            {
                str = sInput;
            }
            if (str.Length > 0x63)
            {
                str = str.Substring(0, 0x63);
            }
            return str;
        }

        public static string GetLink(string sInput)
        {
            return GetText(sInput, "<a[^>]+href=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>", "href");
        }

        public static List<string> GetLinks(string sInput)
        {
            return GetList(sInput, "<a[^>]+href=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>", "href");
        }

        public static List<string> GetList(string sInput, string sRegex, int iGroupIndex)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sInput))
            {
                if (iGroupIndex > 0)
                {
                    list.Add(match.Groups[iGroupIndex].Value);
                }
                else
                {
                    list.Add(match.Value);
                }
            }
            return list;
        }

        public static List<string> GetList(string sInput, string sRegex, string sGroupName)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sInput))
            {
                if (sGroupName != "")
                {
                    list.Add(match.Groups[sGroupName].Value);
                }
                else
                {
                    list.Add(match.Value);
                }
            }
            return list;
        }

        public static List<string> GetPageLinks(string sInput, string sRegex)
        {
            return GetList(sInput, sRegex, "href");
        }

        public static string GetSource(string sInput, string sRegex)
        {
            string str = CString.ClearTag(GetText(sInput, sRegex, "Source"));
            if (str.Length > 0x63)
            {
                str = str.Substring(0, 0x63);
            }
            return str;
        }

        public static string GetText(string sInput, string sRegex, int iGroupIndex)
        {
            Match match = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sInput);
            string str = "";
            if (!match.Success)
            {
                return str;
            }
            if (iGroupIndex > 0)
            {
                return match.Groups[iGroupIndex].Value;
            }
            return match.Value;
        }

        public static string GetText(string sInput, string sRegex, string sGroupName)
        {
            Match match = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sInput);
            string str = "";
            if (!match.Success)
            {
                return str;
            }
            if (sGroupName != "")
            {
                return match.Groups[sGroupName].Value;
            }
            return match.Value;
        }

        public static string GetTitle(string sInput)
        {
            return GetText(sInput, @"<Title[^>]*>(?<Title>[\s\S]{10,})</Title>", "Title");
        }

        public static string GetTitle(string sInput, string sRegex)
        {
            string str = CString.ClearTag(GetText(sInput, sRegex, "Title"));
            if (str.Length > 0x63)
            {
                str = str.Substring(0, 0x63);
            }
            return str;
        }

        public static string GetUrl(string sInput, string sRelativeUrl)
        {
            string sOrg = smethod_0(sInput);
            if (sRelativeUrl.ToLower().StartsWith("http") || sRelativeUrl.ToLower().StartsWith("https"))
            {
                return sRelativeUrl.Trim();
            }
            if (sRelativeUrl.StartsWith("/"))
            {
                return (GetDomain(sInput) + sRelativeUrl);
            }
            if (sRelativeUrl.StartsWith("../"))
            {
                sOrg = sOrg.Substring(0, sOrg.Length - 1);
                while (sRelativeUrl.IndexOf("../") >= 0)
                {
                    string preStrByLast = CString.GetPreStrByLast(sOrg, "/");
                    if (preStrByLast.Length > 6)
                    {
                        sOrg = preStrByLast;
                    }
                    sRelativeUrl = sRelativeUrl.Substring(3);
                }
                return (sOrg + "/" + sRelativeUrl.Trim());
            }
            if (sRelativeUrl.StartsWith("./"))
            {
                return (sOrg + sRelativeUrl.Trim().Substring(2));
            }
            if (sRelativeUrl.Trim() != "")
            {
                return (sOrg + sRelativeUrl.Trim());
            }
            sRelativeUrl = sOrg;
            return "";
        }

        public static bool IsMatch(string sInput, string sRegex)
        {
            if (sRegex == "")
            {
                return true;
            }
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return regex.Match(sInput).Success;
        }

        public static bool IsXml(string sFormartted)
        {
            Regex regex = new Regex(@"<\x3fxml\s+", RegexOptions.IgnoreCase);
            return (regex.Matches(sFormartted).Count > 0);
        }

        public static string Replace(string sInput, string sRegex, string sReplace, int iGroupIndex)
        {
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sInput))
            {
                if (iGroupIndex > 0)
                {
                    sInput = sInput.Replace(match.Groups[iGroupIndex].Value, sReplace);
                }
                else
                {
                    sInput = sInput.Replace(match.Value, sReplace);
                }
            }
            return sInput;
        }

        public static string Replace(string sInput, string sRegex, string sReplace, string sGroupName)
        {
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sInput))
            {
                if (sGroupName != "")
                {
                    sInput = sInput.Replace(match.Groups[sGroupName].Value, sReplace);
                }
                else
                {
                    sInput = sInput.Replace(match.Value, sReplace);
                }
            }
            return sInput;
        }

        private static string smethod_0(string string_0)
        {
            string str = string_0.Trim().ToLower();
            string str2 = "http://";
            if (str.IndexOf("https://") != -1)
            {
                str2 = "https://";
                str = str.Replace("https://", "");
            }
            else
            {
                str = str.Replace("http://", "");
            }
            int startIndex = str.LastIndexOf("/");
            if (startIndex == -1)
            {
                str = str + "/";
            }
            else if (startIndex != (str.Length - 1))
            {
                if (str.Substring(startIndex).IndexOf(".") != -1)
                {
                    str = str.Substring(0, startIndex + 1);
                }
                else
                {
                    str = str + "/";
                }
            }
            return (str2 + str);
        }

        private static string smethod_1(string string_0, string string_1, string string_2, string string_3)
        {
            MatchCollection matchs = new Regex(string_0, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Matches(string_2);
            string oldValue = "";
            string str2 = "";
            string newValue = "";
            foreach (Match match in matchs)
            {
                oldValue = match.Value;
                str2 = match.Groups[string_1].Value;
                newValue = oldValue.Replace(str2, GetUrl(string_3, str2));
                string_2 = string_2.Replace(oldValue, newValue);
            }
            return string_2;
        }

        public static List<string> Split(string sInput, string sRegex, int iStrLen)
        {
            string[] strArray = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Split(sInput);
            List<string> list = new List<string>();
            list.Clear();
            foreach (string str in strArray)
            {
                if (str.Trim().Length >= iStrLen)
                {
                    list.Add(str.Trim());
                }
            }
            return list;
        }
    }
}

