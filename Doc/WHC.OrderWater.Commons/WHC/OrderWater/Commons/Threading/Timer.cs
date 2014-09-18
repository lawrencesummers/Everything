namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Timer
    {
        private volatile bool bool_0;
        private volatile bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        private EventHandler eventHandler_0;
        [CompilerGenerated]
        private int int_0;
        private readonly Timer timer_0;

        public event EventHandler Elapsed
        {
            add
            {
                EventHandler handler2;
                EventHandler handler = this.eventHandler_0;
                do
                {
                    handler2 = handler;
                    EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler handler = this.eventHandler_0;
                do
                {
                    handler2 = handler;
                    EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public Timer(int period) : this(period, false)
        {
        }

        public Timer(int period, bool runOnStart)
        {
            this.Period = period;
            this.RunOnStart = runOnStart;
            this.timer_0 = new Timer(new TimerCallback(this.EtJrYdvXxt), null, -1, -1);
        }

        private void EtJrYdvXxt(object object_0)
        {
            Timer timer;
            lock ((timer = this.timer_0))
            {
                if (!(this.bool_0 && !this.bool_1))
                {
                    return;
                }
                this.timer_0.Change(-1, -1);
                this.bool_1 = true;
            }
            try
            {
                if (this.eventHandler_0 != null)
                {
                    this.eventHandler_0(this, new EventArgs());
                }
            }
            catch
            {
            }
            finally
            {
                lock ((timer = this.timer_0))
                {
                    this.bool_1 = false;
                    if (this.bool_0)
                    {
                        this.timer_0.Change(this.Period, -1);
                    }
                    Monitor.Pulse(this.timer_0);
                }
            }
        }

        public void Start()
        {
            this.bool_0 = true;
            this.timer_0.Change(this.RunOnStart ? 0 : this.Period, -1);
        }

        public void Stop()
        {
            lock (this.timer_0)
            {
                this.bool_0 = false;
                this.timer_0.Change(-1, -1);
            }
        }

        public void WaitToStop()
        {
            lock (this.timer_0)
            {
                while (this.bool_1)
                {
                    Monitor.Wait(this.timer_0);
                }
            }
        }

        public int Period
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }

        public bool RunOnStart
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }
    }
}

