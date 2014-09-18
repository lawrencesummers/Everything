namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net.Sockets;

    public class SmtpMail
    {
        private NetworkStream networkStream_0;
        private StreamReader streamReader_0;
        private StreamWriter streamWriter_0;
        private TcpClient tcpClient_0;

        public void CloseConnection()
        {
            this.method_0("QUIT");
            this.streamReader_0.Close();
            this.streamWriter_0.Close();
            this.networkStream_0.Close();
            this.tcpClient_0.Close();
        }

        public string DeleteEmail(string str)
        {
            if (this.method_0("DELE " + str))
            {
                return "成功删除";
            }
            return "Error";
        }

        private bool method_0(string string_0)
        {
            try
            {
                this.streamWriter_0.WriteLine(string_0);
                this.streamWriter_0.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string method_1()
        {
            string message = null;
            try
            {
                message = this.streamReader_0.ReadLine();
                if (message[0] == '-')
                {
                    message = null;
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }

        public string ReadEmail(string str)
        {
            if (!this.method_0("RETR " + str))
            {
                return "Error";
            }
            return this.streamReader_0.ReadToEnd();
        }

        public ArrayList ReceiveMail(string uid, string pwd)
        {
            ArrayList list = new ArrayList();
            int index = uid.IndexOf('@');
            string hostname = "pop3." + uid.Substring(index + 1);
            this.tcpClient_0 = new TcpClient(hostname, 110);
            this.networkStream_0 = this.tcpClient_0.GetStream();
            this.streamReader_0 = new StreamReader(this.networkStream_0);
            this.streamWriter_0 = new StreamWriter(this.networkStream_0);
            if (this.method_1() != null)
            {
                if (!this.method_0("USER " + uid))
                {
                    return list;
                }
                if (this.method_1() == null)
                {
                    return list;
                }
                if (!this.method_0("PASS " + pwd))
                {
                    return list;
                }
                if (this.method_1() == null)
                {
                    return list;
                }
                if (!this.method_0("LIST"))
                {
                    return list;
                }
                string str2 = this.method_1();
                if (str2 == null)
                {
                    return list;
                }
                int num3 = int.Parse(str2.Split(new char[] { ' ' })[1]);
                if (num3 <= 0)
                {
                    return list;
                }
                for (int i = 0; i < num3; i++)
                {
                    str2 = this.method_1();
                    if (str2 == null)
                    {
                        return list;
                    }
                    string[] strArray = str2.Split(new char[] { ' ' });
                    list.Add(string.Format("第{0}封邮件，{1}字节", strArray[0], strArray[1]));
                }
            }
            return list;
        }
    }
}

