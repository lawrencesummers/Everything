namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;

    public class MailAttachments
    {
        private const int int_0 = 10;
        private IList zygtfKfhMn = new ArrayList();

        public void Add(params string[] filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("非法的附件");
            }
            for (int i = 0; i < filePath.Length; i++)
            {
                this.Add(filePath[i]);
            }
        }

        public void Add(string filePath)
        {
            if (File.Exists(filePath) && (this.zygtfKfhMn.Count < 10))
            {
                this.zygtfKfhMn.Add(filePath);
            }
        }

        public void Clear()
        {
            this.zygtfKfhMn.Clear();
        }

        public int Count
        {
            get
            {
                return this.zygtfKfhMn.Count;
            }
        }

        public string this[int index]
        {
            get
            {
                return (string) this.zygtfKfhMn[index];
            }
        }
    }
}

