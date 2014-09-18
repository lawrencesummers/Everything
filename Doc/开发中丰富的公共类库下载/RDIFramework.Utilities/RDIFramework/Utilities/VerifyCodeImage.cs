namespace RDIFramework.Utilities
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    public class VerifyCodeImage
    {
        private bool bool_0 = true;
        private Color color_0 = Color.LightGray;
        private Color color_1 = Color.White;
        private Color[] color_2 = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        private const double double_0 = 6.2831853071795862;
        private int int_0 = 4;
        private int int_1 = 50;
        private int int_2 = 2;
        private string[] string_0 = new string[] { "Arial", "Georgia" };
        private string string_1 = "2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";

        public Bitmap CreateImage(string code, double multValue)
        {
            int num6;
            int fontSize = this.FontSize;
            int num2 = fontSize + this.Padding;
            int width = ((code.Length * num2) + 4) + (this.Padding * 2);
            int height = (fontSize * 2) + this.Padding;
            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(this.BackgroundColor);
            Random random = new Random();
            if (this.Chaos)
            {
                Pen pen = new Pen(this.ChaosColor, 0f);
                int num5 = this.Length * 10;
                for (num6 = 0; num6 < num5; num6++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    graphics.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            int num9 = 0;
            int num10 = 0;
            int num11 = 1;
            int num12 = 1;
            int num13 = (height - this.FontSize) - (this.Padding * 2);
            int num14 = num13 / 4;
            num11 = num14;
            num12 = num14 * 2;
            for (num6 = 0; num6 < code.Length; num6++)
            {
                int index = random.Next(this.Colors.Length - 1);
                int num16 = random.Next(this.Fonts.Length - 1);
                Font font = new Font(this.Fonts[num16], (float) fontSize, FontStyle.Bold);
                Brush brush = new SolidBrush(this.Colors[index]);
                if ((num6 % 2) == 1)
                {
                    num10 = num12;
                }
                else
                {
                    num10 = num11;
                }
                num9 = num6 * num2;
                graphics.DrawString(code.Substring(num6, 1), font, brush, (float) num9, (float) num10);
            }
            graphics.DrawRectangle(new Pen(Color.Gainsboro, 0f), 0, 0, image.Width - 1, image.Height - 1);
            graphics.Dispose();
            return this.TwistImage(image, true, multValue, 4.0);
        }

        public void CreateImageOnPage(string code, double multValue, HttpContext httpContext)
        {
            MemoryStream stream = new MemoryStream();
            Bitmap bitmap = this.CreateImage(code, multValue);
            bitmap.Save(stream, ImageFormat.Jpeg);
            httpContext.Response.ClearContent();
            httpContext.Response.ContentType = "image/Jpeg";
            httpContext.Response.BinaryWrite(stream.GetBuffer());
            stream.Close();
            stream = null;
            bitmap.Dispose();
            bitmap = null;
        }

        public string CreateVerifyCode()
        {
            return this.CreateVerifyCode(0);
        }

        public string CreateVerifyCode(int codeLength)
        {
            if (codeLength == 0)
            {
                codeLength = this.Length;
            }
            string[] strArray = this.CodeSerial.Split(new char[] { ',' });
            string str = "";
            int index = -1;
            Random random = new Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < codeLength; i++)
            {
                index = random.Next(0, strArray.Length - 1);
                str = str + strArray[index];
            }
            return str;
        }

        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap image = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
            graphics.Dispose();
            double num = bXDir ? ((double) image.Height) : ((double) image.Width);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    double a = 0.0;
                    a = bXDir ? ((6.2831853071795862 * j) / num) : ((6.2831853071795862 * i) / num);
                    a += dPhase;
                    double num5 = Math.Sin(a);
                    int x = 0;
                    int y = 0;
                    x = bXDir ? (i + ((int) (num5 * dMultValue))) : i;
                    y = bXDir ? j : (j + ((int) (num5 * dMultValue)));
                    Color pixel = srcBmp.GetPixel(i, j);
                    if ((((x >= 0) && (x < image.Width)) && (y >= 0)) && (y < image.Height))
                    {
                        image.SetPixel(x, y, pixel);
                    }
                }
            }
            return image;
        }

        public Color BackgroundColor
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
            }
        }

        public bool Chaos
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public Color ChaosColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }

        public string CodeSerial
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public Color[] Colors
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
            }
        }

        public string[] Fonts
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

        public int FontSize
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int Length
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public int Padding
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }
    }
}

