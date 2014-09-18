namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web.UI;

    public class ValidateImgPage : Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            char[] chArray = "023456789".ToCharArray();
            Random random = new Random();
            string str = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                char ch = chArray[random.Next(0, chArray.Length)];
                if (str.IndexOf(ch) > -1)
                {
                    i--;
                }
                else
                {
                    str = str + ch;
                }
            }
            this.Session["validate_code"] = str;
            this.pRdoTymdqT(str);
        }

        private void pRdoTymdqT(string string_0)
        {
            int width = string_0.Length * 11;
            Bitmap image = new Bitmap(width, 0x13);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);
            Color[] colorArray2 = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
            Random random = new Random();
            for (int i = 0; i < string_0.Length; i++)
            {
                int index = random.Next(7);
                Font font = new Font("Microsoft Sans Serif", 11f);
                Brush brush = new SolidBrush(colorArray2[index]);
                graphics.DrawString(string_0.Substring(i, 1), font, brush, (float) ((i * 10) + 1), 0f, StringFormat.GenericDefault);
            }
            graphics.DrawRectangle(new Pen(Color.Black, 0f), 0, 0, image.Width - 1, image.Height - 1);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            base.Response.ClearContent();
            base.Response.ContentType = "image/Jpeg";
            base.Response.BinaryWrite(stream.ToArray());
            graphics.Dispose();
            image.Dispose();
        }
    }
}

