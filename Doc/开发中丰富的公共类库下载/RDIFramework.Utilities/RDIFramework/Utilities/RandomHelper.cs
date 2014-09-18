namespace RDIFramework.Utilities
{
    using System;

    public class RandomHelper
    {
        public static int Maximal = 0xf423f;
        public static int Minimum = 0x186a0;
        private static Random random_0 = new Random(DateTime.Now.Second);
        public static int RandomLength = 6;
        private static string string_0 = "123456789ABCDEFGHIJKMLNPQRSTUVWXYZ";

        public static int GetRandom()
        {
            return random_0.Next(Minimum, Maximal);
        }

        public static int GetRandom(int minimum, int maximal)
        {
            return random_0.Next(minimum, maximal);
        }

        public static string GetRandomString()
        {
            string str = string.Empty;
            for (int i = 0; i < RandomLength; i++)
            {
                int num2 = random_0.Next(0, string_0.Length - 1);
                str = str + string_0[num2];
            }
            return str;
        }
    }
}

