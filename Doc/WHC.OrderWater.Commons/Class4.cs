using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class Class4
{
    public const int int_0 = 0x400;
    public const int int_1 = 0x40000000;
    public const int int_10 = 0x42d;
    public const int int_11 = 0x419;
    public const int int_12 = 0x433;
    public const int int_13 = 0x40e;
    public const int int_14 = 0x429;
    public const int int_15 = 0x42a;
    public const int int_16 = 0x42b;
    public const int int_17 = 0x41e;
    public const int int_18 = 0x440;
    public const int int_19 = 0x441;
    public const int int_2 = 0x10000000;
    public const int int_3 = 2;
    public const int int_4 = 4;
    public const int int_5 = 0x40a;
    public const int int_6 = 0x40b;
    public const int int_7 = 0x405;
    public const int int_8 = 0x432;
    public const int int_9 = 0x434;

    [DllImport("avicap32.dll")]
    public static extern IntPtr capCreateCaptureWindowA(byte[] byte_0, int int_20, int int_21, int int_22, int int_23, int int_24, IntPtr intptr_0, int int_25);
    [DllImport("avicap32.dll")]
    public static extern bool capGetDriverDescriptionA(short short_0, byte[] byte_0, int int_20, byte[] byte_1, int int_21);
    [DllImport("avicap32.dll")]
    public static extern int capGetVideoFormat(IntPtr intptr_0, IntPtr intptr_1, int int_20);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr intptr_0, int int_20, bool bool_0, int int_21);
    [DllImport("User32.dll", EntryPoint="SendMessage")]
    public static extern bool SendMessage_1(IntPtr intptr_0, int int_20, short short_0, int int_21);
    [DllImport("User32.dll", EntryPoint="SendMessage")]
    public static extern bool SendMessage_2(IntPtr intptr_0, int int_20, short short_0, Delegate0 delegate0_0);
    [DllImport("User32.dll", EntryPoint="SendMessage")]
    public static extern bool SendMessage_3(IntPtr intptr_0, int int_20, int int_21, ref Struct6 struct6_0);
    [DllImport("User32.dll", EntryPoint="SendMessage")]
    public static extern bool SendMessage_4(IntPtr intptr_0, int int_20, int int_21, ref Struct7 struct7_0);
    [DllImport("User32.dll")]
    public static extern int SetWindowPos(IntPtr intptr_0, int int_20, int int_21, int int_22, int int_23, int int_24, int int_25);
    public static object smethod_0(IntPtr intptr_0, ValueType valueType_0)
    {
        return Marshal.PtrToStructure(intptr_0, valueType_0.GetType());
    }

    public static object smethod_1(int int_20, ValueType valueType_0)
    {
        return smethod_0(new IntPtr(int_20), valueType_0);
    }

    public static void smethod_2(IntPtr intptr_0, byte[] byte_0)
    {
        Marshal.Copy(intptr_0, byte_0, 0, byte_0.Length);
    }

    public static void smethod_3(int int_20, byte[] byte_0)
    {
        smethod_2(new IntPtr(int_20), byte_0);
    }

    public static int smethod_4(object object_0)
    {
        return Marshal.SizeOf(object_0);
    }

    public delegate void Delegate0(IntPtr lwnd, IntPtr lpVHdr);

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct4
    {
        [MarshalAs(UnmanagedType.I4)]
        public int int_0;
        [MarshalAs(UnmanagedType.I4)]
        public int int_1;
        [MarshalAs(UnmanagedType.I4)]
        public int int_2;
        [MarshalAs(UnmanagedType.I4)]
        public int int_3;
        [MarshalAs(UnmanagedType.I4)]
        public int int_4;
        [MarshalAs(UnmanagedType.I4)]
        public int int_5;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
        public int[] int_6;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct5
    {
        [MarshalAs(UnmanagedType.I4)]
        public int int_0;
        [MarshalAs(UnmanagedType.I4)]
        public int int_1;
        [MarshalAs(UnmanagedType.I4)]
        public int int_2;
        [MarshalAs(UnmanagedType.I2)]
        public short short_0;
        [MarshalAs(UnmanagedType.I2)]
        public short short_1;
        [MarshalAs(UnmanagedType.I4)]
        public int int_3;
        [MarshalAs(UnmanagedType.I4)]
        public int int_4;
        [MarshalAs(UnmanagedType.I4)]
        public int int_5;
        [MarshalAs(UnmanagedType.I4)]
        public int int_6;
        [MarshalAs(UnmanagedType.I4)]
        public int int_7;
        [MarshalAs(UnmanagedType.I4)]
        public int int_8;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct6
    {
        [MarshalAs(UnmanagedType.Struct)]
        public Class4.Struct5 struct5_0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x400)]
        public int[] int_0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct7
    {
        [MarshalAs(UnmanagedType.U2)]
        public ushort ushort_0;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bool_0;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bool_1;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bool_2;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bool_3;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bool_4;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bool_5;
        [MarshalAs(UnmanagedType.I4)]
        public int int_0;
        [MarshalAs(UnmanagedType.I4)]
        public int int_1;
        [MarshalAs(UnmanagedType.I4)]
        public int int_2;
        [MarshalAs(UnmanagedType.I4)]
        public int int_3;
    }
}

