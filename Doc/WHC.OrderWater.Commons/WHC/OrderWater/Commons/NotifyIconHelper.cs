namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class NotifyIconHelper
    {
        private bool bool_0 = false;
        [CompilerGenerated]
        private Icon icon_0;
        [CompilerGenerated]
        private Icon icon_1;
        [CompilerGenerated]
        private Icon icon_2;
        [CompilerGenerated]
        private Icon icon_3;
        private NotifyIcon notifyIcon_0;
        private Status status_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        private Timer timer_0;

        public NotifyIconHelper(NotifyIcon notifyIcon)
        {
            this.notifyIcon_0 = notifyIcon;
            this.NotifyStatus = Status.Offline;
            this.timer_0 = new Timer();
            this.timer_0.Interval = 500;
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
        }

        public void Refresh()
        {
            switch (this.status_0)
            {
                case Status.Offline:
                    this.notifyIcon_0.Icon = this.Icon_UnConntect;
                    this.notifyIcon_0.Text = this.Text_UnConntect;
                    break;

                case Status.Online:
                    this.notifyIcon_0.Icon = this.Icon_Conntected;
                    this.notifyIcon_0.Text = this.Text_Conntected;
                    break;

                case Status.TwinkleNotice:
                    this.timer_0.Start();
                    break;
            }
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            this.notifyIcon_0.Icon = this.bool_0 ? this.Icon_Shrink1 : this.Icon_Shrink2;
            this.bool_0 = !this.bool_0;
        }

        public Icon Icon_Conntected
        {
            [CompilerGenerated]
            get
            {
                return this.icon_1;
            }
            [CompilerGenerated]
            set
            {
                this.icon_1 = value;
            }
        }

        public Icon Icon_Shrink1
        {
            [CompilerGenerated]
            get
            {
                return this.icon_2;
            }
            [CompilerGenerated]
            set
            {
                this.icon_2 = value;
            }
        }

        public Icon Icon_Shrink2
        {
            [CompilerGenerated]
            get
            {
                return this.icon_3;
            }
            [CompilerGenerated]
            set
            {
                this.icon_3 = value;
            }
        }

        public Icon Icon_UnConntect
        {
            [CompilerGenerated]
            get
            {
                return this.icon_0;
            }
            [CompilerGenerated]
            set
            {
                this.icon_0 = value;
            }
        }

        public Status NotifyStatus
        {
            get
            {
                return this.status_0;
            }
            set
            {
                if (value != this.status_0)
                {
                    if ((this.status_0 == Status.TwinkleNotice) && (this.timer_0 != null))
                    {
                        this.timer_0.Stop();
                    }
                    this.status_0 = value;
                    this.Refresh();
                }
            }
        }

        public string Text_Conntected
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public string Text_UnConntect
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }

        public enum Status
        {
            Offline,
            Online,
            TwinkleNotice
        }
    }
}

