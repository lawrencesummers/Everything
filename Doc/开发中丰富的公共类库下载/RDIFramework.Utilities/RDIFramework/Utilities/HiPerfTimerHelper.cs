namespace RDIFramework.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class HiPerfTimerHelper
    {
        private long long_0 = 0L;
        private long long_1 = 0L;
        private long long_2;

        public HiPerfTimerHelper()
        {
            if (!QueryPerformanceFrequency(out this.long_2))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long long_3);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long long_3);
        public void Start()
        {
            Thread.Sleep(0);
            QueryPerformanceCounter(out this.long_0);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out this.long_1);
        }

        public double Duration
        {
            get
            {
                return (((double) (this.long_1 - this.long_0)) / ((double) this.long_2));
            }
        }
    }
}

