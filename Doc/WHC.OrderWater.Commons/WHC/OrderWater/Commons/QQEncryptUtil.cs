namespace WHC.OrderWater.Commons
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class QQEncryptUtil
    {
        public static string EncodePasswordWithVerifyCode(string password, string verifyCode)
        {
            return smethod_1(smethod_0(password) + verifyCode.ToUpper());
        }

        private static string smethod_0(string string_0)
        {
            MD5 md = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(string_0);
            bytes = md.ComputeHash(bytes);
            bytes = md.ComputeHash(bytes);
            return BitConverter.ToString(md.ComputeHash(bytes)).Replace("-", "").ToUpper();
        }

        private static string smethod_1(string string_0)
        {
            MD5 md = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(string_0);
            return BitConverter.ToString(md.ComputeHash(bytes)).Replace("-", "").ToUpper();
        }
    }
}

