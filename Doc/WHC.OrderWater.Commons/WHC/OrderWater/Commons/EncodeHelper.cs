namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public sealed class EncodeHelper
    {
        private static byte[] byte_0 = new byte[] { 0x41, 0x72, 0x65, 0x79, 0x6f, 0x75, 0x6d, 0x79, 0x53, 110, 0x6f, 0x77, 0x6d, 0x61, 110, 0x3f };
        public const string DEFAULT_ENCRYPT_KEY = "12345678";
        private static readonly string string_0 = "@#kim123";

        public static string AES_Decrypt(string decryptString)
        {
            return AES_Decrypt(decryptString, string_0);
        }

        public static string AES_Decrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = smethod_0(decryptKey, 0x20, "");
                decryptKey = decryptKey.PadRight(0x20, ' ');
                ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(decryptKey), IV = byte_0 }.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(decryptString);
                byte[] bytes = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool AES_DecryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string decryptKey = "www.iqidi.com";
                FileStream fs = new FileStream(InputFile, FileMode.Open);
                FileStream stream2 = new FileStream(OutputFile, FileMode.Create);
                CryptoStream stream3 = AES_DecryptStream(fs, decryptKey);
                byte[] buffer = new byte[0x400];
                int count = 0;
                do
                {
                    count = stream3.Read(buffer, 0, buffer.Length);
                    stream2.Write(buffer, 0, count);
                }
                while (count >= buffer.Length);
                stream3.Close();
                fs.Close();
                stream2.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static CryptoStream AES_DecryptStream(FileStream fs, string decryptKey)
        {
            decryptKey = smethod_0(decryptKey, 0x20, "");
            decryptKey = decryptKey.PadRight(0x20, ' ');
            ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(decryptKey), IV = byte_0 }.CreateDecryptor();
            return new CryptoStream(fs, transform, CryptoStreamMode.Read);
        }

        public static string AES_Encrypt(string encryptString)
        {
            return AES_Encrypt(encryptString, string_0);
        }

        public static string AES_Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = smethod_0(encryptKey, 0x20, "");
            encryptKey = encryptKey.PadRight(0x20, ' ');
            ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 0x20)), IV = byte_0 }.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static bool AES_EncryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string encryptKey = "www.iqidi.com";
                FileStream stream = new FileStream(InputFile, FileMode.Open);
                FileStream fs = new FileStream(OutputFile, FileMode.Create);
                CryptoStream stream3 = AES_EncryptStrream(fs, encryptKey);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream3.Write(buffer, 0, buffer.Length);
                stream3.Close();
                stream.Close();
                fs.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static CryptoStream AES_EncryptStrream(FileStream fs, string encryptKey)
        {
            encryptKey = smethod_0(encryptKey, 0x20, "");
            encryptKey = encryptKey.PadRight(0x20, ' ');
            ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(encryptKey), IV = byte_0 }.CreateEncryptor();
            return new CryptoStream(fs, transform, CryptoStreamMode.Write);
        }

        public static string Base64Decrypt(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Base64Encrypt(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string DecryptString(string input, bool throwException)
        {
            string str = "";
            try
            {
                str = input;
                if (!MD5Util.ValidateValue(str))
                {
                    throw new Exception("字符串无法转换成功！");
                }
                return MD5Util.RemoveMD5Profix(Base64Util.Decrypt(MD5Util.RemoveMD5Profix(str)));
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
                return "";
            }
        }

        public static string DesDecrypt(string strText)
        {
            try
            {
                return DesDecrypt(strText, "12345678");
            }
            catch
            {
                return "";
            }
        }

        public static string DesDecrypt(string strText, string sDecrKey)
        {
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            byte[] buffer = new byte[strText.Length];
            rgbKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            buffer = Convert.FromBase64String(strText);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            Encoding encoding = new UTF8Encoding();
            return encoding.GetString(stream.ToArray());
        }

        public static void DesDecrypt(string m_InFilePath, string m_OutFilePath, string sDecrKey)
        {
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            rgbKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            FileStream stream = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            stream2.SetLength(0);
            byte[] buffer = new byte[100];
            long num = 0;
            long length = stream.Length;
            DES des = new DESCryptoServiceProvider();
            CryptoStream stream3 = new CryptoStream(stream2, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            while (num < length)
            {
                int count = stream.Read(buffer, 0, 100);
                stream3.Write(buffer, 0, count);
                num += count;
            }
            stream3.Close();
            stream2.Close();
            stream.Close();
        }

        public static string DesEncrypt(string strText)
        {
            try
            {
                return DesEncrypt(strText, "12345678");
            }
            catch
            {
                return "";
            }
        }

        public static string DesEncrypt(string strText, string strEncrKey)
        {
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            rgbKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(strText);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }

        public static void DesEncrypt(string m_InFilePath, string m_OutFilePath, string strEncrKey)
        {
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            rgbKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            FileStream stream = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            stream2.SetLength(0);
            byte[] buffer = new byte[100];
            long num = 0;
            long length = stream.Length;
            DES des = new DESCryptoServiceProvider();
            CryptoStream stream3 = new CryptoStream(stream2, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            while (num < length)
            {
                int count = stream.Read(buffer, 0, 100);
                stream3.Write(buffer, 0, count);
                num += count;
            }
            stream3.Close();
            stream2.Close();
            stream.Close();
        }

        public static string EncryptString(string input)
        {
            return MD5Util.AddMD5Profix(Base64Util.Encrypt(MD5Util.AddMD5Profix(input)));
        }

        public static string EncyptMD5_3_16(string s)
        {
            MD5 md = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            byte[] buffer = md.ComputeHash(bytes);
            byte[] buffer3 = md.ComputeHash(buffer);
            byte[] buffer4 = md.ComputeHash(buffer3);
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in buffer4)
            {
                builder.Append(num2.ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString().ToUpper();
        }

        public static string MD5EncryptHash(string input)
        {
            byte[] sourceArray = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] destinationArray = new char[sourceArray.Length];
            Array.Copy(sourceArray, destinationArray, sourceArray.Length);
            return new string(destinationArray);
        }

        public static string MD5EncryptHashHex(string input)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            string str = string.Empty;
            for (int i = 0; i < buffer.Length; i++)
            {
                str = str + Uri.HexEscape((char) buffer[i]);
            }
            return str.Replace("%", "").ToLower();
        }

        public static string SHA256(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SHA256Managed managed = new SHA256Managed();
            return Convert.ToBase64String(managed.ComputeHash(bytes));
        }

        private static string smethod_0(string string_1, int int_0, string string_2)
        {
            return smethod_1(string_1, 0, int_0, string_2);
        }

        private static string smethod_1(string string_1, int int_0, int int_1, string string_2)
        {
            string str = string_1;
            if (Regex.IsMatch(string_1, "[ࠀ-一]+") || Regex.IsMatch(string_1, "[가-힣]+"))
            {
                if (int_0 >= string_1.Length)
                {
                    return string.Empty;
                }
                return string_1.Substring(int_0, ((int_1 + int_0) > string_1.Length) ? (string_1.Length - int_0) : int_1);
            }
            if (int_1 > 0)
            {
                byte[] bytes = Encoding.Default.GetBytes(string_1);
                if (bytes.Length > int_0)
                {
                    int length = bytes.Length;
                    if (bytes.Length > (int_0 + int_1))
                    {
                        length = int_1 + int_0;
                    }
                    else
                    {
                        int_1 = bytes.Length - int_0;
                        string_2 = "";
                    }
                    int[] numArray = new int[int_1];
                    int num3 = 0;
                    for (int i = int_0; i < length; i++)
                    {
                        if (bytes[i] > 0x7f)
                        {
                            num3++;
                            if (num3 == 3)
                            {
                                num3 = 1;
                            }
                        }
                        else
                        {
                            num3 = 0;
                        }
                        numArray[i] = num3;
                    }
                    if ((bytes[length - 1] > 0x7f) && (numArray[int_1 - 1] == 1))
                    {
                        int_1++;
                    }
                    byte[] destinationArray = new byte[int_1];
                    Array.Copy(bytes, int_0, destinationArray, 0, int_1);
                    return (Encoding.Default.GetString(destinationArray) + string_2);
                }
            }
            return string.Empty;
        }

        public static string smethod_2(string strText)
        {
            byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(bytes);
        }
    }
}

