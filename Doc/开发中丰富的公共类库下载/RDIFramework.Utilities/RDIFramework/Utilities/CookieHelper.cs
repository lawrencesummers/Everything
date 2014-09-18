namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Specialized;
    using System.Web;

    public class CookieHelper
    {
        public static bool AddCookie(string CookieName, NameValueCollection Nvc)
        {
            if ((Nvc != null) && !string.IsNullOrEmpty(CookieName))
            {
                for (int i = 0; i < Nvc.Count; i++)
                {
                    HttpContext.Current.Request.Cookies[CookieName].Values.Add(Nvc.GetKey(i), Nvc.Get(i));
                    HttpContext.Current.Response.Cookies[CookieName].Values.Add(Nvc.GetKey(i), Nvc.Get(i));
                }
            }
            return false;
        }

        public static bool AddSingleCookie(string CookieName, string KeyName, string Value)
        {
            bool flag = false;
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(KeyName, Value);
            if (string.IsNullOrEmpty(CookieName))
            {
                return flag;
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                if (HttpContext.Current.Request.Cookies[CookieName].Values[KeyName] != null)
                {
                    return UpdateCookie(CookieName, nvc);
                }
                return AddCookie(CookieName, nvc);
            }
            return WriteCookie(CookieName, nvc);
        }

        public static bool DeleteCookie(string CookieName)
        {
            return DeleteCookie(CookieName, null);
        }

        public static bool DeleteCookie(string CookieName, string CookieDomain)
        {
            bool flag = false;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie == null)
            {
                return flag;
            }
            if (!string.IsNullOrEmpty(CookieDomain))
            {
                cookie.Domain = CookieDomain;
            }
            cookie.Expires = DateTime.Now.AddDays(-30.0);
            HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

        public static NameValueCollection GetCookie(string CookieName)
        {
            NameValueCollection values = new NameValueCollection();
            if (!(string.IsNullOrEmpty(CookieName) || (HttpContext.Current.Response.Cookies[CookieName] == null)))
            {
                values = HttpContext.Current.Response.Cookies[CookieName].Values;
            }
            return values;
        }

        public static string GetSingleValue(string CookieName, string KeyName)
        {
            string str = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                str = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[CookieName].Values[KeyName]);
            }
            return str;
        }

        public static string GetSingleValueFromServer(string CookieName, string KeyName)
        {
            string str = string.Empty;
            HttpCookie cookie = HttpContext.Current.Response.Cookies[CookieName];
            if (cookie != null)
            {
                str = HttpContext.Current.Response.Cookies[CookieName].Values[KeyName];
            }
            return str;
        }

        public static bool HasCookie(string CookieName)
        {
            bool flag = false;
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                flag = true;
            }
            return flag;
        }

        public static bool UpdateCookie(string CookieName, NameValueCollection Nvc)
        {
            bool flag = false;
            if ((Nvc == null) || string.IsNullOrEmpty(CookieName))
            {
                return flag;
            }
            HttpCookie cookie = new HttpCookie(CookieName);
            NameValueCollection values = HttpContext.Current.Request.Cookies[CookieName].Values;
            if (values == null)
            {
                return flag;
            }
            string str = string.Empty;
            for (int i = 0; i < values.Count; i++)
            {
                str = values.Get(i);
                int index = 0;
                while (index < Nvc.Count)
                {
                    if (values.GetKey(i) == Nvc.GetKey(index))
                    {
                        goto Label_0085;
                    }
                    index++;
                }
                goto Label_00A1;
            Label_0085:
                if (str != Nvc.Get(index))
                {
                    str = Nvc.Get(index);
                }
            Label_00A1:
                cookie.Values.Add(values.GetKey(i), str);
                str = string.Empty;
            }
            HttpContext.Current.Response.AppendCookie(cookie);
            return true;
        }

        public static bool UpdateSingleValue(string CookieName, string KeyName, string Value)
        {
            bool flag = false;
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(KeyName, Value);
            if (string.IsNullOrEmpty(CookieName))
            {
                return flag;
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                if (HttpContext.Current.Request.Cookies[CookieName].Values[KeyName] != null)
                {
                    return UpdateCookie(CookieName, nvc);
                }
                return AddCookie(CookieName, nvc);
            }
            return WriteCookie(CookieName, nvc);
        }

        public static bool WriteCookie(string CookieName, NameValueCollection Nvc)
        {
            return WriteCookie(CookieName, Nvc, 1, null);
        }

        public static bool WriteCookie(string CookieName, NameValueCollection Nvc, int days)
        {
            return WriteCookie(CookieName, Nvc, days, null);
        }

        public static bool WriteCookie(string CookieName, NameValueCollection Nvc, int days, string Domain)
        {
            bool flag = false;
            if ((Nvc == null) || string.IsNullOrEmpty(CookieName))
            {
                return flag;
            }
            HttpCookie cookie = new HttpCookie(CookieName);
            for (int i = 0; i < Nvc.Count; i++)
            {
                cookie.Values.Add(Nvc.GetKey(i), HttpUtility.UrlEncode(Nvc.Get(i)));
            }
            if (days > 0)
            {
                cookie.Expires = DateTime.Now.AddDays((double) days);
            }
            if (!string.IsNullOrEmpty(Domain))
            {
                cookie.Domain = Domain;
            }
            HttpContext.Current.Response.AppendCookie(cookie);
            return true;
        }

        public static bool WriteCookieNoDay(string CookieName, NameValueCollection Nvc)
        {
            bool flag = false;
            if ((Nvc == null) || string.IsNullOrEmpty(CookieName))
            {
                return flag;
            }
            HttpCookie cookie = new HttpCookie(CookieName);
            for (int i = 0; i < Nvc.Count; i++)
            {
                cookie.Values.Add(Nvc.GetKey(i), Nvc.Get(i));
            }
            HttpContext.Current.Response.AppendCookie(cookie);
            return true;
        }
    }
}

