namespace RDIFramework.Utilities
{
    using System;

    public class HttpItem
    {
        private string string_0;
        private string string_1 = "GET";
        private string string_2 = "text/html, application/xhtml+xml, */*";
        private string string_3 = "text/html";
        private string string_4 = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        private string string_5 = string.Empty;
        private string string_6;
        private string string_7;
        private string string_8;
        private string string_9 = string.Empty;

        public string Accept
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

        public string CerPath
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public string ContentType
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

        public string Cookie
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public string Encoding
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public string Method
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

        public string Postdata
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string Referer
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public string URL
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

        public string UserAgent
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
    }
}

