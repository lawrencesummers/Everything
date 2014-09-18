namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [DefaultEvent("Click")]
    public class VistaButton : UserControl
    {
        private bool bool_0 = false;
        private Color color_0 = Color.White;
        private Color color_1 = Color.White;
        private Color color_2 = Color.Black;
        private Color color_3 = Color.FromArgb(0x8d, 0xbd, 0xff);
        private Color color_4 = Color.Black;
        private Container container_0 = null;
        private ContentAlignment contentAlignment_0 = ContentAlignment.MiddleCenter;
        private ContentAlignment contentAlignment_1 = ContentAlignment.MiddleLeft;
        private System.Drawing.Image image_0;
        private System.Drawing.Image image_1;
        private int int_0 = 0;
        private int int_1 = 8;
        private Enum1 pNyYswXrwU = ((Enum1) 0);
        private Size size_0 = new Size(0x18, 0x18);
        private string string_0;
        private Style style_0 = Style.Default;
        private Timer timer_0 = new Timer();
        private Timer timer_1 = new Timer();

        public VistaButton()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            this.timer_0.Interval = 30;
            this.timer_1.Interval = 30;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.Name = "VistaButton";
            base.Size = new Size(100, 0x20);
            base.Paint += new PaintEventHandler(this.VistaButton_Paint);
            base.KeyUp += new KeyEventHandler(this.VistaButton_KeyUp);
            base.KeyDown += new KeyEventHandler(this.VistaButton_KeyDown);
            base.MouseEnter += new EventHandler(this.VistaButton_GotFocus);
            base.MouseLeave += new EventHandler(this.VistaButton_LostFocus);
            base.MouseUp += new MouseEventHandler(this.VistaButton_MouseUp);
            base.MouseDown += new MouseEventHandler(this.VistaButton_MouseDown);
            base.GotFocus += new EventHandler(this.VistaButton_GotFocus);
            base.LostFocus += new EventHandler(this.VistaButton_LostFocus);
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
            this.timer_1.Tick += new EventHandler(this.timer_1_Tick);
            base.Resize += new EventHandler(this.VistaButton_Resize);
        }

        private GraphicsPath method_0(RectangleF rectangleF_0, float float_0, float float_1, float float_2, float float_3)
        {
            float x = rectangleF_0.X;
            float y = rectangleF_0.Y;
            float width = rectangleF_0.Width;
            float height = rectangleF_0.Height;
            GraphicsPath path = new GraphicsPath();
            path.AddBezier(x, y + float_0, x, y, x + float_0, y, x + float_0, y);
            path.AddLine(x + float_0, y, (x + width) - float_1, y);
            path.AddBezier((x + width) - float_1, y, x + width, y, x + width, y + float_1, x + width, y + float_1);
            path.AddLine((float) (x + width), (float) (y + float_1), (float) (x + width), (float) ((y + height) - float_2));
            path.AddBezier((float) (x + width), (float) ((y + height) - float_2), (float) (x + width), (float) (y + height), (float) ((x + width) - float_2), (float) (y + height), (float) ((x + width) - float_2), (float) (y + height));
            path.AddLine((float) ((x + width) - float_2), (float) (y + height), (float) (x + float_3), (float) (y + height));
            path.AddBezier(x + float_3, y + height, x, y + height, x, (y + height) - float_3, x, (y + height) - float_3);
            path.AddLine(x, (y + height) - float_3, x, y + float_0);
            return path;
        }

        private StringFormat method_1(ContentAlignment contentAlignment_2)
        {
            StringFormat format = new StringFormat();
            ContentAlignment alignment = contentAlignment_2;
            if (alignment <= ContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        format.LineAlignment = StringAlignment.Near;
                        goto Label_0073;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        goto Label_0073;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                        goto Label_0051;
                }
            }
            else
            {
                if (alignment > ContentAlignment.BottomLeft)
                {
                    if ((alignment != ContentAlignment.BottomCenter) && (alignment != ContentAlignment.BottomRight))
                    {
                        goto Label_0073;
                    }
                    goto Label_006C;
                }
                switch (alignment)
                {
                    case ContentAlignment.MiddleRight:
                        goto Label_0051;

                    case ContentAlignment.BottomLeft:
                        goto Label_006C;
                }
            }
            goto Label_0073;
        Label_0051:
            format.LineAlignment = StringAlignment.Center;
            goto Label_0073;
        Label_006C:
            format.LineAlignment = StringAlignment.Far;
        Label_0073:
            alignment = contentAlignment_2;
            if (alignment <= ContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                        goto Label_00B5;

                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                        goto Label_00D9;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        return format;

                    case ContentAlignment.TopRight:
                        goto Label_00D0;
                }
                return format;
            }
            if (alignment > ContentAlignment.BottomLeft)
            {
                if (alignment == ContentAlignment.BottomCenter)
                {
                    goto Label_00D9;
                }
                if (alignment != ContentAlignment.BottomRight)
                {
                    return format;
                }
                goto Label_00D0;
            }
            if (alignment == ContentAlignment.MiddleRight)
            {
                goto Label_00D0;
            }
            if (alignment != ContentAlignment.BottomLeft)
            {
                return format;
            }
        Label_00B5:
            format.Alignment = StringAlignment.Near;
            return format;
        Label_00D0:
            format.Alignment = StringAlignment.Far;
            return format;
        Label_00D9:
            format.Alignment = StringAlignment.Center;
            return format;
        }

        private void method_2(Graphics graphics_0)
        {
            if ((this.ButtonStyle != Style.Flat) || (this.pNyYswXrwU != ((Enum1) 0)))
            {
                Rectangle clientRectangle = base.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                using (GraphicsPath path = this.method_0(clientRectangle, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius))
                {
                    using (Pen pen = new Pen(this.ButtonColor))
                    {
                        graphics_0.DrawPath(pen, path);
                    }
                }
            }
        }

        private void method_3(Graphics graphics_0)
        {
            if ((this.ButtonStyle != Style.Flat) || (this.pNyYswXrwU != ((Enum1) 0)))
            {
                Rectangle clientRectangle = base.ClientRectangle;
                clientRectangle.X++;
                clientRectangle.Y++;
                clientRectangle.Width -= 3;
                clientRectangle.Height -= 3;
                using (GraphicsPath path = this.method_0(clientRectangle, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius))
                {
                    using (Pen pen = new Pen(this.HighlightColor))
                    {
                        graphics_0.DrawPath(pen, path);
                    }
                }
            }
        }

        private void method_4(Graphics graphics_0)
        {
            if ((this.ButtonStyle != Style.Flat) || (this.pNyYswXrwU != ((Enum1) 0)))
            {
                int alpha = (this.pNyYswXrwU == ((Enum1) 2)) ? 0xcc : 0x7f;
                Rectangle clientRectangle = base.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                using (GraphicsPath path = this.method_0(clientRectangle, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius))
                {
                    SolidBrush brush;
                    using (brush = new SolidBrush(this.BaseColor))
                    {
                        graphics_0.FillPath(brush, path);
                    }
                    this.method_9(graphics_0);
                    if (this.BackImage != null)
                    {
                        graphics_0.DrawImage(this.BackImage, base.ClientRectangle);
                    }
                    graphics_0.ResetClip();
                    using (brush = new SolidBrush(Color.FromArgb(alpha, this.ButtonColor)))
                    {
                        graphics_0.FillPath(brush, path);
                    }
                }
            }
        }

        private void method_5(Graphics graphics_0)
        {
            if ((this.ButtonStyle != Style.Flat) || (this.pNyYswXrwU != ((Enum1) 0)))
            {
                int alpha = (this.pNyYswXrwU == ((Enum1) 2)) ? 60 : 150;
                Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height / 2);
                using (GraphicsPath path = this.method_0(rectangle, (float) this.CornerRadius, (float) this.CornerRadius, 0f, 0f))
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(path.GetBounds(), Color.FromArgb(alpha, this.HighlightColor), Color.FromArgb(alpha / 3, this.HighlightColor), LinearGradientMode.Vertical))
                    {
                        graphics_0.FillPath(brush, path);
                    }
                }
            }
        }

        private void method_6(Graphics graphics_0)
        {
            if (this.pNyYswXrwU != ((Enum1) 2))
            {
                this.method_9(graphics_0);
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(-5, (base.Height / 2) - 10, base.Width + 11, base.Height + 11);
                    using (PathGradientBrush brush = new PathGradientBrush(path))
                    {
                        brush.CenterColor = Color.FromArgb(this.int_0, this.GlowColor);
                        brush.SurroundColors = new Color[] { Color.FromArgb(0, this.GlowColor) };
                        graphics_0.FillPath(brush, path);
                    }
                }
                graphics_0.ResetClip();
            }
        }

        private void method_7(Graphics graphics_0)
        {
            StringFormat format = this.method_1(this.TextAlign);
            Rectangle layoutRectangle = new Rectangle(8, 8, base.Width - 0x11, base.Height - 0x11);
            graphics_0.DrawString(this.ButtonText, this.Font, new SolidBrush(this.ForeColor), layoutRectangle, format);
        }

        private void method_8(Graphics graphics_0)
        {
            if (this.Image == null)
            {
                return;
            }
            Rectangle rect = new Rectangle(8, 8, this.ImageSize.Width, this.ImageSize.Height);
            switch (this.ImageAlign)
            {
                case ContentAlignment.TopCenter:
                    rect = new Rectangle((base.Width / 2) - (this.ImageSize.Width / 2), 8, this.ImageSize.Width, this.ImageSize.Height);
                    goto Label_02F6;

                case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                    goto Label_02F6;

                case ContentAlignment.TopRight:
                    rect = new Rectangle((base.Width - 8) - this.ImageSize.Width, 8, this.ImageSize.Width, this.ImageSize.Height);
                    goto Label_02F6;

                case ContentAlignment.MiddleLeft:
                    rect = new Rectangle(8, (base.Height / 2) - (this.ImageSize.Height / 2), this.ImageSize.Width, this.ImageSize.Height);
                    goto Label_02F6;

                case ContentAlignment.MiddleCenter:
                    rect = new Rectangle((base.Width / 2) - (this.ImageSize.Width / 2), (base.Height / 2) - (this.ImageSize.Height / 2), this.ImageSize.Width, this.ImageSize.Height);
                    goto Label_02F6;

                case ContentAlignment.MiddleRight:
                    rect = new Rectangle((base.Width - 8) - this.ImageSize.Width, (base.Height / 2) - (this.ImageSize.Height / 2), this.ImageSize.Width, this.ImageSize.Height);
                    break;

                case ContentAlignment.BottomLeft:
                    rect = new Rectangle(8, (base.Height - 8) - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    break;

                case ContentAlignment.BottomCenter:
                    rect = new Rectangle((base.Width / 2) - (this.ImageSize.Width / 2), (base.Height - 8) - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    break;

                case ContentAlignment.BottomRight:
                    rect = new Rectangle((base.Width - 8) - this.ImageSize.Width, (base.Height - 8) - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    break;
            }
        Label_02F6:
            graphics_0.DrawImage(this.Image, rect);
        }

        private void method_9(Graphics graphics_0)
        {
            Rectangle clientRectangle = base.ClientRectangle;
            clientRectangle.X++;
            clientRectangle.Y++;
            clientRectangle.Width -= 3;
            clientRectangle.Height -= 3;
            using (GraphicsPath path = this.method_0(clientRectangle, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius))
            {
                graphics_0.SetClip(path);
            }
        }

        private void timer_0_Tick(object sender, EventArgs e)
        {
            if (this.ButtonStyle == Style.Flat)
            {
                this.int_0 = 0;
            }
            if ((this.int_0 + 30) >= 0xff)
            {
                this.int_0 = 0xff;
                this.timer_0.Stop();
            }
            else
            {
                this.int_0 += 30;
            }
            base.Invalidate();
        }

        private void timer_1_Tick(object sender, EventArgs e)
        {
            if (this.ButtonStyle == Style.Flat)
            {
                this.int_0 = 0;
            }
            if ((this.int_0 - 30) <= 0)
            {
                this.int_0 = 0;
                this.timer_1.Stop();
            }
            else
            {
                this.int_0 -= 30;
            }
            base.Invalidate();
        }

        private void VistaButton_GotFocus(object sender, EventArgs e)
        {
            this.pNyYswXrwU = (Enum1) 1;
            this.timer_1.Stop();
            this.timer_0.Start();
        }

        private void VistaButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs args = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
                this.VistaButton_MouseDown(sender, args);
            }
        }

        private void VistaButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs args = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
                this.bool_0 = true;
                this.VistaButton_MouseUp(sender, args);
            }
        }

        private void VistaButton_LostFocus(object sender, EventArgs e)
        {
            this.pNyYswXrwU = (Enum1) 0;
            if (this.style_0 == Style.Flat)
            {
                this.int_0 = 0;
            }
            this.timer_0.Stop();
            this.timer_1.Start();
        }

        private void VistaButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pNyYswXrwU = (Enum1) 2;
                if (this.style_0 != Style.Flat)
                {
                    this.int_0 = 0xff;
                }
                this.timer_0.Stop();
                this.timer_1.Stop();
                base.Invalidate();
            }
        }

        private void VistaButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pNyYswXrwU = (Enum1) 1;
                this.timer_0.Stop();
                this.timer_1.Stop();
                base.Invalidate();
                if (this.bool_0)
                {
                    this.OnClick(EventArgs.Empty);
                    this.bool_0 = false;
                }
            }
        }

        private void VistaButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            this.method_4(e.Graphics);
            this.method_5(e.Graphics);
            this.method_8(e.Graphics);
            this.method_7(e.Graphics);
            this.method_6(e.Graphics);
            this.method_2(e.Graphics);
            this.method_3(e.Graphics);
        }

        private void VistaButton_Resize(object sender, EventArgs e)
        {
            Rectangle clientRectangle = base.ClientRectangle;
            clientRectangle.X--;
            clientRectangle.Y--;
            clientRectangle.Width += 2;
            clientRectangle.Height += 2;
            using (GraphicsPath path = this.method_0(clientRectangle, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius, (float) this.CornerRadius))
            {
                base.Region = new Region(path);
            }
        }

        [Description("按钮的背景图片，当鼠标在按钮上面，图片被绘制"), Category("Appearance"), DefaultValue((string) null)]
        public System.Drawing.Image BackImage
        {
            get
            {
                return this.image_1;
            }
            set
            {
                this.image_1 = value;
                base.Invalidate();
            }
        }

        [Category("Appearance"), Description("按钮默认的衬托颜色，如果设置为Transparent，可获得玻璃效果"), DefaultValue(typeof(Color), "Black")]
        public Color BaseColor
        {
            get
            {
                return this.color_4;
            }
            set
            {
                this.color_4 = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Black"), Description("在衬托颜色BaseColor上面的按钮底部颜色"), Category("Appearance")]
        public Color ButtonColor
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
                base.Invalidate();
            }
        }

        [Category("Appearance"), Description("设置当鼠标移开，按钮背景是否绘制"), DefaultValue(typeof(Style), "Default")]
        public Style ButtonStyle
        {
            get
            {
                return this.style_0;
            }
            set
            {
                this.style_0 = value;
                base.Invalidate();
            }
        }

        [Description("显示在按钮上的文本"), Category("Text")]
        public string ButtonText
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
                base.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(8), Description("按钮圆角半径。圆角半径越大，边角越平滑。该属性不该大于控件高度的一半。")]
        public int CornerRadius
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
                base.Invalidate();
            }
        }

        [Category("Text"), Description("绘制文本的颜色（前景色）"), Browsable(true), DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
                base.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "141,189,255"), Description("当鼠标在控件区域的时候，按钮的发光颜色")]
        public Color GlowColor
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
                base.Invalidate();
            }
        }

        [Description("顶部的按钮上的高亮显示的颜色"), DefaultValue(typeof(Color), "White"), Category("Appearance")]
        public Color HighlightColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
                base.Invalidate();
            }
        }

        [DefaultValue((string) null), Category("Image"), Description("按钮显示的图片")]
        public System.Drawing.Image Image
        {
            get
            {
                return this.image_0;
            }
            set
            {
                this.image_0 = value;
                base.Invalidate();
            }
        }

        [Description("按钮图片对齐方式"), Category("Image"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment ImageAlign
        {
            get
            {
                return this.contentAlignment_1;
            }
            set
            {
                this.contentAlignment_1 = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Size), "24, 24"), Category("Image"), Description("按钮图片的大小，默认为24x24")]
        public Size ImageSize
        {
            get
            {
                return this.size_0;
            }
            set
            {
                this.size_0 = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(ContentAlignment), "MiddleCenter"), Category("Text"), Description("按钮文本的对齐方式")]
        public ContentAlignment TextAlign
        {
            get
            {
                return this.contentAlignment_0;
            }
            set
            {
                this.contentAlignment_0 = value;
                base.Invalidate();
            }
        }

        private enum Enum1
        {
        }

        public enum Style
        {
            Default,
            Flat
        }
    }
}

