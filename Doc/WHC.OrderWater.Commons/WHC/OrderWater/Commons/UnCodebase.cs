namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class UnCodebase
    {
        public Bitmap bmpobj;

        public UnCodebase(Bitmap pic)
        {
            this.bmpobj = new Bitmap(pic);
        }

        public UnCodebase(string filename)
        {
            if (File.Exists(filename))
            {
                this.bmpobj = (Bitmap) Image.FromFile(filename);
            }
        }

        public void ClearPicBorder(int borderWidth)
        {
            for (int i = 0; i < this.bmpobj.Height; i++)
            {
                for (int j = 0; j < this.bmpobj.Width; j++)
                {
                    if ((((i < borderWidth) || (j < borderWidth)) || (j > ((this.bmpobj.Width - 1) - borderWidth))) || (i > ((this.bmpobj.Height - 1) - borderWidth)))
                    {
                        this.bmpobj.SetPixel(j, i, Color.FromArgb(0xff, 0xff, 0xff));
                    }
                }
            }
        }

        public void GetPicValidByValue(int dgGrayValue)
        {
            int width = this.bmpobj.Width;
            int height = this.bmpobj.Height;
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < this.bmpobj.Height; i++)
            {
                for (int j = 0; j < this.bmpobj.Width; j++)
                {
                    if (this.bmpobj.GetPixel(j, i).R < dgGrayValue)
                    {
                        if (width > j)
                        {
                            width = j;
                        }
                        if (height > i)
                        {
                            height = i;
                        }
                        if (num3 < j)
                        {
                            num3 = j;
                        }
                        if (num4 < i)
                        {
                            num4 = i;
                        }
                    }
                }
            }
            Rectangle rect = new Rectangle(width, height, (num3 - width) + 1, (num4 - height) + 1);
            this.bmpobj = this.bmpobj.Clone(rect, this.bmpobj.PixelFormat);
        }

        public Bitmap GetPicValidByValue(Bitmap singlepic, int dgGrayValue)
        {
            int width = singlepic.Width;
            int height = singlepic.Height;
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < singlepic.Height; i++)
            {
                for (int j = 0; j < singlepic.Width; j++)
                {
                    if (singlepic.GetPixel(j, i).R < dgGrayValue)
                    {
                        if (width > j)
                        {
                            width = j;
                        }
                        if (height > i)
                        {
                            height = i;
                        }
                        if (num3 < j)
                        {
                            num3 = j;
                        }
                        if (num4 < i)
                        {
                            num4 = i;
                        }
                    }
                }
            }
            Rectangle rect = new Rectangle(width, height, (num3 - width) + 1, (num4 - height) + 1);
            return singlepic.Clone(rect, singlepic.PixelFormat);
        }

        public void GetPicValidByValue(int dgGrayValue, int CharsCount)
        {
            int width = this.bmpobj.Width;
            int height = this.bmpobj.Height;
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < this.bmpobj.Height; i++)
            {
                for (int j = 0; j < this.bmpobj.Width; j++)
                {
                    if (this.bmpobj.GetPixel(j, i).R < dgGrayValue)
                    {
                        if (width > j)
                        {
                            width = j;
                        }
                        if (height > i)
                        {
                            height = i;
                        }
                        if (num3 < j)
                        {
                            num3 = j;
                        }
                        if (num4 < i)
                        {
                            num4 = i;
                        }
                    }
                }
            }
            int num7 = CharsCount - (((num3 - width) + 1) % CharsCount);
            if (num7 < CharsCount)
            {
                int num8 = num7 / 2;
                if (width > num8)
                {
                    width -= num8;
                }
                if (((num3 + num7) - num8) < this.bmpobj.Width)
                {
                    num3 = (num3 + num7) - num8;
                }
            }
            Rectangle rect = new Rectangle(width, height, (num3 - width) + 1, (num4 - height) + 1);
            this.bmpobj = this.bmpobj.Clone(rect, this.bmpobj.PixelFormat);
        }

        public string GetSingleBmpCode(Bitmap singlepic, int dgGrayValue)
        {
            string str = "";
            for (int i = 0; i < singlepic.Height; i++)
            {
                for (int j = 0; j < singlepic.Width; j++)
                {
                    if (singlepic.GetPixel(j, i).R < dgGrayValue)
                    {
                        str = str + "1";
                    }
                    else
                    {
                        str = str + "0";
                    }
                }
            }
            return str;
        }

        public Bitmap[] GetSplitPics(int RowNum, int ColNum)
        {
            if ((RowNum == 0) || (ColNum == 0))
            {
                return null;
            }
            int width = this.bmpobj.Width / RowNum;
            int height = this.bmpobj.Height / ColNum;
            Bitmap[] bitmapArray = new Bitmap[RowNum * ColNum];
            for (int i = 0; i < ColNum; i++)
            {
                for (int j = 0; j < RowNum; j++)
                {
                    Rectangle rect = new Rectangle(j * width, i * height, width, height);
                    bitmapArray[(i * RowNum) + j] = this.bmpobj.Clone(rect, this.bmpobj.PixelFormat);
                }
            }
            return bitmapArray;
        }

        public void GrayByLine()
        {
            Rectangle rect = new Rectangle(0, 0, this.bmpobj.Width, this.bmpobj.Height);
            BitmapData bitmapdata = this.bmpobj.LockBits(rect, ImageLockMode.ReadWrite, this.bmpobj.PixelFormat);
            IntPtr source = bitmapdata.Scan0;
            int length = this.bmpobj.Width * this.bmpobj.Height;
            int[] destination = new int[length];
            Marshal.Copy(source, destination, 0, length);
            int red = 0;
            for (int i = 0; i < length; i++)
            {
                red = this.method_0(Color.FromArgb(destination[i]));
                destination[i] = (byte) Color.FromArgb(red, red, red).ToArgb();
            }
            this.bmpobj.UnlockBits(bitmapdata);
        }

        public void GrayByPixels()
        {
            for (int i = 0; i < this.bmpobj.Height; i++)
            {
                for (int j = 0; j < this.bmpobj.Width; j++)
                {
                    int red = this.method_0(this.bmpobj.GetPixel(j, i));
                    this.bmpobj.SetPixel(j, i, Color.FromArgb(red, red, red));
                }
            }
        }

        private int method_0(Color color_0)
        {
            return ((((color_0.R * 0x4c8b) + (color_0.G * 0x9645)) + (color_0.B * 0x1d30)) >> 0x10);
        }
    }
}

