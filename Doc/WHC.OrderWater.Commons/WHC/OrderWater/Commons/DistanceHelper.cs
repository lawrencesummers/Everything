namespace WHC.OrderWater.Commons
{
    using System;

    public class DistanceHelper
    {
        private const double double_0 = 6378137.0;

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double d = smethod_0(lat1);
            double num2 = smethod_0(lat2);
            double num3 = d - num2;
            double num4 = smethod_0(lng1) - smethod_0(lng2);
            double num5 = 2.0 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(num3 / 2.0), 2.0) + ((Math.Cos(d) * Math.Cos(num2)) * Math.Pow(Math.Sin(num4 / 2.0), 2.0))));
            num5 *= 6378137.0;
            return (Math.Round((double) (num5 * 10000.0)) / 10000.0);
        }

        private static double smethod_0(double double_1)
        {
            return ((double_1 * 3.1415926535897931) / 180.0);
        }
    }
}

