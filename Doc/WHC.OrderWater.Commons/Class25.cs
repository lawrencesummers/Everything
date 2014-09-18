using System;
using System.Drawing;

internal static class Class25
{
    public static SizeF smethod_0(SizeF sizeF_0, SizeF sizeF_1, bool bool_0)
    {
        float num = sizeF_1.Width / sizeF_0.Width;
        float num2 = sizeF_1.Height / sizeF_0.Height;
        float num3 = bool_0 ? Math.Max(num, num2) : Math.Min(num, num2);
        return new SizeF(sizeF_0.Width * num3, sizeF_0.Height * num3);
    }

    public static RectangleF smethod_1(RectangleF rectangleF_0, RectangleF rectangleF_1, ContentAlignment contentAlignment_0)
    {
        RectangleF ef = new RectangleF(rectangleF_1.Location, rectangleF_0.Size);
        ContentAlignment alignment = contentAlignment_0;
        if (alignment <= ContentAlignment.MiddleCenter)
        {
            switch (alignment)
            {
                case ContentAlignment.TopCenter:
                    ef.X = (rectangleF_1.Width - rectangleF_0.Width) / 2f;
                    return ef;

                case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                    return ef;

                case ContentAlignment.TopRight:
                    ef.X = rectangleF_1.Width - rectangleF_0.Width;
                    return ef;

                case ContentAlignment.MiddleLeft:
                    ef.Y = (rectangleF_1.Height - rectangleF_0.Height) / 2f;
                    return ef;

                case ContentAlignment.MiddleCenter:
                    ef.Y = (rectangleF_1.Height - rectangleF_0.Height) / 2f;
                    ef.X = (rectangleF_1.Width - rectangleF_0.Width) / 2f;
                    return ef;
            }
            return ef;
        }
        if (alignment <= ContentAlignment.BottomLeft)
        {
            if (alignment != ContentAlignment.MiddleRight)
            {
                if (alignment == ContentAlignment.BottomLeft)
                {
                    ef.Y = rectangleF_1.Height - rectangleF_0.Height;
                }
                return ef;
            }
            ef.Y = (rectangleF_1.Height - rectangleF_0.Height) / 2f;
            ef.X = rectangleF_1.Width - rectangleF_0.Width;
            return ef;
        }
        if (alignment != ContentAlignment.BottomCenter)
        {
            if (alignment == ContentAlignment.BottomRight)
            {
                ef.Y = rectangleF_1.Height - rectangleF_0.Height;
                ef.X = rectangleF_1.Width - rectangleF_0.Width;
            }
            return ef;
        }
        ef.Y = rectangleF_1.Height - rectangleF_0.Height;
        ef.X = (rectangleF_1.Width - rectangleF_0.Width) / 2f;
        return ef;
    }

    public static RectangleF smethod_2(RectangleF rectangleF_0, RectangleF rectangleF_1, bool bool_0, ContentAlignment contentAlignment_0)
    {
        SizeF size = smethod_0(rectangleF_0.Size, rectangleF_1.Size, bool_0);
        RectangleF ef2 = new RectangleF((PointF) new Point(0, 0), size);
        return smethod_1(ef2, rectangleF_1, contentAlignment_0);
    }
}

