namespace RDIFramework.Utilities
{
    using System;

    public class GmailSender : SmtpSender
    {
        public GmailSender(string accountEmailAddress, string accountPassword) : base("smtp.gmail.com")
        {
            base.Port = 0x24b;
            base.UserName = accountEmailAddress;
            base.Password = accountPassword;
            base.EnableSsl = true;
        }

        protected override void ConfigureSender(Message message)
        {
            if (!base.HasCredentials)
            {
                throw new Exception("Gmail Sender requires account email address and password for authentication");
            }
            base.ConfigureSender(message);
        }
    }
}

