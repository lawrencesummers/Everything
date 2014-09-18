namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class CSocket
    {
        public static string[] DealWithFrame(string url, string content)
        {
            string str = "<frame\\s+[^>]*src\\s*=\\s*(?:\"(?<src>[^\"]+)\"|'(?<src>[^']+)'|(?<src>[^\\s>\"']+))[^>]*>";
            return smethod_2(str, url, content);
        }

        public static string[] DealWithIFrame(string url, string content)
        {
            string str = "<iframe\\s+[^>]*src\\s*=\\s*(?:\"(?<src>[^\"]+)\"|'(?<src>[^']+)'|(?<src>[^\\s>\"']+))[^>]*>";
            return smethod_2(str, url, content);
        }

        public static string GetHtmlByUrl(string sUrl)
        {
            return GetHtmlByUrl(sUrl, "auto");
        }

        public static string GetHtmlByUrl(string sUrl, string sCoding)
        {
            return GetHtmlByUrl(ref sUrl, sCoding);
        }

        public static string GetHtmlByUrl(ref string sUrl, string sCoding)
        {
            string input = "";
            try
            {
                HttpWebResponse response = smethod_0(sUrl);
                if (response == null)
                {
                    return input;
                }
                sUrl = response.ResponseUri.AbsoluteUri;
                Stream responseStream = response.GetResponseStream();
                byte[] bytes = smethod_1(responseStream);
                responseStream.Close();
                responseStream.Dispose();
                string name = "";
                if (((sCoding == null) || (sCoding == "")) || (sCoding.ToLower() == "auto"))
                {
                    string responseHeader = response.GetResponseHeader("Content-Type");
                    response.Close();
                    string pattern = @"[\s\S]*charset=(?<charset>[\S]*)";
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    Match match = regex.Match(responseHeader);
                    name = (match.Captures.Count != 0) ? match.Result("${charset}") : "";
                    switch (name)
                    {
                        case "-8":
                            name = "utf-8";
                            break;

                        case "":
                            input = Encoding.GetEncoding("gb2312").GetString(bytes);
                            pattern = "(<meta[^>]*charset=(?<charset>[^>'\"]*)[\\s\\S]*?>)|(xml[^>]+encoding=(\"|')*(?<charset>[^>'\"]*)[\\s\\S]*?>)";
                            match = new Regex(pattern, RegexOptions.IgnoreCase).Match(input);
                            if (match.Captures.Count == 0)
                            {
                                return input;
                            }
                            name = match.Result("${charset}");
                            break;
                    }
                }
                else
                {
                    response.Close();
                    name = sCoding.ToLower();
                }
                try
                {
                    input = Encoding.GetEncoding(name).GetString(bytes);
                }
                catch (ArgumentException)
                {
                    input = Encoding.GetEncoding("gb2312").GetString(bytes);
                }
            }
            catch
            {
                input = "";
            }
            return input;
        }

        public static List<KeyValuePair<int, string>> GetHtmlByUrlList(List<KeyValuePair<int, string>> listUrl, string sCoding)
        {
            Exception exception;
            string str2;
            int num = int.Parse(ConfigurationManager.AppSettings["SocketTimeOut"]);
            StringBuilder builder = new StringBuilder();
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            int offset = 0;
            Socket socket = null;
            IPHostEntry hostEntry = null;
            try
            {
                KeyValuePair<int, string> pair = listUrl[0];
                Uri uri = new Uri(pair.Value.ToString());
                try
                {
                    hostEntry = Dns.GetHostEntry(uri.Host);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    throw exception;
                }
                IPAddress address = hostEntry.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(address, uri.Port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) {
                    SendTimeout = num,
                    ReceiveTimeout = num
                };
                try
                {
                    socket.Connect(remoteEP);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    throw exception;
                }
                using (List<KeyValuePair<int, string>>.Enumerator enumerator = listUrl.GetEnumerator())
                {
                    int num4;
                Label_00A8:
                    if (!enumerator.MoveNext())
                    {
                        return list;
                    }
                    KeyValuePair<int, string> current = enumerator.Current;
                    uri = new Uri(current.Value);
                    string s = string.Concat(new object[] { "GET ", HttpUtility.UrlDecode(uri.PathAndQuery), " HTTP/1.1\r\nAccept: image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/msword, application/vnd.ms-powerpoint, */*\r\nAccept-Language:en-us\r\nAccept-Encoding:gb2312, deflate\r\nUser-Agent: Mozilla/4.0\r\nHost: ", uri.Host, "\r\n\r\n", '\0' });
                    byte[] bytes = Encoding.GetEncoding(sCoding).GetBytes(s);
                    offset = socket.Send(bytes);
                    if (offset == 0)
                    {
                        goto Label_023F;
                    }
                    byte[] buffer = new byte[0x800];
                    byte num3 = Convert.ToByte('\x007f');
                Label_0156:
                    num4 = 0;
                    try
                    {
                        offset = socket.Receive(buffer, buffer.Length - 1, SocketFlags.None);
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        string message = exception.Message;
                        offset = -1;
                    }
                    while (offset > 0)
                    {
                        if (buffer[offset - 1] > num3)
                        {
                            for (int i = offset - 1; i >= 0; i--)
                            {
                                if (buffer[i] <= num3)
                                {
                                    break;
                                }
                                num4++;
                            }
                            if ((num4 % 2) == 1)
                            {
                                num4 = socket.Receive(buffer, offset, 1, SocketFlags.None);
                                if (num4 < 0)
                                {
                                    break;
                                }
                                offset += num4;
                            }
                        }
                        else
                        {
                            buffer[offset] = 0;
                        }
                        str2 = Encoding.GetEncoding(sCoding).GetString(buffer, 0, offset);
                        builder.Append(str2);
                        if (offset > 0)
                        {
                            goto Label_0156;
                        }
                    Label_020D:
                        list.Add(new KeyValuePair<int, string>(current.Key, builder.ToString()));
                        builder = null;
                        builder = new StringBuilder();
                        goto Label_00A8;
                    }
                    goto Label_020D;
                Label_023F:
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return list;
                }
            }
            catch (Exception exception4)
            {
                exception = exception4;
                str2 = exception.Message;
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch
                {
                }
            }
            finally
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch
                {
                }
            }
            return list;
        }

        public static string GetHttpHead(string sUrl)
        {
            string str = "";
            Uri requestUri = new Uri(sUrl);
            try
            {
                WebHeaderCollection headers = WebRequest.Create(requestUri).GetResponse().Headers;
                foreach (string str2 in headers.AllKeys)
                {
                    string str3 = str;
                    str = str3 + str2 + ":" + headers[str2] + "\r\n";
                }
            }
            catch
            {
            }
            return str;
        }

        public static PageType GetPageType(string sUrl, ref string sHtml)
        {
            PageType hTML = PageType.HTML;
            string pattern = "<link\\s+[^>]*((type=\"application/rss\\+xml\")|(type=application/rss\\+xml))[^>]*>";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(sHtml);
            if (match.Captures.Count != 0)
            {
                string str2 = "href=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))";
                match = new Regex(str2, RegexOptions.IgnoreCase).Match(match.Captures[0].Value);
                if (match.Captures.Count > 0)
                {
                    string url = CRegex.GetUrl(sUrl, match.Groups["href"].Value);
                    sHtml = GetHtmlByUrl(url);
                    hTML = PageType.RSS;
                }
                return hTML;
            }
            regex = new Regex(@"<rss\s+[^>]*>", RegexOptions.IgnoreCase);
            if (regex.Match(sHtml).Captures.Count > 0)
            {
                hTML = PageType.RSS;
            }
            return hTML;
        }

        private static HttpWebResponse smethod_0(string string_0)
        {
            int num = 0x2710;
            bool flag = false;
            bool flag2 = false;
            Uri requestUri = new Uri(string_0);
            while (true)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
                    request.MaximumResponseHeadersLength = -1;
                    request.ReadWriteTimeout = 0x1d4c0;
                    request.Timeout = num;
                    request.MaximumAutomaticRedirections = 50;
                    request.MaximumResponseHeadersLength = 5;
                    request.AllowAutoRedirect = true;
                    if (flag)
                    {
                        request.CookieContainer = new CookieContainer();
                    }
                    request.UserAgent = "Mozilla/6.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                    return (HttpWebResponse) request.GetResponse();
                }
                catch (WebException)
                {
                    if (!flag2)
                    {
                        flag2 = true;
                        flag = true;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        private static byte[] smethod_1(Stream stream_0)
        {
            ArrayList list = new ArrayList();
            try
            {
                byte[] buffer = new byte[0x1000];
                for (int i = stream_0.Read(buffer, 0, 0x1000); i > 0; i = stream_0.Read(buffer, 0, 0x1000))
                {
                    for (int j = 0; j < i; j++)
                    {
                        list.Add(buffer[j]);
                    }
                }
            }
            catch
            {
            }
            return (byte[]) list.ToArray(Type.GetType("System.Byte"));
        }

        private static string[] smethod_2(string string_0, string string_1, string string_2)
        {
            ArrayList list = new ArrayList();
            Regex regex = new Regex(string_0, RegexOptions.IgnoreCase);
            for (Match match = regex.Match(string_2); match.Success; match = match.NextMatch())
            {
                list.Add(CRegex.GetUrl(string_1, match.Groups["src"].Value));
            }
            return (string[]) list.ToArray(Type.GetType("System.String"));
        }

        public enum PageType
        {
            HTML,
            RSS
        }
    }
}

