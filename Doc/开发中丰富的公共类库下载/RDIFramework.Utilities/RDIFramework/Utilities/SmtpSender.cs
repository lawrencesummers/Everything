namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;

    public class SmtpSender : IEmailSender, IRichMessageEmailSender
    {
        private bool bool_0 = false;
        private bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        private int int_0 = 0x19;
        private readonly NetworkCredential networkCredential_0 = new NetworkCredential();
        private readonly SmtpClient smtpClient_0;
        private readonly string string_0;

        public SmtpSender(string hostname)
        {
            this.string_0 = hostname;
            this.smtpClient_0 = new SmtpClient(hostname);
        }

        protected virtual void ConfigureSender(RDIFramework.Utilities.Message message)
        {
            if (!this.bool_1)
            {
                if (this.HasCredentials)
                {
                    this.smtpClient_0.Credentials = this.networkCredential_0;
                }
                this.smtpClient_0.Port = this.int_0;
                this.smtpClient_0.EnableSsl = this.EnableSsl;
                this.bool_1 = true;
            }
        }

        public void Send(RDIFramework.Utilities.Message[] messages)
        {
            foreach (RDIFramework.Utilities.Message message in messages)
            {
                this.Send(message);
            }
        }

        public void Send(RDIFramework.Utilities.Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            this.ConfigureSender(message);
            if (this.bool_0)
            {
                MailMessage msg = smethod_0(message);
                Guid msgGuid = new Guid();
                SendCompletedEventHandler sceh = null;
                sceh = delegate (object sender, AsyncCompletedEventArgs e) {
                    if (msgGuid == ((Guid) e.UserState))
                    {
                        msg.Dispose();
                    }
                    this.smtpClient_0.SendCompleted -= sceh;
                };
                this.smtpClient_0.SendCompleted += sceh;
                this.smtpClient_0.SendAsync(msg, msgGuid);
            }
            else
            {
                using (MailMessage message2 = smethod_0(message))
                {
                    this.smtpClient_0.Send(message2);
                }
            }
        }

        public void Send(string from, string to, string subject, string messageText)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }
            if (messageText == null)
            {
                throw new ArgumentNullException("messageText");
            }
            this.Send(new RDIFramework.Utilities.Message(from, to, subject, messageText));
        }

        private static MailMessage smethod_0(RDIFramework.Utilities.Message message_0)
        {
            MailMessage message = new MailMessage(message_0.From, message_0.To.Replace(';', ','));
            if (!string.IsNullOrEmpty(message_0.Cc))
            {
                message.CC.Add(message_0.Cc);
            }
            if (!string.IsNullOrEmpty(message_0.Bcc))
            {
                message.Bcc.Add(message_0.Bcc);
            }
            message.Subject = message_0.Subject;
            message.Body = message_0.Body;
            message.BodyEncoding = message_0.Encoding;
            message.IsBodyHtml = message_0.Format == Format.Html;
            message.Priority = (MailPriority) System.Enum.Parse(typeof(MailPriority), message_0.Priority.ToString());
            message.ReplyTo = message_0.ReplyTo;
            foreach (DictionaryEntry entry in message_0.Headers)
            {
                message.Headers.Add((string) entry.Key, (string) entry.Value);
            }
            foreach (MessageAttachment attachment in message_0.Attachments)
            {
                Attachment attachment2;
                if (attachment.Stream != null)
                {
                    attachment2 = new Attachment(attachment.Stream, attachment.FileName, attachment.MediaType);
                }
                else
                {
                    attachment2 = new Attachment(attachment.FileName, attachment.MediaType);
                }
                message.Attachments.Add(attachment2);
            }
            if ((message_0.Resources != null) && (message_0.Resources.Count > 0))
            {
                AlternateView item = AlternateView.CreateAlternateViewFromString(message_0.Body, message_0.Encoding, "text/html");
                foreach (string str in message_0.Resources.Keys)
                {
                    LinkedResource resource = message_0.Resources[str];
                    resource.ContentId = str;
                    if (resource.ContentStream != null)
                    {
                        item.LinkedResources.Add(resource);
                    }
                }
                message.AlternateViews.Add(item);
            }
            return message;
        }

        public bool AsyncSend
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public string Domain
        {
            get
            {
                return this.networkCredential_0.Domain;
            }
            set
            {
                this.networkCredential_0.Domain = value;
            }
        }

        public bool EnableSsl
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }

        protected bool HasCredentials
        {
            get
            {
                return ((this.networkCredential_0.UserName != null) && (this.networkCredential_0.Password != null));
            }
        }

        public string Hostname
        {
            get
            {
                return this.string_0;
            }
        }

        public string Password
        {
            get
            {
                return this.networkCredential_0.Password;
            }
            set
            {
                this.networkCredential_0.Password = value;
            }
        }

        public int Port
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

        public int Timeout
        {
            get
            {
                return this.smtpClient_0.Timeout;
            }
            set
            {
                this.smtpClient_0.Timeout = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.networkCredential_0.UserName;
            }
            set
            {
                this.networkCredential_0.UserName = value;
            }
        }
    }
}

