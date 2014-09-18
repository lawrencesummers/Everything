namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public class TimerHelper : Component
    {
        private long long_0;
        private Timer timer_0;
        private TimerExecution timerExecution_0;
        private TimerState timerState_0;

        public event TimerExecution Execute
        {
            add
            {
                TimerExecution execution2;
                TimerExecution execution = this.timerExecution_0;
                do
                {
                    execution2 = execution;
                    TimerExecution execution3 = (TimerExecution) Delegate.Combine(execution2, value);
                    execution = Interlocked.CompareExchange<TimerExecution>(ref this.timerExecution_0, execution3, execution2);
                }
                while (execution != execution2);
            }
            remove
            {
                TimerExecution execution2;
                TimerExecution execution = this.timerExecution_0;
                do
                {
                    execution2 = execution;
                    TimerExecution execution3 = (TimerExecution) Delegate.Remove(execution2, value);
                    execution = Interlocked.CompareExchange<TimerExecution>(ref this.timerExecution_0, execution3, execution2);
                }
                while (execution != execution2);
            }
        }

        public TimerHelper()
        {
            this.long_0 = 100;
            this.timerState_0 = TimerState.Stopped;
            this.timer_0 = new Timer(new TimerCallback(this.Tick), null, -1, this.long_0);
        }

        public TimerHelper(long interval, bool start)
        {
            this.long_0 = interval;
            this.timerState_0 = !start ? TimerState.Stopped : TimerState.Running;
            this.timer_0 = new Timer(new TimerCallback(this.Tick), null, 0, interval);
        }

        public TimerHelper(long interval, int startDelay)
        {
            this.long_0 = interval;
            this.timerState_0 = (startDelay == -1) ? TimerState.Stopped : TimerState.Running;
            this.timer_0 = new Timer(new TimerCallback(this.Tick), null, (long) startDelay, interval);
        }

        public void Pause()
        {
            this.timerState_0 = TimerState.Paused;
            this.timer_0.Change(-1, this.long_0);
        }

        public void Start()
        {
            this.timerState_0 = TimerState.Running;
            this.timer_0.Change(0, this.long_0);
        }

        public void Start(int delayBeforeStart)
        {
            this.timerState_0 = TimerState.Running;
            this.timer_0.Change((long) delayBeforeStart, this.long_0);
        }

        public void Stop()
        {
            this.timerState_0 = TimerState.Stopped;
            this.timer_0.Change(-1, this.long_0);
        }

        public void Tick(object obj)
        {
            if ((this.timerState_0 == TimerState.Running) && (this.timerExecution_0 != null))
            {
                lock (this)
                {
                    this.timerExecution_0();
                }
            }
        }

        public long Interval
        {
            get
            {
                return this.long_0;
            }
            set
            {
                this.timer_0.Change((this.timerState_0 == TimerState.Running) ? value : -1, value);
            }
        }

        public TimerState State
        {
            get
            {
                return this.timerState_0;
            }
        }

        public delegate void TimerExecution();
    }
}

