namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms;

    [HostProtection(SecurityAction.LinkDemand, Resources=HostProtectionResource.ExternalProcessMgmt)]
    public class MouseHelper
    {
        [DllImport("User32.dll")]
        public static extern int GetCursorPos(Point lpPoint);
        [DllImport("User32.dll")]
        public static extern int GetDoubleClickTime();
        [DllImport("User32.dll")]
        private static extern void mouse_event(Enum6 enum6_0, int int_0, int int_1, uint uint_0, UIntPtr uintptr_0);
        public static void MouseClick()
        {
            mouse_event(2, 0, 0, 0, UIntPtr.Zero);
            mouse_event(4, 0, 0, 0, UIntPtr.Zero);
        }

        public static void MouseClick(Point location)
        {
            MouseMove(location);
            mouse_event(2, 0, 0, 0, UIntPtr.Zero);
            mouse_event(4, 0, 0, 0, UIntPtr.Zero);
        }

        public static void MouseMove(Point location)
        {
            SetCursorPos(location.X, location.Y);
        }

        public static void MouseRightClick(Point location)
        {
            MouseMove(location);
            mouse_event(8, 0, 0, 0, UIntPtr.Zero);
            mouse_event(0x10, 0, 0, 0, UIntPtr.Zero);
        }

        [DllImport("User32.dll")]
        public static extern int SetCursorPos(int x, int y);

        public static bool MousePresent
        {
            get
            {
                return SystemInformation.MousePresent;
            }
        }

        public static bool WheelExists
        {
            get
            {
                if (!SystemInformation.MousePresent)
                {
                    throw new InvalidOperationException("没有找到鼠标.");
                }
                return SystemInformation.MouseWheelPresent;
            }
        }

        public static int WheelScrollLines
        {
            get
            {
                if (!WheelExists)
                {
                    throw new InvalidOperationException("没有找到鼠标滑轮.");
                }
                return SystemInformation.MouseWheelScrollLines;
            }
        }

        [Flags]
        private enum Enum6 : uint
        {
        }
    }
}

