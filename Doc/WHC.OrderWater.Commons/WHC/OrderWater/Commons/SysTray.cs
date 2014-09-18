namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Timers;
    using System.Windows.Forms;

    public class SysTray : IDisposable
    {
        private Color color_0 = Color.Black;
        private Font font_0;
        private Icon[] icon_0;
        private int int_0 = 0;
        private int int_1 = 0;
        public Icon m_DefaultIcon;
        public NotifyIcon m_notifyIcon = new NotifyIcon();
        private Timer timer_0;

        public SysTray(string text, Icon icon, ContextMenu menu)
        {
            this.m_notifyIcon.Text = text;
            this.m_notifyIcon.Visible = true;
            this.m_notifyIcon.Icon = icon;
            this.m_DefaultIcon = icon;
            this.m_notifyIcon.ContextMenu = menu;
            this.font_0 = new Font("Helvetica", 8f);
            this.timer_0 = new Timer();
            this.timer_0.Interval = 100.0;
            this.timer_0.Elapsed += new ElapsedEventHandler(this.timer_0_Elapsed);
        }

        public void Dispose()
        {
            this.m_notifyIcon.Dispose();
            if (this.font_0 != null)
            {
                this.font_0.Dispose();
            }
        }

        public void SetAnimationClip(Bitmap[] bitmap)
        {
            try
            {
                this.icon_0 = new Icon[bitmap.Length];
                for (int i = 0; i < bitmap.Length; i++)
                {
                    this.icon_0[i] = Icon.FromHandle(bitmap[i].GetHicon());
                }
            }
            catch (Exception)
            {
            }
        }

        public void SetAnimationClip(Icon[] icons)
        {
            this.icon_0 = icons;
        }

        public void SetAnimationClip(Bitmap bitmapStrip)
        {
            try
            {
                this.icon_0 = new Icon[bitmapStrip.Width / 0x10];
                for (int i = 0; i < this.icon_0.Length; i++)
                {
                    Rectangle rect = new Rectangle(i * 0x10, 0, 0x10, 0x10);
                    this.icon_0[i] = Icon.FromHandle(bitmapStrip.Clone(rect, bitmapStrip.PixelFormat).GetHicon());
                }
            }
            catch (Exception)
            {
            }
        }

        public void ShowText(string text)
        {
            this.ShowText(text, this.font_0, this.color_0);
        }

        public void ShowText(string text, Color col)
        {
            this.ShowText(text, this.font_0, col);
        }

        public void ShowText(string text, Font font)
        {
            this.ShowText(text, font, this.color_0);
        }

        public void ShowText(string text, Font font, Color col)
        {
            Bitmap image = new Bitmap(0x10, 0x10);
            Brush brush = new SolidBrush(col);
            Graphics.FromImage(image).DrawString(text, this.font_0, brush, (float) 0f, (float) 0f);
            Icon icon = Icon.FromHandle(image.GetHicon());
            this.m_notifyIcon.Icon = icon;
        }

        public void StartAnimation(int interval, int loopCount)
        {
            if (this.icon_0 == null)
            {
                throw new ApplicationException("Animation clip not set with SetAnimationClip");
            }
            this.int_1 = loopCount;
            this.timer_0.Interval = interval;
            this.timer_0.Start();
        }

        public void StopAnimation()
        {
            this.timer_0.Stop();
        }

        private void timer_0_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.int_0 < this.icon_0.Length)
            {
                this.m_notifyIcon.Icon = this.icon_0[this.int_0];
                this.int_0++;
            }
            else
            {
                this.int_0 = 0;
                if (this.int_1 <= 0)
                {
                    this.timer_0.Stop();
                    this.m_notifyIcon.Icon = this.m_DefaultIcon;
                }
                else
                {
                    this.int_1--;
                }
            }
        }
    }
}

