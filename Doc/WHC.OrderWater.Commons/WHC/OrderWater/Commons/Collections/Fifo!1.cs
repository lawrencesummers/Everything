namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public sealed class Fifo<T>
    {
        private AutoResetEvent autoResetEvent_0;
        private AutoResetEvent autoResetEvent_1;
        private int int_0;
        private object object_0;
        private object object_1;
        private object object_2;
        private Queue<T> queue_0;

        public Fifo()
        {
            this.queue_0 = null;
            this.int_0 = 0x7ffffffe;
            this.autoResetEvent_0 = new AutoResetEvent(true);
            this.autoResetEvent_1 = new AutoResetEvent(true);
            this.object_0 = new object();
            this.object_1 = new object();
            this.object_2 = new object();
            this.queue_0 = new Queue<T>();
        }

        public Fifo(int capacity)
        {
            this.queue_0 = null;
            this.int_0 = 0x7ffffffe;
            this.autoResetEvent_0 = new AutoResetEvent(true);
            this.autoResetEvent_1 = new AutoResetEvent(true);
            this.object_0 = new object();
            this.object_1 = new object();
            this.object_2 = new object();
            this.queue_0 = new Queue<T>(capacity);
        }

        public Fifo(int MaxCount, int capacity) : this(capacity)
        {
            if ((MaxCount > 1) || (MaxCount < 0x7fffffff))
            {
                this.int_0 = MaxCount;
            }
        }

        public void Append(T obj)
        {
            lock (this.object_1)
            {
                while (this.queue_0.Count >= this.int_0)
                {
                    this.autoResetEvent_0.WaitOne(-1, false);
                }
                lock (this.object_0)
                {
                    this.queue_0.Enqueue(obj);
                    this.autoResetEvent_1.Set();
                }
            }
        }

        public T Pop()
        {
            T local2;
            lock (this.object_2)
            {
                while (this.queue_0.Count <= 0)
                {
                    this.autoResetEvent_1.WaitOne(-1, false);
                }
                lock (this.object_0)
                {
                    T local = this.queue_0.Dequeue();
                    this.autoResetEvent_0.Set();
                    local2 = local;
                }
            }
            return local2;
        }

        public void ResetMaxCount(int MaxCount)
        {
            if ((MaxCount > 1) || (MaxCount < 0x7fffffff))
            {
                this.int_0 = MaxCount;
            }
        }

        public int Count
        {
            get
            {
                return this.queue_0.Count;
            }
        }

        public int MaxCount
        {
            get
            {
                return this.int_0;
            }
        }
    }
}

