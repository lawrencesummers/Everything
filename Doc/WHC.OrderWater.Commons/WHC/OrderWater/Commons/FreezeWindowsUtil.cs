namespace WHC.OrderWater.Commons
{
    using System;
    using System.Diagnostics;

    public class FreezeWindowsUtil : IDisposable
    {
        private uint uint_0;

        public FreezeWindowsUtil(IntPtr windowHandle)
        {
            NativeMethods.GetWindowThreadProcessId(windowHandle, out this.uint_0);
            FreezeThreads((int) this.uint_0);
        }

        public void Dispose()
        {
            UnfreezeThreads((int) this.uint_0);
        }

        public static void FreezeThreads(int intPID)
        {
            if ((intPID != 0) && (Process.GetCurrentProcess().Id != intPID))
            {
                Process processById = Process.GetProcessById(intPID);
                if (!string.IsNullOrEmpty(processById.ProcessName) && (processById.ProcessName != "explorer"))
                {
                    foreach (ProcessThread thread in processById.Threads)
                    {
                        NativeMethods.SuspendThread(NativeMethods.OpenThread(NativeMethods.ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id));
                    }
                }
            }
        }

        public static void UnfreezeThreads(int intPID)
        {
            if ((intPID != 0) && (Process.GetCurrentProcess().Id != intPID))
            {
                Process processById = Process.GetProcessById(intPID);
                if (!string.IsNullOrEmpty(processById.ProcessName))
                {
                    foreach (ProcessThread thread in processById.Threads)
                    {
                        NativeMethods.ResumeThread(NativeMethods.OpenThread(NativeMethods.ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id));
                    }
                }
            }
        }
    }
}

