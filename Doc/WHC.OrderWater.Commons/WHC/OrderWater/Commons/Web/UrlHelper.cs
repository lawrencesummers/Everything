namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class UrlHelper
    {
        private static Encoding encoding_0 = Encoding.UTF8;

        public static string AddParam(string url, string paramName, string value)
        {
            string str;
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                str = HttpContext.Current.Server.UrlEncode(value);
                return (url + ("?" + paramName + "=" + str));
            }
            str = HttpContext.Current.Server.UrlEncode(value);
            return (url + ("&" + paramName + "=" + str));
        }

        public static string Base64Decrypt(string eStr)
        {
            if (!IsBase64(eStr))
            {
                return eStr;
            }
            byte[] bytes = Convert.FromBase64String(eStr);
            return HttpUtility.UrlDecode(encoding_0.GetString(bytes));
        }

        public static string Base64Encrypt(string sourthUrl)
        {
            string s = HttpUtility.UrlEncode(sourthUrl);
            return Convert.ToBase64String(encoding_0.GetBytes(s));
        }

        public static string GetFilePath(string localPath)
        {
            if (Regex.IsMatch(localPath, @"([A-Za-z]):\\([\S]*)"))
            {
                return localPath;
            }
            return HttpContext.Current.Server.MapPath(localPath);
        }

        public static string GetRelativeSiteUrl(string url)
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (!applicationPath.EndsWith("/"))
            {
                applicationPath = applicationPath + "/";
            }
            if (!(string.IsNullOrEmpty(url) || !url.StartsWith("~/")))
            {
                url = url.Substring(2, url.Length - 2);
            }
            return (applicationPath + url);
        }

        public static string GetRequestedFileName(string rawUrl, bool includeExtension)
        {
            string str = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);
            if (includeExtension)
            {
                return str;
            }
            return str.Substring(0, str.IndexOf("."));
        }

        public static string GetSiteRoot()
        {
            string str = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if ((str == null) || (str == "0"))
            {
                str = "http://";
            }
            else
            {
                str = "https://";
            }
            string str2 = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (((str2 == null) || (str2 == "80")) || (str2 == "443"))
            {
                str2 = "";
            }
            else
            {
                str2 = ":" + str2;
            }
            return (str + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + str2 + HttpContext.Current.Request.ApplicationPath);
        }

        public static string GetUrl()
        {
            string str;
            try
            {
                str = HttpContext.Current.Request.Url.ToString();
            }
            catch
            {
            }
            return str;
        }

        public static string GetUrlReferrer()
        {
            string str = null;
            try
            {
                str = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
            }
            if (str == null)
            {
                return "";
            }
            return str;
        }

        public static string GetWebPath()
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath != "/")
            {
                return (applicationPath + "/");
            }
            return applicationPath;
        }

        public static string GetWebPath(string localPath)
        {
            string str3;
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath != "/")
            {
                str3 = applicationPath + "/";
            }
            else
            {
                str3 = applicationPath;
            }
            if (localPath.StartsWith("~/"))
            {
                string str2 = localPath.Substring(2);
                return (str3 + str2);
            }
            return localPath;
        }

        public static bool IsBase64(string eStr)
        {
            if ((eStr.Length % 4) != 0)
            {
                return false;
            }
            if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static string UpdateParam(string url, string paramName, string value)
        {
            string str = paramName + "=";
            int startIndex = url.IndexOf(str) + str.Length;
            int index = url.IndexOf("&", startIndex);
            if (index == -1)
            {
                url = url.Remove(startIndex, url.Length - startIndex);
                url = url + value;
                return url;
            }
            url = url.Remove(startIndex, index - startIndex);
            url = url.Insert(startIndex, value);
            return url;
        }
    }
}

