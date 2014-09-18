namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class ScreenCapture
    {
        private System.Drawing.Imaging.ImageFormat imageFormat_0 = System.Drawing.Imaging.ImageFormat.Png;
        private string string_0 = @"C:\CaptureImages";
        private string string_1 = "";

        public void AutoCaptureScreen()
        {
            DirectoryUtil.AssertDirExist(this.ImageSavePath);
            if (DirectoryUtil.IsExistDirectory(this.ImageSavePath))
            {
                string directoryPath = Path.Combine(this.ImageSavePath, DateTime.Now.ToString("yyyy-MM-dd"));
                DirectoryUtil.CreateDirectory(directoryPath);
                string str3 = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
                string filename = Path.Combine(directoryPath, str3);
                if (!string.IsNullOrEmpty(this.ImageExtension.Trim(new char[] { '.' })))
                {
                    filename = filename + "." + this.ImageExtension.Trim(new char[] { '.' });
                }
                else
                {
                    filename = filename + "." + this.ImageFormat.ToString();
                }
                this.CaptureScreenToFile(filename, this.ImageFormat);
            }
        }

        public Image CaptureScreen()
        {
            return this.CaptureWindow(Class14.GetDesktopWindow());
        }

        public void CaptureScreenToFile(string filename, System.Drawing.Imaging.ImageFormat format)
        {
            this.CaptureScreen().Save(filename, format);
        }

        public Image CaptureWindow(IntPtr handle)
        {
            IntPtr windowDC = Class14.GetWindowDC(handle);
            Class14.Struct16 struct2 = new Class14.Struct16();
            Class14.GetWindowRect(handle, ref struct2);
            int num = struct2.int_2 - struct2.int_0;
            int num2 = struct2.int_3 - struct2.int_1;
            IntPtr ptr2 = Class13.CreateCompatibleDC(windowDC);
            IntPtr ptr3 = Class13.CreateCompatibleBitmap(windowDC, num, num2);
            IntPtr ptr4 = Class13.SelectObject(ptr2, ptr3);
            Class13.BitBlt(ptr2, 0, 0, num, num2, windowDC, 0, 0, 0xcc0020);
            Class13.SelectObject(ptr2, ptr4);
            Class13.DeleteDC(ptr2);
            Class14.ReleaseDC(handle, windowDC);
            Image image = Image.FromHbitmap(ptr3);
            Class13.DeleteObject(ptr3);
            return image;
        }

        public void CaptureWindowToFile(IntPtr handle, string filename, System.Drawing.Imaging.ImageFormat format)
        {
            this.CaptureWindow(handle).Save(filename, format);
        }

        public string ImageExtension
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public System.Drawing.Imaging.ImageFormat ImageFormat
        {
            get
            {
                return this.imageFormat_0;
            }
            set
            {
                this.imageFormat_0 = value;
            }
        }

        public string ImageSavePath
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

        private class Class13
        {
            public const int int_0 = 0xcc0020;

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr intptr_0, int int_1, int int_2, int int_3, int int_4, IntPtr intptr_1, int int_5, int int_6, int int_7);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr intptr_0, int int_1, int int_2);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr intptr_0);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr intptr_0);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr intptr_0);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr intptr_0, IntPtr intptr_1);
        }

        private class Class14
        {
            [DllImport("User32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("User32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr intptr_0);
            [DllImport("User32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr intptr_0, ref Struct16 struct16_0);
            [DllImport("User32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr intptr_0, IntPtr intptr_1);

            [StructLayout(LayoutKind.Sequential)]
            public struct Struct16
            {
                public int int_0;
                public int int_1;
                public int int_2;
                public int int_3;
            }
        }
    }
}

