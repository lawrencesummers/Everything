namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public sealed class FormAnimator : IDisposable
    {
        private AnimationDirection animationDirection_0;
        private AnimationMethod animationMethod_0;
        private System.Windows.Forms.Form form_0;
        private const int int_0 = 0x10000;
        private const int int_1 = 0x20000;
        private int int_2;

        public FormAnimator(System.Windows.Forms.Form form)
        {
            this.form_0 = form;
            this.form_0.Load += new EventHandler(this.form_0_Load);
            this.form_0.VisibleChanged += new EventHandler(this.form_0_VisibleChanged);
            this.form_0.Closing += new CancelEventHandler(this.hdAxnmGarC);
            this.int_2 = 250;
        }

        public FormAnimator(System.Windows.Forms.Form form, AnimationMethod method, int duration) : this(form)
        {
            this.animationMethod_0 = method;
            this.int_2 = duration;
        }

        public FormAnimator(System.Windows.Forms.Form form, AnimationMethod method, AnimationDirection direction, int duration) : this(form, method, duration)
        {
            this.animationDirection_0 = direction;
        }

        [DllImport("user32", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        public static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags);
        public void Dispose()
        {
            this.form_0.Load -= new EventHandler(this.form_0_Load);
            this.form_0.VisibleChanged -= new EventHandler(this.form_0_VisibleChanged);
            this.form_0.Closing -= new CancelEventHandler(this.hdAxnmGarC);
        }

        ~FormAnimator()
        {
            this.form_0 = null;
        }

        private void form_0_Load(object sender, EventArgs e)
        {
            if ((this.form_0.MdiParent == null) || (this.animationMethod_0 != AnimationMethod.Blend))
            {
                AnimateWindow(this.form_0.Handle, this.int_2, (int) ((((AnimationMethod) 0x20000) | this.animationMethod_0) | ((AnimationMethod) ((int) this.animationDirection_0))));
            }
        }

        private void form_0_VisibleChanged(object sender, EventArgs e)
        {
            if (this.form_0.MdiParent == null)
            {
                int dwFlags = (int) (this.animationMethod_0 | ((AnimationMethod) ((int) this.animationDirection_0)));
                if (this.form_0.Visible)
                {
                    dwFlags |= 0x20000;
                }
                else
                {
                    dwFlags |= 0x10000;
                }
                AnimateWindow(this.form_0.Handle, this.int_2, dwFlags);
            }
        }

        private void hdAxnmGarC(object sender, CancelEventArgs e)
        {
            if (!e.Cancel && ((this.form_0.MdiParent == null) || (this.animationMethod_0 != AnimationMethod.Blend)))
            {
                AnimateWindow(this.form_0.Handle, this.int_2, (int) ((((AnimationMethod) 0x10000) | this.animationMethod_0) | ((AnimationMethod) 4)));
            }
        }

        [Description("获取或设置滚动或者滑动窗体的方向")]
        public AnimationDirection Direction
        {
            get
            {
                return this.animationDirection_0;
            }
            set
            {
                this.animationDirection_0 = value;
            }
        }

        [Description("获取或设置动画效果的毫秒数值")]
        public int Duration
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }

        [Description("获取待实现动画的窗体对象")]
        public System.Windows.Forms.Form Form
        {
            get
            {
                return this.form_0;
            }
        }

        [Description("获取或设置显示或者隐藏窗体的动画操作")]
        public AnimationMethod Method
        {
            get
            {
                return this.animationMethod_0;
            }
            set
            {
                this.animationMethod_0 = value;
            }
        }

        [Flags]
        public enum AnimationDirection
        {
            [Description("From top to bottom.")]
            Down = 4,
            [Description("From right to left.")]
            Left = 2,
            [Description("From left to right.")]
            Right = 1,
            [Description("From bottom to top.")]
            Up = 8
        }

        public enum AnimationMethod
        {
            [Description("Fades from transaprent to opaque when showing and from opaque to transparent when hiding.")]
            Blend = 0x80000,
            [Description("Expands out from centre when showing and collapses into centre when hiding.")]
            Centre = 0x10,
            [Description("Default animation method. Rolls out from edge when showing and into edge when hiding. Requires a direction.")]
            Roll = 0,
            [Description("Slides out from edge when showing and slides into edge when hiding. Requires a direction.")]
            Slide = 0x40000
        }
    }
}

