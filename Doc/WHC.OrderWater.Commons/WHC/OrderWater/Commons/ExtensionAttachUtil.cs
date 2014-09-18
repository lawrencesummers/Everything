namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;

    public class ExtensionAttachUtil
    {
        public static void DelReg(string p_FileTypeName)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("", true);
            RegistryKey key2 = key.OpenSubKey(p_FileTypeName);
            if (key2 != null)
            {
                key.DeleteSubKey(p_FileTypeName, true);
            }
            if (key2 != null)
            {
                key.DeleteSubKeyTree("Exec");
            }
        }

        public static void SaveReg(string _FilePathString, string p_FileTypeName)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("", true);
            if (key.OpenSubKey(p_FileTypeName, true) != null)
            {
                key.DeleteSubKey(p_FileTypeName, true);
            }
            key.CreateSubKey(p_FileTypeName);
            key.OpenSubKey(p_FileTypeName, true).SetValue("", "Exec");
            if (key.OpenSubKey("Exec", true) != null)
            {
                key.DeleteSubKeyTree("Exec");
            }
            key.CreateSubKey("Exec");
            RegistryKey key2 = key.OpenSubKey("Exec", true);
            key2.CreateSubKey("shell");
            key2 = key2.OpenSubKey("shell", true);
            key2.CreateSubKey("open");
            key2 = key2.OpenSubKey("open", true);
            key2.CreateSubKey("command");
            key2 = key2.OpenSubKey("command", true);
            string str = "\"" + _FilePathString + "\" \"%1\"";
            key2.SetValue("", str);
        }
    }
}

