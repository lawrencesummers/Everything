namespace WHC.OrderWater.Commons
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class MouseHook
    {
        private static bool bool_0;
        public static MouseButtonHandler ButtonDown;
        public static MouseButtonHandler ButtonUp;
        private static Hooks.HookProc hookProc_0 = new Hooks.HookProc(MouseHook.smethod_0);
        private static IntPtr intptr_0 = IntPtr.Zero;
        public static MouseMoveHandler Moved;
        public static MouseScrollHandler Scrolled;

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
                            intptr_0 = Hooks.SetWindowsHookEx(14, hookProc_0, Hooks.GetModuleHandle(module.ModuleName), 0);
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

        private static IntPtr smethod_0(int int_0, IntPtr intptr_1, IntPtr intptr_2)
        {
            bool flag = true;
            if (int_0 >= 0)
            {
                Hooks.Struct25 struct2 = (Hooks.Struct25) Marshal.PtrToStructure(intptr_2, typeof(Hooks.Struct25));
                switch (((int) intptr_1))
                {
                    case 0x200:
                        flag = smethod_3(new Point(struct2.struct26_0.int_0, struct2.struct26_0.int_1));
                        break;

                    case 0x201:
                        flag = smethod_1(MouseButtons.Left);
                        break;

                    case 0x202:
                        flag = smethod_2(MouseButtons.Left);
                        break;

                    case 0x204:
                        flag = smethod_1(MouseButtons.Right);
                        break;

                    case 0x205:
                        flag = smethod_2(MouseButtons.Right);
                        break;

                    case 0x207:
                        flag = smethod_1(MouseButtons.Middle);
                        break;

                    case 520:
                        flag = smethod_2(MouseButtons.Middle);
                        break;

                    case 0x20a:
                        flag = smethod_4((struct2.int_0 >> 0x10) & 0xffff);
                        break;

                    case 0x20b:
                        if ((struct2.int_0 >> 0x10) != 1)
                        {
                            if ((struct2.int_0 >> 0x10) == 2)
                            {
                                flag = smethod_1(MouseButtons.XButton2);
                            }
                            break;
                        }
                        flag = smethod_1(MouseButtons.XButton1);
                        break;

                    case 0x20c:
                        if ((struct2.int_0 >> 0x10) != 1)
                        {
                            if ((struct2.int_0 >> 0x10) == 2)
                            {
                                flag = smethod_2(MouseButtons.XButton2);
                            }
                            break;
                        }
                        flag = smethod_2(MouseButtons.XButton1);
                        break;
                }
            }
            return (flag ? Hooks.CallNextHookEx(intptr_0, int_0, intptr_1, intptr_2) : new IntPtr(1));
        }

        private static bool smethod_1(MouseButtons mouseButtons_0)
        {
            if (ButtonDown != null)
            {
                return ButtonDown(mouseButtons_0);
            }
            return true;
        }

        private static bool smethod_2(MouseButtons mouseButtons_0)
        {
            if (ButtonUp != null)
            {
                return ButtonUp(mouseButtons_0);
            }
            return true;
        }

        private static bool smethod_3(Point point_0)
        {
            if (Moved != null)
            {
                return Moved(point_0);
            }
            return true;
        }

        private static bool smethod_4(int int_0)
        {
            if (Scrolled != null)
            {
                return Scrolled(int_0);
            }
            return true;
        }

        public delegate bool MouseButtonHandler(MouseButtons button);

        public delegate bool MouseMoveHandler(Point point);

        public delegate bool MouseScrollHandler(int delta);
    }
}

