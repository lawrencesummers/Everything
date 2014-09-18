namespace RDIFramework.Utilities
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Web;

    public class ImageHelper
    {
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            if ((byteArrayIn == null) || (byteArrayIn.Length == 0))
            {
                return null;
            }
            MemoryStream stream = new MemoryStream(byteArrayIn);
            return Image.FromStream(stream);
        }

        public static void CutForCustom(HttpPostedFile postedFile, string fileSaveUrl, int maxWidth, int maxHeight, int quality)
        {
            Image image = Image.FromStream(postedFile.InputStream, true);
            if ((image.Width <= maxWidth) && (image.Height <= maxHeight))
            {
                image.Save(fileSaveUrl, ImageFormat.Jpeg);
            }
            else
            {
                Image image2;
                Graphics graphics;
                double num = ((double) maxWidth) / ((double) maxHeight);
                double num2 = ((double) image.Width) / ((double) image.Height);
                if (num == num2)
                {
                    image2 = new Bitmap(maxWidth, maxHeight);
                    graphics = Graphics.FromImage(image2);
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(image, new Rectangle(0, 0, maxWidth, maxHeight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                    image2.Save(fileSaveUrl, ImageFormat.Jpeg);
                }
                else
                {
                    Image image3 = null;
                    Graphics graphics2 = null;
                    Rectangle srcRect = new Rectangle(0, 0, 0, 0);
                    Rectangle destRect = new Rectangle(0, 0, 0, 0);
                    if (num > num2)
                    {
                        image3 = new Bitmap(image.Width, (int) Math.Floor((double) (((double) image.Width) / num)));
                        graphics2 = Graphics.FromImage(image3);
                        srcRect.X = 0;
                        srcRect.Y = (int) Math.Floor((double) ((image.Height - (((double) image.Width) / num)) / 2.0));
                        srcRect.Width = image.Width;
                        srcRect.Height = (int) Math.Floor((double) (((double) image.Width) / num));
                        destRect.X = 0;
                        destRect.Y = 0;
                        destRect.Width = image.Width;
                        destRect.Height = (int) Math.Floor((double) (((double) image.Width) / num));
                    }
                    else
                    {
                        image3 = new Bitmap((int) Math.Floor((double) (image.Height * num)), image.Height);
                        graphics2 = Graphics.FromImage(image3);
                        srcRect.X = (int) Math.Floor((double) ((image.Width - (image.Height * num)) / 2.0));
                        srcRect.Y = 0;
                        srcRect.Width = (int) Math.Floor((double) (image.Height * num));
                        srcRect.Height = image.Height;
                        destRect.X = 0;
                        destRect.Y = 0;
                        destRect.Width = (int) Math.Floor((double) (image.Height * num));
                        destRect.Height = image.Height;
                    }
                    graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics2.SmoothingMode = SmoothingMode.HighQuality;
                    graphics2.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
                    image2 = new Bitmap(maxWidth, maxHeight);
                    graphics = Graphics.FromImage(image2);
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(image3, new Rectangle(0, 0, maxWidth, maxHeight), new Rectangle(0, 0, image3.Width, image3.Height), GraphicsUnit.Pixel);
                    ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo encoder = null;
                    foreach (ImageCodecInfo info2 in imageEncoders)
                    {
                        if ((((info2.MimeType == "image/jpeg") || (info2.MimeType == "image/bmp")) || (info2.MimeType == "image/png")) || (info2.MimeType == "image/gif"))
                        {
                            encoder = info2;
                        }
                    }
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long) quality);
                    image2.Save(fileSaveUrl, encoder, encoderParams);
                    graphics.Dispose();
                    image2.Dispose();
                    graphics2.Dispose();
                    image3.Dispose();
                }
            }
            image.Dispose();
        }

        public static void CutForSquare(Stream fromFile, string fileSaveUrl, int side, int quality)
        {
            string directoryName = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            Image image = Image.FromStream(fromFile, true);
            if ((image.Width <= side) && (image.Height <= side))
            {
                image.Save(fileSaveUrl, ImageFormat.Jpeg);
            }
            else
            {
                int width = image.Width;
                int height = image.Height;
                if (width != height)
                {
                    Rectangle rectangle;
                    Rectangle rectangle2;
                    Image image2 = null;
                    Graphics graphics = null;
                    if (width > height)
                    {
                        image2 = new Bitmap(height, height);
                        graphics = Graphics.FromImage(image2);
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        rectangle = new Rectangle((width - height) / 2, 0, height, height);
                        rectangle2 = new Rectangle(0, 0, height, height);
                        graphics.DrawImage(image, rectangle2, rectangle, GraphicsUnit.Pixel);
                        width = height;
                    }
                    else
                    {
                        image2 = new Bitmap(width, width);
                        graphics = Graphics.FromImage(image2);
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        rectangle = new Rectangle(0, (height - width) / 2, width, width);
                        rectangle2 = new Rectangle(0, 0, width, width);
                        graphics.DrawImage(image, rectangle2, rectangle, GraphicsUnit.Pixel);
                        height = width;
                    }
                    image = (Image) image2.Clone();
                    graphics.Dispose();
                    image2.Dispose();
                }
                Image image3 = new Bitmap(side, side);
                Graphics graphics2 = Graphics.FromImage(image3);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.Clear(Color.White);
                graphics2.DrawImage(image, new Rectangle(0, 0, side, side), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo encoder = null;
                foreach (ImageCodecInfo info2 in imageEncoders)
                {
                    if ((((info2.MimeType == "image/jpeg") || (info2.MimeType == "image/bmp")) || (info2.MimeType == "image/png")) || (info2.MimeType == "image/gif"))
                    {
                        encoder = info2;
                    }
                }
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long) quality);
                image3.Save(fileSaveUrl, encoder, encoderParams);
                encoderParams.Dispose();
                graphics2.Dispose();
                image3.Dispose();
                image.Dispose();
            }
        }

        public static void CutForSquare(HttpPostedFile postedFile, string fileSaveUrl, int side, int quality)
        {
            string directoryName = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            Image image = Image.FromStream(postedFile.InputStream, true);
            if ((image.Width <= side) && (image.Height <= side))
            {
                image.Save(fileSaveUrl, ImageFormat.Jpeg);
            }
            else
            {
                int width = image.Width;
                int height = image.Height;
                if (width != height)
                {
                    Rectangle rectangle;
                    Rectangle rectangle2;
                    Image image2 = null;
                    Graphics graphics = null;
                    if (width > height)
                    {
                        image2 = new Bitmap(height, height);
                        graphics = Graphics.FromImage(image2);
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        rectangle = new Rectangle((width - height) / 2, 0, height, height);
                        rectangle2 = new Rectangle(0, 0, height, height);
                        graphics.DrawImage(image, rectangle2, rectangle, GraphicsUnit.Pixel);
                        width = height;
                    }
                    else
                    {
                        image2 = new Bitmap(width, width);
                        graphics = Graphics.FromImage(image2);
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        rectangle = new Rectangle(0, (height - width) / 2, width, width);
                        rectangle2 = new Rectangle(0, 0, width, width);
                        graphics.DrawImage(image, rectangle2, rectangle, GraphicsUnit.Pixel);
                        height = width;
                    }
                    image = (Image) image2.Clone();
                    graphics.Dispose();
                    image2.Dispose();
                }
                Image image3 = new Bitmap(side, side);
                Graphics graphics2 = Graphics.FromImage(image3);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.Clear(Color.White);
                graphics2.DrawImage(image, new Rectangle(0, 0, side, side), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo encoder = null;
                foreach (ImageCodecInfo info2 in imageEncoders)
                {
                    if ((((info2.MimeType == "image/jpeg") || (info2.MimeType == "image/bmp")) || (info2.MimeType == "image/png")) || (info2.MimeType == "image/gif"))
                    {
                        encoder = info2;
                    }
                }
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long) quality);
                image3.Save(fileSaveUrl, encoder, encoderParams);
                encoderParams.Dispose();
                graphics2.Dispose();
                image3.Dispose();
                image.Dispose();
            }
        }

        public static byte[] imageToByteArray(Image imageIn)
        {
            if (imageIn == null)
            {
                return null;
            }
            MemoryStream stream = new MemoryStream();
            imageIn.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }

        public static bool IsWebImage(string contentType)
        {
            return ((((contentType == "image/pjpeg") || (contentType == "image/jpeg")) || ((contentType == "image/gif") || (contentType == "image/bmp"))) || (contentType == "image/png"));
        }

        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ZoomType zoomtype)
        {
            Image image = Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            switch (zoomtype)
            {
                case ZoomType.H:
                    num = (image.Width * height) / image.Height;
                    break;

                case ZoomType.W:
                    num2 = (image.Height * width) / image.Width;
                    break;

                case ZoomType.Cut:
                    if ((((double) image.Width) / ((double) image.Height)) <= (((double) num) / ((double) num2)))
                    {
                        num5 = image.Width;
                        num6 = (image.Width * height) / num;
                        x = 0;
                        y = (image.Height - num6) / 2;
                        break;
                    }
                    num6 = image.Height;
                    num5 = (image.Height * num) / num2;
                    y = 0;
                    x = (image.Width - num5) / 2;
                    break;
            }
            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                image2.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }

        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image image = Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            switch (mode)
            {
                case "W":
                    num2 = (image.Height * width) / image.Width;
                    break;

                case "H":
                    num = (image.Width * height) / image.Height;
                    break;

                case "Cut":
                    if ((((double) image.Width) / ((double) image.Height)) > (((double) num) / ((double) num2)))
                    {
                        num6 = image.Height;
                        num5 = (image.Height * num) / num2;
                        y = 0;
                        x = (image.Width - num5) / 2;
                    }
                    else
                    {
                        num5 = image.Width;
                        num6 = (image.Width * height) / num;
                        x = 0;
                        y = (image.Height - num6) / 2;
                    }
                    break;
            }
            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                image2.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }

        public static void Makewater(Image image, string waterImagePath, Point p)
        {
            Makewater(image, waterImagePath, p, ImagePosition.TopLeft);
        }

        public static void Makewater(Image image, string waterImagePath, Point p, ImagePosition imagePosition)
        {
            using (Image image2 = Image.FromFile(waterImagePath))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    switch (imagePosition)
                    {
                        case ImagePosition.BottomLeft:
                            p.Y = (image.Height - image2.Height) - p.Y;
                            break;

                        case ImagePosition.BottomRight:
                            p.Y = (image.Height - image2.Height) - p.Y;
                            p.X = (image.Width - image2.Width) - p.X;
                            break;

                        case ImagePosition.TopRigth:
                            p.X = (image.Width - image2.Width) - p.X;
                            break;
                    }
                    graphics.DrawImage(image2, new Rectangle(p, new Size(image2.Width, image2.Height)));
                }
            }
        }

        public static void Makewater(Image image, string waterStr, Font font, Brush brush, Point p)
        {
            Makewater(image, waterStr, font, brush, p, ImagePosition.TopLeft);
        }

        public static void Makewater(Image image, string waterStr, Font font, Brush brush, Point p, ImagePosition imagePosition)
        {
            using (Graphics graphics = Graphics.FromImage(image))
            {
                int size = (int) font.Size;
                int num2 = (int) (font.Size + 1f);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                switch (imagePosition)
                {
                    case ImagePosition.BottomLeft:
                        p.Y = (image.Height - size) - p.Y;
                        break;

                    case ImagePosition.BottomRight:
                        p.Y = (image.Height - size) - p.Y;
                        p.X = (image.Width - num2) - p.X;
                        break;

                    case ImagePosition.TopRigth:
                        p.X = (image.Width - num2) - p.X;
                        break;
                }
                graphics.DrawString(waterStr, font, brush, (PointF) p);
            }
        }

        public static Image RetrunImage(string originalImagePath, int width, int height, ZoomType zoomtype)
        {
            Image image3;
            Image image = Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            switch (zoomtype)
            {
                case ZoomType.H:
                    num = (image.Width * height) / image.Height;
                    break;

                case ZoomType.W:
                    num2 = (image.Height * width) / image.Width;
                    break;

                case ZoomType.Cut:
                    if ((((double) image.Width) / ((double) image.Height)) <= (((double) num) / ((double) num2)))
                    {
                        num5 = image.Width;
                        num6 = (image.Width * height) / num;
                        x = 0;
                        y = (image.Height - num6) / 2;
                        break;
                    }
                    num6 = image.Height;
                    num5 = (image.Height * num) / num2;
                    y = 0;
                    x = (image.Width - num5) / 2;
                    break;
            }
            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                image3 = image2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                image.Dispose();
                graphics.Dispose();
            }
            return image3;
        }

        public static Bitmap Rotate(Bitmap b, int angle)
        {
            angle = angle % 360;
            double d = (angle * 3.1415926535897931) / 180.0;
            double num2 = Math.Cos(d);
            double num3 = Math.Sin(d);
            int width = b.Width;
            int height = b.Height;
            int num6 = (int) Math.Max(Math.Abs((double) ((width * num2) - (height * num3))), Math.Abs((double) ((width * num2) + (height * num3))));
            int num7 = (int) Math.Max(Math.Abs((double) ((width * num3) - (height * num2))), Math.Abs((double) ((width * num3) + (height * num2))));
            Bitmap image = new Bitmap(num6, num7);
            Graphics graphics = Graphics.FromImage(image);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            Point point = new Point((num6 - width) / 2, (num7 - height) / 2);
            Rectangle rect = new Rectangle(point.X, point.Y, width, height);
            Point point2 = new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2));
            graphics.TranslateTransform((float) point2.X, (float) point2.Y);
            graphics.RotateTransform((float) (360 - angle));
            graphics.TranslateTransform((float) -point2.X, (float) -point2.Y);
            graphics.DrawImage(b, rect);
            graphics.ResetTransform();
            graphics.Save();
            graphics.Dispose();
            return image;
        }

        public static void SaveQuality(Image image, string path)
        {
            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders()[0];
            Encoder quality = Encoder.Quality;
            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter parameter = new EncoderParameter(quality, 100L);
            encoderParams.Param[0] = parameter;
            try
            {
                image.Save(path, info, encoderParams);
            }
            finally
            {
                parameter.Dispose();
                encoderParams.Dispose();
            }
        }

        public static Image Scale(Image image, Size size)
        {
            return image.GetThumbnailImage(size.Width, size.Height, null, new IntPtr());
        }

        public static Image Scale(Image image, int multiple)
        {
            int num2;
            int num3;
            int num = Math.Abs(multiple);
            if (multiple == 0)
            {
                return (image.Clone() as Image);
            }
            if (multiple < 0)
            {
                num2 = image.Width / num;
                num3 = image.Height / num;
            }
            else
            {
                num2 = image.Width * num;
                num3 = image.Height * num;
            }
            return image.GetThumbnailImage(num2, num3, null, new IntPtr());
        }

        public static Image ScaleCut(Image image, int width, int height)
        {
            int x = 0;
            int y = 0;
            int num3 = image.Width;
            int num4 = image.Height;
            if ((width >= num3) && (height >= num4))
            {
                return image;
            }
            if (width > num3)
            {
                width = num3;
            }
            if (height > num4)
            {
                height = num4;
            }
            if ((((double) image.Width) / ((double) image.Height)) > (((double) width) / ((double) height)))
            {
                num4 = image.Height;
                num3 = (image.Height * width) / height;
                y = 0;
                x = (image.Width - num3) / 2;
            }
            else
            {
                num3 = image.Width;
                num4 = (image.Width * height) / width;
                x = 0;
                y = (image.Height - num4) / 2;
            }
            Image image3 = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(image3))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(x, y, num3, num4), GraphicsUnit.Pixel);
            }
            return image3;
        }

        public static Image ScaleFixHeight(Image image, int height)
        {
            int num = height;
            double num2 = ((double) num) / ((double) image.Height);
            int width = (int) (image.Width * num2);
            Image image2 = new Bitmap(width, num);
            using (Graphics graphics = Graphics.FromImage(image2))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, width, num));
            }
            return image2;
        }

        public static Image ScaleFixWidth(Image image, int width)
        {
            int num = width;
            double num2 = ((double) num) / ((double) image.Width);
            int height = (int) (image.Height * num2);
            Image image2 = new Bitmap(num, height);
            using (Graphics graphics = Graphics.FromImage(image2))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, num, height));
            }
            return image2;
        }

        public static void ZoomAuto(HttpPostedFile postedFile, string savePath, double targetWidth, double targetHeight, string watermarkText, string watermarkImage)
        {
            Graphics graphics;
            Font font;
            Brush brush;
            Image image2;
            ImageAttributes attributes;
            ColorMap map;
            ColorMap[] mapArray2;
            float[][] numArray;
            float[] numArray2;
            float[][] numArray3;
            ColorMatrix matrix;
            string directoryName = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            Image image = Image.FromStream(postedFile.InputStream, true);
            if ((image.Width <= targetWidth) && (image.Height <= targetHeight))
            {
                if (watermarkText != "")
                {
                    using (graphics = Graphics.FromImage(image))
                    {
                        font = new Font("黑体", 10f);
                        brush = new SolidBrush(Color.White);
                        graphics.DrawString(watermarkText, font, brush, (float) 10f, (float) 10f);
                        graphics.Dispose();
                    }
                }
                if ((watermarkImage != "") && File.Exists(watermarkImage))
                {
                    using (image2 = Image.FromFile(watermarkImage))
                    {
                        if ((image.Width >= image2.Width) && (image.Height >= image2.Height))
                        {
                            graphics = Graphics.FromImage(image);
                            attributes = new ImageAttributes();
                            map = new ColorMap {
                                OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                                NewColor = Color.FromArgb(0, 0, 0, 0)
                            };
                            mapArray2 = new ColorMap[] { map };
                            attributes.SetRemapTable(mapArray2, ColorAdjustType.Bitmap);
                            numArray = new float[5][];
                            numArray2 = new float[5];
                            numArray2[0] = 1f;
                            numArray[0] = numArray2;
                            numArray2 = new float[5];
                            numArray2[1] = 1f;
                            numArray[1] = numArray2;
                            numArray2 = new float[5];
                            numArray2[2] = 1f;
                            numArray[2] = numArray2;
                            numArray2 = new float[5];
                            numArray2[3] = 0.5f;
                            numArray[3] = numArray2;
                            numArray2 = new float[5];
                            numArray2[4] = 1f;
                            numArray[4] = numArray2;
                            numArray3 = numArray;
                            matrix = new ColorMatrix(numArray3);
                            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width, image.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, attributes);
                            graphics.Dispose();
                        }
                        image2.Dispose();
                    }
                }
                image.Save(savePath, ImageFormat.Jpeg);
            }
            else
            {
                double width = image.Width;
                double height = image.Height;
                if ((image.Width > image.Height) || (image.Width == image.Height))
                {
                    if (image.Width > targetWidth)
                    {
                        width = targetWidth;
                        height = image.Height * (targetWidth / ((double) image.Width));
                    }
                }
                else if (image.Height > targetHeight)
                {
                    height = targetHeight;
                    width = image.Width * (targetHeight / ((double) image.Height));
                }
                Image image3 = new Bitmap((int) width, (int) height);
                Graphics graphics2 = Graphics.FromImage(image3);
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.Clear(Color.White);
                graphics2.DrawImage(image, new Rectangle(0, 0, image3.Width, image3.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                if (watermarkText != "")
                {
                    using (graphics = Graphics.FromImage(image3))
                    {
                        font = new Font("宋体", 10f);
                        brush = new SolidBrush(Color.White);
                        graphics.DrawString(watermarkText, font, brush, (float) 10f, (float) 10f);
                        graphics.Dispose();
                    }
                }
                if ((watermarkImage != "") && File.Exists(watermarkImage))
                {
                    using (image2 = Image.FromFile(watermarkImage))
                    {
                        if ((image3.Width >= image2.Width) && (image3.Height >= image2.Height))
                        {
                            graphics = Graphics.FromImage(image3);
                            attributes = new ImageAttributes();
                            map = new ColorMap {
                                OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                                NewColor = Color.FromArgb(0, 0, 0, 0)
                            };
                            mapArray2 = new ColorMap[] { map };
                            attributes.SetRemapTable(mapArray2, ColorAdjustType.Bitmap);
                            numArray = new float[5][];
                            numArray2 = new float[5];
                            numArray2[0] = 1f;
                            numArray[0] = numArray2;
                            numArray2 = new float[5];
                            numArray2[1] = 1f;
                            numArray[1] = numArray2;
                            numArray2 = new float[5];
                            numArray2[2] = 1f;
                            numArray[2] = numArray2;
                            numArray2 = new float[5];
                            numArray2[3] = 0.5f;
                            numArray[3] = numArray2;
                            numArray2 = new float[5];
                            numArray2[4] = 1f;
                            numArray[4] = numArray2;
                            numArray3 = numArray;
                            matrix = new ColorMatrix(numArray3);
                            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            graphics.DrawImage(image2, new Rectangle(image3.Width - image2.Width, image3.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, attributes);
                            graphics.Dispose();
                        }
                        image2.Dispose();
                    }
                }
                image3.Save(savePath, ImageFormat.Jpeg);
                graphics2.Dispose();
                image3.Dispose();
                image.Dispose();
            }
        }
    }
}

