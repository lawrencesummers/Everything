namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ProxyHelper
    {
        private const int int_0 = 0x25;
        private const int int_1 = 0x27;

        [DllImport("wininet.dll", SetLastError=true)]
        private static extern bool InternetSetOption(IntPtr intptr_0, int int_2, IntPtr intptr_1, int int_3);
        public static void SetIESupportWap()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes\MIME\Database\Content Type\text/vnd.wap.wml", true);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\MIME\Database\Content Type\text/vnd.wap.wml");
            }
            key.SetValue("CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}");
            key.Close();
            smethod_1();
        }

        public static void SetProxySetting(WebRequest request, ProxySettingEntity Proxy)
        {
            if (Proxy != null)
            {
                WebProxy defaultProxy = WebProxy.GetDefaultProxy();
                if (((Proxy.Ip != null) && (Proxy.Ip != "")) && (Proxy.Port != 0))
                {
                    defaultProxy.Address = new Uri(string.Concat(new object[] { "http://", Proxy.Ip, ":", Proxy.Port, "/" }));
                    if (!(string.IsNullOrEmpty(Proxy.UserName) || string.IsNullOrEmpty(Proxy.Password)))
                    {
                        defaultProxy.Credentials = new NetworkCredential(Proxy.UserName, Proxy.Password);
                    }
                }
                request.Proxy = defaultProxy;
            }
        }

        public static string smethod_0(string ProxyServer, int EnableProxy)
        {
            string str = "";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            key.SetValue("ProxyEnable", EnableProxy);
            if (!(ProxyServer.Equals("") || (EnableProxy != 1)))
            {
                key.SetValue("ProxyServer", ProxyServer);
                key.SetValue("ProxyEnable", 1);
                str = "设置代理成功！";
            }
            if (EnableProxy == 0)
            {
                key.SetValue("ProxyEnable", 0);
                str = "取消代理成功！";
            }
            key.Close();
            smethod_1();
            return str;
        }

        private static void smethod_1()
        {
            InternetSetOption(IntPtr.Zero, 0x27, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, 0x25, IntPtr.Zero, 0);
        }

        private static string smethod_2(WebResponse webResponse_0, Encoding encoding_0)
        {
            string str2;
            try
            {
                StreamReader reader = new StreamReader(webResponse_0.GetResponseStream(), encoding_0);
                string str = reader.ReadToEnd();
                reader.Close();
                str2 = str;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static bool TestProxy(ProxySettingEntity setting, TestEntity te)
        {
            if (setting == null)
            {
                return false;
            }
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(te.TestUrl);
            WebProxy defaultProxy = WebProxy.GetDefaultProxy();
            if (((setting.Ip != null) && (setting.Ip != "")) && (setting.Port != 0))
            {
                defaultProxy.Address = new Uri(string.Concat(new object[] { "http://", setting.Ip, ":", setting.Port, "/" }));
                if (!(string.IsNullOrEmpty(setting.UserName) || string.IsNullOrEmpty(setting.Password)))
                {
                    defaultProxy.Credentials = new NetworkCredential(setting.UserName, setting.Password);
                }
            }
            request.Proxy = defaultProxy;
            request.Method = "Get";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";
            WebResponse response = request.GetResponse();
            string str = smethod_2(response, Encoding.GetEncoding(te.TestWebEncoding));
            response.Close();
            return str.Contains(te.TestWebTitle);
        }
    }
}

