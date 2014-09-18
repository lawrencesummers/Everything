namespace RDIFramework.Utilities
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;

    public class HttpHelper
    {
        public string cookie = "";
        public Encoding encoding = Encoding.Default;
        private HttpWebResponse httpWebResponse_0 = null;
        public bool isToLower = true;
        public HttpWebRequest request = null;
        private StreamReader streamReader_0 = null;
        private string string_0 = "String Error";

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public string GetHtml(HttpItem objhttpItem)
        {
            this.method_2(objhttpItem);
            return this.method_1(objhttpItem.Postdata);
        }

        public static string GetUrl(string URL)
        {
            if (!(URL.Contains("http://") || URL.Contains("https://")))
            {
                URL = "http://" + URL;
            }
            return URL;
        }

        private string method_0(string string_1)
        {
            HttpWebResponse response = (HttpWebResponse) this.request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            response.Close();
            return str;
        }

        private string method_1(string string_1)
        {
            try
            {
                this.request.AllowAutoRedirect = true;
                if (!(string.IsNullOrEmpty(string_1) || !this.request.Method.Trim().ToLower().Contains("post")))
                {
                    byte[] bytes = this.encoding.GetBytes(string_1);
                    this.request.ContentLength = bytes.Length;
                    this.request.GetRequestStream().Write(bytes, 0, bytes.Length);
                }
                using (this.httpWebResponse_0 = (HttpWebResponse) this.request.GetResponse())
                {
                    try
                    {
                        this.cookie = this.httpWebResponse_0.Headers["set-cookie"].ToString();
                    }
                    catch (Exception)
                    {
                    }
                    if (this.encoding == null)
                    {
                        MemoryStream stream = new MemoryStream();
                        if ((this.httpWebResponse_0.ContentEncoding != null) && this.httpWebResponse_0.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            stream = smethod_0(this.httpWebResponse_0.GetResponseStream());
                        }
                        else
                        {
                            stream = smethod_0(this.httpWebResponse_0.GetResponseStream());
                        }
                        byte[] buffer2 = stream.ToArray();
                        stream.Close();
                        stream.Dispose();
                        System.Text.RegularExpressions.Match match = Regex.Match(Encoding.Default.GetString(buffer2, 0, buffer2.Length), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                        string name = (match.Groups.Count > 2) ? match.Groups[2].Value : string.Empty;
                        name = name.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(";", string.Empty);
                        if (name.Length > 0)
                        {
                            name = name.ToLower().Replace("iso-8859-1", "gbk");
                            this.encoding = Encoding.GetEncoding(name);
                        }
                        else if (this.httpWebResponse_0.CharacterSet.ToLower().Trim() == "iso-8859-1")
                        {
                            this.encoding = Encoding.GetEncoding("gbk");
                        }
                        else if (string.IsNullOrEmpty(this.httpWebResponse_0.CharacterSet.Trim()))
                        {
                            this.encoding = Encoding.UTF8;
                        }
                        else
                        {
                            this.encoding = Encoding.GetEncoding(this.httpWebResponse_0.CharacterSet);
                        }
                        this.string_0 = this.encoding.GetString(buffer2);
                    }
                    else
                    {
                        StreamReader reader2;
                        if ((this.httpWebResponse_0.ContentEncoding != null) && this.httpWebResponse_0.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            using (reader2 = this.streamReader_0 = new StreamReader(new GZipStream(this.httpWebResponse_0.GetResponseStream(), CompressionMode.Decompress), this.encoding))
                            {
                                this.string_0 = this.streamReader_0.ReadToEnd();
                                goto Label_0352;
                            }
                        }
                        using (reader2 = this.streamReader_0 = new StreamReader(this.httpWebResponse_0.GetResponseStream(), this.encoding))
                        {
                            this.string_0 = this.streamReader_0.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException exception)
            {
                this.string_0 = string.Empty;
                this.httpWebResponse_0 = (HttpWebResponse) exception.Response;
            }
        Label_0352:
            if (this.isToLower)
            {
                this.string_0 = this.string_0.ToLower();
            }
            return this.string_0;
        }

        private void method_2(HttpItem httpItem_0)
        {
            if (!string.IsNullOrEmpty(httpItem_0.CerPath))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                this.request = (HttpWebRequest) WebRequest.Create(GetUrl(httpItem_0.URL));
                X509Certificate certificate = new X509Certificate(httpItem_0.CerPath);
                this.request.ClientCertificates.Add(certificate);
            }
            else
            {
                this.request = (HttpWebRequest) WebRequest.Create(GetUrl(httpItem_0.URL));
            }
            this.request.Method = httpItem_0.Method;
            this.request.Accept = httpItem_0.Accept;
            this.request.ContentType = httpItem_0.ContentType;
            this.request.UserAgent = httpItem_0.UserAgent;
            if (string.IsNullOrEmpty(httpItem_0.Encoding.Trim()))
            {
                this.encoding = null;
            }
            else
            {
                this.encoding = Encoding.GetEncoding(httpItem_0.Encoding);
            }
            this.request.Headers[HttpRequestHeader.Cookie] = httpItem_0.Cookie;
            this.request.Referer = httpItem_0.Referer;
        }

        public void SetWebProxy(string userName, string passWord, string ip)
        {
            WebProxy proxy = new WebProxy(ip, false) {
                Credentials = new NetworkCredential(userName, passWord)
            };
            this.request.Proxy = proxy;
            this.request.Credentials = CredentialCache.DefaultNetworkCredentials;
        }

        private static MemoryStream smethod_0(Stream stream_0)
        {
            MemoryStream stream = new MemoryStream();
            int count = 0x100;
            byte[] buffer = new byte[0x100];
            for (int i = stream_0.Read(buffer, 0, 0x100); i > 0; i = stream_0.Read(buffer, 0, count))
            {
                stream.Write(buffer, 0, i);
            }
            return stream;
        }
    }
}

