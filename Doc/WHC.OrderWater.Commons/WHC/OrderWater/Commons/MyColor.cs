namespace WHC.OrderWater.Commons
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MyColor
    {
        public WHC.OrderWater.Commons.RGB RGB;
        public WHC.OrderWater.Commons.HSB HSB;
        public WHC.OrderWater.Commons.CMYK CMYK;
        public MyColor(Color color)
        {
            this.RGB = color;
            this.HSB = color;
            this.CMYK = color;
        }

        public static implicit operator MyColor(Color color)
        {
            return new MyColor(color);
        }

        public static implicit operator Color(MyColor color)
        {
            return (Color) color.RGB;
        }

        public static bool operator ==(MyColor left, MyColor right)
        {
            return (((left.RGB == right.RGB) && (left.HSB == right.HSB)) && (left.CMYK == right.CMYK));
        }

        public static bool operator !=(MyColor left, MyColor right)
        {
            return !(left == right);
        }

        public void method_0()
        {
            this.HSB = (WHC.OrderWater.Commons.HSB) this.RGB;
            this.CMYK = (WHC.OrderWater.Commons.CMYK) this.RGB;
        }

        public void method_1()
        {
            this.RGB = (WHC.OrderWater.Commons.RGB) this.HSB;
            this.CMYK = (WHC.OrderWater.Commons.CMYK) this.HSB;
        }

        public void method_2()
        {
            this.RGB = (WHC.OrderWater.Commons.RGB) this.CMYK;
            this.HSB = (WHC.OrderWater.Commons.HSB) this.CMYK;
        }

        public override string ToString()
        {
            return string.Format("{0}\r\n{1}\r\n{2}", this.RGB, this.HSB, this.CMYK);
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

