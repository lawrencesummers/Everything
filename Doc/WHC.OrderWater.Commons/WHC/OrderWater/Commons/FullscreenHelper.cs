namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class FullscreenHelper
    {
        private bool bool_0 = false;
        private Form form_0 = null;
        private FormBorderStyle formBorderStyle_0;
        private FormWindowState formWindowState_0;
        private Rectangle rectangle_0;

        public FullscreenHelper(Form form)
        {
            this.form_0 = form;
        }

        public void Toggle()
        {
            this.Fullscreen = !this.Fullscreen;
        }

        public bool Fullscreen
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                if (this.bool_0 != value)
                {
                    this.bool_0 = value;
                    if (this.bool_0)
                    {
                        this.rectangle_0 = this.form_0.Bounds;
                        this.formBorderStyle_0 = this.form_0.FormBorderStyle;
                        this.formWindowState_0 = this.form_0.WindowState;
                        if (this.form_0.MainMenuStrip != null)
                        {
                            this.form_0.MainMenuStrip.Visible = false;
                        }
                        this.form_0.FormBorderStyle = FormBorderStyle.None;
                        this.form_0.Bounds = Screen.PrimaryScreen.Bounds;
                        this.form_0.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        this.form_0.TopMost = false;
                        this.form_0.WindowState = this.formWindowState_0;
                        this.form_0.Bounds = this.rectangle_0;
                        this.form_0.FormBorderStyle = this.formBorderStyle_0;
                        if (this.form_0.MainMenuStrip != null)
                        {
                            this.form_0.MainMenuStrip.Visible = true;
                        }
                    }
                }
            }
        }
    }
}

