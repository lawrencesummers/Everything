namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class ImagePrintHelper
    {
        public bool AllowPrintCenter;
        public bool AllowPrintEnlarge;
        public bool AllowPrintRotate;
        public bool AllowPrintShrink;
        private CoolPrintPreviewDialog coolPrintPreviewDialog_0;
        private Image image_0;
        private PrintDialog printDialog_0;
        private PrintDocument printDocument_0;

        public ImagePrintHelper(Image image) : this(image, "test.png")
        {
        }

        public ImagePrintHelper(Image image, string documentname)
        {
            this.printDocument_0 = new PrintDocument();
            this.printDialog_0 = new PrintDialog();
            this.coolPrintPreviewDialog_0 = new CoolPrintPreviewDialog();
            this.AllowPrintCenter = true;
            this.AllowPrintRotate = true;
            this.AllowPrintEnlarge = true;
            this.AllowPrintShrink = true;
            this.image_0 = (Image) image.Clone();
            this.printDialog_0.UseEXDialog = true;
            this.printDocument_0.DocumentName = documentname;
            this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
            this.printDialog_0.Document = this.printDocument_0;
            this.coolPrintPreviewDialog_0.Document = this.printDocument_0;
        }

        private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
        {
            ContentAlignment topRight = this.AllowPrintCenter ? ContentAlignment.MiddleCenter : ContentAlignment.TopLeft;
            RectangleF printableArea = e.PageSettings.PrintableArea;
            GraphicsUnit pixel = GraphicsUnit.Pixel;
            RectangleF bounds = this.image_0.GetBounds(ref pixel);
            if (this.AllowPrintRotate && !(((printableArea.Width <= printableArea.Height) || (bounds.Width >= bounds.Height)) ? ((printableArea.Width >= printableArea.Height) || (bounds.Width <= bounds.Height)) : false))
            {
                this.image_0.RotateFlip(RotateFlipType.Rotate90FlipNone);
                bounds = this.image_0.GetBounds(ref pixel);
                if (topRight.Equals(ContentAlignment.TopLeft))
                {
                    topRight = ContentAlignment.TopRight;
                }
            }
            RectangleF ef = new RectangleF(0f, 0f, bounds.Width, bounds.Height);
            if (this.AllowPrintEnlarge || this.AllowPrintShrink)
            {
                SizeF ef2 = Class25.smethod_0(bounds.Size, printableArea.Size, false);
                if (!((!this.AllowPrintShrink || (ef2.Width >= ef.Width)) ? (!this.AllowPrintEnlarge || (ef2.Width <= ef.Width)) : false))
                {
                    ef.Size = ef2;
                }
            }
            ef = Class25.smethod_1(ef, new RectangleF(0f, 0f, printableArea.Width, printableArea.Height), topRight);
            e.Graphics.DrawImage(this.image_0, ef, bounds, GraphicsUnit.Pixel);
        }

        public void PrintPreview()
        {
            if (this.coolPrintPreviewDialog_0.ShowDialog() == DialogResult.OK)
            {
                this.printDocument_0.Print();
            }
        }

        public PrinterSettings PrintWithDialog()
        {
            if (this.printDialog_0.ShowDialog() == DialogResult.OK)
            {
                this.printDocument_0.Print();
                return this.printDialog_0.PrinterSettings;
            }
            return null;
        }
    }
}

