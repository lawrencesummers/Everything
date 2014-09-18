using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Threading;
using System.Windows.Forms;

internal class Control0 : UserControl
{
    private bool bool_0;
    private bool bool_1;
    private Class15 class15_0 = new Class15();
    private double double_0;
    private Enum0 enum0_0;
    private EventHandler eventHandler_0;
    private EventHandler eventHandler_1;
    private EventHandler eventHandler_2;
    private const int int_0 = 4;
    private int int_1;
    private Point point_0;
    private PointF pointF_0 = new PointF(-1f, -1f);
    private PrintDocument printDocument_0;
    private SolidBrush solidBrush_0;

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

    public event EventHandler Event_1
    {
        add
        {
            EventHandler handler2;
            EventHandler handler = this.eventHandler_1;
            do
            {
                handler2 = handler;
                EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_1, handler3, handler2);
            }
            while (handler != handler2);
        }
        remove
        {
            EventHandler handler2;
            EventHandler handler = this.eventHandler_1;
            do
            {
                handler2 = handler;
                EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_1, handler3, handler2);
            }
            while (handler != handler2);
        }
    }

    public event EventHandler Event_2
    {
        add
        {
            EventHandler handler2;
            EventHandler handler = this.eventHandler_2;
            do
            {
                handler2 = handler;
                EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_2, handler3, handler2);
            }
            while (handler != handler2);
        }
        remove
        {
            EventHandler handler2;
            EventHandler handler = this.eventHandler_2;
            do
            {
                handler2 = handler;
                EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_2, handler3, handler2);
            }
            while (handler != handler2);
        }
    }

    public Control0()
    {
        this.BackColor = SystemColors.AppWorkspace;
        this.Enum0_0 = (Enum0) 1;
        this.Int32_0 = 0;
        base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void DyJkoTjhuC(PrintDocument printDocument_1)
    {
        if (printDocument_1 != this.printDocument_0)
        {
            this.printDocument_0 = printDocument_1;
            this.method_1();
        }
    }

    protected override bool IsInputKey(Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Prior:
            case Keys.Next:
            case Keys.End:
            case Keys.Home:
            case Keys.Left:
            case Keys.Up:
            case Keys.Right:
            case Keys.Down:
                return true;
        }
        return base.IsInputKey(keyData);
    }

    public PrintDocument method_0()
    {
        return this.printDocument_0;
    }

    public void method_1()
    {
        if (this.printDocument_0 != null)
        {
            this.class15_0.Clear();
            PrintController printController = this.printDocument_0.PrintController;
            try
            {
                this.bool_0 = false;
                this.bool_1 = true;
                this.printDocument_0.PrintController = new PreviewPrintController();
                this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
                this.printDocument_0.EndPrint += new PrintEventHandler(this.printDocument_0_EndPrint);
                this.printDocument_0.Print();
            }
            finally
            {
                this.bool_0 = false;
                this.bool_1 = false;
                this.printDocument_0.PrintPage -= new PrintPageEventHandler(this.printDocument_0_PrintPage);
                this.printDocument_0.EndPrint -= new PrintEventHandler(this.printDocument_0_EndPrint);
                this.printDocument_0.PrintController = printController;
            }
        }
        this.method_5(EventArgs.Empty);
        this.method_13();
        this.method_12();
    }

    private Size method_10(Image image_0)
    {
        SizeF physicalDimension = image_0.PhysicalDimension;
        if (image_0 is Metafile)
        {
            if (this.pointF_0.X < 0f)
            {
                using (Graphics graphics = base.CreateGraphics())
                {
                    this.pointF_0.X = graphics.DpiX / 2540f;
                    this.pointF_0.Y = graphics.DpiY / 2540f;
                }
            }
            physicalDimension.Width *= this.pointF_0.X;
            physicalDimension.Height *= this.pointF_0.Y;
        }
        return Size.Truncate(physicalDimension);
    }

    private void method_11(Graphics graphics_0, Image image_0, Rectangle rectangle_0)
    {
        rectangle_0.Offset(1, 1);
        graphics_0.DrawRectangle(Pens.Black, rectangle_0);
        rectangle_0.Offset(-1, -1);
        graphics_0.FillRectangle(Brushes.White, rectangle_0);
        graphics_0.DrawImage(image_0, rectangle_0);
        graphics_0.DrawRectangle(Pens.Black, rectangle_0);
        rectangle_0.Width++;
        rectangle_0.Height++;
        graphics_0.ExcludeClip(rectangle_0);
        rectangle_0.Offset(1, 1);
        graphics_0.ExcludeClip(rectangle_0);
    }

    private void method_12()
    {
        Rectangle empty = Rectangle.Empty;
        Image image = this.method_8(this.Int32_0);
        if (image != null)
        {
            empty = this.method_9(image);
        }
        Size size = new Size(0, 0);
        switch (this.enum0_0)
        {
            case ((Enum0) 0):
            case ((Enum0) 4):
                size = new Size(empty.Width + 8, empty.Height + 8);
                break;

            case ((Enum0) 2):
                size = new Size(0, empty.Height + 8);
                break;
        }
        if (size != base.AutoScrollMinSize)
        {
            base.AutoScrollMinSize = size;
        }
        this.method_13();
    }

    private void method_13()
    {
        if (this.int_1 < 0)
        {
            this.int_1 = 0;
        }
        if (this.int_1 > (this.Int32_1 - 1))
        {
            this.int_1 = this.Int32_1 - 1;
        }
        base.Invalidate();
    }

    public void method_2()
    {
        this.bool_0 = true;
    }

    public void method_3()
    {
        PrinterSettings printerSettings = this.printDocument_0.PrinterSettings;
        int num = printerSettings.MinimumPage - 1;
        int num2 = printerSettings.MaximumPage - 1;
        PrintRange printRange = printerSettings.PrintRange;
        switch (printRange)
        {
            case PrintRange.AllPages:
                this.method_0().Print();
                return;

            case PrintRange.Selection:
                num = num2 = this.Int32_0;
                if (this.Enum0_0 == ((Enum0) 3))
                {
                    num2 = Math.Min((int) (num + 1), (int) (this.Int32_1 - 1));
                }
                break;

            case PrintRange.SomePages:
                num = printerSettings.FromPage - 1;
                num2 = printerSettings.ToPage - 1;
                break;

            default:
                if (printRange == PrintRange.CurrentPage)
                {
                    num = num2 = this.Int32_0;
                }
                break;
        }
        new Class7(this, num, num2).Print();
    }

    protected void method_4(EventArgs eventArgs_0)
    {
        if (this.eventHandler_0 != null)
        {
            this.eventHandler_0(this, eventArgs_0);
        }
    }

    protected void method_5(EventArgs eventArgs_0)
    {
        if (this.eventHandler_1 != null)
        {
            this.eventHandler_1(this, eventArgs_0);
        }
    }

    protected void method_6(EventArgs eventArgs_0)
    {
        if (this.eventHandler_2 != null)
        {
            this.eventHandler_2(this, eventArgs_0);
        }
    }

    private void method_7(bool bool_2)
    {
        PreviewPrintController printController = (PreviewPrintController) this.printDocument_0.PrintController;
        if (printController != null)
        {
            PreviewPageInfo[] previewPageInfo = printController.GetPreviewPageInfo();
            int num = bool_2 ? previewPageInfo.Length : (previewPageInfo.Length - 1);
            for (int i = this.class15_0.Count; i < num; i++)
            {
                Image item = previewPageInfo[i].Image;
                this.class15_0.Add(item);
                this.method_5(EventArgs.Empty);
                if (this.Int32_0 < 0)
                {
                    this.Int32_0 = 0;
                }
                if ((i == this.Int32_0) || (i == (this.Int32_0 + 1)))
                {
                    this.Refresh();
                }
                Application.DoEvents();
            }
        }
    }

    private Image method_8(int int_2)
    {
        return (((int_2 <= -1) || (int_2 >= this.Int32_1)) ? null : this.class15_0[int_2]);
    }

    private Rectangle method_9(Image image_0)
    {
        Size size = this.method_10(image_0);
        Rectangle rectangle = new Rectangle(0, 0, size.Width, size.Height);
        Rectangle clientRectangle = base.ClientRectangle;
        switch (this.enum0_0)
        {
            case ((Enum0) 0):
                this.double_0 = 1.0;
                goto Label_00F8;

            case ((Enum0) 1):
                break;

            case ((Enum0) 2):
                this.double_0 = (rectangle.Width > 0) ? (((double) clientRectangle.Width) / ((double) rectangle.Width)) : 0.0;
                goto Label_00F8;

            case ((Enum0) 3):
                rectangle.Width *= 2;
                break;

            default:
                goto Label_00F8;
        }
        double num2 = (rectangle.Width > 0) ? (((double) clientRectangle.Width) / ((double) rectangle.Width)) : 0.0;
        double num3 = (rectangle.Height > 0) ? (((double) clientRectangle.Height) / ((double) rectangle.Height)) : 0.0;
        this.double_0 = Math.Min(num2, num3);
    Label_00F8:
        rectangle.Width = (int) (rectangle.Width * this.double_0);
        rectangle.Height = (int) (rectangle.Height * this.double_0);
        int num = (clientRectangle.Width - rectangle.Width) / 2;
        if (num > 0)
        {
            rectangle.X += num;
        }
        int num4 = (clientRectangle.Height - rectangle.Height) / 2;
        if (num4 > 0)
        {
            rectangle.Y += num4;
        }
        rectangle.Inflate(-4, -4);
        if (this.enum0_0 == ((Enum0) 3))
        {
            rectangle.Inflate(-2, 0);
        }
        return rectangle;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        Point autoScrollPosition;
        base.OnKeyDown(e);
        if (!e.Handled)
        {
            switch (e.KeyCode)
            {
                case Keys.Prior:
                    this.Int32_0--;
                    goto Label_0194;

                case Keys.Next:
                    this.Int32_0++;
                    goto Label_0194;

                case Keys.End:
                    base.AutoScrollPosition = Point.Empty;
                    this.Int32_0 = this.Int32_1 - 1;
                    goto Label_0194;

                case Keys.Home:
                    base.AutoScrollPosition = Point.Empty;
                    this.Int32_0 = 0;
                    goto Label_0194;

                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                    if ((this.Enum0_0 != ((Enum0) 1)) && (this.Enum0_0 != ((Enum0) 3)))
                    {
                        autoScrollPosition = base.AutoScrollPosition;
                        switch (e.KeyCode)
                        {
                            case Keys.Left:
                                autoScrollPosition.X += 20;
                                goto Label_0179;

                            case Keys.Up:
                                autoScrollPosition.Y += 20;
                                goto Label_0179;

                            case Keys.Right:
                                autoScrollPosition.X -= 20;
                                goto Label_0179;

                            case Keys.Down:
                                autoScrollPosition.Y -= 20;
                                goto Label_0179;
                        }
                        goto Label_0179;
                    }
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                        case Keys.Up:
                            this.Int32_0--;
                            goto Label_0194;

                        case Keys.Right:
                        case Keys.Down:
                            this.Int32_0++;
                            goto Label_0194;
                    }
                    goto Label_0194;
            }
        }
        return;
    Label_0179:
        base.AutoScrollPosition = new Point(-autoScrollPosition.X, -autoScrollPosition.Y);
    Label_0194:
        e.Handled = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if ((e.Button == MouseButtons.Left) && (base.AutoScrollMinSize != Size.Empty))
        {
            this.Cursor = Cursors.NoMove2D;
            this.point_0 = new Point(e.X, e.Y);
        }
    }

    protected override void OnMouseMove(MouseEventArgs mea)
    {
        base.OnMouseMove(mea);
        if (this.Cursor == Cursors.NoMove2D)
        {
            int num2 = mea.X - this.point_0.X;
            int num = mea.Y - this.point_0.Y;
            if ((num2 != 0) || (num != 0))
            {
                Point autoScrollPosition = base.AutoScrollPosition;
                base.AutoScrollPosition = new Point(-(autoScrollPosition.X + num2), -(autoScrollPosition.Y + num));
                this.point_0 = new Point(mea.X, mea.Y);
            }
        }
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        base.OnMouseUp(mevent);
        if ((mevent.Button == MouseButtons.Left) && (this.Cursor == Cursors.NoMove2D))
        {
            this.Cursor = Cursors.Default;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Image image = this.method_8(this.Int32_0);
        if (image != null)
        {
            Rectangle rectangle = this.method_9(image);
            if ((rectangle.Width > 2) && (rectangle.Height > 2))
            {
                rectangle.Offset(base.AutoScrollPosition);
                if (this.enum0_0 != ((Enum0) 3))
                {
                    this.method_11(e.Graphics, image, rectangle);
                }
                else
                {
                    rectangle.Width = (rectangle.Width - 4) / 2;
                    this.method_11(e.Graphics, image, rectangle);
                    image = this.method_8(this.Int32_0 + 1);
                    if (image != null)
                    {
                        rectangle = this.method_9(image);
                        rectangle.Width = (rectangle.Width - 4) / 2;
                        rectangle.Offset(rectangle.Width + 4, 0);
                        this.method_11(e.Graphics, image, rectangle);
                    }
                }
            }
        }
        e.Graphics.FillRectangle(this.solidBrush_0, base.ClientRectangle);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        this.method_12();
        base.OnSizeChanged(e);
    }

    private void printDocument_0_EndPrint(object sender, PrintEventArgs e)
    {
        this.method_7(true);
    }

    private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
    {
        this.method_7(false);
        if (this.bool_0)
        {
            e.Cancel = true;
        }
    }

    [DefaultValue(typeof(Color), "AppWorkspace")]
    public override Color BackColor
    {
        get
        {
            return base.BackColor;
        }
        set
        {
            base.BackColor = value;
            this.solidBrush_0 = new SolidBrush(value);
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public bool Boolean_0
    {
        get
        {
            return this.bool_1;
        }
    }

    [Browsable(false)]
    public Class15 Class15_0
    {
        get
        {
            return this.class15_0;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public double Double_0
    {
        get
        {
            return this.double_0;
        }
        set
        {
            if ((value != this.double_0) || (this.Enum0_0 != ((Enum0) 4)))
            {
                this.Enum0_0 = (Enum0) 4;
                this.double_0 = value;
                this.method_12();
                this.method_6(EventArgs.Empty);
            }
        }
    }

    [DefaultValue(1)]
    public Enum0 Enum0_0
    {
        get
        {
            return this.enum0_0;
        }
        set
        {
            if (value != this.enum0_0)
            {
                this.enum0_0 = value;
                this.method_12();
                this.method_6(EventArgs.Empty);
            }
        }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Int32_0
    {
        get
        {
            return this.int_1;
        }
        set
        {
            if (value > (this.Int32_1 - 1))
            {
                value = this.Int32_1 - 1;
            }
            if (value < 0)
            {
                value = 0;
            }
            if (value != this.int_1)
            {
                this.int_1 = value;
                this.method_12();
                this.method_4(EventArgs.Empty);
            }
        }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Int32_1
    {
        get
        {
            return this.class15_0.Count;
        }
    }

    internal class Class7 : PrintDocument
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private object object_0;

        public Class7(Control0 control0_0, int int_3, int int_4)
        {
            this.int_0 = int_3;
            this.int_1 = int_4;
            this.object_0 = control0_0.Class15_0;
            base.DefaultPageSettings = control0_0.method_0().DefaultPageSettings;
            base.PrinterSettings = control0_0.method_0().PrinterSettings;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            this.int_2 = this.int_0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Display;
            e.Graphics.DrawImage(this.object_0[this.int_2++], e.PageBounds);
            e.HasMorePages = this.int_2 <= this.int_1;
        }
    }
}

