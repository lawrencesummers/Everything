namespace WHC.OrderWater.Commons
{
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class EmailHelper
    {
        private ArrayList arrayList_0;
        private ArrayList arrayList_1;
        private ArrayList arrayList_2;
        public string Body;
        private bool bool_0;
        public string Charset;
        private Hashtable hashtable_0;
        private Hashtable hashtable_1;
        private Hashtable hashtable_2;
        private static ILog ilog_0 = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int int_0;
        private int int_1;
        public bool IsHtml;
        private List<string> list_0;
        private NetworkStream networkStream_0;
        public string RecipientName;
        public string ReplyTo;
        public bool ReturnReceipt;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private string string_7;
        private string string_8;
        public string Subject;
        private TcpClient tcpClient_0;
        private string VybEgDbhb6;

        public EmailHelper()
        {
            this.Charset = "GB2312";
            this.ReplyTo = "";
            this.Subject = "";
            this.IsHtml = false;
            this.ReturnReceipt = false;
            this.Body = "";
            this.RecipientName = "";
            this.int_0 = 0x19;
            this.string_1 = "";
            this.string_2 = "";
            this.bool_0 = false;
            this.string_3 = "";
            this.string_4 = "";
            this.int_1 = 10;
            this.arrayList_0 = new ArrayList();
            this.arrayList_1 = new ArrayList();
            this.arrayList_2 = new ArrayList();
            this.string_5 = "Normal";
            this.string_7 = "\r\n";
            this.hashtable_0 = new Hashtable();
            this.hashtable_1 = new Hashtable();
            this.VybEgDbhb6 = "=====000_HuolxPubClass113273537350_=====";
            this.string_8 = "=====001_HuolxPubClass113273537350_=====";
            this.list_0 = new List<string>();
            this.hashtable_2 = new Hashtable();
            this.method_0();
        }

        public EmailHelper(string mailServer, string username, string password) : this(mailServer, username, password, 0x19)
        {
        }

        public EmailHelper(string mailServer, string username, string password, int port)
        {
            this.Charset = "GB2312";
            this.ReplyTo = "";
            this.Subject = "";
            this.IsHtml = false;
            this.ReturnReceipt = false;
            this.Body = "";
            this.RecipientName = "";
            this.int_0 = 0x19;
            this.string_1 = "";
            this.string_2 = "";
            this.bool_0 = false;
            this.string_3 = "";
            this.string_4 = "";
            this.int_1 = 10;
            this.arrayList_0 = new ArrayList();
            this.arrayList_1 = new ArrayList();
            this.arrayList_2 = new ArrayList();
            this.string_5 = "Normal";
            this.string_7 = "\r\n";
            this.hashtable_0 = new Hashtable();
            this.hashtable_1 = new Hashtable();
            this.VybEgDbhb6 = "=====000_HuolxPubClass113273537350_=====";
            this.string_8 = "=====001_HuolxPubClass113273537350_=====";
            this.list_0 = new List<string>();
            this.hashtable_2 = new Hashtable();
            this.MailServer = mailServer;
            this.MailServerUsername = username;
            this.MailServerPassword = password;
            this.MailServerPort = port;
            this.method_0();
        }

        public bool AddAttachment(string path)
        {
            if (File.Exists(path))
            {
                this.list_0.Add(path);
                return true;
            }
            this.string_6 = this.string_6 + "要附加的文件不存在" + this.string_7;
            return false;
        }

        public bool AddRecipient(string[] str)
        {
            return this.method_11(str, this.arrayList_0);
        }

        public bool AddRecipient(string str)
        {
            return this.method_10(str, this.arrayList_0);
        }

        public bool AddRecipientBCC(string[] str)
        {
            return this.method_11(str, this.arrayList_2);
        }

        public bool AddRecipientBCC(string str)
        {
            return this.method_10(str, this.arrayList_2);
        }

        public bool AddRecipientCC(string str)
        {
            return this.method_10(str, this.arrayList_1);
        }

        public bool AddRecipientCC(string[] str)
        {
            return this.method_11(str, this.arrayList_1);
        }

        public void ClearRecipient()
        {
            this.arrayList_0.Clear();
        }

        ~EmailHelper()
        {
            if (this.networkStream_0 != null)
            {
                this.networkStream_0.Close();
            }
            if (this.tcpClient_0 != null)
            {
                this.tcpClient_0.Close();
            }
        }

        private void method_0()
        {
            this.hashtable_0.Add("500", "邮箱地址错误");
            this.hashtable_0.Add("501", "参数格式错误");
            this.hashtable_0.Add("502", "命令不可实现");
            this.hashtable_0.Add("503", "服务器需要SMTP验证");
            this.hashtable_0.Add("504", "命令参数不可实现");
            this.hashtable_0.Add("421", "服务未就绪，关闭传输信道");
            this.hashtable_0.Add("450", "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
            this.hashtable_0.Add("550", "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
            this.hashtable_0.Add("451", "放弃要求的操作；处理过程中出错");
            this.hashtable_0.Add("551", "用户非本地，请尝试<forward-path>");
            this.hashtable_0.Add("452", "系统存储不足，要求的操作未执行");
            this.hashtable_0.Add("552", "过量的存储分配，要求的操作未执行");
            this.hashtable_0.Add("553", "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
            this.hashtable_0.Add("432", "需要一个密码转换");
            this.hashtable_0.Add("534", "认证机制过于简单");
            this.hashtable_0.Add("538", "当前请求的认证机制需要加密");
            this.hashtable_0.Add("454", "临时认证失败");
            this.hashtable_0.Add("530", "需要认证");
            this.hashtable_1.Add("220", "服务就绪");
            this.hashtable_1.Add("250", "要求的邮件操作完成");
            this.hashtable_1.Add("251", "用户非本地，将转发向<forward-path>");
            this.hashtable_1.Add("354", "开始邮件输入，以<CRLF>.<CRLF>结束");
            this.hashtable_1.Add("221", "服务关闭传输信道");
            this.hashtable_1.Add("334", "服务器响应验证Base64字符串");
            this.hashtable_1.Add("235", "验证成功");
        }

        private string method_1(string string_9)
        {
            FileStream stream;
            try
            {
                stream = new FileStream(string_9, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception exception)
            {
                this.string_6 = this.string_6 + "要附加的文件不存在" + this.string_7;
                ilog_0.Error(this.string_6, exception);
                return this.method_4("要附加的文件:" + string_9 + "不存在");
            }
            int length = (int) stream.Length;
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            stream.Close();
            return this.method_3(Convert.ToBase64String(buffer));
        }

        private bool method_10(string string_9, ArrayList arrayList_3)
        {
            string_9 = string_9.Trim();
            if (((string_9 == null) || (string_9 == "")) || (string_9.IndexOf("@") == -1))
            {
                return true;
            }
            if (arrayList_3.Count < this.int_1)
            {
                arrayList_3.Add(string_9);
                return true;
            }
            this.string_6 = this.string_6 + "收件人过多";
            return false;
        }

        private bool method_11(string[] string_9, ArrayList arrayList_3)
        {
            for (int i = 0; i < string_9.Length; i++)
            {
                if (!this.method_10(string_9[i], arrayList_3))
                {
                    return false;
                }
            }
            return true;
        }

        private bool method_12(string string_9)
        {
            if ((string_9 != null) && (string_9.Trim() != ""))
            {
                byte[] bytes = Encoding.Default.GetBytes(string_9);
                try
                {
                    this.networkStream_0.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    this.string_6 = "网络连接错误";
                    return false;
                }
            }
            return true;
        }

        private string method_13()
        {
            int num;
            string str = "false";
            byte[] buffer = new byte[0x1000];
            try
            {
                num = this.networkStream_0.Read(buffer, 0, buffer.Length);
            }
            catch
            {
                this.string_6 = "网络连接错误";
                return str;
            }
            if (num == 0)
            {
                return str;
            }
            return Encoding.Default.GetString(buffer).Substring(0, num).Trim();
        }

        private bool method_14(string string_9, string string_10)
        {
            if ((string_9 == null) || (string_9.Trim() == ""))
            {
                return true;
            }
            if (this.method_12(string_9))
            {
                string str = this.method_13();
                if (str == "false")
                {
                    return false;
                }
                string str2 = "";
                if (str.Length >= 3)
                {
                    str2 = str.Substring(0, 3);
                }
                else
                {
                    str2 = str;
                }
                if (this.hashtable_0[str2] != null)
                {
                    this.string_6 = this.string_6 + str2 + this.hashtable_0[str2].ToString();
                    this.string_6 = this.string_6 + this.string_7;
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool method_15(ArrayList arrayList_3, string string_9)
        {
            using (IEnumerator enumerator = arrayList_3.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    string current = (string) enumerator.Current;
                    if (!this.method_14(current, ""))
                    {
                        goto Label_002E;
                    }
                }
                goto Label_006F;
            Label_002E:
                this.string_6 = this.string_6 + this.string_7;
                this.string_6 = this.string_6 + string_9;
                return false;
            }
        Label_006F:
            return true;
        }

        private bool method_16()
        {
            ArrayList list = new ArrayList();
            string str = "EHLO " + this.string_0 + this.string_7;
            if (!this.method_12(str))
            {
                this.string_6 = this.string_6 + "发送ehlo命令失败";
                return false;
            }
            while (true)
            {
                int num = 0;
                if (!this.networkStream_0.DataAvailable)
                {
                    Thread.Sleep(50);
                    num++;
                    if (num > 6)
                    {
                        this.string_6 = this.string_6 + "收不到AUTH指令，可能是连接超时，或者服务器根本不需要验证" + this.string_7;
                        return false;
                    }
                }
                else
                {
                    string str2 = this.method_13();
                    if (str2 == "false")
                    {
                        return false;
                    }
                    string str3 = str2.Substring(0, 3);
                    if (this.hashtable_1[str3] == null)
                    {
                        if (this.hashtable_0[str3] != null)
                        {
                            this.string_6 = this.string_6 + str3 + this.hashtable_0[str3].ToString();
                            this.string_6 = this.string_6 + this.string_7;
                            this.string_6 = this.string_6 + "发送EHLO命令出错，服务器可能不需要验证" + this.string_7;
                        }
                        else
                        {
                            this.string_6 = this.string_6 + str2;
                            this.string_6 = this.string_6 + "发送EHLO命令出错，不明错误,请与作者联系" + this.string_7;
                        }
                        return false;
                    }
                    if (str2.IndexOf("AUTH") != -1)
                    {
                        list.Add("AUTH LOGIN" + this.string_7);
                        list.Add(this.method_4(this.string_3) + this.string_7);
                        list.Add(this.method_4(this.string_4) + this.string_7);
                        if (!this.method_15(list, "SMTP服务器验证失败，请核对用户名和密码。"))
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        private bool method_17()
        {
            if (this.arrayList_0.Count == 0)
            {
                this.string_6 = "收件人列表不能为空";
                return false;
            }
            if (this.RecipientName == "")
            {
                this.RecipientName = this.arrayList_0[0].ToString();
            }
            if (this.string_0.Trim() == "")
            {
                this.string_6 = "必须指定SMTP服务器";
                return false;
            }
            return true;
        }

        private string method_2(string string_9)
        {
            if (Encoding.Default.GetByteCount(string_9) > string_9.Length)
            {
                return ("=?" + this.Charset.ToUpper() + "?B?" + this.method_4(string_9) + "?=");
            }
            return string_9;
        }

        private string method_3(string string_9)
        {
            StringBuilder builder = new StringBuilder(string_9);
            for (int i = 0x4c; i < builder.Length; i += 0x4e)
            {
                builder.Insert(i, this.string_7);
            }
            return builder.ToString();
        }

        private string method_4(string string_9)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(string_9));
        }

        private string method_5(string string_9)
        {
            byte[] bytes = Convert.FromBase64String(string_9);
            return Encoding.Default.GetString(bytes);
        }

        private string method_6(string string_9, ref StringBuilder stringBuilder_0, string string_10)
        {
            string pattern = @"(?<=img+.+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])";
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string str2 = "<\\s*link[^>]+href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))[^>]*>";
            new Regex(str2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string str3 = "href\\s*=\\s*(?:['\"](?<1>[^\"]*)['\"]|(?<1>\\S+))";
            new Regex(str3, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(string_9))
            {
                if (!this.hashtable_2.ContainsKey(match.Groups[1].Value))
                {
                    this.hashtable_2.Add(match.Groups[1].Value, Guid.NewGuid());
                }
            }
            stringBuilder_0.Length = 0;
            ArrayList list = new ArrayList(this.hashtable_2.Keys);
            foreach (string str4 in list)
            {
                string str5 = str4.Substring(str4.LastIndexOf(".") + 1).ToLower();
                stringBuilder_0.AppendFormat(string_10, new object[0]);
                if (str5.Equals("jpg"))
                {
                    str5 = "jpeg";
                }
                string str6 = str5;
                if ((str6 != null) && (((str6 == "jpeg") || (str6 == "gif")) || ((str6 == "png") || (str6 == "bmp"))))
                {
                    stringBuilder_0.AppendFormat("Content-Type: image/{0}; charset=\"iso-8859-1\"\r\n", str5);
                    stringBuilder_0.Append("Content-Transfer-Encoding: base64\r\n");
                    stringBuilder_0.Append("Content-Disposition: inline\r\n");
                    stringBuilder_0.AppendFormat("Content-ID: <{0}>\r\n\r\n", this.hashtable_2[str4]);
                    stringBuilder_0.Append(this.method_8(str4));
                    stringBuilder_0.Append("\r\n");
                }
            }
            string_9 = regex.Replace(string_9, new MatchEvaluator(this.method_7));
            return string_9;
        }

        private string method_7(Match match_0)
        {
            string oldValue = match_0.Groups[1].Value;
            string newValue = string.Format("cid:{0}", this.hashtable_2[oldValue]);
            return match_0.Value.Replace(oldValue, newValue);
        }

        private string method_8(string string_9)
        {
            int num;
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
            MemoryStream stream = new MemoryStream();
            Stream stream2 = client.OpenRead(string_9);
            byte[] buffer = new byte[0x1000];
            while ((num = stream2.Read(buffer, 0, 0x1000)) > 0)
            {
                stream.Write(buffer, 0, num);
            }
            stream2.Close();
            byte[] buffer2 = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buffer2, 0, (int) stream.Length);
            stream.Close();
            string str = Convert.ToBase64String(buffer2);
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            while ((startIndex + 60) < str.Length)
            {
                builder.AppendFormat("{0}\r\n", str.Substring(startIndex, 60));
                startIndex += 60;
            }
            builder.Append(str.Substring(startIndex));
            for (startIndex = 0; startIndex < (60 - (str.Length % 60)); startIndex++)
            {
                builder.Append('=');
            }
            builder.Append("\r\n");
            return builder.ToString();
        }

        private string method_9(string string_9)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
            return new StreamReader(client.OpenRead(string_9)).ReadToEnd();
        }

        public bool SendEmail()
        {
            string str;
            if (!this.method_17())
            {
                return false;
            }
            try
            {
                this.tcpClient_0 = new TcpClient(this.string_0, this.int_0);
                this.networkStream_0 = this.tcpClient_0.GetStream();
            }
            catch (Exception exception)
            {
                this.string_6 = exception.ToString();
                return false;
            }
            if (this.hashtable_1[this.method_13().Substring(0, 3)] == null)
            {
                this.string_6 = "网络连接失败";
                return false;
            }
            ArrayList list = new ArrayList();
            if (this.bool_0)
            {
                if (!this.method_16())
                {
                    return false;
                }
            }
            else
            {
                str = "HELO " + this.string_0 + this.string_7;
                if (!this.method_14(str, ""))
                {
                    return false;
                }
            }
            str = "MAIL FROM:<" + this.From + ">" + this.string_7;
            if (!this.method_14(str, "发件人地址错误，或不能为空"))
            {
                return false;
            }
            list.Clear();
            foreach (string str3 in this.arrayList_0)
            {
                list.Add("RCPT TO:<" + str3 + ">" + this.string_7);
                this.RecipientName = str3;
            }
            if (!this.method_15(list, "收件人地址有误"))
            {
                return false;
            }
            str = "DATA" + this.string_7;
            if (!this.method_14(str, ""))
            {
                return false;
            }
            string str2 = "From:\"" + this.FromName + "\" <" + this.From + ">" + this.string_7;
            str = str2 + "To:\"" + this.RecipientName + "\" <" + this.RecipientName + ">" + this.string_7;
            if (this.ReplyTo.Trim() != "")
            {
                str = str + "Reply-To: " + this.ReplyTo + this.string_7;
            }
            if (this.arrayList_1.Count > 0)
            {
                str = str + "CC:";
                foreach (string str3 in this.arrayList_1)
                {
                    str2 = str;
                    str = str2 + str3 + "<" + str3 + ">," + this.string_7;
                }
                str = str.Substring(0, str.Length - 3) + this.string_7;
            }
            if (this.arrayList_2.Count > 0)
            {
                str = str + "BCC:";
                foreach (string str3 in this.arrayList_2)
                {
                    str2 = str;
                    str = str2 + str3 + "<" + str3 + ">," + this.string_7;
                }
                str = str.Substring(0, str.Length - 3) + this.string_7;
            }
            if (this.Charset == "")
            {
                str = str + "Subject:" + this.Subject + this.string_7;
            }
            else
            {
                str2 = str;
                str = str2 + "Subject:=?" + this.Charset.ToUpper() + "?B?" + this.method_4(this.Subject) + "?=" + this.string_7;
            }
            if (this.ReturnReceipt)
            {
                str2 = str;
                str = str2 + "Disposition-Notification-To: \"" + this.FromName + "\" <" + this.ReplyTo + ">" + this.string_7;
            }
            str2 = (((((str + "X-Priority:" + this.string_5 + this.string_7) + "X-MSMail-Priority:" + this.string_5 + this.string_7) + "Importance:" + this.string_5 + this.string_7) + "X-Mailer: Huolx.Pubclass" + this.string_7) + "MIME-Version: 1.0" + this.string_7) + "Content-Type: multipart/mixed;" + this.string_7;
            str2 = (((str2 + "\tboundary=\"" + this.VybEgDbhb6 + "\"" + this.string_7 + this.string_7) + "This is a multi-part message in MIME format." + this.string_7 + this.string_7) + "--" + this.VybEgDbhb6 + this.string_7) + "Content-Type: multipart/alternative;" + this.string_7;
            str = (str2 + "\tboundary=\"" + this.string_8 + "\"" + this.string_7 + this.string_7 + this.string_7) + "--" + this.string_8 + this.string_7;
            if (this.IsHtml)
            {
                str = str + "Content-Type: text/html;" + this.string_7;
            }
            else
            {
                str = str + "Content-Type: text/plain;" + this.string_7;
            }
            if (this.Charset == "")
            {
                str = str + "\tcharset=\"iso-8859-1\"" + this.string_7;
            }
            else
            {
                str2 = str;
                str = str2 + "\tcharset=\"" + this.Charset.ToLower() + "\"" + this.string_7;
            }
            str = str + "Content-Transfer-Encoding: base64" + this.string_7;
            StringBuilder builder = new StringBuilder();
            string str4 = "--" + this.VybEgDbhb6 + this.string_7;
            string str5 = this.method_6(this.Body, ref builder, str4);
            str2 = (str + this.string_7 + this.string_7) + this.method_3(this.method_4(str5)) + this.string_7;
            str = (str2 + this.string_7 + "--" + this.string_8 + "--" + this.string_7 + this.string_7) + builder.ToString();
            if (this.list_0.Count > 0)
            {
                str2 = str;
                str = str2 + this.string_7 + "--" + this.string_8 + "--" + this.string_7 + this.string_7;
                foreach (string str3 in this.list_0)
                {
                    str = str + "--" + this.VybEgDbhb6 + this.string_7;
                    str = str + "Content-Type: application/octet-stream;" + this.string_7;
                    str2 = str;
                    str = str2 + "\tname=\"" + this.method_2(str3.Substring(str3.LastIndexOf(@"\") + 1)) + "\"" + this.string_7;
                    str = str + "Content-Transfer-Encoding: base64" + this.string_7;
                    str = str + "Content-Disposition: attachment;" + this.string_7;
                    str2 = str;
                    str = str2 + "\tfilename=\"" + this.method_2(str3.Substring(str3.LastIndexOf(@"\") + 1)) + "\"" + this.string_7 + this.string_7;
                    str = str + this.method_1(str3) + this.string_7 + this.string_7;
                }
                str2 = str;
                str = str2 + "--" + this.VybEgDbhb6 + "--" + this.string_7 + this.string_7;
            }
            str = str + this.string_7 + "." + this.string_7;
            if (!this.method_14(str, "错误信件信息"))
            {
                return false;
            }
            str = "QUIT" + this.string_7;
            if (!this.method_14(str, "断开连接时错误"))
            {
                return false;
            }
            this.networkStream_0.Close();
            this.tcpClient_0.Close();
            return true;
        }

        public void SetRecipient(string str)
        {
            this.arrayList_0.Clear();
            this.arrayList_0.Add(str);
        }

        public string ErrorMessage
        {
            get
            {
                return this.string_6;
            }
        }

        public string From
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
                if (string.IsNullOrEmpty(this.string_2))
                {
                    this.string_2 = this.string_1;
                }
            }
        }

        public string FromName
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

        public string MailServer
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

        public string MailServerPassword
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public int MailServerPort
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public string MailServerUsername
        {
            get
            {
                return this.string_3;
            }
            set
            {
                if (value.Trim() != "")
                {
                    this.string_3 = value.Trim();
                    this.bool_0 = true;
                }
                else
                {
                    this.string_3 = "";
                    this.bool_0 = false;
                }
            }
        }

        public string Priority
        {
            set
            {
                switch (value.ToLower())
                {
                    case "high":
                        this.string_5 = "High";
                        break;

                    case "1":
                        this.string_5 = "High";
                        break;

                    case "normal":
                        this.string_5 = "Normal";
                        break;

                    case "3":
                        this.string_5 = "Normal";
                        break;

                    case "low":
                        this.string_5 = "Low";
                        break;

                    case "5":
                        this.string_5 = "Low";
                        break;

                    default:
                        this.string_5 = "Normal";
                        break;
                }
            }
        }
    }
}

