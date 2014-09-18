namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    [Serializable, ComVisible(true)]
    public sealed class IniFileSectionCollection : List<IniFileSection>
    {
        public void Add(IniFileSection item)
        {
            if (base.Contains(item))
            {
                base.Remove(item);
            }
            base.Add(item);
        }

        public bool Contains(string sectionName)
        {
            return (this[sectionName] != IniFileSection.None);
        }

        public void Remove(string sectionName)
        {
            base.RemoveAll(item => item.Name == sectionName);
        }

        public override string ToString()
        {
            if (base.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (IniFileSection section in this)
            {
                builder.Append(section.ToString());
            }
            return builder.ToString();
        }

        public IniFileSection this[string name]
        {
            get
            {
                for (int i = 0; i < base.Count; i++)
                {
                    IniFileSection section = base[i];
                    if (string.Compare(section.Name, name, true) == 0)
                    {
                        return base[i];
                    }
                }
                return IniFileSection.None;
            }
            set
            {
                for (int i = 0; i < base.Count; i++)
                {
                    IniFileSection section = base[i];
                    if (string.Compare(section.Name, name, true) == 0)
                    {
                        base[i] = value;
                    }
                }
            }
        }
    }
}

