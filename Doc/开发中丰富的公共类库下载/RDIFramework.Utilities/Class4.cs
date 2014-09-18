using System;
using System.Drawing;

internal class Class4
{
    public static string smethod_0(Color color_0)
    {
        if (color_0.IsNamedColor)
        {
            return color_0.Name;
        }
        return ("#" + color_0.R.ToString("X2") + color_0.G.ToString("X2") + color_0.B.ToString("X2"));
    }
}

