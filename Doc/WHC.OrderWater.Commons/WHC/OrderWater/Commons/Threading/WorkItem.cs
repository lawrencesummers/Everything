namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Threading;

    public sealed class WorkItem
    {
        private ExecutionContext executionContext_0;
        private object object_0;
        private WaitCallback waitCallback_0;

        internal WorkItem(WaitCallback callback, object state, ExecutionContext context)
        {
            this.waitCallback_0 = callback;
            this.object_0 = state;
            this.executionContext_0 = context;
        }

        public WaitCallback Callback
        {
            get
            {
                return this.waitCallback_0;
            }
        }

        public ExecutionContext Context
        {
            get
            {
                return this.executionContext_0;
            }
        }

        public object State
        {
            get
            {
                return this.object_0;
            }
        }
    }
}

