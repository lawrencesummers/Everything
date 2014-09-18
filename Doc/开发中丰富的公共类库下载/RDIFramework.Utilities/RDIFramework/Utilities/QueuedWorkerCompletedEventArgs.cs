namespace RDIFramework.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class QueuedWorkerCompletedEventArgs : AsyncCompletedEventArgs
    {
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private object object_0;

        public QueuedWorkerCompletedEventArgs(object argument, object result, int priority, Exception error, bool cancelled) : base(error, cancelled, argument)
        {
            this.Result = result;
            this.Priority = priority;
        }

        public int Priority
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            private set
            {
                this.int_0 = value;
            }
        }

        public object Result
        {
            [CompilerGenerated]
            get
            {
                return this.object_0;
            }
            [CompilerGenerated]
            private set
            {
                this.object_0 = value;
            }
        }
    }
}

