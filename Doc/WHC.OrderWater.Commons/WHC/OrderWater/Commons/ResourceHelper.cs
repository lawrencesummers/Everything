namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class ResourceHelper
    {
        public static Bitmap LoadBitmap(Type assemblyType, string imageName)
        {
            return LoadBitmap(assemblyType, imageName, false, new Point(0, 0));
        }

        public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
        {
            return LoadBitmap(assemblyType, imageName, true, transparentPixel);
        }

        protected static Bitmap LoadBitmap(Type assemblyType, string imageName, bool makeTransparent, Point transparentPixel)
        {
            Bitmap bitmap = new Bitmap(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(imageName));
            if (makeTransparent)
            {
                Color pixel = bitmap.GetPixel(transparentPixel.X, transparentPixel.Y);
                bitmap.MakeTransparent(pixel);
            }
            return bitmap;
        }

        public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
        {
            return LoadBitmapStrip(assemblyType, imageName, imageSize, false, new Point(0, 0));
        }

        public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize, Point transparentPixel)
        {
            return LoadBitmapStrip(assemblyType, imageName, imageSize, true, transparentPixel);
        }

        protected static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize, bool makeTransparent, Point transparentPixel)
        {
            ImageList list = new ImageList {
                ImageSize = imageSize
            };
            Bitmap bitmap = new Bitmap(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(imageName));
            if (makeTransparent)
            {
                Color pixel = bitmap.GetPixel(transparentPixel.X, transparentPixel.Y);
                bitmap.MakeTransparent(pixel);
            }
            list.Images.AddStrip(bitmap);
            return list;
        }

        public static Cursor LoadCursor(Type assemblyType, string cursorName)
        {
            return new Cursor(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(cursorName));
        }

        public static Icon LoadIcon(Type assemblyType, string iconName)
        {
            return new Icon(Assembly.GetAssembly(assemblyType).GetManifestResourceStream(iconName));
        }

        public static Icon LoadIcon(Type assemblyType, string iconName, Size iconSize)
        {
            return new Icon(LoadIcon(assemblyType, iconName), iconSize);
        }
    }
}

