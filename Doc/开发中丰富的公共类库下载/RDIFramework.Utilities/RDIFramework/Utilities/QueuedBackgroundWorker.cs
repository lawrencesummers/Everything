namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;

    [DefaultEvent("DoWork"), ToolboxBitmap(typeof(QueuedBackgroundWorker)), Description("A background worker with a work queue.")]
    public class QueuedBackgroundWorker : Component
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private Dictionary<object, bool> dictionary_0;
        private int int_0 = 5;
        private int int_1;
        private LinkedList<AsyncOperation>[] linkedList_0;
        private readonly object object_0 = new object();
        private RDIFramework.Utilities.ProcessingMode processingMode_0;
        private readonly SendOrPostCallback sendOrPostCallback_0;
        private Thread[] thread_0;

        [Description("Occurs when RunWorkerAsync is called."), Category("Behavior"), Browsable(true)]
        public event QueuedWorkerDoWorkEventHandler DoWork;

        [Category("Behavior"), Browsable(true), Description("Occurs when the background operation of an item has completed.")]
        public event RunQueuedWorkerCompletedEventHandler RunWorkerCompleted;

        public QueuedBackgroundWorker()
        {
            this.method_6();
            this.processingMode_0 = RDIFramework.Utilities.ProcessingMode.FIFO;
            this.int_1 = 5;
            this.method_3();
            this.dictionary_0 = new Dictionary<object, bool>();
            this.sendOrPostCallback_0 = new SendOrPostCallback(this.method_8);
        }

        public void CancelAsync()
        {
            lock (this.object_0)
            {
                this.method_4();
                Monitor.Pulse(this.object_0);
            }
        }

        public void CancelAsync(int priority)
        {
            if ((priority < 0) || (priority >= this.int_1))
            {
                int num = this.int_1 - 1;
                throw new ArgumentException("priority must be between 0 and " + num.ToString() + "  inclusive.", "priority");
            }
            lock (this.object_0)
            {
                this.method_5(priority);
                Monitor.Pulse(this.object_0);
            }
        }

        public void CancelAsync(object argument)
        {
            lock (this.object_0)
            {
                if (!this.dictionary_0.ContainsKey(argument))
                {
                    this.dictionary_0.Add(argument, false);
                    Monitor.Pulse(this.object_0);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!this.bool_2)
            {
                lock (this.object_0)
                {
                    if (!this.bool_0)
                    {
                        this.bool_0 = true;
                        this.method_4();
                        this.dictionary_0.Clear();
                        Monitor.Pulse(this.object_0);
                    }
                }
                this.bool_2 = true;
            }
        }

        public ApartmentState GetApartmentState()
        {
            return this.thread_0[0].GetApartmentState();
        }

        private bool method_0()
        {
            foreach (LinkedList<AsyncOperation> list in this.linkedList_0)
            {
                if (list.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void method_1(object object_1, int int_2)
        {
            AsyncOperation operation = AsyncOperationManager.CreateOperation(object_1);
            if (this.processingMode_0 == RDIFramework.Utilities.ProcessingMode.FIFO)
            {
                this.linkedList_0[int_2].AddLast(operation);
            }
            else
            {
                this.linkedList_0[int_2].AddFirst(operation);
            }
        }

        private Class7<AsyncOperation, int> method_2()
        {
            AsyncOperation operation = null;
            int num = 0;
            for (int i = this.int_1 - 1; i >= 0; i--)
            {
                if (this.linkedList_0[i].Count > 0)
                {
                    num = i;
                    operation = this.linkedList_0[i].First.Value;
                    this.linkedList_0[i].RemoveFirst();
                    break;
                }
            }
            return Class5.smethod_1<AsyncOperation, int>(operation, num);
        }

        private void method_3()
        {
            this.linkedList_0 = new LinkedList<AsyncOperation>[this.int_1];
            for (int i = 0; i < this.int_1; i++)
            {
                this.linkedList_0[i] = new LinkedList<AsyncOperation>();
            }
        }

        private void method_4()
        {
            for (int i = 0; i < this.int_1; i++)
            {
                this.method_5(i);
            }
        }

        private void method_5(int int_2)
        {
            while (this.linkedList_0[int_2].Count > 0)
            {
                this.linkedList_0[int_2].First.Value.OperationCompleted();
                this.linkedList_0[int_2].RemoveFirst();
            }
        }

        private void method_6()
        {
            this.thread_0 = new Thread[this.int_0];
            for (int i = 0; i < this.int_0; i++)
            {
                this.thread_0[i] = new Thread(new ThreadStart(this.method_9));
                this.thread_0[i].IsBackground = true;
            }
        }

        private bool method_7()
        {
            lock (this.object_0)
            {
                return this.bool_0;
            }
        }

        private void method_8(object object_1)
        {
            this.OnRunWorkerCompleted((QueuedWorkerCompletedEventArgs) object_1);
        }

        private void method_9()
        {
            // This item is obfuscated and can not be translated.
            object obj2;
        Label_0001:
            if (this.method_7())
            {
                return;
            }
            lock ((obj2 = this.object_0))
            {
                if (this.method_0())
                {
                    Monitor.Wait(this.object_0);
                }
            }
            bool flag2 = true;
            while (!flag2)
            {
                if (0 == 0)
                {
                    goto Label_0001;
                }
                AsyncOperation operation = null;
                object key = null;
                int priority = 0;
                lock ((obj2 = this.object_0))
                {
                    Class7<AsyncOperation, int> class2 = this.method_2();
                    operation = class2.method_0();
                    priority = class2.method_1();
                    if (operation != null)
                    {
                        key = operation.UserSuppliedState;
                    }
                    if ((key != null) && this.dictionary_0.ContainsKey(key))
                    {
                        key = null;
                    }
                }
                if (key != null)
                {
                    Exception error = null;
                    QueuedWorkerDoWorkEventArgs e = new QueuedWorkerDoWorkEventArgs(key, priority);
                    try
                    {
                        this.OnDoWork(e);
                    }
                    catch (Exception exception2)
                    {
                        error = exception2;
                    }
                    QueuedWorkerCompletedEventArgs arg = new QueuedWorkerCompletedEventArgs(key, e.Result, priority, error, e.Cancel);
                    if (!this.method_7())
                    {
                        operation.PostOperationCompleted(this.sendOrPostCallback_0, arg);
                    }
                }
                else if (operation != null)
                {
                    operation.OperationCompleted();
                }
                lock ((obj2 = this.object_0))
                {
                    flag2 = !this.method_0();
                    continue;
                }
                break;
            }
            goto Label_004E;
        }

        protected virtual void OnDoWork(QueuedWorkerDoWorkEventArgs e)
        {
            if (this.queuedWorkerDoWorkEventHandler_0 != null)
            {
                this.queuedWorkerDoWorkEventHandler_0(this, e);
            }
        }

        protected virtual void OnRunWorkerCompleted(QueuedWorkerCompletedEventArgs e)
        {
            if (this.runQueuedWorkerCompletedEventHandler_0 != null)
            {
                this.runQueuedWorkerCompletedEventHandler_0(this, e);
            }
        }

        public void RunWorkerAsync()
        {
            this.RunWorkerAsync(null, 0);
        }

        public void RunWorkerAsync(object argument)
        {
            this.RunWorkerAsync(argument, 0);
        }

        public void RunWorkerAsync(object argument, int priority)
        {
            if ((priority < 0) || (priority >= this.int_1))
            {
                int num = this.int_1 - 1;
                throw new ArgumentException("priority must be between 0 and " + num.ToString() + "  inclusive.", "priority");
            }
            if (!this.bool_1)
            {
                for (int i = 0; i < this.int_0; i++)
                {
                    this.thread_0[i].Start();
                    while (!this.thread_0[i].IsAlive)
                    {
                    }
                }
                this.bool_1 = true;
            }
            lock (this.object_0)
            {
                this.method_1(argument, priority);
                Monitor.Pulse(this.object_0);
            }
        }

        public void SetApartmentState(ApartmentState state)
        {
            for (int i = 0; i < this.int_0; i++)
            {
                this.thread_0[i].SetApartmentState(state);
            }
        }

        [Category("Behavior"), Browsable(true), Description("Gets or sets a value indicating whether or not the worker thread is a background thread.")]
        public bool IsBackground
        {
            get
            {
                return this.thread_0[0].IsBackground;
            }
            set
            {
                for (int i = 0; i < this.int_0; i++)
                {
                    this.thread_0[i].IsBackground = value;
                }
            }
        }

        [Browsable(true), DefaultValue(5), Category("Behaviour")]
        public int PriorityQueues
        {
            get
            {
                return this.int_1;
            }
            set
            {
                if (this.bool_1)
                {
                    throw new ThreadStateException("The thread has already been started.");
                }
                this.int_1 = value;
                this.method_3();
            }
        }

        [Browsable(true), DefaultValue(typeof(RDIFramework.Utilities.ProcessingMode), "FIFO"), Category("Behaviour")]
        public RDIFramework.Utilities.ProcessingMode ProcessingMode
        {
            get
            {
                return this.processingMode_0;
            }
            set
            {
                if (this.bool_1)
                {
                    throw new ThreadStateException("The thread has already been started.");
                }
                this.processingMode_0 = value;
                this.method_3();
            }
        }

        [Browsable(false), Category("Behavior"), Description("Determines whether the QueuedBackgroundWorker started working.")]
        public bool Started
        {
            get
            {
                return this.bool_1;
            }
        }

        [Browsable(true), Category("Behaviour"), DefaultValue(5)]
        public int Threads
        {
            get
            {
                return this.int_0;
            }
            set
            {
                if (this.bool_1)
                {
                    throw new ThreadStateException("The thread has already been started.");
                }
                this.int_0 = value;
                this.method_6();
            }
        }
    }
}

