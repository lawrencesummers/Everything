namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class MD5Util
    {
        public static bool AddMD5(string path)
        {
            bool flag = true;
            if (CheckMD5(path))
            {
                flag = false;
            }
            try
            {
                FileStream stream2;
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int) stream.Length);
                stream.Close();
                if (flag)
                {
                    string s = smethod_0(buffer, 0, buffer.Length);
                    byte[] bytes = Encoding.ASCII.GetBytes(s);
                    stream2 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.Close();
                }
                else
                {
                    stream2 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string AddMD5Profix(string input)
        {
            return (GetMD5_4(input) + input);
        }

        public static bool CheckMD5(string path)
        {
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int) stream.Length);
                stream.Close();
                string str = smethod_0(buffer, 0, buffer.Length - 0x20);
                string str2 = Encoding.ASCII.GetString(buffer, buffer.Length - 0x20, 0x20);
                return (str == str2);
            }
            catch
            {
                return false;
            }
        }

        public static string GetMD5_16(string input)
        {
            return GetMD5_32(input).Substring(8, 0x10);
        }

        public static string GetMD5_32(string input)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string GetMD5_4(string input)
        {
            return GetMD5_32(input).Substring(8, 4);
        }

        public static string GetMD5_8(string input)
        {
            return GetMD5_32(input).Substring(8, 8);
        }

        private void method_0()
        {
            string input = "i love u";
            input = AddMD5Profix(input);
            Console.WriteLine(input);
            Console.WriteLine(ValidateValue(input));
            Console.WriteLine(RemoveMD5Profix(input));
        }

        public static string RemoveMD5Profix(string input)
        {
            return input.Substring(4);
        }

        private static string smethod_0(byte[] byte_0, int int_0, int int_1)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(provider.ComputeHash(byte_0, int_0, int_1)).Replace("-", "");
        }

        public static bool ValidateValue(string input)
        {
            bool flag = false;
            if (input.Length >= 4)
            {
                string str = input.Substring(4);
                if (input.Substring(0, 4) == GetMD5_4(str))
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}

