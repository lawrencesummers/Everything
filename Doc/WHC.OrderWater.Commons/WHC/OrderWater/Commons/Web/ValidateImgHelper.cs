namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Drawing;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Web;

    public class ValidateImgHelper
    {
        private Bitmap bitmap_0;
        private static byte[] byte_0 = new byte[4];
        private Font[] font_0 = new Font[] { new Font(new FontFamily("Times New Roman"), (float) (10 + smethod_0(1)), FontStyle.Regular), new Font(new FontFamily("Georgia"), (float) (10 + smethod_0(1)), FontStyle.Regular), new Font(new FontFamily("Arial"), (float) (10 + smethod_0(1)), FontStyle.Regular), new Font(new FontFamily("Comic Sans MS"), (float) (10 + smethod_0(1)), FontStyle.Regular) };
        private int int_0 = 4;
        private int int_1 = 0x10;
        private int int_2 = 20;
        private static RNGCryptoServiceProvider rngcryptoServiceProvider_0 = new RNGCryptoServiceProvider();
        private string string_0;

        public ValidateImgHelper()
        {
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            this.string_0 = Class29.smethod_0(4);
            this.CreateImage();
        }

        public void CreateImage()
        {
            int num2;
            int width = this.string_0.Length * this.int_1;
            Bitmap image = new Bitmap(width, this.int_2);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);
            for (num2 = 0; num2 < 2; num2++)
            {
                int num8 = smethod_0(image.Width - 1);
                int num9 = smethod_0(image.Width - 1);
                int num10 = smethod_0(image.Height - 1);
                int num11 = smethod_0(image.Height - 1);
                graphics.DrawLine(new Pen(Color.Silver), num8, num10, num9, num11);
            }
            int x = -12;
            int y = 0;
            for (int i = 0; i < this.string_0.Length; i++)
            {
                x += smethod_1(12, 0x10);
                y = smethod_1(-2, 2);
                string s = this.string_0.Substring(i, 1);
                s = (smethod_0(1) == 1) ? s.ToLower() : s.ToUpper();
                Brush brush = new SolidBrush(this.GetRandomColor());
                Point point = new Point(x, y);
                graphics.DrawString(s, this.font_0[smethod_0(this.font_0.Length - 1)], brush, (PointF) point);
            }
            for (num2 = 0; num2 < 10; num2++)
            {
                int num6 = smethod_0(image.Width - 1);
                int num7 = smethod_0(image.Height - 1);
                image.SetPixel(num6, num7, Color.FromArgb(smethod_1(0, 0xff), smethod_1(0, 0xff), smethod_1(0, 0xff)));
            }
            image = this.TwistImage(image, true, (double) smethod_1(1, 3), (double) smethod_1(4, 6));
            graphics.DrawRectangle(new Pen(Color.LightGray, 1f), 0, 0, width - 1, this.int_2 - 1);
            this.bitmap_0 = image;
        }

        public Color GetRandomColor()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            Thread.Sleep(random.Next(50));
            Random random2 = new Random((int) DateTime.Now.Ticks);
            int red = random.Next(180);
            int green = random2.Next(180);
            int blue = ((red + green) > 300) ? 0 : ((400 - red) - green);
            blue = (blue > 0xff) ? 0xff : blue;
            return Color.FromArgb(red, green, blue);
        }

        private static int smethod_0(int int_3)
        {
            rngcryptoServiceProvider_0.GetBytes(byte_0);
            int num = BitConverter.ToInt32(byte_0, 0) % (int_3 + 1);
            if (num < 0)
            {
                num = -num;
            }
            return num;
        }

        private static int smethod_1(int int_3, int int_4)
        {
            return (smethod_0(int_4 - int_3) + int_3);
        }

        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double num = 6.2831853071795862;
            Bitmap image = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
            graphics.Dispose();
            double num4 = bXDir ? ((double) image.Height) : ((double) image.Width);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    double a = 0.0;
                    a = bXDir ? ((num * j) / num4) : ((num * i) / num4);
                    a += dPhase;
                    double num6 = Math.Sin(a);
                    int x = 0;
                    int y = 0;
                    x = bXDir ? (i + ((int) (num6 * dMultValue))) : i;
                    y = bXDir ? j : (j + ((int) (num6 * dMultValue)));
                    Color pixel = srcBmp.GetPixel(i, j);
                    if ((((x >= 0) && (x < image.Width)) && (y >= 0)) && (y < image.Height))
                    {
                        image.SetPixel(x, y, pixel);
                    }
                }
            }
            srcBmp.Dispose();
            return image;
        }

        public Bitmap Image
        {
            get
            {
                return this.bitmap_0;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

