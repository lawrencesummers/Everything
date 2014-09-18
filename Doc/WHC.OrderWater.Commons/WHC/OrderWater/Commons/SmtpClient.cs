namespace WHC.OrderWater.Commons
{
    using System;

    public class SmtpClient
    {
        private string string_0;
        private string string_1;

        public SmtpClient()
        {
        }

        public SmtpClient(string _smtpServer)
        {
            this.string_1 = _smtpServer;
        }

        public bool Send(MailMessage mailMessage, string username, string password)
        {
            SmtpServerHelper helper = new SmtpServerHelper();
            if (helper.SendEmail(this.string_1, 0x19, username, password, mailMessage))
            {
                return true;
            }
            this.string_0 = helper.ErrMsg;
            return false;
        }

        public string ErrMsg
        {
            get
            {
                return this.string_0;
            }
        }

        public string SmtpServer
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

