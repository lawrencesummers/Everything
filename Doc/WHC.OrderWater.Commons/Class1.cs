using System;
using System.Runtime.InteropServices;

internal class Class1
{
    public const uint uint_0 = 1;
    public const uint uint_1 = 2;
    public const uint uint_10 = 0x2000;
    public const uint uint_11 = 0x4000;
    public const uint uint_12 = 0x8000;
    public const uint uint_13 = 0x100;
    public const uint uint_14 = 0x200;
    public const uint uint_15 = 0x400;
    public const uint uint_16 = 0x800;
    public const uint uint_17 = 0x1000;
    public const uint uint_18 = 0x2000;
    public const uint uint_19 = 0x4000;
    public const uint uint_2 = 4;
    public const uint uint_20 = 0x8000;
    public const uint uint_21 = 0x10000;
    public const uint uint_22 = 0x20000;
    public const uint uint_23 = 0;
    public const uint uint_24 = 1;
    public const uint uint_25 = 2;
    public const uint uint_26 = 4;
    public const uint uint_27 = 8;
    public const uint uint_28 = 0x10;
    public const uint uint_29 = 0x20;
    public const uint uint_3 = 8;
    public const uint uint_30 = 0x40;
    public const uint uint_31 = 0x10;
    public const uint uint_32 = 0x80;
    public const uint uint_4 = 0x10;
    public const uint uint_5 = 0x20;
    public const uint uint_6 = 0x40;
    public const uint uint_7 = 80;
    public const uint uint_8 = 0x80;
    public const uint uint_9 = 0x1000;

    [DllImport("shell32.dll")]
    public static extern IntPtr SHGetFileInfo(string string_0, uint uint_33, ref Struct3 struct3_0, uint uint_34, uint uint_35);

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct0
    {
        public ushort ushort_0;
        [MarshalAs(UnmanagedType.LPArray)]
        public byte[] byte_0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct1
    {
        public Class1.Struct0 struct0_0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct2
    {
        public IntPtr intptr_0;
        public IntPtr intptr_1;
        public IntPtr intptr_2;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string string_0;
        public uint uint_0;
        public IntPtr intptr_3;
        public int int_0;
        public IntPtr intptr_4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct3
    {
        public IntPtr intptr_0;
        public int int_0;
        public uint uint_0;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
        public string string_0;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
        public string string_1;
    }
}

