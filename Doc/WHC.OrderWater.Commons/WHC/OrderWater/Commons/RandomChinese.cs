namespace WHC.OrderWater.Commons
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Threading;

    public class RandomChinese
    {
        public static string GetRandomChars(int Length, params int[] Seed)
        {
            Random random;
            char[] separator = ",".ToCharArray();
            string str2 = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] strArray = str2.Split(separator, str2.Length);
            string str3 = string.Empty;
            if ((Seed != null) && (Seed.Length > 0))
            {
                random = new Random(Seed[0]);
            }
            else
            {
                random = new Random();
            }
            for (int i = 0; i < Length; i++)
            {
                str3 = str3 + strArray[random.Next(strArray.Length)];
            }
            return str3;
        }

        public static string GetRandomChinese(int strlength)
        {
            Encoding encoding = Encoding.GetEncoding("gb2312");
            object[] objArray = smethod_0(strlength);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strlength; i++)
            {
                string str = encoding.GetString((byte[]) Convert.ChangeType(objArray[i], typeof(byte[])));
                builder.Append(str);
            }
            return builder.ToString();
        }

        public static string GetRandomChinese2(int strlength)
        {
            int num;
            string[] strArray = new string[strlength];
            Random random = new Random();
            for (num = 0; num < strlength; num++)
            {
                int num2;
                int num3 = random.Next(0x10, 0x58);
                if (num3 == 0x37)
                {
                    num2 = random.Next(1, 90);
                }
                else
                {
                    num2 = random.Next(1, 0x5e);
                }
                strArray[num] = Encoding.GetEncoding("GB2312").GetString(new byte[] { Convert.ToByte((int) (num3 + 160)), Convert.ToByte((int) (num2 + 160)) });
            }
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < strArray.Length; num++)
            {
                builder.Append(strArray[num]);
            }
            return builder.ToString();
        }

        public static string GetRandomNumber(int Length)
        {
            return GetRandomNumber(Length, false);
        }

        public static string GetRandomNumber(int Length, bool Sleep)
        {
            if (Sleep)
            {
                Thread.Sleep(3);
            }
            string str = "";
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                str = str + random.Next(10).ToString();
            }
            return str;
        }

        public static string GetRandomPureChar(int Length)
        {
            return GetRandomPureChar(Length, false);
        }

        public static string GetRandomPureChar(int Length, bool Sleep)
        {
            if (Sleep)
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
            for (int i = 0; i < Length; i++)
            {
                int index = random.Next(0, length);
                str = str + chArray[index];
            }
            return str;
        }

        public static string RandomFileName()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + GetRandomNumber(4));
        }

        public static string RandomPassword(int pwdlen)
        {
            string str = "";
            string str2 = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            for (int i = 0; i < pwdlen; i++)
            {
                int num2 = random.Next(str2.Length);
                str = str + str2[num2];
            }
            return str;
        }

        private static object[] smethod_0(int int_0)
        {
            string[] strArray2 = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            Random random = new Random();
            object[] objArray = new object[int_0];
            for (int i = 0; i < int_0; i++)
            {
                int num2;
                int num4;
                int index = random.Next(11, 14);
                string str4 = strArray2[index].Trim();
                random = new Random((index * ((int) DateTime.Now.Ticks)) + i);
                if (index == 13)
                {
                    num2 = random.Next(0, 7);
                }
                else
                {
                    num2 = random.Next(0, 0x10);
                }
                string str = strArray2[num2].Trim();
                random = new Random((num2 * ((int) DateTime.Now.Ticks)) + i);
                int num3 = random.Next(10, 0x10);
                string str2 = strArray2[num3].Trim();
                random = new Random((num3 * ((int) DateTime.Now.Ticks)) + i);
                if (num3 == 10)
                {
                    num4 = random.Next(1, 0x10);
                }
                else if (num3 == 15)
                {
                    num4 = random.Next(0, 15);
                }
                else
                {
                    num4 = random.Next(0, 0x10);
                }
                string str3 = strArray2[num4].Trim();
                byte num5 = Convert.ToByte(str4 + str, 0x10);
                byte num6 = Convert.ToByte(str2 + str3, 0x10);
                byte[] buffer2 = new byte[] { num5, num6 };
                objArray.SetValue(buffer2, i);
            }
            return objArray;
        }
    }
}

