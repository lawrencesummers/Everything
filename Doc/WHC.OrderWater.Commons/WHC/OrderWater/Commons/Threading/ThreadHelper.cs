namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Globalization;
    using System.Threading;
    using WHC.OrderWater.Commons;

    public class ThreadHelper
    {
        private static CultureInfo cultureInfo_0;

        public static bool Queue(WaitCallback callBack, string threadName, ThreadPriority priority)
        {
            return Queue(callBack, threadName, null, priority);
        }

        public static bool Queue(WaitCallback callBack, string threadName, object state, ThreadPriority priority)
        {
            WaitCallback callback = delegate (object _state) {
                SetThreadName(threadName);
                SetThreadPriority(priority);
                callBack(_state);
            };
            return ThreadPool.QueueUserWorkItem(callback, state);
        }

        public static void SetMainThreadUICulture(string cultureName)
        {
            try
            {
                LogTextHelper.Info(string.Format("UICulture = {0}", cultureName));
                CultureInfo info = new CultureInfo(cultureName);
                cultureInfo_0 = info;
                Thread.CurrentThread.CurrentUICulture = info;
            }
            catch (Exception exception)
            {
                LogTextHelper.Error(string.Format("Error setting UICulture: {0}", cultureName), exception);
            }
        }

        public static void SetThreadName(string name)
        {
            if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = "SVNM_" + name.PadRight(10);
                if (cultureInfo_0 != null)
                {
                    Thread.CurrentThread.CurrentUICulture = cultureInfo_0;
                }
            }
        }

        public static void SetThreadPriority(ThreadPriority priority)
        {
            if (Thread.CurrentThread.Priority != priority)
            {
                Thread.CurrentThread.Priority = priority;
            }
        }

        public static void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public static void Sleep(TimeSpan timeOut)
        {
            Thread.Sleep(timeOut);
        }
    }
}

