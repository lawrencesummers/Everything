namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ConnectStrings : List<ConnectString>
    {
        public ConnectString this[string name]
        {
            get
            {
                foreach (ConnectString str in this)
                {
                    if (str.LinkName.Equals(name))
                    {
                        return str;
                    }
                }
                return null;
            }
        }
    }
}

