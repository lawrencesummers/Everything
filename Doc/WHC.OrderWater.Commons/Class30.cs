using System;

internal class Class30
{
    public static double smethod_0(double double_0)
    {
        return smethod_3(double_0, 0.0, 1.0);
    }

    public static int smethod_1(int int_0)
    {
        return smethod_2(int_0, 0, 0xff);
    }

    public static int smethod_2(int int_0, int int_1, int int_2)
    {
        return Math.Max(Math.Min(int_0, int_2), int_1);
    }

    public static double smethod_3(double double_0, double double_1, double double_2)
    {
        return Math.Max(Math.Min(double_0, double_2), double_1);
    }

    public static int smethod_4(double double_0)
    {
        int num = (int) double_0;
        int num2 = (int) (double_0 * 100.0);
        if ((num2 % 100) >= 50)
        {
            num++;
        }
        return num;
    }
}

