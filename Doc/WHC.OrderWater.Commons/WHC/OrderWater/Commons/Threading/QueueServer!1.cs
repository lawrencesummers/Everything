namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class QueueServer<T> : DisposableObject
    {
        private Action<T> action_0;
        private bool bool_0;
        private bool bool_1;
        private Queue<T> queue_0;
        private Thread thread_0;

        public event Action<T> ProcessItem
        {
            add
            {
                Action<T> action2;
                Action<T> action = this.action_0;
                do
                {
                    action2 = action;
                    Action<T> action3 = (Action<T>) Delegate.Combine(action2, value);
                    action = Interlocked.CompareExchange<Action<T>>(ref this.action_0, action3, action2);
                }
                while (action != action2);
            }
            remove
            {
                Action<T> action2;
                Action<T> action = this.action_0;
                do
                {
                    action2 = action;
                    Action<T> action3 = (Action<T>) Delegate.Remove(action2, value);
                    action = Interlocked.CompareExchange<Action<T>>(ref this.action_0, action3, action2);
                }
                while (action != action2);
            }
        }

        public QueueServer()
        {
            this.thread_0 = null;
            this.queue_0 = new Queue<T>();
            this.bool_0 = false;
            this.bool_1 = false;
        }

        public void ClearItems()
        {
            lock (this.queue_0)
            {
                this.queue_0.Clear();
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.bool_1)
                {
                    this.ClearItems();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public void EnqueueItem(T item)
        {
            lock (this.queue_0)
            {
                this.queue_0.Enqueue(item);
            }
            if (!((this.thread_0 != null) && this.thread_0.IsAlive))
            {
                this.method_0();
                this.thread_0.Start();
            }
        }

        private void method_0()
        {
            this.thread_0 = new Thread(new ThreadStart(this.method_1));
            this.thread_0.IsBackground = this.bool_0;
        }

        private void method_1()
        {
            T item = default(T);
            while (true)
            {
                lock (this.queue_0)
                {
                    if (this.queue_0.Count > 0)
                    {
                        item = this.queue_0.Dequeue();
                    }
                    else
                    {
                        return;
                    }
                }
                try
                {
                    this.OnProcessItem(item);
                }
                catch
                {
                }
            }
        }

        protected virtual void OnProcessItem(T item)
        {
            if (this.action_0 != null)
            {
                this.action_0(item);
            }
        }

        public bool IsBackground
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = true;
                if ((this.thread_0 != null) && this.thread_0.IsAlive)
                {
                    this.thread_0.IsBackground = this.bool_0;
                }
            }
        }

        public T[] Items
        {
            get
            {
                lock (this.queue_0)
                {
                    return this.queue_0.ToArray();
                }
            }
        }

        public int QueueCount
        {
            get
            {
                lock (this.queue_0)
                {
                    return this.queue_0.Count;
                }
            }
        }
    }
}

