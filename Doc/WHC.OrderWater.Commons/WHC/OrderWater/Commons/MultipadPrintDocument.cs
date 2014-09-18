namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Text;

    public class MultipadPrintDocument : PrintDocument
    {
        private System.Drawing.Font font_0 = null;
        private const int int_0 = -1;
        private const int int_1 = -2;
        private int int_2 = 0;
        private int int_3 = 0;
        private string string_0 = "";

        private bool method_0()
        {
            int length = Environment.NewLine.Length;
            int num2 = this.string_0.Length - this.int_2;
            if (num2 < length)
            {
                return false;
            }
            string newLine = Environment.NewLine;
            for (int i = 0; i < length; i++)
            {
                if (this.string_0[this.int_2 + i] != newLine[i])
                {
                    return false;
                }
            }
            return true;
        }

        private int method_1()
        {
            if (this.int_2 >= this.string_0.Length)
            {
                return -1;
            }
            if (this.method_0())
            {
                this.int_2 += Environment.NewLine.Length;
                return -2;
            }
            return this.string_0[this.int_2++];
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            this.int_2 = 0;
            this.int_3 = 1;
            base.OnBeginPrint(e);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            // This item is obfuscated and can not be translated.
            base.OnPrintPage(e);
            float width = e.MarginBounds.Width * 3f;
            float num2 = e.MarginBounds.Height * 3f;
            float num3 = 0f;
            float num4 = 0f;
            float num5 = e.MarginBounds.Left * 3f;
            float num6 = e.MarginBounds.Top * 3f;
            float x = num5;
            float y = num6;
            StringBuilder builder = new StringBuilder(0x100);
            StringFormat genericTypographic = StringFormat.GenericTypographic;
            genericTypographic.FormatFlags = StringFormatFlags.DisplayFormatControl;
            genericTypographic.SetTabStops(0f, new float[] { 300f });
            Graphics graphics = e.Graphics;
            graphics.PageUnit = GraphicsUnit.Document;
            float height = graphics.MeasureString("X", this.font_0, 1, genericTypographic).Height;
            if ((height + (height * 3f)) > num2)
            {
                graphics.Dispose();
                e.HasMorePages = false;
                return;
            }
            int num10 = -1;
            int num11 = -1;
        Label_0274:
            num11 = this.method_1();
            if (num11 == -2)
            {
                if (1 == 0)
                {
                    char ch = Convert.ToChar(num11);
                    builder.Append(ch);
                    switch (ch)
                    {
                        case ' ':
                        case '\t':
                            num10 = builder.Length - 1;
                            goto Label_0274;
                    }
                }
                if (builder.Length > 0)
                {
                    num3 = graphics.MeasureString(builder.ToString(), this.font_0, 0x7fffffff, StringFormat.GenericTypographic).Width;
                }
                if (((num11 == -1) || (num3 > width)) || (num11 == -2))
                {
                    if (num3 > width)
                    {
                        if (num10 != -1)
                        {
                            this.int_2 -= (builder.Length - num10) - 1;
                            builder.Length = num10 + 1;
                        }
                        else
                        {
                            builder.Length--;
                            this.int_2--;
                        }
                    }
                    if (builder.Length > 0)
                    {
                        RectangleF layoutRectangle = new RectangleF(x, y, width, height);
                        genericTypographic.Alignment = StringAlignment.Near;
                        graphics.DrawString(builder.ToString(), this.font_0, Brushes.Black, layoutRectangle, genericTypographic);
                    }
                    y += height;
                    num4 += height;
                    builder.Length = 0;
                    num3 = 0f;
                    num10 = -1;
                }
                if ((num4 > (num2 - height)) || (num11 == -1))
                {
                    graphics.Dispose();
                    this.int_3++;
                    e.HasMorePages = num11 != -1;
                    return;
                }
                goto Label_0274;
            }
            goto Label_0113;
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this.font_0;
            }
            set
            {
                this.font_0 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

