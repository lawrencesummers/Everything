namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;
    using System.Windows.Forms;

    public class MyScreenCapture
    {
        private Bitmap bitmap_0;
        private Bitmap[] bitmap_1;
        private ImageFormatHandler imageFormatHandler_0;
        private PrintDocument printDocument_0;

        public MyScreenCapture()
        {
            this.bitmap_1 = null;
            this.printDocument_0 = new PrintDocument();
            this.imageFormatHandler_0 = null;
            this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
            this.imageFormatHandler_0 = new ImageFormatHandler();
        }

        public MyScreenCapture(ImageFormatHandler formatHandler)
        {
            this.bitmap_1 = null;
            this.printDocument_0 = new PrintDocument();
            this.imageFormatHandler_0 = null;
            this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
            this.imageFormatHandler_0 = formatHandler;
        }

        public virtual Bitmap Capture(IntPtr handle)
        {
            NativeMethods.BringWindowToTop(handle);
            CaptureHandleDelegateHandler handler = new CaptureHandleDelegateHandler(this.CaptureHandle);
            IAsyncResult result = handler.BeginInvoke(handle, null, null);
            return handler.EndInvoke(result);
        }

        public virtual Bitmap Capture(Form window)
        {
            Rectangle rectangle = new Rectangle(window.Location, window.Size);
            return this.method_0(window, rectangle);
        }

        public virtual Bitmap[] Capture(CaptureType typeOfCapture)
        {
            int length = 1;
            try
            {
                Rectangle virtualScreen;
                Screen[] allScreens = Screen.AllScreens;
                switch (typeOfCapture)
                {
                    case CaptureType.VirtualScreen:
                        virtualScreen = SystemInformation.VirtualScreen;
                        break;

                    case CaptureType.PrimaryScreen:
                        virtualScreen = Screen.PrimaryScreen.Bounds;
                        break;

                    case CaptureType.WorkingArea:
                        virtualScreen = Screen.PrimaryScreen.WorkingArea;
                        break;

                    case CaptureType.AllScreens:
                        length = allScreens.Length;
                        typeOfCapture = CaptureType.WorkingArea;
                        virtualScreen = allScreens[0].WorkingArea;
                        break;

                    default:
                        virtualScreen = SystemInformation.VirtualScreen;
                        break;
                }
                this.bitmap_1 = new Bitmap[length];
                for (int i = 0; i < length; i++)
                {
                    if (i > 0)
                    {
                        virtualScreen = allScreens[i].WorkingArea;
                    }
                    Bitmap image = new Bitmap(virtualScreen.Width, virtualScreen.Height, PixelFormat.Format32bppArgb);
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        graphics.CopyFromScreen(virtualScreen.X, virtualScreen.Y, 0, 0, virtualScreen.Size, CopyPixelOperation.SourceCopy);
                    }
                    this.bitmap_1[i] = image;
                }
            }
            catch (Exception)
            {
            }
            return this.bitmap_1;
        }

        public virtual Bitmap Capture(Form window, bool onlyClient)
        {
            if (!onlyClient)
            {
                return this.Capture(window);
            }
            Rectangle rectangle = window.RectangleToScreen(window.ClientRectangle);
            return this.method_0(window, rectangle);
        }

        public virtual Bitmap Capture(IntPtr handle, string filename, ImageFormatHandler.ImageFormatTypes format)
        {
            this.Capture(handle);
            this.Save(filename, format);
            return this.bitmap_1[0];
        }

        public virtual Bitmap Capture(Form window, string filename, ImageFormatHandler.ImageFormatTypes format)
        {
            return this.Capture(window, filename, format, false);
        }

        public virtual Bitmap[] Capture(CaptureType typeOfCapture, string filename, ImageFormatHandler.ImageFormatTypes format)
        {
            this.Capture(typeOfCapture);
            this.Save(filename, format);
            return this.bitmap_1;
        }

        public virtual Bitmap Capture(Form window, string filename, ImageFormatHandler.ImageFormatTypes format, bool onlyClient)
        {
            this.Capture(window, onlyClient);
            this.Save(filename, format);
            return this.bitmap_1[0];
        }

        public virtual Bitmap CaptureControl(Control window)
        {
            Rectangle rectangle = window.RectangleToScreen(window.DisplayRectangle);
            return this.method_0(window, rectangle);
        }

        public virtual Bitmap CaptureControl(Control window, string filename, ImageFormatHandler.ImageFormatTypes format)
        {
            this.CaptureControl(window);
            this.Save(filename, format);
            return this.bitmap_1[0];
        }

        protected virtual Bitmap CaptureHandle(IntPtr handle)
        {
            Bitmap image = null;
            this.bitmap_1 = new Bitmap[1];
            try
            {
                using (Graphics graphics = Graphics.FromHwnd(handle))
                {
                    Rectangle windowRect = NativeMethods.GetWindowRect(handle);
                    if ((((int) graphics.VisibleClipBounds.Width) > 0) && (((int) graphics.VisibleClipBounds.Height) > 0))
                    {
                        image = new Bitmap(windowRect.Width, windowRect.Height, graphics);
                        using (Graphics graphics2 = Graphics.FromImage(image))
                        {
                            graphics2.CopyFromScreen(windowRect.X, windowRect.Y, 0, 0, windowRect.Size, CopyPixelOperation.SourceCopy);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.bitmap_1[0] = image;
            return image;
        }

        private Bitmap method_0(Control control_0, Rectangle rectangle_0)
        {
            Bitmap image = null;
            this.bitmap_1 = new Bitmap[1];
            try
            {
                using (Graphics graphics = control_0.CreateGraphics())
                {
                    image = new Bitmap(rectangle_0.Width, rectangle_0.Height, graphics);
                    using (Graphics graphics2 = Graphics.FromImage(image))
                    {
                        graphics2.CopyFromScreen(rectangle_0.X, rectangle_0.Y, 0, 0, rectangle_0.Size, CopyPixelOperation.SourceCopy);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.bitmap_1[0] = image;
            return image;
        }

        public virtual void Print()
        {
            if (this.bitmap_1 != null)
            {
                try
                {
                    for (int i = 0; i < this.bitmap_1.Length; i++)
                    {
                        this.bitmap_0 = this.bitmap_1[i];
                        this.printDocument_0.DefaultPageSettings.Landscape = this.bitmap_0.Width > this.bitmap_0.Height;
                        this.printDocument_0.Print();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
        {
            RectangleF bounds = this.printDocument_0.DefaultPageSettings.Bounds;
            float num = ((float) this.bitmap_0.Height) / ((this.bitmap_0.Width != 0) ? ((float) this.bitmap_0.Width) : ((float) 1));
            bounds.Height = (bounds.Height - this.printDocument_0.DefaultPageSettings.Margins.Top) - this.printDocument_0.DefaultPageSettings.Margins.Bottom;
            bounds.Y += this.printDocument_0.DefaultPageSettings.Margins.Top;
            bounds.Width = (bounds.Width - this.printDocument_0.DefaultPageSettings.Margins.Left) - this.printDocument_0.DefaultPageSettings.Margins.Right;
            bounds.X += this.printDocument_0.DefaultPageSettings.Margins.Left;
            if ((bounds.Height / bounds.Width) > num)
            {
                bounds.Height = bounds.Width * num;
            }
            else
            {
                bounds.Width = bounds.Height / ((num != 0f) ? num : 1f);
            }
            e.Graphics.DrawImage(this.bitmap_0, bounds);
        }

        public virtual void Save(string filename, ImageFormatHandler.ImageFormatTypes format)
        {
            string directoryName = Path.GetDirectoryName(filename);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
            string extension = Path.GetExtension(filename);
            extension = this.imageFormatHandler_0.GetDefaultFilenameExtension(format);
            if (extension.Length == 0)
            {
                format = ImageFormatHandler.ImageFormatTypes.imgPNG;
                extension = "png";
            }
            try
            {
                ImageCodecInfo info;
                EncoderParameters encoderParameters = this.imageFormatHandler_0.GetEncoderParameters(format, out info);
                for (int i = 0; i < this.bitmap_1.Length; i++)
                {
                    if (this.bitmap_1.Length > 1)
                    {
                        filename = string.Format(@"{0}\{1}.{2:D2}.{3}", new object[] { directoryName, fileNameWithoutExtension, i + 1, extension });
                    }
                    else
                    {
                        filename = string.Format(@"{0}\{1}.{2}", directoryName, fileNameWithoutExtension, extension);
                    }
                    this.bitmap_0 = this.bitmap_1[i];
                    if (encoderParameters != null)
                    {
                        this.bitmap_0.Save(filename, info, encoderParameters);
                    }
                    else
                    {
                        this.bitmap_0.Save(filename, ImageFormatHandler.GetImageFormat(format));
                    }
                }
            }
            catch (Exception exception)
            {
                string.Format("Saving image to [{0}] in format [{1}].\n{2}", filename, format.ToString(), exception.ToString());
            }
        }

        public ImageFormatHandler FormatHandler
        {
            set
            {
                this.imageFormatHandler_0 = value;
            }
        }

        public enum CaptureType
        {
            VirtualScreen,
            PrimaryScreen,
            WorkingArea,
            AllScreens
        }
    }
}

