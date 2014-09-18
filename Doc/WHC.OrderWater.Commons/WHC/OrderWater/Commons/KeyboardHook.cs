namespace WHC.OrderWater.Commons
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using WHC.OrderWater.Commons.Collections;

    public static class KeyboardHook
    {
        public static bool Alt = false;
        private static bool bool_0;
        private static CDictionary<Keys, KeyPressed> cdictionary_0 = new CDictionary<Keys, KeyPressed>();
        private static CDictionary<Keys, KeyPressed> cdictionary_1 = new CDictionary<Keys, KeyPressed>();
        public static bool Control = false;
        private static Hooks.HookProc hookProc_0 = new Hooks.HookProc(KeyboardHook.smethod_0);
        private static IntPtr intptr_0 = IntPtr.Zero;
        public static KeyboardHookHandler KeyDown;
        public static bool Shift = false;
        public static bool Win = false;

        public static bool Add(Keys key, KeyPressed callback)
        {
            return AddKeyDown(key, callback);
        }

        public static bool AddKeyDown(Keys key, KeyPressed callback)
        {
            KeyDown = null;
            if (!cdictionary_0.ContainsKey(key))
            {
                cdictionary_0.Add(key, callback);
                return true;
            }
            return false;
        }

        public static bool AddKeyUp(Keys key, KeyPressed callback)
        {
            if (!cdictionary_1.ContainsKey(key))
            {
                cdictionary_1.Add(key, callback);
                return true;
            }
            return false;
        }

        public static bool Disable()
        {
            if (bool_0)
            {
                try
                {
                    Hooks.UnhookWindowsHookEx(intptr_0);
                    bool_0 = false;
                    return true;
                }
                catch
                {
                    bool_0 = true;
                    return false;
                }
            }
            return false;
        }

        public static bool Enable()
        {
            if (!bool_0)
            {
                try
                {
                    using (Process process = Process.GetCurrentProcess())
                    {
                        using (ProcessModule module = process.MainModule)
                        {
                            intptr_0 = Hooks.SetWindowsHookEx(13, hookProc_0, Hooks.GetModuleHandle(module.ModuleName), 0);
                        }
                    }
                    bool_0 = true;
                    return true;
                }
                catch
                {
                    bool_0 = false;
                    return false;
                }
            }
            return false;
        }

        public static string KeyToString(Keys key)
        {
            return ((Control ? "Ctrl + " : "") + (Alt ? "Alt + " : "") + (Shift ? "Shift + " : "") + (Win ? "Win + " : "") + key.ToString());
        }

        public static bool Remove(Keys key)
        {
            return RemoveDown(key);
        }

        public static bool RemoveDown(Keys key)
        {
            return cdictionary_0.Remove(key);
        }

        public static bool RemoveUp(Keys key)
        {
            return cdictionary_1.Remove(key);
        }

        private static IntPtr smethod_0(int int_0, IntPtr intptr_1, IntPtr intptr_2)
        {
            bool flag = true;
            if (int_0 >= 0)
            {
                int num;
                if ((intptr_1 == ((IntPtr) 0x100)) || (intptr_1 == ((IntPtr) 260)))
                {
                    num = Marshal.ReadInt32(intptr_2);
                    if ((num == 0xa2) || (num == 0xa3))
                    {
                        Control = true;
                    }
                    else if ((num == 160) || (num == 0xa1))
                    {
                        Shift = true;
                    }
                    else if ((num == 0xa5) || (num == 0xa4))
                    {
                        Alt = true;
                    }
                    else if ((num == 0x5c) || (num == 0x5b))
                    {
                        Win = true;
                    }
                    else
                    {
                        flag = smethod_1((Keys) num);
                    }
                }
                else if ((intptr_1 == ((IntPtr) 0x101)) || (intptr_1 == ((IntPtr) 0x105)))
                {
                    num = Marshal.ReadInt32(intptr_2);
                    switch (num)
                    {
                        case 0xa2:
                        case 0xa3:
                            Control = false;
                            goto Label_0186;

                        case 160:
                        case 0xa1:
                            Shift = false;
                            goto Label_0186;
                    }
                    if ((num == 0xa5) || (num == 0xa4))
                    {
                        Alt = false;
                    }
                    else if ((num == 0x5c) || (num == 0x5b))
                    {
                        Win = false;
                    }
                    else
                    {
                        flag = smethod_2((Keys) num);
                    }
                }
            }
        Label_0186:
            return (flag ? Hooks.CallNextHookEx(intptr_0, int_0, intptr_1, intptr_2) : new IntPtr(1));
        }

        private static bool smethod_1(Keys keys_0)
        {
            if (KeyDown != null)
            {
                return KeyDown(keys_0);
            }
            if (cdictionary_0.ContainsKey(keys_0))
            {
                return cdictionary_0[keys_0]();
            }
            return true;
        }

        private static bool smethod_2(Keys keys_0)
        {
            if (cdictionary_1.ContainsKey(keys_0))
            {
                return cdictionary_1[keys_0]();
            }
            return true;
        }

        public delegate bool KeyboardHookHandler(Keys key);

        public delegate bool KeyPressed();
    }
}

