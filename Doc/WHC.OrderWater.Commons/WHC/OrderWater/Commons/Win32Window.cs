namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class Win32Window
    {
        private static ArrayList arrayList_0 = null;
        private static ArrayList arrayList_1 = null;
        private ArrayList arrayList_2 = null;
        private static Image image_0 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        private const int int_0 = -20;
        private const int int_1 = 0x80;
        private const int int_2 = 0x40000;
        private const int int_3 = 0xcc0020;
        private IntPtr intptr_0;
        private string string_0 = null;

        public Win32Window(IntPtr window)
        {
            this.intptr_0 = window;
        }

        [DllImport("gdi32.dll")]
        private static extern ulong BitBlt(IntPtr intptr_1, int int_4, int int_5, int int_6, int int_7, IntPtr intptr_2, int int_8, int int_9, int int_10);
        public void BringWindowToTop()
        {
            BringWindowToTop_1(this.intptr_0);
            Thread.Sleep(500);
        }

        [DllImport("User32.dll", EntryPoint="BringWindowToTop")]
        private static extern bool BringWindowToTop_1(IntPtr intptr_1);
        public void ClickWindow(string button, int x, int y, bool doubleklick)
        {
            int num = this.method_0(x, y);
            int num2 = 0;
            int num3 = 0;
            if (button == "left")
            {
                num2 = 0x201;
                num3 = 0x202;
            }
            if (button == "right")
            {
                num2 = 0x204;
                num3 = 0x205;
            }
            if (doubleklick)
            {
                SendMessage_1(this.intptr_0, num2, 0, num);
                SendMessage_1(this.intptr_0, num3, 0, num);
                SendMessage_1(this.intptr_0, num2, 0, num);
                SendMessage_1(this.intptr_0, num3, 0, num);
            }
            else
            {
                SendMessage_1(this.intptr_0, num2, 0, num);
                SendMessage_1(this.intptr_0, num3, 0, num);
            }
        }

        public void ClickWindow_Post(string button, int x, int y, bool doubleklick)
        {
            int num = this.method_0(x, y);
            int num2 = 0;
            int num3 = 0;
            if (button == "left")
            {
                num2 = 0x201;
                num3 = 0x202;
            }
            if (button == "right")
            {
                num2 = 0x204;
                num3 = 0x205;
            }
            if (doubleklick)
            {
                PostMessage_1(this.intptr_0, num2, 0, num);
                PostMessage_1(this.intptr_0, num3, 0, num);
                PostMessage_1(this.intptr_0, num2, 0, num);
                PostMessage_1(this.intptr_0, num3, 0, num);
            }
            else
            {
                PostMessage_1(this.intptr_0, num2, 0, num);
                PostMessage_1(this.intptr_0, num3, 0, num);
            }
        }

        [DllImport("User32.dll")]
        private static extern bool EnumChildWindows(IntPtr intptr_1, Delegate8 delegate8_0, int int_4);
        [DllImport("User32.dll")]
        private static extern bool EnumThreadWindows(int int_4, Delegate8 delegate8_0, int int_5);
        [DllImport("User32.dll")]
        private static extern bool EnumWindows(Delegate8 delegate8_0, int int_4);
        public Win32Window FindChild(string className, string windowName)
        {
            return new Win32Window(FindWindowEx(this.intptr_0, IntPtr.Zero, className, windowName));
        }

        public static Win32Window FindWindow(string className, string windowName)
        {
            return new Win32Window(FindWindow_1(className, windowName));
        }

        [DllImport("User32.dll", EntryPoint="FindWindow")]
        private static extern IntPtr FindWindow_1(string string_1, string string_2);
        [DllImport("User32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr intptr_1, IntPtr intptr_2, string string_1, string string_2);
        [DllImport("user32.dll ")]
        public static extern int GetClassName(IntPtr hWnd, [Out] StringBuilder className, int maxCount);
        [DllImport("User32.dll")]
        private static extern bool GetClientRect(IntPtr intptr_1, ref Rect rect_0);
        [DllImport("User32.dll")]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("User32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("User32.dll")]
        private static extern IntPtr GetLastActivePopup(IntPtr intptr_1);
        [DllImport("User32.dll")]
        private static extern IntPtr GetParent(IntPtr intptr_1);
        public static ArrayList GetThreadWindows(int threadId)
        {
            arrayList_0 = new ArrayList();
            EnumThreadWindows(threadId, new Delegate8(Win32Window.smethod_0), 0);
            ArrayList list = arrayList_0;
            arrayList_0 = null;
            return list;
        }

        [DllImport("User32.dll")]
        private static extern int GetWindowClassNameLength(IntPtr intptr_1);
        [DllImport("User32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr intptr_1);
        [DllImport("User32.dll")]
        private static extern bool GetWindowInfo(IntPtr intptr_1, ref Struct24 struct24_0);
        public int GetWindowLong(int index)
        {
            return GetWindowLong_1(this.intptr_0, index);
        }

        [DllImport("User32.dll", EntryPoint="GetWindowLong")]
        private static extern int GetWindowLong_1(IntPtr intptr_1, int int_4);
        [DllImport("User32.dll")]
        private static extern bool GetWindowPlacement(IntPtr intptr_1, ref WindowPlacement windowPlacement_0);
        [DllImport("User32.dll")]
        private static extern bool GetWindowRect(IntPtr intptr_1, ref Rect rect_0);
        [DllImport("User32.dll")]
        private static extern int GetWindowText(IntPtr intptr_1, [In, Out] StringBuilder stringBuilder_0, int int_4);
        [DllImport("User32.dll")]
        private static extern int GetWindowTextLength(IntPtr intptr_1);
        [DllImport("User32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr intptr_1, ref int int_4);
        [DllImport("User32.dll", EntryPoint="GetWindowThreadProcessId")]
        private static extern int GetWindowThreadProcessId_1(IntPtr intptr_1, IntPtr intptr_2);
        public static bool IsChild(Win32Window parent, Win32Window window)
        {
            return IsChild_1(parent.intptr_0, window.intptr_0);
        }

        [DllImport("User32.dll", EntryPoint="IsChild")]
        private static extern bool IsChild_1(IntPtr intptr_1, IntPtr intptr_2);
        [DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr intptr_1);
        [DllImport("User32.dll")]
        private static extern int IsWindowVisible(IntPtr intptr_1);
        [DllImport("User32.dll")]
        private static extern bool IsZoomed(IntPtr intptr_1);
        public void MakeToolWindow()
        {
            int windowLong = this.GetWindowLong(-20);
            this.SetWindowLong(-20, windowLong | 0x80);
        }

        private int method_0(int int_4, int int_5)
        {
            return ((int_5 << 0x10) | (int_4 & 0xffff));
        }

        private bool method_1(IntPtr intptr_1, int int_4)
        {
            this.arrayList_2.Add(new Win32Window(intptr_1));
            return true;
        }

        public int PostMessage(int message, int wparam, int lparam)
        {
            return PostMessage_1(this.intptr_0, message, wparam, lparam);
        }

        [DllImport("User32.dll", EntryPoint="PostMessage")]
        private static extern int PostMessage_1(IntPtr intptr_1, int int_4, int int_5, int int_6);
        [DllImport("User32.dll", EntryPoint="PostMessage")]
        private static extern int PostMessage_2(IntPtr intptr_1, int int_4, IntPtr intptr_2, string string_1);
        [DllImport("User32.dll")]
        private static extern bool PrintWindow(IntPtr intptr_1, IntPtr intptr_2, uint uint_0);
        public int SendMessage(int message, int wparam, int lparam)
        {
            return SendMessage_1(this.intptr_0, message, wparam, lparam);
        }

        public int SendMessage(int wMsg, int wParam, string lpstring)
        {
            return SendMessage_2(this.intptr_0, wMsg, wParam, lpstring);
        }

        [DllImport("User32.dll", EntryPoint="SendMessage")]
        private static extern int SendMessage_1(IntPtr intptr_1, int int_4, int int_5, int int_6);
        [DllImport("user32", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
        private static extern int SendMessage_2(IntPtr intptr_1, int int_4, int int_5, string string_1);
        public int SetWindowLong(int index, int value)
        {
            return SetWindowLong_1(this.intptr_0, index, value);
        }

        [DllImport("User32.dll", EntryPoint="SetWindowLong")]
        private static extern int SetWindowLong_1(IntPtr intptr_1, int int_4, int int_5);
        [DllImport("User32.dll")]
        public static extern bool SetWindowText(IntPtr window, [MarshalAs(UnmanagedType.LPTStr)] string text);
        private static bool smethod_0(IntPtr intptr_1, int int_4)
        {
            arrayList_0.Add(new Win32Window(intptr_1));
            return true;
        }

        private static bool smethod_1(IntPtr intptr_1, int int_4)
        {
            Win32Window window = new Win32Window(intptr_1);
            if (window.Parent.intptr_0 == IntPtr.Zero)
            {
                if (!window.Visible)
                {
                    return true;
                }
                if (window.Text == string.Empty)
                {
                    return true;
                }
                if (window.ClassName.Substring(0, 8) == "IDEOwner")
                {
                    return true;
                }
                arrayList_1.Add(window);
            }
            return true;
        }

        private static bool smethod_2(IntPtr intptr_1, int int_4)
        {
            arrayList_0.Add(new Win32Window(intptr_1));
            return true;
        }

        public override string ToString()
        {
            string text = this.string_0;
            if (text == null)
            {
                text = this.Text;
            }
            return text;
        }

        public static ArrayList ApplicationWindows
        {
            get
            {
                arrayList_1 = new ArrayList();
                EnumWindows(new Delegate8(Win32Window.smethod_1), 0);
                ArrayList list = arrayList_1;
                arrayList_1 = null;
                return list;
            }
        }

        public ArrayList Children
        {
            get
            {
                this.arrayList_2 = new ArrayList();
                EnumChildWindows(this.intptr_0, new Delegate8(this.method_1), 0);
                ArrayList list = this.arrayList_2;
                this.arrayList_2 = null;
                return list;
            }
        }

        public string ClassName
        {
            get
            {
                StringBuilder className = new StringBuilder(0x100);
                int length = GetClassName(this.intptr_0, className, 0x100);
                return className.ToString(0, length);
            }
        }

        public static Image DesktopAsBitmap
        {
            get
            {
                Graphics graphics = Graphics.FromImage(image_0);
                IntPtr hdc = graphics.GetHdc();
                IntPtr windowDC = GetWindowDC(GetDesktopWindow());
                BitBlt(hdc, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, windowDC, 0, 0, 0xcc0020);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
                return image_0;
            }
        }

        public static Win32Window DesktopWindow
        {
            get
            {
                return new Win32Window(GetDesktopWindow()) { Name = "Desktop" };
            }
        }

        public static Win32Window ForegroundWindow
        {
            get
            {
                return new Win32Window(GetForegroundWindow());
            }
        }

        public bool IsNull
        {
            get
            {
                return (this.intptr_0 == IntPtr.Zero);
            }
        }

        public Win32Window LastActivePopup
        {
            get
            {
                IntPtr lastActivePopup = GetLastActivePopup(this.intptr_0);
                if (lastActivePopup == this.intptr_0)
                {
                    return new Win32Window(IntPtr.Zero);
                }
                return new Win32Window(lastActivePopup);
            }
        }

        public bool Maximized
        {
            get
            {
                return IsZoomed(this.intptr_0);
            }
        }

        public bool Minimized
        {
            get
            {
                return IsIconic(this.intptr_0);
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public Win32Window Parent
        {
            get
            {
                return new Win32Window(GetParent(this.intptr_0));
            }
        }

        public int ProcessId
        {
            get
            {
                int num = 0;
                GetWindowThreadProcessId(this.intptr_0, ref num);
                return num;
            }
        }

        public string Text
        {
            get
            {
                StringBuilder builder = new StringBuilder(GetWindowTextLength(this.intptr_0) + 1);
                GetWindowText(this.intptr_0, builder, builder.Capacity);
                return builder.ToString();
            }
            set
            {
                SetWindowText(this.intptr_0, value);
            }
        }

        public int ThreadId
        {
            get
            {
                return GetWindowThreadProcessId_1(this.intptr_0, IntPtr.Zero);
            }
        }

        public static ArrayList TopLevelWindows
        {
            get
            {
                arrayList_0 = new ArrayList();
                EnumWindows(new Delegate8(Win32Window.smethod_2), 0);
                ArrayList list = arrayList_0;
                arrayList_0 = null;
                return list;
            }
        }

        public bool Visible
        {
            get
            {
                return (IsWindowVisible(this.intptr_0) != 0);
            }
        }

        public IntPtr Window
        {
            get
            {
                return this.intptr_0;
            }
        }

        public Image WindowAsBitmap
        {
            get
            {
                if (this.IsNull)
                {
                    return null;
                }
                this.BringWindowToTop();
                Rect rect = new Rect();
                if (!GetWindowRect(this.intptr_0, ref rect))
                {
                    return null;
                }
                Struct24 struct2 = new Struct24 {
                    int_0 = Marshal.SizeOf(typeof(Struct24))
                };
                if (!GetWindowInfo(this.intptr_0, ref struct2))
                {
                    return null;
                }
                Image image = new Bitmap(rect.Width, rect.Height);
                Graphics graphics = Graphics.FromImage(image);
                IntPtr hdc = graphics.GetHdc();
                IntPtr windowDC = GetWindowDC(this.intptr_0);
                BitBlt(hdc, 0, 0, rect.Width, rect.Height, windowDC, 0, 0, 0xcc0020);
                graphics.ReleaseHdc(hdc);
                return image;
            }
        }

        public Image WindowClientAsBitmap
        {
            get
            {
                if (this.IsNull)
                {
                    return null;
                }
                this.BringWindowToTop();
                Rect rect = new Rect();
                if (!GetClientRect(this.intptr_0, ref rect))
                {
                    return null;
                }
                Struct24 struct2 = new Struct24 {
                    int_0 = Marshal.SizeOf(typeof(Struct24))
                };
                if (!GetWindowInfo(this.intptr_0, ref struct2))
                {
                    return null;
                }
                int num = struct2.rectangle_1.X - struct2.rectangle_0.X;
                int num2 = struct2.rectangle_1.Y - struct2.rectangle_0.Y;
                Image image = new Bitmap(rect.Width, rect.Height);
                Graphics graphics = Graphics.FromImage(image);
                IntPtr hdc = graphics.GetHdc();
                IntPtr windowDC = GetWindowDC(this.intptr_0);
                BitBlt(hdc, 0, 0, rect.Width, rect.Height, windowDC, num, num2, 0xcc0020);
                graphics.ReleaseHdc(hdc);
                return image;
            }
        }

        public WindowPlacement WindowPlacement
        {
            get
            {
                WindowPlacement placement = new WindowPlacement();
                GetWindowPlacement(this.intptr_0, ref placement);
                return placement;
            }
        }

        private delegate bool Delegate8(IntPtr window, int i);

        [StructLayout(LayoutKind.Sequential)]
        private struct Struct24
        {
            public int int_0;
            public Rectangle rectangle_0;
            public Rectangle rectangle_1;
            public int int_1;
            public int int_2;
            public int int_3;
            public uint uint_0;
            public uint uint_1;
            public short short_0;
            public short short_1;
        }
    }
}

