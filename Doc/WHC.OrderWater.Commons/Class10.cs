using System;
using System.Runtime.InteropServices;
using System.Security;

[SuppressUnmanagedCodeSecurity]
internal static class Class10
{
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
    public static extern bool GetVersionEx([In, Out] Class11 class11_0);
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", EntryPoint="GetVersionEx", CharSet=CharSet.Auto, SetLastError=true)]
    public static extern bool GetVersionEx_1([In, Out] Class12 class12_0);
    [DllImport("User32.dll")]
    public static extern bool IsIconic(IntPtr intptr_0);
    [DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    public static extern bool IsWindowEnabled(IntPtr intptr_0);
    [DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    public static extern bool ShowWindow(IntPtr intptr_0, int int_0);

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public class Class11
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public Class10.Enum2 enum2_0;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x80)]
        public string string_0;
        public Class11()
        {
            this.int_0 = Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public class Class12
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public Class10.Enum2 enum2_0;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x80)]
        public string string_0;
        public ushort ushort_0;
        public ushort ushort_1;
        public Class10.Enum3 enum3_0;
        public Class10.Enum4 enum4_0;
        public byte byte_0;
        public Class12()
        {
            this.int_0 = Marshal.SizeOf(this);
        }
    }

    public enum Enum2
    {
    }

    [Flags]
    public enum Enum3 : ushort
    {
    }

    public enum Enum4 : byte
    {
    }
}

