namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using WHC.OrderWater.Commons;

    [Serializable]
    public class FileServerManage
    {
        private CredentialCache credentialCache_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public FileServerManage()
        {
            this.credentialCache_0 = null;
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.string_0 = ConfigurationSettings.AppSettings["FileServer"];
            this.string_1 = ConfigurationSettings.AppSettings["FileServerUser"];
            this.string_2 = ConfigurationSettings.AppSettings["FileServerPass"];
            if ((this.string_0.Length > 0) && (this.string_0.Substring(this.string_0.Length - 1, 1) != "/"))
            {
                this.string_0 = this.string_0 + "/";
            }
            this.credentialCache_0 = new CredentialCache();
            this.credentialCache_0.Add(new Uri(this.string_0), "NTLM", new NetworkCredential(this.string_1, this.string_2));
        }

        public FileServerManage(string url, string username, string password)
        {
            this.credentialCache_0 = null;
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            if ((url.Length > 0) && (url.Substring(url.Length - 1, 1) != "/"))
            {
                url = url + "/";
            }
            this.string_0 = url;
            this.string_1 = username;
            this.string_2 = password;
            this.credentialCache_0 = new CredentialCache();
            this.credentialCache_0.Add(new Uri(this.string_0), "NTLM", new NetworkCredential(this.string_1, this.string_2));
        }

        public bool DeleteFile(string fileName)
        {
            if (!(!string.IsNullOrEmpty(fileName) && this.IsFileExist(fileName)))
            {
                return false;
            }
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.method_0(this.string_0, fileName));
            request.Credentials = this.credentialCache_0;
            request.Method = "DELETE";
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return true;
        }

        public bool IsFileExist(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            bool flag2 = false;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.method_0(this.string_0, fileName));
            request.Credentials = this.credentialCache_0;
            request.Method = "Get";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    flag2 = true;
                }
            }
            catch (WebException)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return flag2;
        }

        private string method_0(string string_3, string string_4)
        {
            string str = string_3;
            if (string_3.EndsWith("/"))
            {
                return (str + string_4.TrimStart(new char[] { '/' }));
            }
            if (string_4.StartsWith("/"))
            {
                return (str + string_4);
            }
            return (str + string.Format("/{0}", string_4));
        }

        public string ReadFile(string newFileName, string oldFileName)
        {
            if (string.IsNullOrEmpty(newFileName) || string.IsNullOrEmpty(oldFileName))
            {
                return string.Empty;
            }
            if (!this.IsFileExist(newFileName))
            {
                return "文件不存在";
            }
            try
            {
                string address = this.method_0(this.string_0, newFileName);
                WebClient client = new WebClient {
                    Credentials = this.credentialCache_0
                };
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.BinaryWrite(client.DownloadData(address));
                response.Charset = "GB2312";
                response.ContentEncoding = Encoding.UTF8;
                oldFileName = HttpUtility.UrlEncode(oldFileName, Encoding.UTF8);
                string str3 = "attachment;filename=" + oldFileName;
                response.AppendHeader("Content-Disposition", str3);
                response.Flush();
                response.End();
                return string.Empty;
            }
            catch (WebException exception)
            {
                return exception.Message.ToString();
            }
        }

        public byte[] ReadFileBytes(string fileName)
        {
            string address = this.method_0(this.string_0, fileName);
            WebClient client = new WebClient {
                Credentials = this.credentialCache_0
            };
            return client.DownloadData(address);
        }

        public bool UploadFile(Stream inputStream, string fileName)
        {
            if ((inputStream == null) || string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            WebClient client = new WebClient {
                Credentials = this.credentialCache_0
            };
            int length = (int) inputStream.Length;
            byte[] buffer = new byte[length];
            inputStream.Read(buffer, 0, length);
            try
            {
                string address = this.method_0(this.string_0, fileName);
                using (Stream stream = client.OpenWrite(address, "PUT"))
                {
                    stream.Write(buffer, 0, length);
                }
            }
            catch (WebException exception)
            {
                LogTextHelper.Error(exception);
                return false;
            }
            return true;
        }

        public bool UploadFile(string fileUrl, string fileName)
        {
            if (string.IsNullOrEmpty(fileUrl) || string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            WebClient client = new WebClient {
                Credentials = this.credentialCache_0
            };
            int count = 0;
            byte[] buffer = null;
            using (Stream stream = client.OpenRead(fileUrl))
            {
                count = (int) stream.Length;
                buffer = new byte[count];
                stream.Read(buffer, 0, count);
            }
            try
            {
                string address = this.method_0(this.string_0, fileName);
                using (Stream stream2 = client.OpenWrite(address, "PUT"))
                {
                    stream2.Write(buffer, 0, count);
                }
            }
            catch (WebException)
            {
                return false;
            }
            return true;
        }
    }
}

