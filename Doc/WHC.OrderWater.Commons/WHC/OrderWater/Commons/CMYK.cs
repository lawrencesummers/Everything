namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CMYK
    {
        private double double_0;
        private double double_1;
        private double double_2;
        private double double_3;
        public double Cyan
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
        public double Cyan100
        {
            get
            {
                return (this.double_0 * 100.0);
            }
            set
            {
                this.double_0 = Class30.smethod_0(value / 100.0);
            }
        }
        public double Magenta
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
        public double Double_0
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
        public double Yellow
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
        public double Double_1
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
        public double Key
        {
            get
            {
                return this.double_3;
            }
            set
            {
                this.double_3 = Class30.smethod_0(value);
            }
        }
        public double Key100
        {
            get
            {
                return (this.double_3 * 100.0);
            }
            set
            {
                this.double_3 = Class30.smethod_0(value / 100.0);
            }
        }
        public CMYK(double cyan, double magenta, double yellow, double key)
        {
            this = new CMYK();
            this.Cyan = cyan;
            this.Magenta = magenta;
            this.Yellow = yellow;
            this.Key = key;
        }

        public CMYK(int cyan, int magenta, int yellow, int key)
        {
            this = new CMYK();
            this.Cyan100 = cyan;
            this.Double_0 = magenta;
            this.Double_1 = yellow;
            this.Key100 = key;
        }

        public CMYK(Color color)
        {
            this = RGB.ToCMYK(color);
        }

        public static implicit operator CMYK(Color color)
        {
            return RGB.ToCMYK(color);
        }

        public static implicit operator Color(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator RGB(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator HSB(CMYK color)
        {
            return color.ToColor();
        }

        public static bool operator ==(CMYK left, CMYK right)
        {
            return ((((left.Cyan == right.Cyan) && (left.Magenta == right.Magenta)) && (left.Yellow == right.Yellow)) && (left.Key == right.Key));
        }

        public static bool operator !=(CMYK left, CMYK right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("Cyan: {0}, Magenta: {1}, Yellow: {2}, Key: {3}", new object[] { Class30.smethod_4(this.Cyan100), Class30.smethod_4(this.Double_0), Class30.smethod_4(this.Double_1), Class30.smethod_4(this.Key100) });
        }

        public static Color ToColor(CMYK cmyk)
        {
            int red = Class30.smethod_4(255.0 - (255.0 * cmyk.Cyan));
            int green = Class30.smethod_4(255.0 - (255.0 * cmyk.Magenta));
            int blue = Class30.smethod_4(255.0 - (255.0 * cmyk.Yellow));
            return Color.FromArgb(red, green, blue);
        }

        public static Color ToColor(double cyan, double magenta, double yellow, double key)
        {
            return ToColor(new CMYK(cyan, magenta, yellow, key));
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

