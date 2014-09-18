namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class WebPageCapture : IDisposable
    {
        [CompilerGenerated]
        private System.Drawing.Image image_0;
        private ImageEventHandler imageEventHandler_0;
        [CompilerGenerated]
        private Size size_0;
        [CompilerGenerated]
        private string string_0;
        private WebBrowser webBrowser_0;

        public event ImageEventHandler DownloadCompleted
        {
            add
            {
                ImageEventHandler handler2;
                ImageEventHandler handler = this.imageEventHandler_0;
                do
                {
                    handler2 = handler;
                    ImageEventHandler handler3 = (ImageEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<ImageEventHandler>(ref this.imageEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                ImageEventHandler handler2;
                ImageEventHandler handler = this.imageEventHandler_0;
                do
                {
                    handler2 = handler;
                    ImageEventHandler handler3 = (ImageEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<ImageEventHandler>(ref this.imageEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public WebPageCapture() : this(Screen.PrimaryScreen.Bounds.Size)
        {
        }

        public WebPageCapture(Size browserSize)
        {
            this.webBrowser_0 = new WebBrowser();
            this.BrowserSize = browserSize;
            this.webBrowser_0.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser_0_DocumentCompleted);
            this.webBrowser_0.ScrollBarsEnabled = false;
        }

        public WebPageCapture(int width, int height) : this(new Size(width, height))
        {
        }

        public void Dispose()
        {
            this.Image.Dispose();
            this.webBrowser_0.Dispose();
        }

        public void DownloadPage()
        {
            if (!string.IsNullOrEmpty(this.URL))
            {
                this.DownloadPage(this.URL);
            }
        }

        public void DownloadPage(string url)
        {
            this.URL = url;
            this.webBrowser_0.Size = this.BrowserSize;
            this.webBrowser_0.Navigate(url);
        }

        private void webBrowser_0_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Rectangle scrollRectangle = this.webBrowser_0.Document.ActiveElement.ScrollRectangle;
            this.webBrowser_0.Size = new Size(scrollRectangle.Width, scrollRectangle.Height);
            Bitmap bitmap = new Bitmap(scrollRectangle.Width, scrollRectangle.Height);
            try
            {
                this.webBrowser_0.DrawToBitmap(bitmap, scrollRectangle);
                this.Image = bitmap;
            }
            finally
            {
                if (this.imageEventHandler_0 != null)
                {
                    this.imageEventHandler_0(bitmap);
                }
            }
        }

        public Size BrowserSize
        {
            [CompilerGenerated]
            get
            {
                return this.size_0;
            }
            [CompilerGenerated]
            set
            {
                this.size_0 = value;
            }
        }

        public System.Drawing.Image Image
        {
            [CompilerGenerated]
            get
            {
                return this.image_0;
            }
            [CompilerGenerated]
            private set
            {
                this.image_0 = value;
            }
        }

        public string URL
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

        public delegate void ImageEventHandler(Image image);
    }
}

