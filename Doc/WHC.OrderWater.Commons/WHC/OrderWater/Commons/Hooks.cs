namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class Hooks
    {
        internal const int int_0 = 5;
        internal const int int_1 = 0x100;
        internal const int int_10 = 0x207;
        internal const int int_11 = 0x20b;
        internal const int int_12 = 0x202;
        internal const int int_13 = 0x205;
        internal const int int_14 = 520;
        internal const int int_15 = 0x20c;
        internal const int int_16 = 1;
        internal const int int_17 = 2;
        internal const int int_2 = 0x101;
        internal const int int_3 = 0x102;
        internal const int int_4 = 260;
        internal const int int_5 = 0x105;
        internal const int int_6 = 0x200;
        internal const int int_7 = 0x20a;
        internal const int int_8 = 0x201;
        internal const int int_9 = 0x204;

        [DllImport("User32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern IntPtr CallNextHookEx(IntPtr intptr_0, int int_18, IntPtr intptr_1, IntPtr intptr_2);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern IntPtr GetModuleHandle(string string_0);
        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto)]
        internal static extern IntPtr SetWindowsHookEx(int int_18, HookProc hookProc_0, IntPtr intptr_0, int int_19);
        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Auto)]
        internal static extern bool UnhookWindowsHookEx(IntPtr intptr_0);

        internal enum Enum7
        {
        }

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Struct25
        {
            public Hooks.Struct26 struct26_0;
            public int int_0;
            public int int_1;
            public int int_2;
            public IntPtr intptr_0;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Struct26
        {
            public int int_0;
            public int int_1;
        }
    }
}

