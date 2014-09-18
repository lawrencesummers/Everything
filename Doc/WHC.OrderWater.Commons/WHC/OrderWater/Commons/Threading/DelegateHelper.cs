namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Threading;

    public static class DelegateHelper
    {
        private static WaitCallback waitCallback_0 = new WaitCallback(DelegateHelper.smethod_0);

        public static WorkItemStatus AbortDelegate(WorkItem target)
        {
            return AbortableThreadPool.Cancel(target, true);
        }

        public static WorkItem InvokeDelegate(Delegate target)
        {
            return AbortableThreadPool.QueueUserWorkItem(waitCallback_0, new Class28(target, null));
        }

        public static WorkItem InvokeDelegate(Delegate target, params object[] args)
        {
            return AbortableThreadPool.QueueUserWorkItem(waitCallback_0, new Class28(target, args));
        }

        private static void smethod_0(Class28 class28_0)
        {
            Class28 class2 = class28_0;
            class2.delegate_0.DynamicInvoke(class2.object_0);
        }
    }
}

