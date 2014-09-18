namespace RDIFramework.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Message
    {
        private System.Text.Encoding encoding_0;
        private RDIFramework.Utilities.Format format_0;
        private IDictionary idictionary_0;
        private IDictionary idictionary_1;
        private readonly IDictionary<string, LinkedResource> idictionary_2;
        [CompilerGenerated]
        private MailAddress mailAddress_0;
        private MessageAttachmentList messageAttachmentList_0;
        private MessagePriority messagePriority_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;
        [CompilerGenerated]
        private string string_4;
        [CompilerGenerated]
        private string string_5;

        public Message()
        {
            this.format_0 = RDIFramework.Utilities.Format.Text;
            this.encoding_0 = System.Text.Encoding.ASCII;
            this.idictionary_0 = new HybridDictionary();
            this.idictionary_1 = new HybridDictionary();
            this.messagePriority_0 = MessagePriority.Normal;
            this.messageAttachmentList_0 = new MessageAttachmentList();
            this.idictionary_2 = new Dictionary<string, LinkedResource>();
        }

        public Message(string from, string to, string subject, string body)
        {
            this.format_0 = RDIFramework.Utilities.Format.Text;
            this.encoding_0 = System.Text.Encoding.ASCII;
            this.idictionary_0 = new HybridDictionary();
            this.idictionary_1 = new HybridDictionary();
            this.messagePriority_0 = MessagePriority.Normal;
            this.messageAttachmentList_0 = new MessageAttachmentList();
            this.idictionary_2 = new Dictionary<string, LinkedResource>();
            this.To = to;
            this.From = from;
            this.Body = body;
            this.Subject = subject;
        }

        public MessageAttachmentList Attachments
        {
            get
            {
                return this.messageAttachmentList_0;
            }
        }

        public string Bcc
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

        public string Body
        {
            [CompilerGenerated]
            get
            {
                return this.string_4;
            }
            [CompilerGenerated]
            set
            {
                this.string_4 = value;
            }
        }

        public string Cc
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

        public System.Text.Encoding Encoding
        {
            get
            {
                return this.encoding_0;
            }
            set
            {
                this.encoding_0 = value;
            }
        }

        public IDictionary Fields
        {
            get
            {
                return this.idictionary_1;
            }
        }

        public RDIFramework.Utilities.Format Format
        {
            get
            {
                return this.format_0;
            }
            set
            {
                this.format_0 = value;
            }
        }

        public string From
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

        public IDictionary Headers
        {
            get
            {
                return this.idictionary_0;
            }
        }

        public MessagePriority Priority
        {
            get
            {
                return this.messagePriority_0;
            }
            set
            {
                this.messagePriority_0 = value;
            }
        }

        public MailAddress ReplyTo
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

        public IDictionary<string, LinkedResource> Resources
        {
            get
            {
                return this.idictionary_2;
            }
        }

        public string Subject
        {
            [CompilerGenerated]
            get
            {
                return this.string_5;
            }
            [CompilerGenerated]
            set
            {
                this.string_5 = value;
            }
        }

        public string To
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

