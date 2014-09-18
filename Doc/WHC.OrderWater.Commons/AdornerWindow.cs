using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

internal class AdornerWindow : Form
{
    private Bitmap bitmap_0 = null;
    private bool bool_0 = false;
    private EventHandler eventHandler_0;
    private IDisposable idisposable_0 = null;
    private Point point_0 = Point.Empty;
    private Point point_1 = Point.Empty;

    public event EventHandler Event_0
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

    public AdornerWindow()
    {
        this.InitializeComponent();
        this.BackgroundImage = this.method_7();
        this.BackgroundImageLayout = ImageLayout.Stretch;
        this.Cursor = Cursors.Cross;
        base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        base.SetStyle(ControlStyles.UserPaint, true);
        base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        base.AutoScaleMode = AutoScaleMode.None;
        base.Size = SystemInformation.VirtualScreen.Size;
        base.Location = SystemInformation.VirtualScreen.Location;
        base.TopMost = true;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.idisposable_0 != null))
        {
            this.idisposable_0.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        base.SuspendLayout();
        base.AutoScaleDimensions = new SizeF(8f, 16f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x11a, 0xff);
        base.FormBorderStyle = FormBorderStyle.None;
        base.Name = "AdornerWindow";
        this.Text = "AdornerWindow";
        base.WindowState = FormWindowState.Maximized;
        base.ResumeLayout(false);
    }

    public Bitmap method_0()
    {
        return this.bitmap_0;
    }

    public void method_1(Bitmap bitmap_1)
    {
        this.bitmap_0 = bitmap_1;
    }

    public Point method_2()
    {
        return this.point_0;
    }

    public Point method_3()
    {
        return this.point_1;
    }

    private void method_4()
    {
        base.Hide();
        if (this.method_3() != this.method_2())
        {
            Rectangle rectangle = Rectangle.FromLTRB(this.method_2().X, this.method_2().Y, this.method_3().X, this.method_3().Y);
            this.method_6(rectangle);
        }
        if (this.eventHandler_0 != null)
        {
            this.eventHandler_0(this, EventArgs.Empty);
        }
    }

    public void method_5()
    {
        this.point_0 = Point.Empty;
        this.point_1 = Point.Empty;
        this.bool_0 = false;
    }

    private void method_6(Rectangle rectangle_0)
    {
        if (rectangle_0.Width < 0)
        {
            rectangle_0.X += rectangle_0.Width;
            rectangle_0.Width *= -1;
        }
        if (rectangle_0.Height < 0)
        {
            rectangle_0.Y += rectangle_0.Height;
            rectangle_0.Height *= -1;
        }
        Bitmap image = new Bitmap(rectangle_0.Width, rectangle_0.Height);
        Graphics.FromImage(image).DrawImage(this.method_0(), new Rectangle(Point.Empty, image.Size), rectangle_0, GraphicsUnit.Pixel);
        this.method_1(image);
        Clipboard.SetImage(image);
    }

    private Bitmap method_7()
    {
        Rectangle rectangle = new Rectangle(SystemInformation.VirtualScreen.Location, SystemInformation.VirtualScreen.Size);
        Bitmap image = new Bitmap(rectangle.Width, rectangle.Height);
        Graphics.FromImage(image).CopyFromScreen(Point.Empty, Point.Empty, rectangle.Size, CopyPixelOperation.SourceCopy);
        this.bitmap_0 = image;
        return image;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        this.method_4();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        this.point_0 = Control.MousePosition;
        this.Refresh();
    }

    protected override void OnMouseMove(MouseEventArgs mea)
    {
        base.OnMouseMove(mea);
        this.bool_0 = mea.Button == MouseButtons.Left;
        this.point_1 = Control.MousePosition;
        this.Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        base.OnMouseUp(mevent);
        this.point_1 = Control.MousePosition;
        this.method_4();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Region clip = e.Graphics.Clip;
        if ((this.bool_0 && (this.point_0 != Point.Empty)) && (this.point_0 != this.point_1))
        {
            Rectangle rect = Rectangle.FromLTRB(this.point_0.X, this.point_0.Y, this.point_1.X, this.point_1.Y);
            using (Pen pen = new Pen(Color.Black))
            {
                e.Graphics.DrawRectangle(pen, Rectangle.Inflate(rect, -1, -1));
            }
            e.Graphics.SetClip(rect, CombineMode.Exclude);
        }
        using (Brush brush = new SolidBrush(Color.FromArgb(210, Color.WhiteSmoke)))
        {
            e.Graphics.FillRectangle(brush, base.ClientRectangle);
        }
        e.Graphics.SetClip(clip, CombineMode.Replace);
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        this.method_5();
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == 0x200)
        {
            this.Refresh();
        }
    }
}

