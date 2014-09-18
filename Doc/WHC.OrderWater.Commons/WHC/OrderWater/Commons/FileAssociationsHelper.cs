namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;

    public class FileAssociationsHelper
    {
        private static RegistryKey registryKey_0;

        public static void RegisterFileAssociations(string progId, bool registerInHKCU, string appId, string openWith, params string[] extensions)
        {
            smethod_5(false, progId, registerInHKCU, appId, openWith, extensions);
        }

        private static void smethod_0(object object_0)
        {
            if (object_0.Length < 6)
            {
                string message = "Usage: <ProgId> <Register in HKCU: true|false> <AppId> <OpenWithSwitch> <Unregister: true|false> <Ext1> [Ext2 [Ext3] ...]";
                throw new ArgumentException(message);
            }
            try
            {
                <>c__DisplayClass3 class2;
                Action<string> action = null;
                string progId = (string) object_0[0];
                bool flag = bool.Parse((string) object_0[1]);
                string str2 = (string) object_0[2];
                string str3 = (string) object_0[3];
                bool flag2 = bool.Parse((string) object_0[4]);
                List<string> list = new List<string>();
                for (int i = 0; i < object_0.Length; i++)
                {
                    if (i >= 5)
                    {
                        list.Add((string) object_0[i]);
                    }
                }
                string[] array = list.ToArray();
                if (flag)
                {
                    registryKey_0 = Registry.CurrentUser.OpenSubKey(@"Software\Classes");
                }
                else
                {
                    registryKey_0 = Registry.ClassesRoot;
                }
                Array.ForEach<string>(array, new Action<string>(class2.<Process>b__0));
                smethod_2(progId);
                if (!flag2)
                {
                    smethod_1(progId, str2, str3);
                    if (action == null)
                    {
                        action = new Action<string>(class2.<Process>b__1);
                    }
                    Array.ForEach<string>(array, action);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.ReadLine();
            }
        }

        private static void smethod_1(string string_0, object object_0, object object_1)
        {
            RegistryKey key = registryKey_0.CreateSubKey(string_0);
            key.SetValue("FriendlyTypeName", "@shell32.dll,-8975");
            key.SetValue("DefaultIcon", "@shell32.dll,-47");
            key.SetValue("CurVer", string_0);
            key.SetValue("AppUserModelID", object_0);
            RegistryKey key2 = key.CreateSubKey("shell");
            key2.SetValue(string.Empty, "Open");
            key2 = key2.CreateSubKey("Open").CreateSubKey("Command");
            key2.SetValue(string.Empty, object_1);
            key2.Close();
            key.Close();
        }

        private static void smethod_2(string string_0)
        {
            try
            {
                registryKey_0.DeleteSubKeyTree(string_0);
            }
            catch
            {
            }
        }

        private static void smethod_3(string string_0, string string_1)
        {
            RegistryKey key = registryKey_0.CreateSubKey(Path.Combine(string_1, "OpenWithProgIds"));
            key.SetValue(string_0, string.Empty);
            key.Close();
        }

        private static void smethod_4(string string_0, string string_1)
        {
            try
            {
                RegistryKey key = registryKey_0.CreateSubKey(Path.Combine(string_1, "OpenWithProgIds"));
                key.DeleteValue(string_0);
                key.Close();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error while unregistering file association: " + exception.Message);
            }
        }

        private static void smethod_5(bool bool_0, object object_0, bool bool_1, object object_1, object object_2, string[] string_0)
        {
            string str = string.Format("{0} {1} {2} \"{3}\" {4} {5}", new object[] { object_0, bool_1, object_1, object_2, bool_0, string.Join(" ", string_0) });
            try
            {
                smethod_0(str.Split(new char[] { ' ' }));
            }
            catch (Win32Exception exception)
            {
                if (exception.NativeErrorCode == 0x4c7)
                {
                    LogTextHelper.Info("该操作已经被用户取消。");
                }
            }
        }

        public static void UnregisterFileAssociations(string progId, bool registerInHKCU, string appId, string openWith, params string[] extensions)
        {
            smethod_5(true, progId, registerInHKCU, appId, openWith, extensions);
        }
    }
}

