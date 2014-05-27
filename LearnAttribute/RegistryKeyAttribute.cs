using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnAttribute
{
    public enum RegistryHives
    {
        HKEY_CLASSES_ROOT,
        HKEY_CURRENT_USER,
        HKEY_LOCAL_MACHINE,
        HKEY_USERS,
        HKEY_CURRENT_CONFIG
    }
    public class RegistryKeyAttribute : Attribute
    {
        public RegistryKeyAttribute(RegistryHives Hive, string valueName)
        {
            this.ValueName = valueName;
            this.Hive = Hive;
        }

        public RegistryHives Hive { get; set; }

        public string ValueName { get; set; }
    }
}
