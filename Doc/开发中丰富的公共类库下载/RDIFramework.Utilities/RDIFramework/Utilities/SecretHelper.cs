namespace RDIFramework.Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SecretHelper
    {
        public static bool CheckRegister()
        {
            bool flag = true;
            if (SystemInfo.NeedRegister && ((DateTime.Now.Year >= 0x7dc) && (DateTime.Now.Month > 12)))
            {
                flag = false;
            }
            return flag;
        }

        public static string smethod_0(string toEncrypt)
        {
            if (string.IsNullOrEmpty(toEncrypt.Trim()))
            {
                return string.Empty;
            }
            byte[] bytes = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] inputBuffer = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged managed = new RijndaelManaged {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] inArray = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static string smethod_1(string toDecrypt)
        {
            if (string.IsNullOrEmpty(toDecrypt.Trim()))
            {
                return string.Empty;
            }
            byte[] bytes = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] inputBuffer = Convert.FromBase64String(toDecrypt);
            RijndaelManaged managed = new RijndaelManaged {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] buffer3 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer3);
        }
    }
}

