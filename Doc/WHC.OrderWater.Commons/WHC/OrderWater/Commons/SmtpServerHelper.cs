namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;

    public class SmtpServerHelper
    {
        private Hashtable hashtable_0 = new Hashtable();
        private Hashtable hashtable_1 = new Hashtable();
        private NetworkStream networkStream_0;
        private string string_0 = "\r\n";
        private string string_1;
        private string string_2 = "";
        private TcpClient tcpClient_0;

        public SmtpServerHelper()
        {
            this.method_3();
        }

        ~SmtpServerHelper()
        {
            this.networkStream_0.Close();
            this.tcpClient_0.Close();
        }

        private string method_0(string string_3)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(string_3));
        }

        private string method_1(string string_3)
        {
            byte[] bytes = Convert.FromBase64String(string_3);
            return Encoding.Default.GetString(bytes);
        }

        private bool method_10(string string_3, int int_0, bool bool_0, string string_4, string string_5, MailMessage mailMessage_0)
        {
            string str;
            int num;
            string[] strArray2;
            if (!this.method_8(string_3, int_0))
            {
                return false;
            }
            string str4 = this.method_9(mailMessage_0.Priority);
            bool flag2 = mailMessage_0.BodyFormat == MailFormat.HTML;
            if (bool_0)
            {
                strArray2 = new string[] { "EHLO " + string_3 + this.string_0, "AUTH LOGIN" + this.string_0, this.method_0(string_4) + this.string_0, this.method_0(string_5) + this.string_0 };
                if (!this.method_7(strArray2, "SMTP服务器验证失败，请核对用户名和密码。"))
                {
                    return false;
                }
            }
            else
            {
                str = "HELO " + string_3 + this.string_0;
                if (!this.method_6(str, ""))
                {
                    return false;
                }
            }
            str = "MAIL FROM:<" + string_4 + ">" + this.string_0;
            if (!this.method_6(str, "发件人地址错误，或不能为空"))
            {
                return false;
            }
            strArray2 = new string[mailMessage_0.Recipients.Count];
            for (num = 0; num < mailMessage_0.Recipients.Count; num++)
            {
                strArray2[num] = "RCPT TO:<" + ((string) mailMessage_0.Recipients[num]) + ">" + this.string_0;
            }
            if (!this.method_7(strArray2, "收件人地址有误"))
            {
                return false;
            }
            str = "DATA" + this.string_0;
            if (!this.method_6(str, ""))
            {
                return false;
            }
            str = "From:" + mailMessage_0.FromName + "<" + mailMessage_0.From + ">" + this.string_0;
            if (mailMessage_0.Recipients.Count == 0)
            {
                return false;
            }
            string str2 = str;
            str = ((((((str2 + "To:=?" + mailMessage_0.Charset.ToUpper() + "?B?" + this.method_0((string) mailMessage_0.Recipients[0]) + "?=<" + ((string) mailMessage_0.Recipients[0]) + ">" + this.string_0) + (((mailMessage_0.Subject == string.Empty) || (mailMessage_0.Subject == null)) ? "Subject:" : ((mailMessage_0.Charset == "") ? ("Subject:" + mailMessage_0.Subject) : ("Subject:=?" + mailMessage_0.Charset.ToUpper() + "?B?" + this.method_0(mailMessage_0.Subject) + "?="))) + this.string_0) + "X-Priority:" + str4 + this.string_0) + "X-MSMail-Priority:" + str4 + this.string_0) + "Importance:" + str4 + this.string_0) + "X-Mailer: Lion.Web.Mail.SmtpMail Pubclass [cn]" + this.string_0) + "MIME-Version: 1.0" + this.string_0;
            if (mailMessage_0.Attachments.Count != 0)
            {
                str2 = str + "Content-Type: multipart/mixed;" + this.string_0;
                str = str2 + " boundary=\"=====" + (flag2 ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====\"" + this.string_0 + this.string_0;
            }
            if (flag2)
            {
                if (mailMessage_0.Attachments.Count == 0)
                {
                    str = ((str + "Content-Type: multipart/alternative;" + this.string_0) + " boundary=\"=====003_Dragon520636771063_=====\"" + this.string_0 + this.string_0) + "This is a multi-part message in MIME format." + this.string_0 + this.string_0;
                }
                else
                {
                    str = (((str + "This is a multi-part message in MIME format." + this.string_0 + this.string_0) + "--=====001_Dragon520636771063_=====" + this.string_0) + "Content-Type: multipart/alternative;" + this.string_0) + " boundary=\"=====003_Dragon520636771063_=====\"" + this.string_0 + this.string_0;
                }
                str = ((((((((((str + "--=====003_Dragon520636771063_=====" + this.string_0) + "Content-Type: text/plain;" + this.string_0) + ((mailMessage_0.Charset == "") ? " charset=\"iso-8859-1\"" : (" charset=\"" + mailMessage_0.Charset.ToLower() + "\"")) + this.string_0) + "Content-Transfer-Encoding: base64" + this.string_0 + this.string_0) + this.method_0("邮件内容为HTML格式，请选择HTML方式查看") + this.string_0 + this.string_0) + "--=====003_Dragon520636771063_=====" + this.string_0) + "Content-Type: text/html;" + this.string_0) + ((mailMessage_0.Charset == "") ? " charset=\"iso-8859-1\"" : (" charset=\"" + mailMessage_0.Charset.ToLower() + "\"")) + this.string_0) + "Content-Transfer-Encoding: base64" + this.string_0 + this.string_0) + this.method_0(mailMessage_0.Body) + this.string_0 + this.string_0) + "--=====003_Dragon520636771063_=====--" + this.string_0;
            }
            else
            {
                if (mailMessage_0.Attachments.Count != 0)
                {
                    str = str + "--=====001_Dragon303406132050_=====" + this.string_0;
                }
                str = (((str + "Content-Type: text/plain;" + this.string_0) + ((mailMessage_0.Charset == "") ? " charset=\"iso-8859-1\"" : (" charset=\"" + mailMessage_0.Charset.ToLower() + "\"")) + this.string_0) + "Content-Transfer-Encoding: base64" + this.string_0 + this.string_0) + this.method_0(mailMessage_0.Body) + this.string_0;
            }
            if (mailMessage_0.Attachments.Count != 0)
            {
                for (num = 0; num < mailMessage_0.Attachments.Count; num++)
                {
                    string str3 = mailMessage_0.Attachments[num];
                    str2 = str;
                    str2 = (str2 + "--=====" + (flag2 ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====" + this.string_0) + "Content-Type: text/plain;" + this.string_0;
                    str2 = ((str2 + " name=\"=?" + mailMessage_0.Charset.ToUpper() + "?B?" + this.method_0(str3.Substring(str3.LastIndexOf(@"\") + 1)) + "?=\"" + this.string_0) + "Content-Transfer-Encoding: base64" + this.string_0) + "Content-Disposition: attachment;" + this.string_0;
                    str = (str2 + " filename=\"=?" + mailMessage_0.Charset.ToUpper() + "?B?" + this.method_0(str3.Substring(str3.LastIndexOf(@"\") + 1)) + "?=\"" + this.string_0 + this.string_0) + this.method_2(str3) + this.string_0 + this.string_0;
                }
                str2 = str;
                str = str2 + "--=====" + (flag2 ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====--" + this.string_0 + this.string_0;
            }
            str = str + this.string_0 + "." + this.string_0;
            if (!this.method_6(str, "错误信件信息"))
            {
                return false;
            }
            str = "QUIT" + this.string_0;
            if (!this.method_6(str, "断开连接时错误"))
            {
                return false;
            }
            this.networkStream_0.Close();
            this.tcpClient_0.Close();
            return true;
        }

        private string method_2(string string_3)
        {
            FileStream stream = new FileStream(string_3, FileMode.Open);
            byte[] buffer = new byte[Convert.ToInt32(stream.Length)];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return Convert.ToBase64String(buffer);
        }

        private void method_3()
        {
            this.hashtable_0.Add("421", "服务未就绪，关闭传输信道");
            this.hashtable_0.Add("432", "需要一个密码转换");
            this.hashtable_0.Add("450", "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
            this.hashtable_0.Add("451", "放弃要求的操作；处理过程中出错");
            this.hashtable_0.Add("452", "系统存储不足，要求的操作未执行");
            this.hashtable_0.Add("454", "临时认证失败");
            this.hashtable_0.Add("500", "邮箱地址错误");
            this.hashtable_0.Add("501", "参数格式错误");
            this.hashtable_0.Add("502", "命令不可实现");
            this.hashtable_0.Add("503", "服务器需要SMTP验证");
            this.hashtable_0.Add("504", "命令参数不可实现");
            this.hashtable_0.Add("530", "需要认证");
            this.hashtable_0.Add("534", "认证机制过于简单");
            this.hashtable_0.Add("538", "当前请求的认证机制需要加密");
            this.hashtable_0.Add("550", "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
            this.hashtable_0.Add("551", "用户非本地，请尝试<forward-path>");
            this.hashtable_0.Add("552", "过量的存储分配，要求的操作未执行");
            this.hashtable_0.Add("553", "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
            this.hashtable_0.Add("554", "传输失败");
            this.hashtable_1.Add("220", "服务就绪");
            this.hashtable_1.Add("221", "服务关闭传输信道");
            this.hashtable_1.Add("235", "验证成功");
            this.hashtable_1.Add("250", "要求的邮件操作完成");
            this.hashtable_1.Add("251", "非本地用户，将转发向<forward-path>");
            this.hashtable_1.Add("334", "服务器响应验证Base64字符串");
            this.hashtable_1.Add("354", "开始邮件输入，以<CRLF>.<CRLF>结束");
        }

        private bool method_4(string string_3)
        {
            if ((string_3 != null) && (string_3.Trim() != string.Empty))
            {
                this.string_2 = this.string_2 + string_3;
                byte[] bytes = Encoding.Default.GetBytes(string_3);
                try
                {
                    this.networkStream_0.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    this.string_1 = "网络连接错误";
                    return false;
                }
            }
            return true;
        }

        private string method_5()
        {
            int num;
            string str = string.Empty;
            byte[] buffer = new byte[0x400];
            try
            {
                num = this.networkStream_0.Read(buffer, 0, buffer.Length);
            }
            catch
            {
                this.string_1 = "网络连接错误";
                return "false";
            }
            if (num != 0)
            {
                str = Encoding.Default.GetString(buffer).Substring(0, num);
                this.string_2 = this.string_2 + str + this.string_0;
            }
            return str;
        }

        private bool method_6(string string_3, string string_4)
        {
            if ((string_3 == null) || (string_3.Trim() == string.Empty))
            {
                return true;
            }
            if (this.method_4(string_3))
            {
                string str = this.method_5();
                if (str != "false")
                {
                    string str2 = str.Substring(0, 3);
                    if (this.hashtable_1[str2] != null)
                    {
                        return true;
                    }
                    if (this.hashtable_0[str2] != null)
                    {
                        this.string_1 = this.string_1 + str2 + this.hashtable_0[str2].ToString();
                        this.string_1 = this.string_1 + this.string_0;
                    }
                    else
                    {
                        this.string_1 = this.string_1 + str;
                    }
                    this.string_1 = this.string_1 + string_4;
                }
                return false;
            }
            return false;
        }

        private bool method_7(string[] string_3, string string_4)
        {
            for (int i = 0; i < string_3.Length; i++)
            {
                if (!this.method_6(string_3[i], ""))
                {
                    this.string_1 = this.string_1 + this.string_0;
                    this.string_1 = this.string_1 + string_4;
                    return false;
                }
            }
            return true;
        }

        private bool method_8(string string_3, int int_0)
        {
            try
            {
                this.tcpClient_0 = new TcpClient(string_3, int_0);
            }
            catch (Exception exception)
            {
                this.string_1 = exception.ToString();
                return false;
            }
            this.networkStream_0 = this.tcpClient_0.GetStream();
            if (this.hashtable_1[this.method_5().Substring(0, 3)] == null)
            {
                this.string_1 = "网络连接失败";
                return false;
            }
            return true;
        }

        private string method_9(MailPriority mailPriority_0)
        {
            string str = "Normal";
            if (mailPriority_0 == MailPriority.Low)
            {
                return "Low";
            }
            if (mailPriority_0 == MailPriority.High)
            {
                str = "High";
            }
            return str;
        }

        public bool SendEmail(string smtpServer, int port, MailMessage mailMessage)
        {
            return this.method_10(smtpServer, port, false, "", "", mailMessage);
        }

        public bool SendEmail(string smtpServer, int port, string username, string password, MailMessage mailMessage)
        {
            return this.method_10(smtpServer, port, true, username, password, mailMessage);
        }

        public string ErrMsg
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
    }
}

