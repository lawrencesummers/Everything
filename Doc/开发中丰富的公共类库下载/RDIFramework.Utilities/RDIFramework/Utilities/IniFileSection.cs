namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true)]
    public struct IniFileSection
    {
        public static readonly IniFileSection None;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private List<string> list_0;
        [CompilerGenerated]
        private IniFileSectionItemCollection iniFileSectionItemCollection_0;
        [CompilerGenerated]
        private static Func<IniFileSectionItem, string> LwkdeSkKg;
        public IniFileSection(string name)
        {
            this = new IniFileSection();
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length == 0)
            {
                throw new ArgumentOutOfRangeException("name");
            }
            this.Name = name;
            this.Items = new IniFileSectionItemCollection();
            this.Comment = new List<string>();
        }

        public string GetValue(string keyName)
        {
            if (keyName == null)
            {
                throw new ArgumentNullException("keyName");
            }
            if (keyName.Length == 0)
            {
                throw new ArgumentOutOfRangeException("keyName");
            }
            if (LwkdeSkKg == null)
            {
                LwkdeSkKg = new Func<IniFileSectionItem, string>(IniFileSection.smethod_0);
            }
            string str = (from item in this.Items
                where item.Key == keyName
                select item).Select<IniFileSectionItem, string>(LwkdeSkKg).FirstOrDefault<string>();
            return (string.IsNullOrEmpty(str) ? string.Empty : str);
        }

        public static bool operator !=(IniFileSection left, IniFileSection right)
        {
            return !(left == right);
        }

        public static bool operator ==(IniFileSection left, IniFileSection right)
        {
            return (string.Compare(left.Name, right.Name, true) == 0);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IniFileSection))
            {
                return false;
            }
            IniFileSection section = (IniFileSection) obj;
            return (this.Name == section.Name);
        }

        public override int GetHashCode()
        {
            return ((this.Name.GetHashCode() ^ this.Items.GetHashCode()) ^ this.Comment.GetHashCode());
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str2 in this.Comment)
            {
                builder.AppendFormat("; {0}{1}", str2.Trim(), Environment.NewLine);
            }
            builder.AppendFormat("[{0}]{1}", this.Name, Environment.NewLine);
            foreach (IniFileSectionItem item in this.Items)
            {
                builder.Append(item.ToString());
            }
            return builder.ToString();
        }

        public IniFileSectionItem this[string keyName]
        {
            get
            {
                if (keyName == null)
                {
                    throw new ArgumentNullException("keyName");
                }
                if (keyName.Length == 0)
                {
                    throw new ArgumentOutOfRangeException("keyName");
                }
                return this.Items[keyName];
            }
            set
            {
                this.Items[keyName] = value;
            }
        }
        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            private set
            {
                this.string_0 = value;
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
        public IniFileSectionItemCollection Items
        {
            [CompilerGenerated]
            get
            {
                return this.iniFileSectionItemCollection_0;
            }
            [CompilerGenerated]
            private set
            {
                this.iniFileSectionItemCollection_0 = value;
            }
        }
        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public bool Remove(string item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [CompilerGenerated]
        private static string smethod_0(IniFileSectionItem iniFileSectionItem_0)
        {
            return iniFileSectionItem_0.Value;
        }

        static IniFileSection()
        {
            IniFileSection section = new IniFileSection {
                Name = string.Empty
            };
            None = section;
        }
    }
}

