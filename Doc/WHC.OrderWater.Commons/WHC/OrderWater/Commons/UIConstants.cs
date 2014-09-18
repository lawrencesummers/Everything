﻿namespace WHC.OrderWater.Commons
{
    using System;

    public class UIConstants
    {
        public static string ApplicationExpiredDate = "12/29/2009";
        public static string IsolatedStorage = @"UserNameDir\OrderWaterEnterprise.txt";
        public static string IsolatedStorageDirectoryName = "UserNameDir";
        public static string IsolatedStorageEncryptKey = "12345678";
        public static string PublicKey = "<RSAKeyValue><Modulus>mtDtu679/0quhftVyOc6/cBov/i534Dkh3AB8RwrpC9Vq2RIFB3uvjRUuaAEPR8vMcijQjVzqLZgMM7jFKclzbh21rWTM+YlOeraKz5FPCC7rSLnv6Tfbzia9VI/r5cfM8ogVMuUKCZeU+PTEmVviasCl8nPYyqOQchlf/MftMM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static int SoftwareProbationDay = 20;
        public static string SoftwareProductName = "OrderWaterEnterprise";
        public static string SoftwareRegistryKey = (@"SOFTWARE\Microsoft\OrderWaterEnterprise\" + SoftwareVersion);
        public static string SoftwareVersion = "3.0";
        public static string WebRegisterURL = "http://www.iqidi.com/WebRegister.aspx";

        public static void SetValue(string expiredDate, string version, string name, string publicKey)
        {
            ApplicationExpiredDate = expiredDate;
            SoftwareVersion = version;
            SoftwareProductName = name;
            SoftwareRegistryKey = @"SOFTWARE\Microsoft\" + name + @"\" + version;
            IsolatedStorage = @"UserNameDir\" + name + ".txt";
            PublicKey = publicKey;
        }
    }
}

