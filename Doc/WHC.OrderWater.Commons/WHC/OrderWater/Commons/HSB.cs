namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct HSB
    {
        private double double_0;
        private double double_1;
        private double double_2;
        public double Hue
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = Class30.smethod_0(value);
            }
        }
        public double Hue360
        {
            get
            {
                return (this.double_0 * 360.0);
            }
            set
            {
                this.double_0 = Class30.smethod_0(value / 360.0);
            }
        }
        public double Saturation
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = Class30.smethod_0(value);
            }
        }
        public double Saturation100
        {
            get
            {
                return (this.double_1 * 100.0);
            }
            set
            {
                this.double_1 = Class30.smethod_0(value / 100.0);
            }
        }
        public double Brightness
        {
            get
            {
                return this.double_2;
            }
            set
            {
                this.double_2 = Class30.smethod_0(value);
            }
        }
        public double Brightness100
        {
            get
            {
                return (this.double_2 * 100.0);
            }
            set
            {
                this.double_2 = Class30.smethod_0(value / 100.0);
            }
        }
        public HSB(double hue, double saturation, double brightness)
        {
            this = new HSB();
            this.Hue = hue;
            this.Saturation = saturation;
            this.Brightness = brightness;
        }

        public HSB(int hue, int saturation, int brightness)
        {
            this = new HSB();
            this.Hue360 = hue;
            this.Saturation100 = saturation;
            this.Brightness100 = brightness;
        }

        public HSB(Color color)
        {
            this = RGB.ToHSB(color);
        }

        public static implicit operator HSB(Color color)
        {
            return RGB.ToHSB(color);
        }

        public static implicit operator Color(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator RGB(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator CMYK(HSB color)
        {
            return color.ToColor();
        }

        public static bool operator ==(HSB left, HSB right)
        {
            return (((left.Hue == right.Hue) && (left.Saturation == right.Saturation)) && (left.Brightness == right.Brightness));
        }

        public static bool operator !=(HSB left, HSB right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("Hue: {0}, Saturation: {1}, Brightness: {2}", Class30.smethod_4(this.Hue360), Class30.smethod_4(this.Saturation100), Class30.smethod_4(this.Brightness100));
        }

        public static Color ToColor(HSB hsb)
        {
            int num4;
            int red = Class30.smethod_4(hsb.Brightness * 255.0);
            int blue = Class30.smethod_4(((1.0 - hsb.Saturation) * (hsb.Brightness / 1.0)) * 255.0);
            double num3 = ((double) (red - blue)) / 255.0;
            if ((hsb.Hue >= 0.0) && (hsb.Hue <= 0.16666666666666666))
            {
                num4 = Class30.smethod_4((((hsb.Hue - 0.0) * num3) * 1530.0) + blue);
                return Color.FromArgb(red, num4, blue);
            }
            if (hsb.Hue <= 0.33333333333333331)
            {
                return Color.FromArgb(Class30.smethod_4((-((hsb.Hue - 0.16666666666666666) * num3) * 1530.0) + red), red, blue);
            }
            if (hsb.Hue <= 0.5)
            {
                num4 = Class30.smethod_4((((hsb.Hue - 0.33333333333333331) * num3) * 1530.0) + blue);
                return Color.FromArgb(blue, red, num4);
            }
            if (hsb.Hue <= 0.66666666666666663)
            {
                num4 = Class30.smethod_4((-((hsb.Hue - 0.5) * num3) * 1530.0) + red);
                return Color.FromArgb(blue, num4, red);
            }
            if (hsb.Hue <= 0.83333333333333337)
            {
                return Color.FromArgb(Class30.smethod_4((((hsb.Hue - 0.66666666666666663) * num3) * 1530.0) + blue), blue, red);
            }
            if (hsb.Hue <= 1.0)
            {
                num4 = Class30.smethod_4((-((hsb.Hue - 0.83333333333333337) * num3) * 1530.0) + red);
                return Color.FromArgb(red, blue, num4);
            }
            return Color.FromArgb(0, 0, 0);
        }

        public static Color ToColor(double hue, double saturation, double brightness)
        {
            return ToColor(new HSB(hue, saturation, brightness));
        }

        public Color ToColor()
        {
            return ToColor(this);
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

