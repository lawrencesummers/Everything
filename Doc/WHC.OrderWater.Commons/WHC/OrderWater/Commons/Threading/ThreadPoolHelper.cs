namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ThreadPoolHelper
    {
        private static List<WaitHandle> list_0;

        public static bool QueueUserWorkItem(WaitCallbackNew callback)
        {
            Class26 state = new Class26();
            state.method_1(callback);
            state.method_3(new AutoResetEvent(false));
            if (list_0 == null)
            {
                list_0 = new List<WaitHandle>();
            }
            list_0.Add(state.method_2());
            return ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolHelper.smethod_0), state);
        }

        public static bool QueueUserWorkItems(params WaitCallbackNew[] proc)
        {
            bool flag = true;
            foreach (WaitCallbackNew new2 in proc)
            {
                flag &= QueueUserWorkItem(new2);
            }
            return flag;
        }

        private static void smethod_0(object object_0)
        {
            Class26 class2 = object_0 as Class26;
            class2.method_0()();
            (class2.method_2() as AutoResetEvent).Set();
        }

        public static bool WaitAll()
        {
            return WaitHandle.WaitAll(list_0.ToArray());
        }

        public static int WaitAny()
        {
            return WaitHandle.WaitAny(list_0.ToArray());
        }

        private class Class26
        {
            [CompilerGenerated]
            private ThreadPoolHelper.WaitCallbackNew waitCallbackNew_0;
            [CompilerGenerated]
            private WaitHandle waitHandle_0;

            [CompilerGenerated]
            public ThreadPoolHelper.WaitCallbackNew method_0()
            {
                return this.waitCallbackNew_0;
            }

            [CompilerGenerated]
            public void method_1(ThreadPoolHelper.WaitCallbackNew waitCallbackNew_1)
            {
                this.waitCallbackNew_0 = waitCallbackNew_1;
            }

            [CompilerGenerated]
            public WaitHandle method_2()
            {
                return this.waitHandle_0;
            }

            [CompilerGenerated]
            public void method_3(WaitHandle waitHandle_1)
            {
                this.waitHandle_0 = waitHandle_1;
            }
        }

        public delegate void WaitCallbackNew();
    }
}

