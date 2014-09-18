namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class MyColors
    {
        public static int ColorToDecimal(Color color)
        {
            return HexToDecimal(ColorToHex(color));
        }

        public static string ColorToHex(Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public static Color DecimalToColor(int dec)
        {
            return Color.FromArgb(dec & 0xff, (dec & 0xff00) / 0x100, dec / 0x10000);
        }

        public static string DecimalToHex(int dec)
        {
            return dec.ToString("X6");
        }

        public static Color GetPixelColor(Point point)
        {
            Bitmap image = new Bitmap(1, 1);
            Graphics.FromImage(image).CopyFromScreen(point, new Point(0, 0), new Size(1, 1));
            return image.GetPixel(0, 0);
        }

        public static Color HexToColor(string hex)
        {
            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }
            string str4 = string.Empty;
            if (hex.Length == 8)
            {
                str4 = hex.Substring(0, 2);
                hex = hex.Substring(2);
            }
            string str = hex.Substring(0, 2);
            string str2 = hex.Substring(2, 2);
            string str3 = hex.Substring(4, 2);
            if (string.IsNullOrEmpty(str4))
            {
                return Color.FromArgb(HexToDecimal(str), HexToDecimal(str2), HexToDecimal(str3));
            }
            return Color.FromArgb(HexToDecimal(str4), HexToDecimal(str), HexToDecimal(str2), HexToDecimal(str3));
        }

        public static int HexToDecimal(string hex)
        {
            return Convert.ToInt32(hex, 0x10);
        }

        public static Color ModifyBrightness(Color c, double brightness)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Brightness *= brightness;
            return hsb.ToColor();
        }

        public static Color ModifyHue(Color c, double Hue)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Hue *= Hue;
            return hsb.ToColor();
        }

        public static Color ModifySaturation(Color c, double Saturation)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Saturation *= Saturation;
            return hsb.ToColor();
        }

        public static Color ParseColor(string color)
        {
            if (color.StartsWith("#"))
            {
                return HexToColor(color);
            }
            if (color.Contains(","))
            {
                string[] strArray2 = color.Split(new char[] { ',' });
                List<int> list = new List<int>();
                foreach (string str in strArray2)
                {
                    list.Add(Convert.ToInt32(str));
                }
                int[] numArray = list.ToArray();
                if (numArray.Length == 3)
                {
                    return Color.FromArgb(numArray[0], numArray[1], numArray[2]);
                }
                if (numArray.Length == 4)
                {
                    return Color.FromArgb(numArray[0], numArray[1], numArray[2], numArray[3]);
                }
            }
            return Color.FromName(color);
        }

        public static Color RandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(0x100), random.Next(0x100), random.Next(0x100));
        }

        public static Color SetBrightness(Color c, double brightness)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Brightness = brightness;
            return hsb.ToColor();
        }

        public static Color SetHue(Color c, double Hue)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Hue = Hue;
            return hsb.ToColor();
        }

        public static Color SetSaturation(Color c, double Saturation)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Saturation = Saturation;
            return hsb.ToColor();
        }
    }
}

