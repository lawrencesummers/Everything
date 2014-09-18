namespace RDIFramework.Utilities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Timers;

    public sealed class SimpleTimerProvider<T>
    {
        private Action<T, ElapsedEventArgs> action_0;
        private Action<T, System.Timers.Timer> action_1;
        private Action<T, System.Timers.Timer, ElapsedEventArgs> action_2;
        private Action<T> ffhdejsAf;
        [CompilerGenerated]
        private T gparam_0;
        [CompilerGenerated]
        private System.Timers.Timer timer_0;

        public SimpleTimerProvider(T obj, double interval) : this(obj, interval, true)
        {
        }

        public SimpleTimerProvider(T obj, double interval, bool autoReset)
        {
            this.Object = obj;
            if (interval <= 0.0)
            {
                interval = 100.0;
            }
            System.Timers.Timer timer = new System.Timers.Timer {
                AutoReset = autoReset,
                Interval = interval
            };
            this.Timer = timer;
        }

        public void Close()
        {
            this.Timer.Stop();
            this.Timer.Close();
        }

        private void method_0(object sender, ElapsedEventArgs e)
        {
            this.ffhdejsAf(this.Object);
        }

        private void method_1(object sender, ElapsedEventArgs e)
        {
            this.action_0(this.Object, e);
        }

        private void method_2(object sender, ElapsedEventArgs e)
        {
            this.action_1(this.Object, this.Timer);
        }

        private void method_3(object sender, ElapsedEventArgs e)
        {
            this.action_2(this.Object, this.Timer, e);
        }

        public void Run(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.ffhdejsAf = action;
            this.Timer.Elapsed += new ElapsedEventHandler(this.method_0);
            this.Timer.Start();
        }

        public void Run(Action<T, ElapsedEventArgs> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action_0 = action;
            this.Timer.Elapsed += new ElapsedEventHandler(this.method_1);
            this.Timer.Start();
        }

        public void Run(Action<T, System.Timers.Timer> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action_1 = action;
            this.Timer.Elapsed += new ElapsedEventHandler(this.method_2);
            this.Timer.Start();
        }

        public void Run(Action<T, System.Timers.Timer, ElapsedEventArgs> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action_2 = action;
            this.Timer.Elapsed += new ElapsedEventHandler(this.method_3);
            this.Timer.Start();
        }

        public T Object
        {
            [CompilerGenerated]
            get
            {
                return this.gparam_0;
            }
            [CompilerGenerated]
            private set
            {
                this.gparam_0 = value;
            }
        }

        public System.Timers.Timer Timer
        {
            [CompilerGenerated]
            get
            {
                return this.timer_0;
            }
            [CompilerGenerated]
            private set
            {
                this.timer_0 = value;
            }
        }
    }
}

