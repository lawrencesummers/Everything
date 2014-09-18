namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;

    public class MailMessage
    {
        private IList ilist_0 = new ArrayList();
        private int int_0 = 30;
        private MailAttachments mailAttachments_0 = new MailAttachments();
        private MailFormat mailFormat_0 = MailFormat.HTML;
        private MailPriority mailPriority_0 = MailPriority.Normal;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4 = "GB2312";

        public MailMessage()
        {
            this.string_4 = "GB2312";
        }

        public void AddRecipients(string recipient)
        {
            if (this.ilist_0.Count < this.MaxRecipientNum)
            {
                this.ilist_0.Add(recipient);
            }
        }

        public void AddRecipients(params string[] recipient)
        {
            if (recipient == null)
            {
                throw new ArgumentException("收件人不能为空.");
            }
            for (int i = 0; i < recipient.Length; i++)
            {
                this.AddRecipients(recipient[i]);
            }
        }

        public MailAttachments Attachments
        {
            get
            {
                return this.mailAttachments_0;
            }
            set
            {
                this.mailAttachments_0 = value;
            }
        }

        public string Body
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

        public MailFormat BodyFormat
        {
            get
            {
                return this.mailFormat_0;
            }
            set
            {
                this.mailFormat_0 = value;
            }
        }

        public string Charset
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

        public string From
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

        public string FromName
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

        public int MaxRecipientNum
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

        public MailPriority Priority
        {
            get
            {
                return this.mailPriority_0;
            }
            set
            {
                this.mailPriority_0 = value;
            }
        }

        public IList Recipients
        {
            get
            {
                return this.ilist_0;
            }
        }

        public string Subject
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }
    }
}

