namespace RDIFramework.Utilities
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class ImageHandler
    {
        private Bitmap bitmap_0;
        private Bitmap bitmap_1;
        private Bitmap bitmap_2;
        private string string_0;

        public void ClearImage()
        {
            this.bitmap_0 = new Bitmap(1, 1);
        }

        public void Crop(int xPosition, int yPosition, int width, int height)
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            if ((xPosition + width) > this.bitmap_0.Width)
            {
                width = this.bitmap_0.Width - xPosition;
            }
            if ((yPosition + height) > this.bitmap_0.Height)
            {
                height = this.bitmap_0.Height - yPosition;
            }
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            this.bitmap_0 = bitmap2.Clone(rect, bitmap2.PixelFormat);
        }

        public void DrawOutCropArea(int xPosition, int yPosition, int width, int height)
        {
            this.bitmap_2 = this.bitmap_0;
            Bitmap image = (Bitmap) this.bitmap_2.Clone();
            Graphics graphics = Graphics.FromImage(image);
            Brush brush = new Pen(Color.FromArgb(150, Color.White)).Brush;
            Rectangle rect = new Rectangle(0, 0, this.bitmap_0.Width, yPosition);
            Rectangle rectangle2 = new Rectangle(0, yPosition, xPosition, height);
            Rectangle rectangle3 = new Rectangle(0, yPosition + height, this.bitmap_0.Width, this.bitmap_0.Height);
            Rectangle rectangle4 = new Rectangle(xPosition + width, yPosition, (this.bitmap_0.Width - xPosition) - width, height);
            graphics.FillRectangle(brush, rect);
            graphics.FillRectangle(brush, rectangle2);
            graphics.FillRectangle(brush, rectangle3);
            graphics.FillRectangle(brush, rectangle4);
            this.bitmap_0 = (Bitmap) image.Clone();
        }

        public void InsertImage(string imagePath, int xPosition, int yPosition)
        {
            Bitmap image = (Bitmap) this.bitmap_0.Clone();
            Graphics graphics = Graphics.FromImage(image);
            if (!string.IsNullOrEmpty(imagePath))
            {
                Bitmap bitmap3 = (Bitmap) Image.FromFile(imagePath);
                Rectangle rect = new Rectangle(xPosition, yPosition, bitmap3.Width, bitmap3.Height);
                graphics.DrawImage(Image.FromFile(imagePath), rect);
            }
            this.bitmap_0 = (Bitmap) image.Clone();
        }

        public void InsertShape(string shapeType, int xPosition, int yPosition, int width, int height, string colorName)
        {
            Bitmap image = (Bitmap) this.bitmap_0.Clone();
            Graphics graphics = Graphics.FromImage(image);
            if (string.IsNullOrEmpty(colorName))
            {
                colorName = "Black";
            }
            Pen pen = new Pen(Color.FromName(colorName));
            string str = shapeType.ToLower();
            switch (str)
            {
                case null:
                    break;

                case "filledellipse":
                    graphics.FillEllipse(pen.Brush, xPosition, yPosition, width, height);
                    goto Label_00C4;

                default:
                    if (str != "filledrectangle")
                    {
                        if (!(str == "ellipse"))
                        {
                            if (str == "rectangle")
                            {
                            }
                            break;
                        }
                        graphics.DrawEllipse(pen, xPosition, yPosition, width, height);
                    }
                    else
                    {
                        graphics.FillRectangle(pen.Brush, xPosition, yPosition, width, height);
                    }
                    goto Label_00C4;
            }
            graphics.DrawRectangle(pen, xPosition, yPosition, width, height);
        Label_00C4:
            this.bitmap_0 = (Bitmap) image.Clone();
        }

        public void InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, string fontStyle, string colorName1, string colorName2)
        {
            Bitmap image = (Bitmap) this.bitmap_0.Clone();
            Graphics graphics = Graphics.FromImage(image);
            if (string.IsNullOrEmpty(fontName))
            {
                fontName = "Times New Roman";
            }
            if (fontSize.Equals(null))
            {
                fontSize = 10f;
            }
            Font font = new Font(fontName, fontSize);
            if (!string.IsNullOrEmpty(fontStyle))
            {
                FontStyle regular = FontStyle.Regular;
                string str = fontStyle.ToLower();
                switch (str)
                {
                    case null:
                        break;

                    case "bold":
                        regular = FontStyle.Bold;
                        break;

                    case "italic":
                        regular = FontStyle.Italic;
                        break;

                    default:
                        if (!(str == "underline"))
                        {
                            if (str == "strikeout")
                            {
                                regular = FontStyle.Strikeout;
                            }
                        }
                        else
                        {
                            regular = FontStyle.Underline;
                        }
                        break;
                }
                font = new Font(fontName, fontSize, regular);
            }
            if (string.IsNullOrEmpty(colorName1))
            {
                colorName1 = "Black";
            }
            if (string.IsNullOrEmpty(colorName2))
            {
                colorName2 = colorName1;
            }
            Color color = Color.FromName(colorName1);
            Color color2 = Color.FromName(colorName2);
            int width = (int) (text.Length * fontSize);
            width = (width == 0) ? 10 : width;
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, (int) fontSize), color, color2, LinearGradientMode.Vertical);
            graphics.DrawString(text, font, brush, (float) xPosition, (float) yPosition);
            this.bitmap_0 = (Bitmap) image.Clone();
        }

        private byte[] method_0(double double_0)
        {
            byte[] buffer = new byte[0x100];
            for (int i = 0; i < 0x100; i++)
            {
                buffer[i] = (byte) Math.Min(0xff, (int) ((255.0 * Math.Pow(((double) i) / 255.0, 1.0 / double_0)) + 0.5));
            }
            return buffer;
        }

        public static Bitmap QuickWhiteAndBlack(string filePath)
        {
            Bitmap bitmap = new Bitmap(filePath);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr source = bitmapdata.Scan0;
            int length = (bitmap.Width * bitmap.Height) * 3;
            byte[] destination = new byte[length];
            Marshal.Copy(source, destination, 0, length);
            for (int i = 0; i < destination.Length; i += 3)
            {
                byte num3 = (byte) (((destination[i] * 0.299) + (destination[i + 2] * 0.114)) + (destination[i + 1] * 0.587));
                destination[i] = num3;
                destination[i + 1] = num3;
                destination[i + 2] = num3;
            }
            Marshal.Copy(destination, 0, source, length);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public void RemoveCropAreaDraw()
        {
            this.bitmap_0 = (Bitmap) this.bitmap_2.Clone();
        }

        public void ResetBitmap()
        {
            if ((this.bitmap_0 != null) && (this.bitmap_1 != null))
            {
                Bitmap bitmap = (Bitmap) this.bitmap_0.Clone();
                this.bitmap_0 = (Bitmap) this.bitmap_1.Clone();
                this.bitmap_1 = (Bitmap) bitmap.Clone();
            }
        }

        public void Resize(int newWidth, int newHeight)
        {
            if ((newWidth != 0) && (newHeight != 0))
            {
                Bitmap bitmap = this.bitmap_0;
                Bitmap bitmap2 = new Bitmap(newWidth, newHeight, bitmap.PixelFormat);
                double num = ((double) bitmap.Width) / ((double) newWidth);
                double num2 = ((double) bitmap.Height) / ((double) newHeight);
                Color pixel = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                Color color4 = new Color();
                for (int i = 0; i < bitmap2.Width; i++)
                {
                    for (int j = 0; j < bitmap2.Height; j++)
                    {
                        int x = (int) Math.Floor((double) (i * num));
                        int y = (int) Math.Floor((double) (j * num2));
                        int num7 = x + 1;
                        if (num7 >= bitmap.Width)
                        {
                            num7 = x;
                        }
                        int num8 = y + 1;
                        if (num8 >= bitmap.Height)
                        {
                            num8 = y;
                        }
                        double num9 = (i * num) - x;
                        double num10 = (j * num2) - y;
                        double num11 = 1.0 - num9;
                        double num12 = 1.0 - num10;
                        pixel = bitmap.GetPixel(x, y);
                        color2 = bitmap.GetPixel(num7, y);
                        color3 = bitmap.GetPixel(x, num8);
                        color4 = bitmap.GetPixel(num7, num8);
                        byte num13 = (byte) ((num11 * pixel.B) + (num9 * color2.B));
                        byte num14 = (byte) ((num11 * color3.B) + (num9 * color4.B));
                        byte blue = (byte) ((num12 * num13) + (num10 * num14));
                        num13 = (byte) ((num11 * pixel.G) + (num9 * color2.G));
                        num14 = (byte) ((num11 * color3.G) + (num9 * color4.G));
                        byte green = (byte) ((num12 * num13) + (num10 * num14));
                        num13 = (byte) ((num11 * pixel.R) + (num9 * color2.R));
                        num14 = (byte) ((num11 * color3.R) + (num9 * color4.R));
                        byte red = (byte) ((num12 * num13) + (num10 * num14));
                        bitmap2.SetPixel(i, j, Color.FromArgb(0xff, red, green, blue));
                    }
                }
                this.bitmap_0 = (Bitmap) bitmap2.Clone();
            }
        }

        public void RestorePrevious()
        {
            this.bitmap_1 = this.bitmap_0;
        }

        public void RotateFlip(RotateFlipType rotateFlipType)
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            bitmap2.RotateFlip(rotateFlipType);
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public void SaveBitmap(string saveFilePath)
        {
            this.string_0 = saveFilePath;
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            this.bitmap_0.Save(saveFilePath);
        }

        public void SetBrightness(int brightness)
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            if (brightness < -255)
            {
                brightness = -255;
            }
            if (brightness > 0xff)
            {
                brightness = 0xff;
            }
            for (int i = 0; i < bitmap2.Width; i++)
            {
                for (int j = 0; j < bitmap2.Height; j++)
                {
                    Color pixel = bitmap2.GetPixel(i, j);
                    int num3 = pixel.R + brightness;
                    int num4 = pixel.G + brightness;
                    int num5 = pixel.B + brightness;
                    if (num3 < 0)
                    {
                        num3 = 1;
                    }
                    if (num3 > 0xff)
                    {
                        num3 = 0xff;
                    }
                    if (num4 < 0)
                    {
                        num4 = 1;
                    }
                    if (num4 > 0xff)
                    {
                        num4 = 0xff;
                    }
                    if (num5 < 0)
                    {
                        num5 = 1;
                    }
                    if (num5 > 0xff)
                    {
                        num5 = 0xff;
                    }
                    bitmap2.SetPixel(i, j, Color.FromArgb((byte) num3, (byte) num4, (byte) num5));
                }
            }
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public void SetColorFilter(ColorFilterTypes colorFilterType)
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            for (int i = 0; i < bitmap2.Width; i++)
            {
                for (int j = 0; j < bitmap2.Height; j++)
                {
                    Color pixel = bitmap2.GetPixel(i, j);
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    if (colorFilterType == ColorFilterTypes.Red)
                    {
                        r = pixel.R;
                        g = pixel.G - 0xff;
                        b = pixel.B - 0xff;
                    }
                    else if (colorFilterType == ColorFilterTypes.Green)
                    {
                        r = pixel.R - 0xff;
                        g = pixel.G;
                        b = pixel.B - 0xff;
                    }
                    else if (colorFilterType == ColorFilterTypes.Blue)
                    {
                        r = pixel.R - 0xff;
                        g = pixel.G - 0xff;
                        b = pixel.B;
                    }
                    r = Math.Max(r, 0);
                    r = Math.Min(0xff, r);
                    g = Math.Max(g, 0);
                    g = Math.Min(0xff, g);
                    b = Math.Max(b, 0);
                    b = Math.Min(0xff, b);
                    bitmap2.SetPixel(i, j, Color.FromArgb((byte) r, (byte) g, (byte) b));
                }
            }
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public void SetContrast(double contrast)
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            if (contrast < -100.0)
            {
                contrast = -100.0;
            }
            if (contrast > 100.0)
            {
                contrast = 100.0;
            }
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            for (int i = 0; i < bitmap2.Width; i++)
            {
                for (int j = 0; j < bitmap2.Height; j++)
                {
                    Color pixel = bitmap2.GetPixel(i, j);
                    double num3 = ((double) pixel.R) / 255.0;
                    num3 -= 0.5;
                    num3 *= contrast;
                    num3 += 0.5;
                    num3 *= 255.0;
                    if (num3 < 0.0)
                    {
                        num3 = 0.0;
                    }
                    if (num3 > 255.0)
                    {
                        num3 = 255.0;
                    }
                    double num4 = ((double) pixel.G) / 255.0;
                    num4 -= 0.5;
                    num4 *= contrast;
                    num4 += 0.5;
                    num4 *= 255.0;
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    if (num4 > 255.0)
                    {
                        num4 = 255.0;
                    }
                    double num5 = ((double) pixel.B) / 255.0;
                    num5 -= 0.5;
                    num5 *= contrast;
                    num5 += 0.5;
                    num5 *= 255.0;
                    if (num5 < 0.0)
                    {
                        num5 = 0.0;
                    }
                    if (num5 > 255.0)
                    {
                        num5 = 255.0;
                    }
                    bitmap2.SetPixel(i, j, Color.FromArgb((byte) num3, (byte) num4, (byte) num5));
                }
            }
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public void SetGamma(double red, double green, double blue)
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            byte[] buffer = this.method_0(red);
            byte[] buffer2 = this.method_0(green);
            byte[] buffer3 = this.method_0(blue);
            for (int i = 0; i < bitmap2.Width; i++)
            {
                for (int j = 0; j < bitmap2.Height; j++)
                {
                    Color pixel = bitmap2.GetPixel(i, j);
                    bitmap2.SetPixel(i, j, Color.FromArgb(buffer[pixel.R], buffer2[pixel.G], buffer3[pixel.B]));
                }
            }
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public void SetGrayscale()
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            for (int i = 0; i < bitmap2.Width; i++)
            {
                for (int j = 0; j < bitmap2.Height; j++)
                {
                    Color pixel = bitmap2.GetPixel(i, j);
                    byte red = (byte) (((0.299 * pixel.R) + (0.587 * pixel.G)) + (0.114 * pixel.B));
                    bitmap2.SetPixel(i, j, Color.FromArgb(red, red, red));
                }
            }
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public void SetInvert()
        {
            Bitmap bitmap2 = (Bitmap) this.bitmap_0.Clone();
            for (int i = 0; i < bitmap2.Width; i++)
            {
                for (int j = 0; j < bitmap2.Height; j++)
                {
                    Color pixel = bitmap2.GetPixel(i, j);
                    bitmap2.SetPixel(i, j, Color.FromArgb(0xff - pixel.R, 0xff - pixel.G, 0xff - pixel.B));
                }
            }
            this.bitmap_0 = (Bitmap) bitmap2.Clone();
        }

        public Bitmap BitmapBeforeProcessing
        {
            get
            {
                return this.bitmap_1;
            }
            set
            {
                this.bitmap_1 = value;
            }
        }

        public string BitmapPath
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

        public Bitmap CurrentBitmap
        {
            get
            {
                if (this.bitmap_0 == null)
                {
                    this.bitmap_0 = new Bitmap(1, 1);
                }
                return this.bitmap_0;
            }
            set
            {
                this.bitmap_0 = value;
            }
        }

        public enum ColorFilterTypes
        {
            Red,
            Green,
            Blue
        }
    }
}

