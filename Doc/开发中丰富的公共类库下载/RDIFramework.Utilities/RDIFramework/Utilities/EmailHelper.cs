namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class EmailHelper
    {
        public static class Account
        {
            public const string EmailSMTPName = "---";
            public const string EmailSMTPPass = "---";
            public const int EmailSMTPPort = 0x24b;
            public const string EmailSMTPServer = "smtp.gmail.com";
            public const string EmailSMTPUser = "---";
        }

        public class MailSender
        {
            [CompilerGenerated]
            private bool bool_0;
            [CompilerGenerated]
            private bool bool_1;
            [CompilerGenerated]
            private List<MailAddress> list_0;
            [CompilerGenerated]
            private List<MailAddress> list_1;
            [CompilerGenerated]
            private List<MailAddress> list_2;
            [CompilerGenerated]
            private MailAddress mailAddress_0;
            [CompilerGenerated]
            private string string_0;
            [CompilerGenerated]
            private string string_1;

            public MailSender()
            {
                this.Receiver = new List<MailAddress>();
                this.CC = new List<MailAddress>();
                this.BCC = new List<MailAddress>();
                this.Boolean_0 = true;
                this.Boolean_1 = true;
            }

            public void Send()
            {
                MailAddress address = new MailAddress("---", "---", Encoding.UTF8);
                MailMessage message = new MailMessage {
                    From = address
                };
                foreach (MailAddress address2 in this.Receiver)
                {
                    message.To.Add(address2);
                }
                message.IsBodyHtml = this.Boolean_1;
                message.Body = this.Content;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = this.Subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Priority = MailPriority.High;
                message.ReplyTo = this.ReplayTo;
                foreach (MailAddress address2 in this.CC)
                {
                    message.CC.Add(address2);
                }
                foreach (MailAddress address2 in this.BCC)
                {
                    message.Bcc.Add(address2);
                }
                new SmtpClient("smtp.gmail.com", 0x24b) { Credentials = new NetworkCredential("---", "---"), EnableSsl = this.Boolean_0 }.Send(message);
            }

            public void Send(string Subject, string Content, List<MailAddress> Receiver)
            {
                this.Subject = Subject;
                this.Content = Content;
                this.Receiver = Receiver;
                this.Send();
            }

            public List<MailAddress> BCC
            {
                [CompilerGenerated]
                get
                {
                    return this.list_2;
                }
                [CompilerGenerated]
                set
                {
                    this.list_2 = value;
                }
            }

            public bool Boolean_0
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

            public bool Boolean_1
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

            public List<MailAddress> CC
            {
                [CompilerGenerated]
                get
                {
                    return this.list_1;
                }
                [CompilerGenerated]
                set
                {
                    this.list_1 = value;
                }
            }

            public string Content
            {
                [CompilerGenerated]
                get
                {
                    return this.string_1;
                }
                [CompilerGenerated]
                set
                {
                    this.string_1 = value;
                }
            }

            public List<MailAddress> Receiver
            {
                [CompilerGenerated]
                get
                {
                    return this.list_0;
                }
                [CompilerGenerated]
                set
                {
                    this.list_0 = value;
                }
            }

            public MailAddress ReplayTo
            {
                [CompilerGenerated]
                get
                {
                    return this.mailAddress_0;
                }
                [CompilerGenerated]
                set
                {
                    this.mailAddress_0 = value;
                }
            }

            public string Subject
            {
                [CompilerGenerated]
                get
                {
                    return this.string_0;
                }
                [CompilerGenerated]
                set
                {
                    this.string_0 = value;
                }
            }
        }
    }
}

