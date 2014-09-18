namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    [Serializable, ComVisible(true)]
    public sealed class IniFileSectionItemCollection : List<IniFileSectionItem>
    {
        public void Add(IniFileSectionItem item)
        {
            if (base.Contains(item))
            {
                base.Remove(item);
            }
            base.Add(item);
        }

        public bool Contains(string keyName)
        {
            return (this[keyName] != IniFileSectionItem.None);
        }

        public void Remove(string keyName)
        {
            base.RemoveAll(item => item.Key == keyName);
        }

        public override string ToString()
        {
            if (base.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (IniFileSectionItem item in this)
            {
                builder.Append(item.ToString());
            }
            return builder.ToString();
        }

        public IniFileSectionItem this[string key]
        {
            get
            {
                for (int i = 0; i < base.Count; i++)
                {
                    IniFileSectionItem item = base[i];
                    if (string.Compare(item.Key, key, true) == 0)
                    {
                        return base[i];
                    }
                }
                return IniFileSectionItem.None;
            }
            set
            {
                for (int i = 0; i < base.Count; i++)
                {
                    IniFileSectionItem item = base[i];
                    if (string.Compare(item.Key, key, true) == 0)
                    {
                        base[i] = value;
                    }
                }
            }
        }
    }
}

