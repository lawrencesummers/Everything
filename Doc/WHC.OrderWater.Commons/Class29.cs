using System;
using System.Threading;

internal class Class29
{
    public static string smethod_0(int int_0)
    {
        return smethod_1(int_0, false);
    }

    public static string smethod_1(int int_0, bool bool_0)
    {
        if (bool_0)
        {
            Thread.Sleep(3);
        }
        string str = "";
        Random random = new Random();
        for (int i = 0; i < int_0; i++)
        {
            str = str + random.Next(10).ToString();
        }
        return str;
    }

    public static string smethod_2(int int_0)
    {
        return smethod_3(int_0, false);
    }

    public static string smethod_3(int int_0, bool bool_0)
    {
        if (bool_0)
        {
            Thread.Sleep(3);
        }
        char[] chArray = new char[] { 
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 
            'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 
            'W', 'X', 'Y', 'Z'
         };
        string str = "";
        int length = chArray.Length;
        Random random = new Random(~((int) DateTime.Now.Ticks));
        for (int i = 0; i < int_0; i++)
        {
            int index = random.Next(0, length);
            str = str + chArray[index];
        }
        return str;
    }

    public static string smethod_4(int int_0)
    {
        return smethod_5(int_0, false);
    }

    public static string smethod_5(int int_0, bool bool_0)
    {
        if (bool_0)
        {
            Thread.Sleep(3);
        }
        char[] chArray = new char[] { 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
         };
        string str = "";
        int length = chArray.Length;
        Random random = new Random(~((int) DateTime.Now.Ticks));
        for (int i = 0; i < int_0; i++)
        {
            int index = random.Next(0, length);
            str = str + chArray[index];
        }
        return str;
    }
}

