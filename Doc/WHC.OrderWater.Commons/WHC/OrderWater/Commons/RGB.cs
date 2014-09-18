namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RGB
    {
        private int int_0;
        private int int_1;
        private int int_2;
        public int Red
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = Class30.smethod_1(value);
            }
        }
        public int Green
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = Class30.smethod_1(value);
            }
        }
        public int Blue
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = Class30.smethod_1(value);
            }
        }
        public RGB(int red, int green, int blue)
        {
            this = new RGB();
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public RGB(Color color)
        {
            this = new RGB(color.R, color.G, color.B);
        }

        public static implicit operator RGB(Color color)
        {
            return new RGB(color.R, color.G, color.B);
        }

        public static implicit operator Color(RGB color)
        {
            return color.ToColor();
        }

        public static implicit operator HSB(RGB color)
        {
            return color.ToHSB();
        }

        public static implicit operator CMYK(RGB color)
        {
            return color.ToCMYK();
        }

        public static bool operator ==(RGB left, RGB right)
        {
            return (((left.Red == right.Red) && (left.Green == right.Green)) && (left.Blue == right.Blue));
        }

        public static bool operator !=(RGB left, RGB right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("Red: {0}, Green: {1}, Blue: {2}", this.Red, this.Green, this.Blue);
        }

        public static Color ToColor(int red, int green, int blue)
        {
            return Color.FromArgb(red, green, blue);
        }

        public Color ToColor()
        {
            return ToColor(this.Red, this.Green, this.Blue);
        }

        public static HSB ToHSB(Color color)
        {
            double num;
            int r;
            int g;
            HSB hsb = new HSB();
            if (color.R > color.G)
            {
                r = color.R;
                g = color.G;
            }
            else
            {
                r = color.G;
                g = color.R;
            }
            if (color.B > r)
            {
                r = color.B;
            }
            else if (color.B < g)
            {
                g = color.B;
            }
            int num4 = r - g;
            hsb.Brightness = ((double) r) / 255.0;
            if (r == 0)
            {
                hsb.Saturation = 0.0;
            }
            else
            {
                hsb.Saturation = ((double) num4) / ((double) r);
            }
            if (num4 == 0)
            {
                num = 0.0;
            }
            else
            {
                num = 60.0 / ((double) num4);
            }
            if (r == color.R)
            {
                if (color.G < color.B)
                {
                    hsb.Hue = (360.0 + (num * (color.G - color.B))) / 360.0;
                    return hsb;
                }
                hsb.Hue = (num * (color.G - color.B)) / 360.0;
                return hsb;
            }
            if (r == color.G)
            {
                hsb.Hue = (120.0 + (num * (color.B - color.R))) / 360.0;
                return hsb;
            }
            if (r == color.B)
            {
                hsb.Hue = (240.0 + (num * (color.R - color.G))) / 360.0;
                return hsb;
            }
            hsb.Hue = 0.0;
            return hsb;
        }

        public unsafe HSB ToHSB()
        {
            return ToHSB(*((Color*) this));
        }

        public static CMYK ToCMYK(Color color)
        {
            CMYK cmyk = new CMYK();
            double cyan = 1.0;
            cmyk.Cyan = ((double) (0xff - color.R)) / 255.0;
            if (cyan > cmyk.Cyan)
            {
                cyan = cmyk.Cyan;
            }
            cmyk.Magenta = ((double) (0xff - color.G)) / 255.0;
            if (cyan > cmyk.Magenta)
            {
                cyan = cmyk.Magenta;
            }
            cmyk.Yellow = ((double) (0xff - color.B)) / 255.0;
            if (cyan > cmyk.Yellow)
            {
                cyan = cmyk.Yellow;
            }
            if (cyan > 0.0)
            {
                cmyk.Key = cyan;
            }
            return cmyk;
        }

        public unsafe CMYK ToCMYK()
        {
            return ToCMYK(*((Color*) this));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}

