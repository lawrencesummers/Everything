namespace RDIFramework.Utilities
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class MessageAttachment
    {
        private readonly System.IO.Stream stream_0;
        private readonly string string_0;
        [CompilerGenerated]
        private string string_1;

        public MessageAttachment(string mediaType, System.IO.Stream stream)
        {
            this.string_0 = mediaType;
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this.stream_0 = stream;
        }

        public MessageAttachment(string mediaType, string fileName)
        {
            this.string_0 = mediaType;
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            FileInfo info = new FileInfo(fileName);
            if (!info.Exists)
            {
                throw new ArgumentException("The specified file does not exists", "fileName");
            }
            this.FileName = fileName;
        }

        public MessageAttachment(string fileName, string mediaType, System.IO.Stream stream) : this(mediaType, stream)
        {
            this.FileName = fileName;
        }

        public string FileName
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

        public string MediaType
        {
            get
            {
                return this.string_0;
            }
        }

        public System.IO.Stream Stream
        {
            get
            {
                return this.stream_0;
            }
        }
    }
}

