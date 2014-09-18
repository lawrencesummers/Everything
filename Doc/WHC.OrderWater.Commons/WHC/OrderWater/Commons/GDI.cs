namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public static class GDI
    {
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nReghtRect, int nBottomRect);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nReghtRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdiplus.dll", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        public static extern int GdipDisposeImage(HandleRef image);
        [DllImport("gdiplus.dll", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        public static extern int GdipGetImageType(HandleRef image, out int type);
        [DllImport("gdiplus.dll", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        public static extern int GdipImageForceValidation(HandleRef image);
        [DllImport("gdiplus.dll", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
        public static extern int GdipLoadImageFromFile(string filename, out IntPtr image);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        public enum TernaryRasterOperations : uint
        {
            BLACKNESS = 0x42,
            DSTINVERT = 0x550009,
            MERGECOPY = 0xc000ca,
            MERGEPAINT = 0xbb0226,
            NOTSRCCOPY = 0x330008,
            NOTSRCERASE = 0x1100a6,
            PATCOPY = 0xf00021,
            PATINVERT = 0x5a0049,
            PATPAINT = 0xfb0a09,
            SRCAND = 0x8800c6,
            SRCCOPY = 0xcc0020,
            SRCERASE = 0x440328,
            SRCINVERT = 0x660046,
            SRCPAINT = 0xee0086,
            WHITENESS = 0xff0062
        }
    }
}

