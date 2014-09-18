namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;

    public class CText
    {
        public static string GetBody(string sContent)
        {
            sContent = new Regex(@"[\s\S]*?<\bbody\b[^>]*>", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase).Replace(sContent, "");
            sContent = new Regex(@"</\bbody\b[^>]*>\s*</html>", RegexOptions.RightToLeft | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase).Replace(sContent, "");
            return sContent;
        }

        public static DateTime GetCreateDate(string sContent, string sRegex)
        {
            DateTime now = DateTime.Now;
            Match match = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sContent);
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

        public static string GetDomainUrl(string sUrl)
        {
            try
            {
                Uri uri = new Uri(sUrl);
                return (uri.Scheme + "://" + uri.Authority);
            }
            catch
            {
                return sUrl;
            }
        }

        public static List<string> GetKeys(string sOri)
        {
            if (sOri.Trim().Length == 0)
            {
                return null;
            }
            string[] strArray = sOri.Split(new char[] { ',', '，', '\\', '/', '、' });
            List<string> list2 = new List<string>();
            foreach (string str in strArray)
            {
                if (str.Length != 0)
                {
                    list2.Add(str);
                }
            }
            return list2;
        }

        public static string GetLink(string sContent)
        {
            string str = "";
            Regex regex = new Regex("<a\\s+[^>]*href\\s*=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Match match = new Regex(@"(href|onclick)=[^>]+javascript[^>]+(('(?<href>[\w\d/-]+\.[^']*)')|(&quot;(?<href>[\w\d/-]+\.[^;]*)&quot;))[^>]*>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sContent);
            if (match.Success)
            {
                return match.Groups["href"].Value;
            }
            Match match2 = regex.Match(sContent);
            if (match2.Success)
            {
                str = RemoveByReg(HttpUtility.HtmlDecode(match2.Groups["href"].Value), ";[^?&]*|javascript:.*");
            }
            return str;
        }

        public static Dictionary<string, string> GetLinks(string sContent, string sUrl)
        {
            Dictionary<string, string> lisDes = new Dictionary<string, string>();
            return GetLinks(sContent, sUrl, ref lisDes);
        }

        public static Dictionary<string, string> GetLinks(string sContent, string sUrl, ref Dictionary<string, string> lisDes)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            smethod_0(sContent, sUrl, ref dictionary);
            string str = CRegex.GetDomain(sUrl).ToLower();
            MatchCollection matchs = new Regex("<script[^>]+src\\s*=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Matches(sContent);
            for (int i = matchs.Count - 1; i >= 0; i--)
            {
                Match match = matchs[i];
                string url = CRegex.GetUrl(sUrl, match.Groups["src"].Value);
                if (str.CompareTo(CRegex.GetDomain(url).ToLower()) == 0)
                {
                    string htmlByUrl = CSocket.GetHtmlByUrl(url);
                    if (htmlByUrl.Length != 0)
                    {
                        smethod_0(htmlByUrl, url, ref dictionary);
                    }
                }
            }
            if (dictionary.Count == 0)
            {
                return GetLinksFromRss(sContent, sUrl, ref lisDes);
            }
            return dictionary;
        }

        public static Dictionary<string, string> GetLinksByKey(Dictionary<string, string> listA, List<string> listKey)
        {
            if (listKey == null)
            {
                return listA;
            }
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            string pattern = "";
            foreach (string str2 in listKey)
            {
                pattern = pattern + @"([\s\S]*" + smethod_1(str2) + @"[\s\S]*)|";
            }
            pattern = (pattern != "") ? pattern.Substring(0, pattern.Length - 1) : @"[\s\S]+";
            Regex regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (KeyValuePair<string, string> pair in listA)
            {
                if (regex.Match(pair.Value).Success && !dictionary2.ContainsKey(pair.Key))
                {
                    dictionary2.Add(pair.Key, pair.Value);
                }
            }
            return dictionary2;
        }

        [Obsolete("已过时的方法。")]
        public static List<string> GetLinksByKey(string sContent, List<string> listKey)
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            string pattern = "";
            Regex regex = new Regex(@"<a\s+[^>]*href\s*=\s*[^>]+>[\s\S]*?</a>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sContent))
            {
                if (RemoveByReg(GetLink(match.Value), "^http.*/$").Length > 0)
                {
                    list2.Add(match.Value);
                }
            }
            foreach (string str3 in listKey)
            {
                pattern = pattern + @"([\s\S]*" + str3 + @"[\s\S]*)|";
            }
            if (pattern != "")
            {
                pattern = pattern.Substring(0, pattern.Length - 1);
            }
            if (pattern == "")
            {
                pattern = @"[\s\S]+";
            }
            Regex regex2 = new Regex(pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            regex = new Regex(@"<a\s+[^>]+>([\s\S]{5,})?</a>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (string str3 in list2)
            {
                Match match2 = regex.Match(str3);
                if (match2.Success)
                {
                    string str = RemoveByReg(CString.ClearTag(match2.Groups[1].Value.Trim()), @"更多|登录|添加|推荐|收藏夹|加盟|关于|订阅|阅读器|我的|有限|免费|公司|more|RSS|about|\.");
                    if ((CString.GetLength(str) > 8) && regex2.Match(str).Success)
                    {
                        list.Add(str3);
                    }
                }
            }
            if (list.Count == 0)
            {
                return GetLinksByKeyFromRss(sContent, listKey);
            }
            return list;
        }

        [Obsolete("已过时的方法。")]
        public static List<string> GetLinksByKeyFromRss(string sContent, List<string> listKey)
        {
            XmlNodeList list2;
            int num;
            XmlNamespaceManager manager;
            List<string> list = new List<string>();
            string pattern = "";
            foreach (string str2 in listKey)
            {
                pattern = pattern + @"([\s\S]*" + str2 + @"[\s\S]*)|";
            }
            if (pattern != "")
            {
                pattern = pattern.Substring(0, pattern.Length - 1);
            }
            if (pattern == "")
            {
                pattern = @"[\s\S]+";
            }
            new Regex(pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            XmlDocument document = new XmlDocument();
            try
            {
                document.LoadXml(sContent.Trim());
                list2 = document.SelectNodes("/rss/channel/item");
                if (list2.Count > 0)
                {
                    for (num = 0; num < list2.Count; num++)
                    {
                        list.Add("<a href=\"" + list2[num].SelectSingleNode("link").InnerText + "\">" + list2[num].SelectSingleNode("title").InnerText + "</a>");
                    }
                    return list;
                }
            }
            catch
            {
            }
            try
            {
                manager = new XmlNamespaceManager(document.NameTable);
                manager.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                manager.AddNamespace("rss", "http://purl.org/rss/1.0/");
                list2 = document.SelectNodes("/rdf:RDF//rss:item", manager);
                if (list2.Count > 0)
                {
                    for (num = 0; num < list2.Count; num++)
                    {
                        list.Add("<a href=\"" + list2[num].SelectSingleNode("rss:link", manager).InnerText + "\">" + list2[num].SelectSingleNode("rss:title", manager).InnerText + "</a>");
                    }
                    return list;
                }
            }
            catch
            {
            }
            try
            {
                manager = new XmlNamespaceManager(document.NameTable);
                manager.AddNamespace("atom", "http://purl.org/atom/ns#");
                list2 = document.SelectNodes("/atom:feed/atom:entry", manager);
                if (list2.Count > 0)
                {
                    for (num = 0; num < list2.Count; num++)
                    {
                        list.Add("<a href=\"" + list2[num].SelectSingleNode("atom:link", manager).Attributes["href"].InnerText + "\">" + list2[num].SelectSingleNode("atom:title", manager).InnerText + "</a>");
                    }
                    return list;
                }
            }
            catch
            {
            }
            return list;
        }

        public static Dictionary<string, string> GetLinksFromRss(string sContent, string sUrl)
        {
            Dictionary<string, string> lisDes = new Dictionary<string, string>();
            return GetLinksFromRss(sContent, sUrl, ref lisDes);
        }

        public static Dictionary<string, string> GetLinksFromRss(string sContent, string sUrl, ref Dictionary<string, string> lisDes)
        {
            Dictionary<string, string> dictionary2;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XmlDocument document = new XmlDocument();
            try
            {
                int num;
                string urlByRelative;
                document.LoadXml(sContent.Trim());
                XmlNodeList list = document.SelectNodes("/rss/channel/item");
                if (list.Count <= 0)
                {
                    try
                    {
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
                        nsmgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                        nsmgr.AddNamespace("rss", "http://purl.org/rss/1.0/");
                        list = document.SelectNodes("/rdf:RDF//rss:item", nsmgr);
                        if (list.Count <= 0)
                        {
                            try
                            {
                                nsmgr = new XmlNamespaceManager(document.NameTable);
                                nsmgr.AddNamespace("atom", "http://purl.org/atom/ns#");
                                list = document.SelectNodes("/atom:feed/atom:entry", nsmgr);
                                if (list.Count <= 0)
                                {
                                    return dictionary;
                                }
                                num = list.Count - 1;
                                while (true)
                                {
                                    if (num < 0)
                                    {
                                        break;
                                    }
                                    try
                                    {
                                        urlByRelative = GetUrlByRelative(sUrl, list[num].SelectSingleNode("atom:link", nsmgr).Attributes["href"].InnerText);
                                        dictionary.Add(urlByRelative, list[num].SelectSingleNode("atom:title", nsmgr).InnerText);
                                        lisDes.Add(urlByRelative, list[num].SelectSingleNode("atom:content", nsmgr).InnerText);
                                    }
                                    catch
                                    {
                                    }
                                    num--;
                                }
                                dictionary2 = dictionary;
                            }
                            catch
                            {
                            }
                            return dictionary2;
                        }
                        num = list.Count - 1;
                        while (true)
                        {
                            if (num < 0)
                            {
                                break;
                            }
                            try
                            {
                                urlByRelative = GetUrlByRelative(sUrl, list[num].SelectSingleNode("rss:link", nsmgr).InnerText);
                                dictionary.Add(urlByRelative, list[num].SelectSingleNode("rss:title", nsmgr).InnerText);
                                lisDes.Add(urlByRelative, list[num].SelectSingleNode("rss:description", nsmgr).InnerText);
                            }
                            catch
                            {
                            }
                            num--;
                        }
                        dictionary2 = dictionary;
                    }
                    catch
                    {
                    }
                    return dictionary2;
                }
                num = list.Count - 1;
                while (true)
                {
                    if (num < 0)
                    {
                        break;
                    }
                    try
                    {
                        urlByRelative = GetUrlByRelative(sUrl, list[num].SelectSingleNode("link").InnerText);
                        dictionary.Add(urlByRelative, list[num].SelectSingleNode("title").InnerText);
                        lisDes.Add(urlByRelative, list[num].SelectSingleNode("description").InnerText);
                    }
                    catch
                    {
                    }
                    num--;
                }
                dictionary2 = dictionary;
            }
            catch
            {
            }
            return dictionary2;
        }

        public static List<string> GetListByReg(string sContent, string sRegex)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sContent))
            {
                list.Add(match.Groups["href"].Value);
            }
            return list;
        }

        public static string GetTextByLink(string sContent)
        {
            Regex regex = new Regex(@"<a(?:\s+[^>]*)?>([\s\S]*)?</a>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Regex regex2 = new Regex("(href|onclick)=[^>]+mailto[^>]+@[^>]+>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (!regex2.Match(sContent).Success)
            {
                Match match2 = regex.Match(sContent);
                if (match2.Success)
                {
                    return match2.Groups[1].Value;
                }
            }
            return "";
        }

        public static string GetTextByReg(string sContent, string sRegex)
        {
            Match match = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sContent);
            string sOrg = "";
            if (match.Success)
            {
                sOrg = match.Groups[0].Value;
            }
            while (sOrg.EndsWith("_"))
            {
                sOrg = CString.RemoveEndWith(sOrg, "_");
            }
            return sOrg;
        }

        public static string GetTextByReg(string sContent, string sRegex, string sGroupName)
        {
            Match match = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(sContent);
            string str = "";
            if (match.Success)
            {
                str = match.Groups[sGroupName].Value;
            }
            return str;
        }

        public static string GetTitleFromRss(string sContent)
        {
            string innerText = "";
            XmlDocument document = new XmlDocument();
            try
            {
                document.LoadXml(sContent.Trim());
                innerText = document.SelectSingleNode("/rss/channel/title").InnerText;
            }
            catch
            {
            }
            return innerText;
        }

        public static string GetTxtFromHtml(string sHtml)
        {
            string sRegex = @"<head[^>]*>[\s\S]*?</head>";
            string sContent = RemoveByReg(sHtml, sRegex);
            sRegex = @"(<script[^>]*>[\s\S]*?</script>)|(<IFRAME[^>]*>[\s\S]*?</IFRAME>)|(<style[^>]*>[\s\S]*?</style>|<title[^>]*>[\s\S]*?</title>|<meta[^>]*>|<option[^>]*>[\s\S]*?</option>)";
            sContent = RemoveByReg(sContent, sRegex);
            sRegex = @"(&nbsp;)|([\n\t]+)";
            sContent = RemoveByReg(sContent, sRegex);
            string str3 = @"(<table(\s+[^>]*)*>)|(<td(\s+[^>]*)*>)|(<tr(\s+[^>]*)*>)|(<p(\s+[^>]*)*>)|(<div(\s+[^>]*)*>)|(<ul(\s+[^>]*)*>)|(<li(\s+[^>]*)*>)|</table>|</td>|</tr>|</p>|<br>|</div>|</li>|</ul>|<p />|<br />";
            return RemoveByReg(RemoveByReg(ReplaceByReg(ReplaceByReg(sContent, "", str3), "", @"[\f\n\r\v]+"), @"<a(\s+[^>]*)*>[\s\S]*?</a>"), "<[^>]+>").Replace("\n", "").Replace("\r", "").Trim();
        }

        public static string GetTxtFromHtml2(string sHtml)
        {
            string sRegex = @"<head[^>]*>[\s\S]*?</head>";
            string sContent = RemoveByReg(sHtml, sRegex);
            sRegex = @"(<script[^>]*>[\s\S]*?</script>)|(<IFRAME[^>]*>[\s\S]*?</IFRAME>)|(<style[^>]*>[\s\S]*?</style>|<title[^>]*>[\s\S]*?</title>|<meta[^>]*>|<option[^>]*>[\s\S]*?</option>)";
            sContent = RemoveByReg(sContent, sRegex);
            sRegex = @"(&nbsp;)|([\t]+)";
            sContent = RemoveByReg(sContent, sRegex);
            string str3 = @"(<table(\s+[^>]*)*>)|(<td(\s+[^>]*)*>)|(<tr(\s+[^>]*)*>)|(<p(\s+[^>]*)*>)|(<div(\s+[^>]*)*>)|(<ul(\s+[^>]*)*>)|(<li(\s+[^>]*)*>)|</table>|</td>|</tr>|</p>|<br>|</div>|</li>|</ul>|<p />|<br />";
            return RemoveByReg(RemoveByReg(ReplaceByReg(sContent, "", str3), @"<a(\s+[^>]*)*>[\s\S]*?</a>"), "<[^>]+>").Trim();
        }

        public static string GetUrlByRelative(string sUrl, string sRUrl)
        {
            try
            {
                Uri baseUri = new Uri(sUrl);
                if (!sUrl.EndsWith("/"))
                {
                    int index = baseUri.Segments.Length - 1;
                    if (index > 0)
                    {
                        string str = baseUri.Segments[index];
                        if (str.IndexOf('.') < 1)
                        {
                            baseUri = new Uri(sUrl + "/");
                        }
                    }
                }
                Uri uri2 = new Uri(baseUri, sRUrl);
                return uri2.AbsoluteUri;
            }
            catch
            {
                return sUrl;
            }
        }

        public static bool IsExistsScriptLink(string sHtml)
        {
            Regex regex = new Regex("<script[^>]+src\\s*=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return regex.IsMatch(sHtml);
        }

        public static string RemoveByReg(string sContent, string sRegex)
        {
            Regex regex = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(sContent))
            {
                sContent = sContent.Replace(match.Value, "");
            }
            return sContent;
        }

        public static string ReplaceByReg(string sContent, string sReplace, string sRegex)
        {
            sContent = new Regex(sRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(sContent, sReplace);
            return sContent;
        }

        private static void smethod_0(string string_0, string string_1, ref Dictionary<string, string> dictionary_0)
        {
            Regex regex = new Regex(@"<a\s+[^>]*href\s*=\s*[^>]+>[\s\S]*?</a>", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Regex regex2 = new Regex("\"|'", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection matchs = regex.Matches(string_0);
            for (int i = matchs.Count - 1; i >= 0; i--)
            {
                Match match = matchs[i];
                string sContent = GetLink(match.Value).Trim().Replace("\\\"", "").Replace(@"\'", "");
                if (RemoveByReg(sContent, "^http.*/$").Length >= 2)
                {
                    string str = CString.ClearTag(GetTextByLink(match.Value)).Trim();
                    if ((CString.GetLength(RemoveByReg(str, "首页|下载|中文|English|反馈|讨论区|投诉|建议|联系|关于|about|诚邀|工作|简介|新闻|掠影|风采\r\n|登录|注销|注册|使用|体验|立即|收藏夹|收藏|添加|加入\r\n|更多|more|专题|精选|热卖|热销|推荐|精彩\r\n|加盟|联盟|友情|链接|相关\r\n|订阅|阅读器|RSS\r\n|免责|条款|声明|我的|我们|组织|概况|有限|免费|公司|法律|导航|广告|地图|隐私\r\n|〖|〗|【|】|（|）|［|］|『|』|\\.")) >= 9) && !regex2.IsMatch(str))
                    {
                        sContent = GetUrlByRelative(string_1, sContent);
                        if (sContent.Length > 0x12)
                        {
                            int index = sContent.IndexOf('#');
                            if (index > -1)
                            {
                                sContent = sContent.Substring(0, index);
                            }
                            sContent = sContent.Trim(new char[] { '/', '\\' });
                            string domain = CRegex.GetDomain(sContent);
                            if (!sContent.Equals(domain, StringComparison.OrdinalIgnoreCase) && !(dictionary_0.ContainsKey(sContent) || dictionary_0.ContainsValue(str)))
                            {
                                dictionary_0.Add(sContent, str);
                            }
                        }
                    }
                }
            }
        }

        private static string smethod_1(string string_0)
        {
            string[] strArray2 = new string[] { ".", "$", "^", "{", "[", "(", "|", ")", "*", "+", "?", "#" };
            string str = string_0;
            foreach (string str2 in strArray2)
            {
                str = str.Replace(str2, @"\" + str2);
            }
            return str;
        }

        public static string Split(string sOri)
        {
            Regex regex = new Regex(@"(,{1})|(，{1})|(\+{1})|(＋{1})|(。{1})|(;{1})|(；{1})|(：{1})|(:{1})|(“{1})|(”{1})|(、{1})|(_{1})", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            string[] strArray = regex.Split(sOri);
            List<string> list = new List<string>();
            list.Clear();
            foreach (string str6 in strArray)
            {
                if (str6.Length > 2)
                {
                    regex = new Regex("[a-zA-z]+", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                    MatchCollection matchs = regex.Matches(str6);
                    string input = str6;
                    foreach (Match match in matchs)
                    {
                        if (match.Value.ToString().Length > 2)
                        {
                            list.Add(match.Value.ToString());
                        }
                        input = input.Replace(match.Value.ToString(), ",");
                    }
                    regex = new Regex(",{1}", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                    matchs = regex.Matches(input);
                    foreach (string str3 in regex.Split(input))
                    {
                        if (str3.Trim().Length > 2)
                        {
                            list.Add(str3);
                        }
                    }
                }
            }
            string str = "";
            for (int i = 0; i < (list.Count - 1); i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] == list[j])
                    {
                        list[j] = "";
                    }
                }
            }
            foreach (string str4 in list)
            {
                if (str4.Length > 2)
                {
                    str = str + str4 + ",";
                }
            }
            if (str.Length > 0)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }
    }
}

