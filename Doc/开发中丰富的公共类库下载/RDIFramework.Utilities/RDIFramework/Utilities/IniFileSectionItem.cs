namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true)]
    public struct IniFileSectionItem
    {
        public static readonly IniFileSectionItem None;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private List<string> list_0;
        public IniFileSectionItem(string key) : this(key, string.Empty)
        {
        }

        public IniFileSectionItem(string key, string value)
        {
            this = new IniFileSectionItem();
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (key.Length == 0)
            {
                throw new ArgumentOutOfRangeException("key");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.Key = key;
            this.Value = value;
            this.Comment = new List<string>();
        }

        public static bool operator !=(IniFileSectionItem left, IniFileSectionItem right)
        {
            return !(left == right);
        }

        public static bool operator ==(IniFileSectionItem left, IniFileSectionItem right)
        {
            return (string.Compare(left.Key, right.Key, true) == 0);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IniFileSectionItem))
            {
                return false;
            }
            IniFileSectionItem item = (IniFileSectionItem) obj;
            return (this.Key == item.Key);
        }

        public override int GetHashCode()
        {
            return ((this.Key.GetHashCode() ^ this.Value.GetHashCode()) ^ this.Comment.GetHashCode());
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Key))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str2 in this.Comment)
            {
                builder.AppendFormat("; {0}{1}", str2.Trim(), Environment.NewLine);
            }
            if (string.IsNullOrEmpty(this.Value))
            {
                builder.AppendFormat("{0}{1}", this.Key, Environment.NewLine);
            }
            else
            {
                builder.AppendFormat("{0} = {1}{2}", this.Key, this.Value, Environment.NewLine);
            }
            return builder.ToString();
        }

        public string Key
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
        public string Value
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
        public List<string> Comment
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
        static IniFileSectionItem()
        {
            IniFileSectionItem item = new IniFileSectionItem {
                Key = string.Empty,
                Value = string.Empty,
                Comment = new List<string>()
            };
            None = item;
        }
    }
}

