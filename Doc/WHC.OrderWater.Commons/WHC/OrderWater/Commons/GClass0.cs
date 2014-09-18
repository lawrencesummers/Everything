namespace WHC.OrderWater.Commons
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    public class GClass0
    {
        public static bool HasInjectionData(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
            {
                return false;
            }
            return Regex.IsMatch(inputData.ToLower(), smethod_0());
        }

        private static string smethod_0()
        {
            string[] strArray2 = new string[] { 
                @"select\s", @"from\s", @"insert\s", @"delete\s", @"update\s", @"drop\s", @"truncate\s", @"exec\s", @"count\(", @"declare\s", @"asc\(", @"mid\(", @"\schar\(", "net user", "xp_cmdshell", @"/add\s", 
                "exec master.dbo.xp_cmdshell", "net localgroup administrators"
             };
            string str = ".*(";
            for (int i = 0; i < (strArray2.Length - 1); i++)
            {
                str = str + strArray2[i] + "|";
            }
            return (str + strArray2[strArray2.Length - 1] + ").*");
        }

        private static string smethod_1()
        {
            string[] strArray2 = new string[] { 
                "and", "exec", "insert", "select", "delete", "update", "count", "from", "drop", "asc", "char", "or", "%", ";", ":", "'", 
                "\"", "-", "chr", "mid", "master", "truncate", "char", "declare", "SiteName", "net user", "xp_cmdshell", "/add", "exec master.dbo.xp_cmdshell", "net localgroup administrators"
             };
            string str = ".*(";
            for (int i = 0; i < (strArray2.Length - 1); i++)
            {
                str = str + strArray2[i] + "|";
            }
            return (str + strArray2[strArray2.Length - 1] + ").*");
        }

        public static bool ValidUrlGetData()
        {
            bool flag = false;
            for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                if (flag = HasInjectionData(HttpContext.Current.Request.QueryString[i].ToString()))
                {
                    LogTextHelper.Info("检测出GET恶意数据: 【" + HttpContext.Current.Request.QueryString[i].ToString() + "】 URL: 【" + HttpContext.Current.Request.RawUrl + "】来源: 【" + HttpContext.Current.Request.UserHostAddress + "】");
                    return flag;
                }
            }
            return flag;
        }

        public static bool ValidUrlPostData()
        {
            bool flag = false;
            for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                if (flag = HasInjectionData(HttpContext.Current.Request.Form[i].ToString()))
                {
                    LogTextHelper.Info("检测出POST恶意数据: 【" + HttpContext.Current.Request.Form[i].ToString() + "】 URL: 【" + HttpContext.Current.Request.RawUrl + "】来源: 【" + HttpContext.Current.Request.UserHostAddress + "】");
                    return flag;
                }
            }
            return flag;
        }
    }
}

