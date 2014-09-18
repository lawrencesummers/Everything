namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class HttpHelper
    {
        private System.Net.CookieContainer cookieContainer_0;
        private System.Text.Encoding encoding_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private string string_0;
        private string string_1;
        private string string_2;

        public HttpHelper()
        {
            this.string_0 = "application/x-www-form-urlencoded";
            this.string_1 = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            this.string_2 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            this.encoding_0 = System.Text.Encoding.GetEncoding("utf-8");
            this.int_0 = 0x3e8;
            this.int_1 = 3;
            this.int_2 = 0;
            this.cookieContainer_0 = new System.Net.CookieContainer();
        }

        public HttpHelper(System.Net.CookieContainer cc)
        {
            this.string_0 = "application/x-www-form-urlencoded";
            this.string_1 = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            this.string_2 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            this.encoding_0 = System.Text.Encoding.GetEncoding("utf-8");
            this.int_0 = 0x3e8;
            this.int_1 = 3;
            this.int_2 = 0;
            this.cookieContainer_0 = cc;
        }

        public HttpHelper(string contentType, string accept, string userAgent)
        {
            this.string_0 = "application/x-www-form-urlencoded";
            this.string_1 = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            this.string_2 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            this.encoding_0 = System.Text.Encoding.GetEncoding("utf-8");
            this.int_0 = 0x3e8;
            this.int_1 = 3;
            this.int_2 = 0;
            this.string_0 = contentType;
            this.string_1 = accept;
            this.string_2 = userAgent;
        }

        public HttpHelper(System.Net.CookieContainer cc, string contentType, string accept, string userAgent)
        {
            this.string_0 = "application/x-www-form-urlencoded";
            this.string_1 = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            this.string_2 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            this.encoding_0 = System.Text.Encoding.GetEncoding("utf-8");
            this.int_0 = 0x3e8;
            this.int_1 = 3;
            this.int_2 = 0;
            this.cookieContainer_0 = cc;
            this.string_0 = contentType;
            this.string_1 = accept;
            this.string_2 = userAgent;
        }

        public CookieCollection GetCookieCollection(string cookieString)
        {
            CookieCollection cookies = new CookieCollection();
            Regex regex = new Regex("([^;,]+)=([^;,]+);Domain=([^;,]+);Path=([^;,]+)", RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(cookieString))
            {
                Cookie cookie = new Cookie(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[3].Value);
                cookies.Add(cookie);
            }
            return cookies;
        }

        public string GetEncoding(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(url);
                request.Timeout = 0x4e20;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse) request.GetResponse();
                if ((response.StatusCode == HttpStatusCode.OK) && (response.ContentLength < 0x100000))
                {
                    if ((response.ContentEncoding != null) && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
                    }
                    string input = reader.ReadToEnd();
                    Regex regex = new Regex("charset\\b\\s*=\\s*(?<charset>[^\"]*)");
                    if (regex.IsMatch(input))
                    {
                        return regex.Match(input).Groups["charset"].Value;
                    }
                    if (response.CharacterSet != string.Empty)
                    {
                        return response.CharacterSet;
                    }
                    return System.Text.Encoding.Default.BodyName;
                }
            }
            catch
            {
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                {
                    reader.Close();
                }
                if (request != null)
                {
                    request = null;
                }
            }
            return System.Text.Encoding.Default.BodyName;
        }

        public string GetHiddenKeyValue(string html, string key)
        {
            string str = "";
            Match match = new Regex(string.Format("<input\\s*type=\"hidden\".*?name=\"{0}\".*?\\s*value=[\"|'](?<value>.*?)[\"|'^/]", key), RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase).Match(html);
            if (match.Success)
            {
                str = match.Groups[1].Value;
            }
            return str;
        }

        public string GetHtml(string url)
        {
            return this.GetHtml(url, this.cookieContainer_0, url);
        }

        public string GetHtml(string url, string reference)
        {
            return this.GetHtml(url, this.cookieContainer_0, reference);
        }

        public string GetHtml(string url, System.Net.CookieContainer cookieContainer, string reference)
        {
            this.int_2++;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.ContentType = this.string_0;
                request.Referer = reference;
                request.Accept = this.string_1;
                request.UserAgent = this.string_2;
                request.Method = "GET";
                Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, this.encoding_0);
                string str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                this.int_2 = 0;
                return str;
            }
            catch (Exception)
            {
                if (this.int_2 <= this.int_1)
                {
                    this.GetHtml(url, cookieContainer, reference);
                }
                this.int_2 = 0;
                return string.Empty;
            }
        }

        public string GetHtml(string url, string postData, bool isPost)
        {
            string referer = "";
            return this.GetHtml(url, this.cookieContainer_0, postData, isPost, referer);
        }

        public string GetHtml(string url, System.Net.CookieContainer cookieContainer, string postData, bool isPost)
        {
            return this.GetHtml(url, cookieContainer, postData, isPost, url);
        }

        public string GetHtml(string url, System.Net.CookieContainer cookieContainer, string postData, bool isPost, string referer)
        {
            if (string.IsNullOrEmpty(postData))
            {
                new CookieCollection();
                return this.GetHtml(url, cookieContainer, referer);
            }
            this.int_2++;
            try
            {
                HttpWebResponse response;
                byte[] bytes = System.Text.Encoding.Default.GetBytes(postData);
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.ContentType = this.string_0;
                request.Referer = referer;
                request.Accept = this.string_1;
                request.UserAgent = this.string_2;
                request.Method = isPost ? "POST" : "GET";
                request.ContentLength = bytes.Length;
                request.AllowAutoRedirect = false;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                try
                {
                    response = (HttpWebResponse) request.GetResponse();
                }
                catch (WebException exception)
                {
                    response = (HttpWebResponse) exception.Response;
                }
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, this.encoding_0);
                string str2 = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                this.int_2 = 0;
                return str2;
            }
            catch (Exception)
            {
                if (this.int_2 <= this.int_1)
                {
                    this.GetHtml(url, cookieContainer, postData, isPost);
                }
                this.int_2 = 0;
                return string.Empty;
            }
        }

        public Stream GetStream(string url, System.Net.CookieContainer cookieContainer)
        {
            return this.GetStream(url, cookieContainer, url);
        }

        public Stream GetStream(string url, System.Net.CookieContainer cookieContainer, string reference)
        {
            this.int_2++;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.ContentType = this.string_0;
                request.Referer = reference;
                request.Accept = this.string_1;
                request.UserAgent = this.string_2;
                request.Method = "GET";
                Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
                this.int_2 = 0;
                return responseStream;
            }
            catch (Exception)
            {
                if (this.int_2 <= this.int_1)
                {
                    new CookieCollection();
                    this.GetHtml(url, cookieContainer, url);
                }
                this.int_2 = 0;
                return null;
            }
        }

        public int GetUrlError(string url)
        {
            int num = 200;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(new Uri(url));
                ServicePointManager.Expect100Continue = false;
                ((HttpWebResponse) request.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    return 0x191;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 0x194;
                }
            }
            catch
            {
                num = 0x191;
            }
            return num;
        }

        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }

        public string RemoveHtml(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public string Accept
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public System.Net.CookieContainer CookieContainer
        {
            get
            {
                return this.cookieContainer_0;
            }
        }

        public System.Text.Encoding Encoding
        {
            get
            {
                return this.encoding_0;
            }
            set
            {
                this.encoding_0 = value;
            }
        }

        public int MaxTry
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int NetworkDelay
        {
            get
            {
                Random random = new Random();
                return (random.Next(this.int_0 / 0x3e8, (this.int_0 / 0x3e8) * 2) * 0x3e8);
            }
            set
            {
                this.int_0 = value;
            }
        }

        public string UserAgent
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }
    }
}

