namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class HttpWebRequestHelper
    {
        private CookieContainer SjLxrlTowj;

        public HttpWebRequestHelper()
        {
            this.SjLxrlTowj = new CookieContainer();
        }

        public HttpWebRequestHelper(CookieContainer cookie)
        {
            this.SjLxrlTowj = cookie;
        }

        public string Get(string uri)
        {
            return this.Get(uri, uri, "gb2312");
        }

        public string Get(string uri, string refererUri)
        {
            return this.Get(uri, refererUri, "gb2312");
        }

        public string Get(string uri, string refererUri, string encodingName)
        {
            return this.Get(uri, refererUri, encodingName, null);
        }

        public string Get(string uri, string refererUri, string encodingName, WebProxy webproxy)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.ContentType = "text/html;charset=" + encodingName;
            request.Method = "Get";
            request.CookieContainer = this.SjLxrlTowj;
            if (null != webproxy)
            {
                request.Proxy = webproxy;
                if (null != webproxy.Credentials)
                {
                    request.UseDefaultCredentials = true;
                }
            }
            if (!string.IsNullOrEmpty(refererUri))
            {
                request.Referer = refererUri;
            }
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(encodingName)))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public byte[] GetBytes(string uri)
        {
            return this.GetBytes(uri, uri);
        }

        public byte[] GetBytes(string uri, string refererUri)
        {
            return this.GetBytes(uri, refererUri, null);
        }

        public byte[] GetBytes(string uri, string refererUri, WebProxy webproxy)
        {
            byte[] buffer2;
            byte[] buffer = new byte[0x400];
            using (Stream stream = this.GetStream(uri, refererUri, webproxy))
            {
                using (MemoryStream stream2 = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        stream2.Write(buffer, 0, count);
                    }
                    while (count != 0);
                    buffer2 = stream2.ToArray();
                }
            }
            return buffer2;
        }

        public Stream GetStream(string uri)
        {
            return this.GetStream(uri, uri);
        }

        public Stream GetStream(string uri, string refererUri)
        {
            return this.GetStream(uri, refererUri, null);
        }

        public Stream GetStream(string uri, string refererUri, WebProxy webproxy)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.Method = "GET";
            request.CookieContainer = this.SjLxrlTowj;
            if (null != webproxy)
            {
                request.Proxy = webproxy;
                if (null != webproxy.Credentials)
                {
                    request.UseDefaultCredentials = true;
                }
            }
            if (!string.IsNullOrEmpty(refererUri))
            {
                request.Referer = refererUri;
            }
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                return response.GetResponseStream();
            }
        }

        public string Post(string uri, string postData)
        {
            return this.Post(uri, uri, postData, "gb2312");
        }

        public string Post(string uri, string refererUri, string postData)
        {
            return this.Post(uri, refererUri, postData, "gb2312");
        }

        public string Post(string uri, string refererUri, string postData, string encodingName)
        {
            return this.Post(uri, refererUri, postData, encodingName, null);
        }

        public string Post(string uri, string refererUri, string postData, string encodingName, WebProxy webproxy)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.Accept = "*/*";
            request.Headers.Add("Accept-Language", "zh-cn");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("UA-CPU", "x86");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";
            request.Headers.Add("Cache-Control", "no-cache");
            request.CookieContainer = this.SjLxrlTowj;
            request.Method = "POST";
            if (!string.IsNullOrEmpty(refererUri))
            {
                request.Referer = refererUri;
            }
            if (null != webproxy)
            {
                request.Proxy = webproxy;
                if (null != webproxy.Credentials)
                {
                    request.UseDefaultCredentials = true;
                }
            }
            Encoding encoding = Encoding.GetEncoding(encodingName);
            byte[] bytes = encoding.GetBytes(postData);
            request.ContentLength = bytes.Length;
            StringBuilder builder = new StringBuilder();
            if (this.SjLxrlTowj != null)
            {
                foreach (Cookie cookie in this.SjLxrlTowj.GetCookies(request.RequestUri))
                {
                    builder.Append(cookie + ";");
                }
            }
            if (builder.Length > 0)
            {
                DateTime time = new DateTime(0x7b2, 1, 1);
                long num = DateTime.UtcNow.Ticks - time.Ticks;
                num /= 0x2710;
                request.Headers.Add("Cookie", "cookLastGetMsgTime=" + num.ToString() + "; " + builder.ToString());
            }
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                using (Stream stream2 = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream2, encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}

