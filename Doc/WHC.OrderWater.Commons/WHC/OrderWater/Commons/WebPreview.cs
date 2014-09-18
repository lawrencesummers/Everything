namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Threading;

    public class WebPreview
    {
        private Bitmap bitmap_0;
        private bool bool_0;
        private Exception exception_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private Uri uri_0;

        private WebPreview(Uri uri) : this(uri, 0x7530, 200, 150, true)
        {
        }

        private WebPreview(Uri uri, Size size) : this(uri, 0x7530, size.Width, size.Height, true)
        {
        }

        private WebPreview(Uri uri, int timeout, int width, int height, bool fullPage)
        {
            this.uri_0 = null;
            this.exception_0 = null;
            this.bitmap_0 = null;
            this.int_0 = 0x7530;
            this.int_1 = 200;
            this.int_2 = 150;
            this.bool_0 = true;
            this.uri_0 = uri;
            this.int_0 = timeout;
            this.int_1 = width;
            this.int_2 = height;
            this.bool_0 = fullPage;
        }

        public static Bitmap GetWebPreview(Uri uri)
        {
            WebPreview preview = new WebPreview(uri);
            return preview.method_0();
        }

        public static Bitmap GetWebPreview(Uri uri, Size size)
        {
            WebPreview preview = new WebPreview(uri, size);
            return preview.method_0();
        }

        public static Bitmap GetWebPreview(Uri uri, int timeout, int width, int height, bool fullPage)
        {
            WebPreview preview = new WebPreview(uri, timeout, width, height, fullPage);
            return preview.method_0();
        }

        internal Bitmap method_0()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(WebPreview.smethod_0));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(this);
            if (!thread.Join(this.int_0))
            {
                thread.Abort();
                throw new TimeoutException();
            }
            if (this.exception_0 != null)
            {
                throw this.exception_0;
            }
            if (this.bitmap_0 == null)
            {
                throw new ExecutionEngineException();
            }
            return this.bitmap_0;
        }

        private static void smethod_0(WebPreview webPreview_0)
        {
            WebPreview preview = webPreview_0;
            try
            {
                preview.bitmap_0 = new Class16(preview.uri_0, preview.int_1, preview.int_2, preview.bool_0).method_7();
            }
            catch (Exception exception)
            {
                preview.exception_0 = exception;
            }
        }
    }
}

