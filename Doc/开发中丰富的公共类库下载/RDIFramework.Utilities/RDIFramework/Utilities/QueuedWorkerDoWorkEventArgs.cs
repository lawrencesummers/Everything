namespace RDIFramework.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class QueuedWorkerDoWorkEventArgs : DoWorkEventArgs
    {
        [CompilerGenerated]
        private int int_0;

        public QueuedWorkerDoWorkEventArgs(object argument, int priority) : base(argument)
        {
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
    }
}

