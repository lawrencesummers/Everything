namespace WHC.OrderWater.Commons
{
    using System;
    using System.Globalization;
    using System.Threading;

    public class CultureInfoUtil
    {
        public static void InitializeCulture()
        {
            string str = LoadLanguage();
            if (!string.IsNullOrEmpty(str))
            {
                CultureInfo info = new CultureInfo(str);
                Thread.CurrentThread.CurrentCulture = info;
                Thread.CurrentThread.CurrentUICulture = info;
            }
        }

        public static string LoadLanguage()
        {
            string str = RegistryHelper.GetValue("language");
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (Thread.CurrentThread.CurrentCulture.Name.IndexOf("CN", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "zh-CN";
            }
            return "en-US";
        }

        public static RegionInfo CurrentRegion
        {
            get
            {
                return RegionInfo.CurrentRegion;
            }
        }
    }
}

