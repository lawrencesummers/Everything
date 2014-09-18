namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class HttpWebHelper
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        protected string certFilepath;
        protected bool certificatedMode;
        protected CookieContainer cookieContainer;
        protected CredentialCache credentialCache;
        protected readonly int DEFAULT_BUFFER_SIZE;
        protected HttpWebRequest httpRequest;
        protected HttpWebResponse httpResponse;
        private static readonly int int_0 = 300;
        public OnGetPostReady OnGetPostReadyHandler;
        public OnGetPostReady OnGetResponseReadyHandler;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;
        public WebProxy webProxySrv;

        public HttpWebHelper()
        {
            this.certificatedMode = false;
            this.certFilepath = string.Empty;
            this.OnGetPostReadyHandler = null;
            this.OnGetResponseReadyHandler = null;
            this.DEFAULT_BUFFER_SIZE = 0x1000;
            this.webProxySrv = null;
            this.cookieContainer = new CookieContainer();
            ServicePointManager.DefaultConnectionLimit = int_0;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0x2710;
        }

        public HttpWebHelper(bool cred) : this()
        {
            this.certificatedMode = cred;
        }

        public HttpWebHelper(WebProxy wp) : this()
        {
            this.webProxySrv = wp;
        }

        public HttpWebHelper(bool cred, WebProxy wp) : this()
        {
            this.certificatedMode = cred;
            this.webProxySrv = wp;
        }

        public HttpWebHelper(bool cred, string certFilepath) : this(cred)
        {
            this.certFilepath = certFilepath;
        }

        public HttpWebHelper(bool cred, WebProxy wp, string certFilepath) : this(cred, wp)
        {
            this.certFilepath = certFilepath;
        }

        public HttpWebHelper(string uri, string method, string username, string password) : this(true)
        {
            this.credentialCache = new CredentialCache();
            this.credentialCache.Add(new Uri(uri), method, new NetworkCredential(username, password));
        }

        public bool AbortHttpRequest()
        {
            if (null != this.httpRequest)
            {
                this.httpRequest.Abort();
            }
            return (this.CheckGotoRecv && this.DoBetIsGotoRecv);
        }

        public void AddCookieInCookieContainer(Cookie cookie)
        {
            this.cookieContainer.Add(cookie);
        }

        public string BuildRequestArguments(Dictionary<string, string> dicArguments)
        {
            return this.BuildRequestArguments(dicArguments, null);
        }

        public string BuildRequestArguments(Dictionary<string, string> dicArguments, Encoding coding)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            if (0 == dicArguments.Count)
            {
                return str;
            }
            using (Dictionary<string, string>.Enumerator enumerator = dicArguments.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, string> current = enumerator.Current;
                }
            }
            str = builder.ToString();
            return str.Substring(0, str.Length - 1);
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public string DoPostWrapper(string url, Dictionary<string, string> dicArguments, string method, Encoding coding)
        {
            return this.DoPostWrapper(url, dicArguments, method, coding, null, null);
        }

        public string DoPostWrapper(string url, string data, string method, Encoding coding)
        {
            return this.DoPostWrapper(url, data, method, coding, null, null);
        }

        public string DoPostWrapper(string url, Dictionary<string, string> dicArguments, string method, Encoding coding, CookieCollection cc)
        {
            return this.DoPostWrapper(url, dicArguments, method, coding, cc, null);
        }

        public string DoPostWrapper(string url, Dictionary<string, string> dicArguments, string method, Encoding coding, string referUrl)
        {
            return this.DoPostWrapper(url, dicArguments, method, coding, null, referUrl);
        }

        public string DoPostWrapper(string url, string data, string method, Encoding coding, CookieCollection cc)
        {
            return this.DoPostWrapper(url, data, method, coding, cc, null);
        }

        public string DoPostWrapper(string url, string data, string method, Encoding coding, string referUrl)
        {
            return this.DoPostWrapper(url, data, method, coding, null, referUrl);
        }

        public string DoPostWrapper(string url, Dictionary<string, string> dicArguments, string method, Encoding coding, CookieCollection cc, string referUrl)
        {
            string data = this.BuildRequestArguments(dicArguments);
            return this.DoPostWrapper(url, data, method, coding, cc, referUrl);
        }

        public string DoPostWrapper(string url, string data, string method, Encoding coding, CookieCollection cc, string referUrl)
        {
            string str = string.Empty;
            MemoryStream stream = this.GetMemoryStream(url, data, method, coding, cc, referUrl);
            StreamReader reader = new StreamReader(stream, coding);
            str = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return str;
        }

        public MemoryStream DownloadStream(string url, string method)
        {
            return this.SimpleGetMemoryStream(url, method, "*/*");
        }

        public MemoryStream DownloadStream(string url, string method, CookieCollection cc)
        {
            return this.SimpleGetMemoryStream(url, method, "*/*", cc);
        }

        public List<Cookie> GetAllCookies()
        {
            new WebClient();
            return this.GetAllCookies(this.cookieContainer);
        }

        public List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> list = new List<Cookie>();
            Hashtable hashtable = (Hashtable) cc.GetType().InvokeMember("m_domainTable", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, cc, new object[0]);
            foreach (object obj2 in hashtable.Values)
            {
                SortedList list2 = (SortedList) obj2.GetType().InvokeMember("m_list", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, obj2, new object[0]);
                foreach (CookieCollection cookies in list2.Values)
                {
                    foreach (Cookie cookie in cookies)
                    {
                        list.Add(cookie);
                    }
                }
            }
            return list;
        }

        public string GetCookieSting()
        {
            if (0 == this.cookieContainer.Count)
            {
                return string.Empty;
            }
            List<Cookie> allCookies = this.GetAllCookies(this.cookieContainer);
            StringBuilder builder = new StringBuilder();
            foreach (Cookie cookie in allCookies)
            {
                builder.AppendFormat("{0};{1};{2};{3};{4};{5}\r/n", new object[] { cookie.Domain, cookie.Name, cookie.Path, cookie.Port, cookie.Secure.ToString(), cookie.Value });
            }
            return builder.ToString();
        }

        public string GetCookieValue(string key, string domain)
        {
            if (0 == this.cookieContainer.Count)
            {
                return string.Empty;
            }
            return this.cookieContainer.GetCookies(new Uri(domain))[key].Value;
        }

        public MemoryStream GetMemoryStream(string url, string method, CookieCollection cc, string referUrl, string httpAccept)
        {
            Exception exception;
            MemoryStream stream = new MemoryStream();
            try
            {
                int expressionStack_BA_0;
                this.method_2(url, method, cc, referUrl, "*/*");
                this.httpRequest.Accept = httpAccept;
                this.httpResponse = (HttpWebResponse) this.httpRequest.GetResponse();
                if (!this.httpRequest.HaveResponse)
                {
                    this.httpResponse.Close();
                    this.httpRequest.Abort();
                    return stream;
                }
                this.method_3();
                if (null != this.OnGetResponseReadyHandler)
                {
                    try
                    {
                        this.OnGetResponseReadyHandler(this.httpRequest);
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        this.LastAccessError = true;
                        LogHelper.WriteLog(exception);
                    }
                }
                this.DoBetIsGotoRecv = true;
                Stream responseStream = this.httpResponse.GetResponseStream();
                if (responseStream != null)
                {
                    expressionStack_BA_0 = (int) !responseStream.CanRead;
                }
                else
                {
                    expressionStack_BA_0 = 1;
                }
                if (expressionStack_BA_0 != 0)
                {
                    goto Label_010F;
                }
                BinaryReader reader = new BinaryReader(responseStream);
                byte[] buffer = reader.ReadBytes(this.DEFAULT_BUFFER_SIZE);
                while (buffer == null)
                {
                    int expressionStack_D6_0 = 0;
                Label_00D6:
                    if (expressionStack_D6_0 == 0)
                    {
                        goto Label_0108;
                    }
                    stream.Write(buffer, 0, buffer.Length);
                    buffer = reader.ReadBytes(this.DEFAULT_BUFFER_SIZE);
                    continue;
                Label_00F6:
                    expressionStack_D6_0 = (int) (buffer.Length != 0);
                    goto Label_00D6;
                }
                goto Label_00F6;
            Label_0108:
                reader.Close();
            Label_010F:
                if (this.httpResponse.Headers["Set-Cookie"] != null)
                {
                    this.CurSetCookie = this.httpResponse.Headers["Set-Cookie"].ToString();
                }
                CookieCollection cookies = this.httpResponse.Cookies;
                this.httpResponse.Close();
                if (null != responseStream)
                {
                    responseStream.Close();
                }
                stream.Seek(0L, SeekOrigin.Begin);
            }
            catch (Exception exception4)
            {
                exception = exception4;
                this.LastAccessError = true;
                LogHelper.WriteLog(exception);
                if (null != this.httpRequest)
                {
                    this.httpRequest.Abort();
                }
            }
            return stream;
        }

        public MemoryStream GetMemoryStream(string url, string method, CookieCollection cc, string referUrl, string httpAccept, out CookieCollection ccOut)
        {
            Exception exception;
            MemoryStream stream = new MemoryStream();
            CookieCollection cookies = new CookieCollection();
            try
            {
                int expressionStack_C7_0;
                this.method_2(url, method, cc, referUrl, "*/*");
                this.httpRequest.Accept = httpAccept;
                this.httpResponse = (HttpWebResponse) this.httpRequest.GetResponse();
                if (!this.httpRequest.HaveResponse)
                {
                    ccOut = null;
                    this.httpResponse.Close();
                    this.httpRequest.Abort();
                    return stream;
                }
                this.method_3();
                if (null != this.OnGetResponseReadyHandler)
                {
                    try
                    {
                        this.OnGetResponseReadyHandler(this.httpRequest);
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        this.LastAccessError = true;
                        LogHelper.WriteLog(exception);
                    }
                }
                this.DoBetIsGotoRecv = true;
                Stream responseStream = this.httpResponse.GetResponseStream();
                if (responseStream != null)
                {
                    expressionStack_C7_0 = (int) !responseStream.CanRead;
                }
                else
                {
                    expressionStack_C7_0 = 1;
                }
                if (expressionStack_C7_0 != 0)
                {
                    goto Label_011D;
                }
                BinaryReader reader = new BinaryReader(responseStream);
                byte[] buffer = reader.ReadBytes(this.DEFAULT_BUFFER_SIZE);
                while (buffer == null)
                {
                    int expressionStack_E4_0 = 0;
                Label_00E4:
                    if (expressionStack_E4_0 == 0)
                    {
                        goto Label_0116;
                    }
                    stream.Write(buffer, 0, buffer.Length);
                    buffer = reader.ReadBytes(this.DEFAULT_BUFFER_SIZE);
                    continue;
                Label_0104:
                    expressionStack_E4_0 = (int) (buffer.Length != 0);
                    goto Label_00E4;
                }
                goto Label_0104;
            Label_0116:
                reader.Close();
            Label_011D:
                if (this.httpResponse.Headers["Set-Cookie"] != null)
                {
                    this.CurSetCookie = this.httpResponse.Headers["Set-Cookie"].ToString();
                }
                cookies = this.httpResponse.Cookies;
                this.httpResponse.Close();
                if (null != responseStream)
                {
                    responseStream.Close();
                }
                stream.Seek(0L, SeekOrigin.Begin);
            }
            catch (Exception exception4)
            {
                exception = exception4;
                this.LastAccessError = true;
                LogHelper.WriteLog(exception);
                if (null != this.httpRequest)
                {
                    this.httpRequest.Abort();
                }
            }
            ccOut = cookies;
            return stream;
        }

        public MemoryStream GetMemoryStream(string url, string data, string method, Encoding coding, CookieCollection cc, string referUrl)
        {
            Exception exception;
            MemoryStream stream = new MemoryStream();
            try
            {
                int expressionStack_DF_0;
                this.method_2(url, method, cc, referUrl, "text/html");
                byte[] bytes = coding.GetBytes(data);
                Stream requestStream = this.httpRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
                requestStream.Close();
                this.httpResponse = (HttpWebResponse) this.httpRequest.GetResponse();
                if (!this.httpRequest.HaveResponse)
                {
                    this.httpResponse.Close();
                    this.httpRequest.Abort();
                    return stream;
                }
                this.method_3();
                if (null != this.OnGetResponseReadyHandler)
                {
                    try
                    {
                        this.OnGetResponseReadyHandler(this.httpRequest);
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        this.LastAccessError = true;
                        LogHelper.WriteLog(exception);
                    }
                }
                this.DoBetIsGotoRecv = true;
                Stream responseStream = this.httpResponse.GetResponseStream();
                if (responseStream != null)
                {
                    expressionStack_DF_0 = (int) !responseStream.CanRead;
                }
                else
                {
                    expressionStack_DF_0 = 1;
                }
                if (expressionStack_DF_0 != 0)
                {
                    goto Label_0135;
                }
                BinaryReader reader = new BinaryReader(responseStream);
                byte[] buffer = reader.ReadBytes(this.DEFAULT_BUFFER_SIZE);
                while (buffer == null)
                {
                    int expressionStack_FC_0 = 0;
                Label_00FC:
                    if (expressionStack_FC_0 == 0)
                    {
                        goto Label_012E;
                    }
                    stream.Write(buffer, 0, buffer.Length);
                    buffer = reader.ReadBytes(this.DEFAULT_BUFFER_SIZE);
                    continue;
                Label_011C:
                    expressionStack_FC_0 = (int) (buffer.Length != 0);
                    goto Label_00FC;
                }
                goto Label_011C;
            Label_012E:
                reader.Close();
            Label_0135:
                if (this.httpResponse.Headers["Set-Cookie"] != null)
                {
                    this.CurSetCookie = this.httpResponse.Headers["Set-Cookie"].ToString();
                }
                this.httpResponse.Close();
                if (null != responseStream)
                {
                    responseStream.Close();
                }
                stream.Seek(0L, SeekOrigin.Begin);
            }
            catch (Exception exception4)
            {
                exception = exception4;
                this.LastAccessError = true;
                LogHelper.WriteLog(exception);
                if (null != this.httpRequest)
                {
                    this.httpRequest.Abort();
                }
            }
            return stream;
        }

        private void method_0(string string_4, string string_5, CookieCollection cookieCollection_0, string string_6, bool bool_3, DecompressionMethods decompressionMethods_0, string string_7)
        {
            this.method_1(string_4, string_5, cookieCollection_0, string_6, bool_3, decompressionMethods_0);
            this.httpRequest.Accept = string_7;
        }

        private void method_1(string string_4, string string_5, CookieCollection cookieCollection_0, string string_6, bool bool_3, DecompressionMethods decompressionMethods_0)
        {
            this.httpRequest = (HttpWebRequest) WebRequest.Create(string_4);
            this.httpRequest.UnsafeAuthenticatedConnectionSharing = true;
            this.httpRequest.ServicePoint.ConnectionLimit = int_0;
            if (null != this.webProxySrv)
            {
                this.httpRequest.Proxy = this.webProxySrv;
            }
            if (this.certificatedMode && string_4.ToLower().Substring(0, 5).Equals("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                if (null == this.credentialCache)
                {
                    this.httpRequest.UseDefaultCredentials = true;
                }
                else
                {
                    this.httpRequest.Credentials = this.credentialCache;
                }
                if (!string.IsNullOrEmpty(this.certFilepath))
                {
                    this.httpRequest.ClientCertificates.Add(X509Certificate.CreateFromCertFile(this.certFilepath));
                }
            }
            this.httpRequest.CookieContainer = this.cookieContainer;
            if (!string.IsNullOrEmpty(string_6))
            {
                this.httpRequest.Referer = string_6;
            }
            this.httpRequest.AutomaticDecompression = decompressionMethods_0;
            this.httpRequest.ServicePoint.Expect100Continue = false;
            this.httpRequest.ServicePoint.UseNagleAlgorithm = false;
            this.httpRequest.ContentType = "application/x-www-form-urlencoded";
            this.httpRequest.Method = string_5;
            this.httpRequest.Timeout = 0x7530;
            this.httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E)";
            this.httpRequest.Headers.Add("Accept-Language", "zh-CN");
            this.httpRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            if (bool_3)
            {
                this.httpRequest.Headers.Add("Cache-Control", "no-cache");
            }
            if (null != cookieCollection_0)
            {
                this.httpRequest.CookieContainer.Add(cookieCollection_0);
            }
            if (null != this.OnGetPostReadyHandler)
            {
                try
                {
                    this.OnGetPostReadyHandler(this.httpRequest);
                }
                catch (Exception exception)
                {
                    this.LastAccessError = true;
                    LogHelper.WriteLog(exception);
                }
            }
        }

        private void method_2(string string_4, string string_5, CookieCollection cookieCollection_0, string string_6, string string_7)
        {
            this.method_0(string_4, string_5, cookieCollection_0, string_6, false, DecompressionMethods.Deflate | DecompressionMethods.GZip, string_7);
        }

        private void method_3()
        {
            this.cookieContainer = this.httpRequest.CookieContainer;
            this.CurrentUrl = this.httpRequest.Address.OriginalString;
            this.CurrentLocation = this.httpResponse.Headers["Location"];
        }

        public void SetCookieContainer(CookieContainer cc)
        {
            this.cookieContainer = cc;
        }

        public string SimpleDoPostWrapper(string url, string method)
        {
            return this.SimpleDoPostWrapper(url, method, null, null, null);
        }

        public string SimpleDoPostWrapper(string url, string method, CookieCollection cc)
        {
            return this.SimpleDoPostWrapper(url, method, null, cc, null);
        }

        public string SimpleDoPostWrapper(string url, string method, string referUrl)
        {
            return this.SimpleDoPostWrapper(url, method, null, null, referUrl);
        }

        public string SimpleDoPostWrapper(string url, string method, Encoding coding, CookieCollection cc, string referUrl)
        {
            string str = string.Empty;
            StreamReader reader = null;
            MemoryStream stream = null;
            if (null == coding)
            {
                stream = this.GetMemoryStream(url, method, cc, referUrl, "text/html");
                reader = new StreamReader(stream);
            }
            else
            {
                stream = this.GetMemoryStream(url, method, cc, referUrl, "text/html");
                reader = new StreamReader(stream, coding);
            }
            str = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return str;
        }

        public string SimpleDoPostWrapper(string url, string method, Encoding coding, CookieCollection cc, string referUrl, out CookieCollection ccOut)
        {
            CookieCollection cookies = new CookieCollection();
            string str = string.Empty;
            StreamReader reader = null;
            MemoryStream stream = null;
            if (null == coding)
            {
                stream = this.GetMemoryStream(url, method, cc, referUrl, "text/html", out cookies);
                reader = new StreamReader(stream);
            }
            else
            {
                stream = this.GetMemoryStream(url, method, cc, referUrl, "text/html", out cookies);
                reader = new StreamReader(stream, coding);
            }
            str = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            ccOut = cookies;
            return str;
        }

        public MemoryStream SimpleGetMemoryStream(string url, string method)
        {
            return this.GetMemoryStream(url, method, null, null, "text/html");
        }

        public MemoryStream SimpleGetMemoryStream(string url, string method, CookieCollection cc)
        {
            return this.GetMemoryStream(url, method, cc, null, "text/html");
        }

        public MemoryStream SimpleGetMemoryStream(string url, string method, string httpAccept)
        {
            return this.GetMemoryStream(url, method, null, null, httpAccept);
        }

        public MemoryStream SimpleGetMemoryStream(string url, string method, string httpAccept, CookieCollection cc)
        {
            return this.GetMemoryStream(url, method, cc, null, httpAccept);
        }

        public MemoryStream SimpleGetMemoryStream(string url, string data, string method, Encoding coding)
        {
            return this.GetMemoryStream(url, data, method, coding, null, null);
        }

        public MemoryStream SimpleGetMemoryStream(string url, string data, string method, Encoding coding, string referUrl)
        {
            return this.GetMemoryStream(url, data, method, coding, null, referUrl);
        }

        public bool CheckGotoRecv
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public string CurrentLocation
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            private set
            {
                this.string_1 = value;
            }
        }

        public string CurrentUrl
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            private set
            {
                this.string_0 = value;
            }
        }

        public string CurSetCookie
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public string CurSetCookie2
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            set
            {
                this.string_3 = value;
            }
        }

        public bool DoBetIsGotoRecv
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }

        public bool LastAccessError
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            private set
            {
                this.bool_2 = value;
            }
        }
    }
}

