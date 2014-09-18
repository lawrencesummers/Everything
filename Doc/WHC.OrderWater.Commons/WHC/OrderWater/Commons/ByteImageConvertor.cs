namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;

    public sealed class ByteImageConvertor
    {
        private ByteImageConvertor()
        {
        }

        public static Bitmap BitmapFromBytes(byte[] bytes)
        {
            Bitmap bitmap = null;
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    bitmap = new Bitmap(new Bitmap(stream));
                }
            }
            catch
            {
            }
            return bitmap;
        }

        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            byte[] buffer = null;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, bitmap.RawFormat);
                    buffer = new byte[stream.Length];
                    buffer = stream.ToArray();
                }
            }
            catch
            {
            }
            return buffer;
        }

        public static Image ImageFromBytes(byte[] bytes)
        {
            Image image = null;
            try
            {
                if (bytes == null)
                {
                    return image;
                }
                MemoryStream stream = new MemoryStream(bytes, false);
                using (stream)
                {
                    image = smethod_0(stream);
                }
            }
            catch
            {
            }
            return image;
        }

        public static Image ImageFromUrl(string url)
        {
            Image image = null;
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return image;
                }
                Uri address = new Uri(url);
                if (address.IsFile)
                {
                    FileStream stream = new FileStream(address.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (stream)
                    {
                        return smethod_0(stream);
                    }
                }
                WebClient client = new WebClient();
                using (client)
                {
                    MemoryStream stream3 = new MemoryStream(client.DownloadData(address), false);
                    using (stream3)
                    {
                        return smethod_0(stream3);
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        public static byte[] ImageToBytes(Image image)
        {
            byte[] buffer = null;
            if (image != null)
            {
                lock (image)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        image.Save(stream, ImageFormat.Png);
                        buffer = stream.GetBuffer();
                    }
                }
            }
            return buffer;
        }

        public static byte[] ImageToBytes(Image image, ImageFormat imageFormat)
        {
            if (image == null)
            {
                return null;
            }
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(image))
                {
                    bitmap.Save(stream, imageFormat);
                    stream.Position = 0;
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    stream.Flush();
                }
            }
            return buffer;
        }

        private static Image smethod_0(Stream stream_0)
        {
            Image image = null;
            try
            {
                stream_0.Position = 0;
                Image original = Image.FromStream(stream_0);
                using (original)
                {
                    image = new Bitmap(original);
                }
            }
            catch
            {
                try
                {
                    stream_0.Position = 0;
                    Icon icon = new Icon(stream_0);
                    if (icon != null)
                    {
                        image = icon.ToBitmap();
                    }
                }
                catch
                {
                }
            }
            return image;
        }
    }
}

