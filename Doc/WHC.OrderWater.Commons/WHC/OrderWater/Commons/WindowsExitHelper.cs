namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.InteropServices;

    public class WindowsExitHelper
    {
        internal const int int_0 = 2;
        internal const int int_1 = 8;
        internal const int int_2 = 0x20;
        internal const int int_3 = 0;
        internal const int int_4 = 1;
        internal const int int_5 = 2;
        internal const int int_6 = 4;
        internal const int int_7 = 8;
        internal const int int_8 = 0x10;
        private static readonly IntPtr intptr_0 = new IntPtr(0xffff);
        internal const string string_0 = "SeShutdownPrivilege";
        private const uint uint_0 = 0x112;
        private const uint uint_1 = 0xf170;

        [DllImport("advapi32.dll", SetLastError=true, ExactSpelling=true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr intptr_1, bool bool_0, ref Struct19 struct19_0, int int_9, IntPtr intptr_2, IntPtr intptr_3);
        public static void CloseMonitor()
        {
            SendMessage(intptr_0, 0x112, 0xf170, 2);
        }

        [DllImport("User32.dll", SetLastError=true, ExactSpelling=true)]
        internal static extern bool ExitWindowsEx(int int_9, int int_10);
        [DllImport("kernel32.dll", ExactSpelling=true)]
        internal static extern IntPtr GetCurrentProcess();
        public static void Lock()
        {
            LockWorkStation();
        }

        [DllImport("User32.dll")]
        private static extern void LockWorkStation();
        public static void LogoOff()
        {
            smethod_0(4);
        }

        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern bool LookupPrivilegeValue(string string_1, string string_2, ref long long_0);
        [DllImport("advapi32.dll", SetLastError=true, ExactSpelling=true)]
        internal static extern bool OpenProcessToken(IntPtr intptr_1, int int_9, ref IntPtr intptr_2);
        public static void PowerOff()
        {
            smethod_0(12);
        }

        public static void Reboot()
        {
            smethod_0(6);
        }

        [DllImport("user32")]
        private static extern IntPtr SendMessage(IntPtr intptr_1, uint uint_2, uint uint_3, int int_9);
        private static void smethod_0(int int_9)
        {
            Struct19 struct2;
            IntPtr currentProcess = GetCurrentProcess();
            IntPtr zero = IntPtr.Zero;
            OpenProcessToken(currentProcess, 40, ref zero);
            struct2.int_0 = 1;
            struct2.long_0 = 0;
            struct2.int_1 = 2;
            LookupPrivilegeValue(null, "SeShutdownPrivilege", ref struct2.long_0);
            AdjustTokenPrivileges(zero, false, ref struct2, 0, IntPtr.Zero, IntPtr.Zero);
            ExitWindowsEx(int_9, 0);
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct Struct19
        {
            public int int_0;
            public long long_0;
            public int int_1;
        }
    }
}

