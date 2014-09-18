namespace WHC.OrderWater.Commons
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public sealed class NativeMethods
    {
        public const int CURSOR_SHOWING = 1;
        public static readonly int DWM_TNP_OPACITY = 4;
        public static readonly int DWM_TNP_RECTDESTINATION = 1;
        public static readonly int DWM_TNP_VISIBLE = 8;
        public const int GWL_STYLE = -16;
        public const int HT_CAPTION = 2;
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        internal static int int_0 = 9;
        public const int SC_MINIMIZE = 0xf020;
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const uint SWP_FRAMECHANGED = 0x20;
        public const uint SWP_HIDEWINDOW = 0x80;
        public const uint SWP_NOACTIVATE = 0x10;
        public const uint SWP_NOCOPYBITS = 0x100;
        public const uint SWP_NOMOVE = 2;
        public const uint SWP_NOOWNERZORDER = 0x200;
        public const uint SWP_NOREDRAW = 8;
        public const uint SWP_NOSENDCHANGING = 0x400;
        public const uint SWP_NOSIZE = 1;
        public const uint SWP_NOZORDER = 4;
        public const uint SWP_SHOWWINDOW = 0x40;
        public const ulong TARGETWINDOW = 0x10800000L;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_SYSCOMMAND = 0x112;

        private NativeMethods()
        {
        }

        public static void ActivateWindow(IntPtr handle)
        {
            SetForegroundWindow(handle);
            SetActiveWindow(handle);
        }

        public static void ActivateWindowRepeat(IntPtr handle, int count)
        {
            // This item is obfuscated and can not be translated.
            for (int i = 0; !(GetForegroundWindow() != handle); i++)
            {
                if (0 == 0)
                {
                    return;
                }
                BringWindowToTop(handle);
                Thread.Sleep(1);
                Application.DoEvents();
            }
            goto Label_0005;
        }

        public static void BringToFront(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, int_0);
            }
            SetForegroundWindow(hWnd);
        }

        [DllImport("User32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);
        public static MyCursor CaptureCursor()
        {
            CursorInfo info;
            info = new CursorInfo {
                cbSize = Marshal.SizeOf(info)
            };
            if (GetCursorInfo(out info) && (info.flags == 1))
            {
                IconInfo info2;
                info.ptScreenPos = ConvertPoint(Cursor.Position);
                IntPtr hIcon = CopyIcon(info.hCursor);
                if ((hIcon != IntPtr.Zero) && GetIconInfo(hIcon, out info2))
                {
                    Point position = new Point(info.ptScreenPos.X - info2.xHotspot, info.ptScreenPos.Y - info2.yHotspot);
                    using (Bitmap bitmap = Image.FromHbitmap(info2.hbmMask))
                    {
                        Bitmap image = null;
                        if (smethod_0(bitmap))
                        {
                            image = new Bitmap(bitmap.Width, bitmap.Width);
                            Graphics graphics = Graphics.FromHwnd(GetDesktopWindow());
                            IntPtr hdc = graphics.GetHdc();
                            IntPtr ptr3 = GDI.CreateCompatibleDC(hdc);
                            IntPtr hgdiobj = GDI.SelectObject(ptr3, bitmap.GetHbitmap());
                            using (Graphics graphics2 = Graphics.FromImage(image))
                            {
                                IntPtr ptr5 = graphics2.GetHdc();
                                GDI.BitBlt(ptr5, 0, 0, 0x20, 0x20, ptr3, 0, 0x20, CopyPixelOperation.SourceCopy);
                                GDI.BitBlt(ptr5, 0, 0, 0x20, 0x20, ptr3, 0, 0, CopyPixelOperation.SourceInvert);
                                graphics2.ReleaseHdc(ptr5);
                            }
                            GDI.DeleteDC(GDI.SelectObject(ptr3, hgdiobj));
                            GDI.DeleteDC(ptr3);
                            graphics.ReleaseHdc(hdc);
                            image.MakeTransparent(Color.White);
                        }
                        else
                        {
                            image = Icon.FromHandle(hIcon).ToBitmap();
                        }
                        return new MyCursor(new Cursor(info.hCursor), position, image);
                    }
                }
            }
            return new MyCursor();
        }

        public static Point ConvertPoint(Point p)
        {
            int num = 0;
            int num2 = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                num = Math.Min(num, screen.Bounds.X);
                num2 = Math.Min(num2, screen.Bounds.Y);
            }
            return new Point(p.X - num, p.Y - num2);
        }

        [DllImport("User32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);
        [DllImport("dwmapi.dll", PreserveSig=false)]
        public static extern bool DwmIsCompositionEnabled();
        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out SIZE size);
        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetDxFrameDuration(IntPtr hwnd, uint cRefreshes);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr thumb);
        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);
        public static bool DWMWA_NCRENDERING_ENABLED(IntPtr handle)
        {
            bool flag;
            DwmGetWindowAttribute(handle, 1, out flag, 1);
            return flag;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        public static string GetClassName(IntPtr handle)
        {
            StringBuilder lpClassName = new StringBuilder(100);
            if (GetClassName(handle, lpClassName, 100) > 0)
            {
                return lpClassName.ToString();
            }
            return string.Empty;
        }

        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("User32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("User32.dll")]
        public static extern bool GetCursorInfo(out CursorInfo pci);
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern IntPtr GetDesktopWindow();
        public static bool GetExtendedFrameBounds(IntPtr handle, out Rectangle rectangle)
        {
            RECT rect;
            int num = DwmGetWindowAttribute(handle, 9, out rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect.ToRectangle();
            return (num >= 0);
        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("User32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);
        [DllImport("User32.dll")]
        public static extern int GetSystemMetrics(int smIndex);
        public static Rectangle GetTaskbarRectangle()
        {
            APPBARDATA pData = new APPBARDATA();
            SHAppBarMessage(5, out pData);
            return pData.rc.ToRectangle();
        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowConstants wCmd);
        [DllImport("User32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        public static IntPtr GetWindowHandle()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            StringBuilder lpString = new StringBuilder(0x100);
            if (GetWindowText(foregroundWindow, lpString, 0x100) > 0)
            {
                return foregroundWindow;
            }
            return IntPtr.Zero;
        }

        public static string GetWindowLabel()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            StringBuilder lpString = new StringBuilder(0x100);
            if (GetWindowText(foregroundWindow, lpString, 0x100) > 0)
            {
                return lpString.ToString();
            }
            return string.Empty;
        }

        [DllImport("User32.dll")]
        public static extern ulong GetWindowLong(IntPtr hWnd, int nIndex);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect.ToRectangle();
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            Rectangle rectangle;
            if ((Environment.OSVersion.Version.Major >= 6) && GetExtendedFrameBounds(handle, out rectangle))
            {
                return rectangle;
            }
            return GetWindowRect(handle);
        }

        public static bool GetWindowRegion(IntPtr hWnd, out Region region)
        {
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            RegionType windowRgn = (RegionType) GetWindowRgn(hWnd, hRgn);
            region = Region.FromHrgn(hRgn);
            return ((windowRgn != RegionType.ERROR) && (windowRgn != RegionType.NULLREGION));
        }

        [DllImport("User32.dll")]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);
        [DllImport("User32.dll", SetLastError=true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("User32.dll")]
        internal static extern bool IsIconic(IntPtr intptr_0);
        public static bool IsWindowMaximized(IntPtr handle)
        {
            WINDOWPLACEMENT lpwndpl = new WINDOWPLACEMENT();
            GetWindowPlacement(handle, ref lpwndpl);
            return (lpwndpl.showCmd == 3);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        public static Rectangle MaximizedWindowFix(IntPtr handle, Rectangle windowRect)
        {
            if (IsWindowMaximized(handle))
            {
                Rectangle bounds = Screen.FromRectangle(windowRect).Bounds;
                if (windowRect.X < bounds.X)
                {
                    windowRect.Width -= (bounds.X - windowRect.X) * 2;
                    windowRect.X = bounds.X;
                }
                if (windowRect.Y < bounds.Y)
                {
                    windowRect.Height -= (bounds.Y - windowRect.Y) * 2;
                    windowRect.Y = bounds.Y;
                }
                windowRect.Intersect(bounds);
            }
            return windowRect;
        }

        [DllImport("User32.dll")]
        public static extern bool MessageBeep(uint beepType);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("winmm.dll")]
        public static extern long PlaySound(string lpszName, long hModule, long dwFlags);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", SetLastError=true)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        public static void SetDWMWindowAttributeNCRenderingPolicy(IntPtr handle, DwmNCRenderingPolicy renderingPolicy)
        {
            int attrValue = (int) renderingPolicy;
            DwmSetWindowAttribute(handle, 2, ref attrValue, 4);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, IntPtr min, IntPtr max);
        [DllImport("User32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("shell32.dll", CallingConvention=CallingConvention.StdCall)]
        public static extern uint SHAppBarMessage(int dwMessage, out APPBARDATA pData);
        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("User32.dll")]
        internal static extern bool ShowWindowAsync(IntPtr intptr_0, int int_1);
        private static bool smethod_0(object object_0)
        {
            bool flag = object_0.Height == (object_0.Width * 2);
            Color color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            for (int i = 0; i < (object_0.Height / 2); i++)
            {
                for (int j = 0; j < object_0.Width; j++)
                {
                    if (!(flag &= (object_0.GetPixel(j, i) == color)))
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        private static void smethod_1(out TaskBarEdge taskBarEdge_0, out int int_1, out bool bool_0)
        {
            APPBARDATA pData = new APPBARDATA();
            int_1 = 0;
            taskBarEdge_0 = TaskBarEdge.Bottom;
            bool_0 = false;
            SHAppBarMessage(5, out pData);
            switch (pData.uEdge)
            {
                case 0:
                    taskBarEdge_0 = TaskBarEdge.Left;
                    int_1 = pData.rc.Width;
                    break;

                case 1:
                    taskBarEdge_0 = TaskBarEdge.Top;
                    int_1 = pData.rc.Bottom;
                    break;

                case 2:
                    taskBarEdge_0 = TaskBarEdge.Right;
                    int_1 = pData.rc.Width;
                    break;

                case 3:
                    taskBarEdge_0 = TaskBarEdge.Bottom;
                    int_1 = pData.rc.Height;
                    break;
            }
            pData = new APPBARDATA();
            switch (SHAppBarMessage(4, out pData))
            {
                case 0:
                    bool_0 = false;
                    break;

                case 1:
                    bool_0 = true;
                    break;

                case 2:
                    bool_0 = false;
                    break;

                case 3:
                    bool_0 = true;
                    break;
            }
        }

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);
        public static void TrimMemoryUse()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (IntPtr) (-1), (IntPtr) (-1));
        }

        [DllImport("User32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);

        public enum ABEdge
        {
            ABE_LEFT,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        public enum ABMsg
        {
            ABM_NEW,
            ABM_REMOVE,
            ABM_QUERYPOS,
            ABM_SETPOS,
            ABM_GETSTATE,
            ABM_GETTASKBARPOS,
            ABM_ACTIVATE,
            ABM_GETAUTOHIDEBAR,
            ABM_SETAUTOHIDEBAR,
            ABM_WINDOWPOSCHANGED,
            ABM_SETSTATE
        }

        public enum ABState
        {
            ABS_MANUAL,
            ABS_AUTOHIDE,
            ABS_ALWAYSONTOP,
            ABS_AUTOHIDEANDONTOP
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public NativeMethods.RECT rc;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [Flags]
        public enum ClassStyles : uint
        {
            CS_BYTEALIGNCLIENT = 0x1000,
            CS_BYTEALIGNWINDOW = 0x2000,
            CS_CLASSDC = 0x40,
            CS_DBLCLKS = 8,
            CS_DROPSHADOW = 0x20000,
            CS_GLOBALCLASS = 0x4000,
            CS_HREDRAW = 2,
            CS_IME = 0x10000,
            CS_NOCLOSE = 0x200,
            CS_OWNDC = 0x20,
            CS_PARENTDC = 0x80,
            CS_SAVEBITS = 0x800,
            CS_VREDRAW = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CursorInfo
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        [Flags]
        public enum DWM_BB
        {
            BlurRegion = 2,
            Enable = 1,
            TransitionMaximized = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND
        {
            public NativeMethods.DWM_BB dwFlags;
            public bool fEnable;
            public IntPtr hRgnBlur;
            public bool fTransitionOnMaximized;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public NativeMethods.RECT rcDestination;
            public NativeMethods.RECT rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }

        [Flags]
        public enum DwmNCRenderingPolicy
        {
            UseWindowStyle,
            Disabled,
            Enabled,
            Last
        }

        [Flags]
        public enum DwmWindowAttribute
        {
            AllowNCPaint = 4,
            CaptionButtonBounds = 5,
            DisallowPeek = 11,
            ExcludedFromPeek = 12,
            ExtendedFrameBounds = 9,
            Flip3DPolicy = 8,
            ForceIconicRepresentation = 7,
            HasIconicBitmap = 10,
            Last = 13,
            NCRenderingEnabled = 1,
            NCRenderingPolicy = 2,
            NonClientRtlLayout = 6,
            TransitionsForceDisabled = 3
        }

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public enum GetWindowConstants : uint
        {
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6,
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_MAX = 6,
            GW_OWNER = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        public class MyCursor : IDisposable
        {
            public System.Drawing.Bitmap Bitmap;
            public System.Windows.Forms.Cursor Cursor;
            public Point Position;

            public MyCursor()
            {
                this.Cursor = System.Windows.Forms.Cursor.Current;
                this.Position = new Point(System.Windows.Forms.Cursor.Position.X - this.Cursor.HotSpot.X, System.Windows.Forms.Cursor.Position.Y - this.Cursor.HotSpot.Y);
                this.Bitmap = Icon.FromHandle(this.Cursor.Handle).ToBitmap();
            }

            public MyCursor(System.Windows.Forms.Cursor cursor, Point position, System.Drawing.Bitmap bitmap)
            {
                this.Cursor = cursor;
                this.Position = position;
                this.Bitmap = bitmap;
            }

            public void Dispose()
            {
                this.Cursor.Dispose();
                this.Bitmap.Dispose();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static explicit operator Point(NativeMethods.POINT p)
            {
                return new Point(p.X, p.Y);
            }

            public static explicit operator NativeMethods.POINT(Point p)
            {
                return new NativeMethods.POINT(p.X, p.Y);
            }
        }

        public enum PRF
        {
            CHECKVISIBLE = 1,
            CHILDREN = 0x10,
            CLIENT = 4,
            ERASEBKGND = 8,
            NONCLIENT = 2,
            OWNED = 0x20
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public RECT(int left_, int top_, int right_, int bottom_)
            {
                this.Left = left_;
                this.Top = top_;
                this.Right = right_;
                this.Bottom = bottom_;
            }

            public int Height
            {
                get
                {
                    return (this.Bottom - this.Top);
                }
            }
            public int Width
            {
                get
                {
                    return (this.Right - this.Left);
                }
            }
            public System.Drawing.Size Size
            {
                get
                {
                    return new System.Drawing.Size(this.Width, this.Height);
                }
            }
            public Point Location
            {
                get
                {
                    return new Point(this.Left, this.Top);
                }
            }
            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
            }

            public static NativeMethods.RECT FromRectangle(Rectangle rectangle)
            {
                return new NativeMethods.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode()
            {
                return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Width << 0x1a) | (this.Width >> 6))) ^ ((this.Height << 7) | (this.Height >> 0x19)));
            }

            public static implicit operator Rectangle(NativeMethods.RECT rect)
            {
                return rect.ToRectangle();
            }

            public static implicit operator NativeMethods.RECT(Rectangle rect)
            {
                return FromRectangle(rect);
            }
        }

        public enum RegionType
        {
            ERROR,
            NULLREGION,
            SIMPLEREGION,
            COMPLEXREGION
        }

        public enum SHOWWINDOW : uint
        {
            SW_FORCEMINIMIZE = 11,
            SW_HIDE = 0,
            SW_MAX = 11,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_NORMAL = 1,
            SW_RESTORE = 9,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOWNORMAL = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int x;
            public int y;
            public SIZE(int width, int height)
            {
                this.x = width;
                this.y = height;
            }
        }

        public enum TaskBarEdge
        {
            Bottom,
            Top,
            Left,
            Right
        }

        [Flags]
        public enum ThreadAccess
        {
            DIRECT_IMPERSONATION = 0x200,
            GET_CONTEXT = 8,
            IMPERSONATE = 0x100,
            QUERY_INFORMATION = 0x40,
            SET_CONTEXT = 0x10,
            SET_INFORMATION = 0x20,
            SET_THREAD_TOKEN = 0x80,
            SUSPEND_RESUME = 2,
            TERMINATE = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        public enum WindowShowStyle : uint
        {
            ForceMinimized = 11,
            Hide = 0,
            Maximize = 3,
            Minimize = 6,
            Restore = 9,
            Show = 5,
            ShowDefault = 10,
            ShowMaximized = 3,
            ShowMinimized = 2,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            ShowNormal = 1,
            ShowNormalNoActivate = 4
        }

        [Flags]
        public enum WindowStyles : uint
        {
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CHILDWINDOW = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_EX_ACCEPTFILES = 0x10,
            WS_EX_APPWINDOW = 0x40000,
            WS_EX_CLIENTEDGE = 0x200,
            WS_EX_COMPOSITED = 0x2000000,
            WS_EX_CONTEXTHELP = 0x400,
            WS_EX_CONTROLPARENT = 0x10000,
            WS_EX_DLGMODALFRAME = 1,
            WS_EX_LAYERED = 0x80000,
            WS_EX_LAYOUTRTL = 0x400000,
            WS_EX_LEFT = 0,
            WS_EX_LEFTSCROLLBAR = 0x4000,
            WS_EX_LTRREADING = 0,
            WS_EX_MDICHILD = 0x40,
            WS_EX_NOACTIVATE = 0x8000000,
            WS_EX_NOINHERITLAYOUT = 0x100000,
            WS_EX_NOPARENTNOTIFY = 4,
            WS_EX_OVERLAPPEDWINDOW = 0x300,
            WS_EX_PALETTEWINDOW = 0x188,
            WS_EX_RIGHT = 0x1000,
            WS_EX_RIGHTSCROLLBAR = 0,
            WS_EX_RTLREADING = 0x2000,
            WS_EX_STATICEDGE = 0x20000,
            WS_EX_TOOLWINDOW = 0x80,
            WS_EX_TOPMOST = 8,
            WS_EX_TRANSPARENT = 0x20,
            WS_EX_WINDOWEDGE = 0x100,
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,
            WS_ICONIC = 0x20000000,
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x20000,
            WS_OVERLAPPED = 0,
            WS_OVERLAPPEDWINDOW = 0xcf0000,
            WS_POPUP = 0x80000000,
            WS_POPUPWINDOW = 0x80880000,
            WS_SIZEBOX = 0x40000,
            WS_SYSMENU = 0x80000,
            WS_TABSTOP = 0x10000,
            WS_THICKFRAME = 0x40000,
            WS_TILED = 0,
            WS_TILEDWINDOW = 0xcf0000,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x200000
        }

        public enum WM : uint
        {
            ACTIVATE = 6,
            ACTIVATEAPP = 0x1c,
            AFXFIRST = 0x360,
            AFXLAST = 0x37f,
            APP = 0x8000,
            APPCOMMAND = 0x319,
            ASKCBFORMATNAME = 780,
            CANCELJOURNAL = 0x4b,
            CANCELMODE = 0x1f,
            CAPTURECHANGED = 0x215,
            CHANGECBCHAIN = 0x30d,
            CHANGEUISTATE = 0x127,
            CHAR = 0x102,
            CHARTOITEM = 0x2f,
            CHILDACTIVATE = 0x22,
            CLEAR = 0x303,
            CLIPBOARDUPDATE = 0x31d,
            CLOSE = 0x10,
            COMMAND = 0x111,
            [Obsolete]
            COMMNOTIFY = 0x44,
            COMPACTING = 0x41,
            COMPAREITEM = 0x39,
            CONTEXTMENU = 0x7b,
            COPY = 0x301,
            COPYDATA = 0x4a,
            CPL_LAUNCH = 0x1400,
            CPL_LAUNCHED = 0x1401,
            CREATE = 1,
            CTLCOLORBTN = 0x135,
            CTLCOLORDLG = 310,
            CTLCOLOREDIT = 0x133,
            CTLCOLORLISTBOX = 0x134,
            CTLCOLORMSGBOX = 0x132,
            CTLCOLORSCROLLBAR = 0x137,
            CTLCOLORSTATIC = 0x138,
            CUT = 0x300,
            DEADCHAR = 0x103,
            DELETEITEM = 0x2d,
            DESTROY = 2,
            DESTROYCLIPBOARD = 0x307,
            DEVICECHANGE = 0x219,
            DEVMODECHANGE = 0x1b,
            DISPLAYCHANGE = 0x7e,
            DRAWCLIPBOARD = 0x308,
            DRAWITEM = 0x2b,
            DROPFILES = 0x233,
            DWMCOLORIZATIONCOLORCHANGED = 800,
            DWMCOMPOSITIONCHANGED = 0x31e,
            DWMNCRENDERINGCHANGED = 0x31f,
            DWMWINDOWMAXIMIZEDCHANGE = 0x321,
            ENABLE = 10,
            ENDSESSION = 0x16,
            ENTERIDLE = 0x121,
            ENTERMENULOOP = 0x211,
            ENTERSIZEMOVE = 0x231,
            ERASEBKGND = 20,
            EXITMENULOOP = 530,
            EXITSIZEMOVE = 0x232,
            FONTCHANGE = 0x1d,
            GETDLGCODE = 0x87,
            GETFONT = 0x31,
            GETHOTKEY = 0x33,
            GETICON = 0x7f,
            GETMINMAXINFO = 0x24,
            GETOBJECT = 0x3d,
            GETTEXT = 13,
            GETTEXTLENGTH = 14,
            GETTITLEBARINFOEX = 0x33f,
            HANDHELDFIRST = 0x358,
            HANDHELDLAST = 0x35f,
            HELP = 0x53,
            HOTKEY = 0x312,
            HSCROLL = 0x114,
            HSCROLLCLIPBOARD = 0x30e,
            ICONERASEBKGND = 0x27,
            IME_CHAR = 0x286,
            IME_COMPOSITION = 0x10f,
            IME_COMPOSITIONFULL = 0x284,
            IME_CONTROL = 0x283,
            IME_ENDCOMPOSITION = 270,
            IME_KEYDOWN = 0x290,
            IME_KEYLAST = 0x10f,
            IME_KEYUP = 0x291,
            IME_NOTIFY = 0x282,
            IME_REQUEST = 0x288,
            IME_SELECT = 0x285,
            IME_SETCONTEXT = 0x281,
            IME_STARTCOMPOSITION = 0x10d,
            INITDIALOG = 0x110,
            INITMENU = 0x116,
            INITMENUPOPUP = 0x117,
            INPUT = 0xff,
            INPUT_DEVICE_CHANGE = 0xfe,
            INPUTLANGCHANGE = 0x51,
            INPUTLANGCHANGEREQUEST = 80,
            KEYDOWN = 0x100,
            KEYFIRST = 0x100,
            KEYLAST = 0x109,
            KEYUP = 0x101,
            KILLFOCUS = 8,
            LBUTTONDBLCLK = 0x203,
            LBUTTONDOWN = 0x201,
            LBUTTONUP = 0x202,
            MBUTTONDBLCLK = 0x209,
            MBUTTONDOWN = 0x207,
            MBUTTONUP = 520,
            MDIACTIVATE = 0x222,
            MDICASCADE = 0x227,
            MDICREATE = 0x220,
            MDIDESTROY = 0x221,
            MDIGETACTIVE = 0x229,
            MDIICONARRANGE = 0x228,
            MDIMAXIMIZE = 0x225,
            MDINEXT = 0x224,
            MDIREFRESHMENU = 0x234,
            MDIRESTORE = 0x223,
            MDISETMENU = 560,
            MDITILE = 550,
            MEASUREITEM = 0x2c,
            MENUCHAR = 0x120,
            MENUCOMMAND = 0x126,
            MENUDRAG = 0x123,
            MENUGETOBJECT = 0x124,
            MENURBUTTONUP = 290,
            MENUSELECT = 0x11f,
            MOUSEACTIVATE = 0x21,
            MOUSEFIRST = 0x200,
            MOUSEHOVER = 0x2a1,
            MOUSEHWHEEL = 0x20e,
            MOUSELAST = 0x20e,
            MOUSELEAVE = 0x2a3,
            MOUSEMOVE = 0x200,
            MOUSEWHEEL = 0x20a,
            MOVE = 3,
            MOVING = 0x216,
            NCACTIVATE = 0x86,
            NCCALCSIZE = 0x83,
            NCCREATE = 0x81,
            NCDESTROY = 130,
            NCHITTEST = 0x84,
            NCLBUTTONDBLCLK = 0xa3,
            NCLBUTTONDOWN = 0xa1,
            NCLBUTTONUP = 0xa2,
            NCMBUTTONDBLCLK = 0xa9,
            NCMBUTTONDOWN = 0xa7,
            NCMBUTTONUP = 0xa8,
            NCMOUSEHOVER = 0x2a0,
            NCMOUSELEAVE = 0x2a2,
            NCMOUSEMOVE = 160,
            NCPAINT = 0x85,
            NCRBUTTONDBLCLK = 0xa6,
            NCRBUTTONDOWN = 0xa4,
            NCRBUTTONUP = 0xa5,
            NCXBUTTONDBLCLK = 0xad,
            NCXBUTTONDOWN = 0xab,
            NCXBUTTONUP = 0xac,
            NEXTDLGCTL = 40,
            NEXTMENU = 0x213,
            NOTIFY = 0x4e,
            NOTIFYFORMAT = 0x55,
            NULL = 0,
            PAINT = 15,
            PAINTCLIPBOARD = 0x309,
            PAINTICON = 0x26,
            PALETTECHANGED = 0x311,
            PALETTEISCHANGING = 0x310,
            PARENTNOTIFY = 0x210,
            PASTE = 770,
            PENWINFIRST = 0x380,
            PENWINLAST = 0x38f,
            [Obsolete]
            POWER = 0x48,
            POWERBROADCAST = 0x218,
            PRINT = 0x317,
            PRINTCLIENT = 0x318,
            QUERYDRAGICON = 0x37,
            QUERYENDSESSION = 0x11,
            QUERYNEWPALETTE = 0x30f,
            QUERYOPEN = 0x13,
            QUERYUISTATE = 0x129,
            QUEUESYNC = 0x23,
            QUIT = 0x12,
            RBUTTONDBLCLK = 0x206,
            RBUTTONDOWN = 0x204,
            RBUTTONUP = 0x205,
            RENDERALLFORMATS = 0x306,
            RENDERFORMAT = 0x305,
            SETCURSOR = 0x20,
            SETFOCUS = 7,
            SETFONT = 0x30,
            SETHOTKEY = 50,
            SETICON = 0x80,
            SETREDRAW = 11,
            SETTEXT = 12,
            SETTINGCHANGE = 0x1a,
            SHOWWINDOW = 0x18,
            SIZE = 5,
            SIZECLIPBOARD = 0x30b,
            SIZING = 0x214,
            SPOOLERSTATUS = 0x2a,
            STYLECHANGED = 0x7d,
            STYLECHANGING = 0x7c,
            SYNCPAINT = 0x88,
            SYSCHAR = 0x106,
            SYSCOLORCHANGE = 0x15,
            SYSCOMMAND = 0x112,
            SYSDEADCHAR = 0x107,
            SYSKEYDOWN = 260,
            SYSKEYUP = 0x105,
            SYSTIMER = 280,
            TABLET_FIRST = 0x2c0,
            TABLET_LAST = 0x2df,
            TCARD = 0x52,
            THEMECHANGED = 0x31a,
            TIMECHANGE = 30,
            TIMER = 0x113,
            UNDO = 0x304,
            UNICHAR = 0x109,
            UNINITMENUPOPUP = 0x125,
            UPDATEUISTATE = 0x128,
            USER = 0x400,
            USERCHANGED = 0x54,
            VKEYTOITEM = 0x2e,
            VSCROLL = 0x115,
            VSCROLLCLIPBOARD = 0x30a,
            WINDOWPOSCHANGED = 0x47,
            WINDOWPOSCHANGING = 70,
            WININICHANGE = 0x1a,
            WTSSESSION_CHANGE = 0x2b1,
            XBUTTONDBLCLK = 0x20d,
            XBUTTONDOWN = 0x20b,
            XBUTTONUP = 0x20c
        }
    }
}

