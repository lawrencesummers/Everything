namespace WHC.OrderWater.Commons
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public class RSASecurityHelper
    {
        public static void GenerateRSAKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            privateKey = provider.ToXmlString(true);
            publicKey = provider.ToXmlString(false);
        }

        public static string RSAEncrypSignature(string privateKey, string originalString)
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
            {
                provider.FromXmlString(privateKey);
                RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(provider);
                formatter.SetHashAlgorithm("SHA1");
                byte[] bytes = Encoding.ASCII.GetBytes(originalString);
                byte[] rgbHash = new SHA1Managed().ComputeHash(bytes);
                return Convert.ToBase64String(formatter.CreateSignature(rgbHash));
            }
        }

        public static string smethod_0(string publicKey, string originalString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            byte[] bytes = new UnicodeEncoding().GetBytes(originalString);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }

        public static string smethod_1(string publicKey, byte[] originalBytes)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            return Convert.ToBase64String(provider.Encrypt(originalBytes, false));
        }

        public static string smethod_2(string privateKey, string encryptedString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            byte[] rgb = Convert.FromBase64String(encryptedString);
            byte[] bytes = provider.Decrypt(rgb, false);
            return new UnicodeEncoding().GetString(bytes);
        }

        public static string smethod_3(string privateKey, byte[] encryptedBytes)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            byte[] bytes = provider.Decrypt(encryptedBytes, false);
            return new UnicodeEncoding().GetString(bytes);
        }

        public static bool Validate(string originalString, string encrytedString)
        {
            return Validate(originalString, encrytedString, UIConstants.PublicKey);
        }

        public static bool Validate(string originalString, string encrytedString, string publicKey)
        {
            bool flag = false;
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            try
            {
                key.FromXmlString(publicKey);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("SHA1");
                byte[] rgbSignature = Convert.FromBase64String(encrytedString);
                byte[] rgbHash = new SHA1Managed().ComputeHash(Encoding.ASCII.GetBytes(originalString));
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    flag = true;
                }
            }
            catch
            {
            }
            finally
            {
                if (key != null)
                {
                    key.Dispose();
                }
            }
            return flag;
        }
    }
}

