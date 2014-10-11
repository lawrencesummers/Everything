using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class HashPassword
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="password">明文</param>
        /// <returns>加密后</returns>
        public static string GetHashPassword(string password)
        {
            return Hmacsha1(password);
        }

        private static string Hmacsha1(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            const string key = "3.14159265358979323846"; //为了提升加密等级 用户自定义key参与加密
            var cc = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(cc.ComputeHash(Encoding.Default.GetBytes(value)));
        }
    }
}