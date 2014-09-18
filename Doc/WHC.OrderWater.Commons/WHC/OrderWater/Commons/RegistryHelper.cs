namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.Windows.Forms;

    public sealed class RegistryHelper
    {
        private static string string_0 = @"Software\DeepLand\OrderWater";

        public static bool CheckStartWithWindows()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if ((key != null) && (((string) key.GetValue(Application.ProductName, "null", RegistryValueOptions.None)) != "null"))
            {
                Registry.CurrentUser.Flush();
                return true;
            }
            Registry.CurrentUser.Flush();
            return false;
        }

        public static string GetValue(string key)
        {
            return GetValue(string_0, key);
        }

        public static string GetValue(string softwareKey, string key)
        {
            return GetValue(softwareKey, key, Registry.CurrentUser);
        }

        public static string GetValue(string softwareKey, string key, RegistryKey rootRegistry)
        {
            if (null == key)
            {
                throw new ArgumentNullException("key");
            }
            try
            {
                return rootRegistry.OpenSubKey(softwareKey).GetValue(key).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static bool SaveValue(string key, string value)
        {
            return SaveValue(string_0, key, value);
        }

        public static bool SaveValue(string softwareKey, string key, string value)
        {
            return SaveValue(softwareKey, key, value, Registry.CurrentUser);
        }

        public static bool SaveValue(string softwareKey, string key, string value, RegistryKey rootRegistry)
        {
            if (null == key)
            {
                throw new ArgumentNullException("key");
            }
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            RegistryKey key2 = rootRegistry.OpenSubKey(softwareKey, true);
            if (null == key2)
            {
                key2 = rootRegistry.CreateSubKey(softwareKey);
            }
            key2.SetValue(key, value);
            return true;
        }

        public static void SetStartWithWindows(bool startWin)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key != null)
            {
                if (startWin)
                {
                    key.SetValue(Application.ProductName, Application.ExecutablePath, RegistryValueKind.String);
                }
                else
                {
                    key.DeleteValue(Application.ProductName, false);
                }
                Registry.CurrentUser.Flush();
            }
        }
    }
}

