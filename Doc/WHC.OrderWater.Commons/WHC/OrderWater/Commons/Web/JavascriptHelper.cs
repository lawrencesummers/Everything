namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class JavascriptHelper
    {
        public static void AlertAndClose(Control control, string message)
        {
            control.Page.RegisterStartupScript("", string.Format("<script>javascript:alert(\"{0}\");window.close();</script>", EncodeJS(message)));
        }

        public static void AlertAndLocation(Control control, string page, string message)
        {
            string script = "<script language='JavaScript'>";
            script = ((script + "alert('" + message + "');") + "top.location='" + page + "'") + "</script>";
            control.Page.RegisterStartupScript("", script);
        }

        public static void AlertAndLocation(Control control, string page, string message, string target)
        {
            string script = "<script language='JavaScript'>";
            script = (((script + "alert('" + message + "');") + ";window.target='" + target + "'") + ";window.location='" + page + "'") + "</script>";
            control.Page.RegisterStartupScript("", script);
        }

        public static void AlertAndLocationOpener(Control control, string page, string message)
        {
            string script = "<script language='JavaScript'>";
            script = ((script + "alert('" + message + "');") + ";window.opener.location='" + page + "'") + ";window.close();" + "</script>";
            control.Page.RegisterStartupScript("", script);
        }

        public static void AlertAndLocationPopWin(Control control, string page, string message)
        {
            string script = "<script language='JavaScript'>";
            script = ((script + "alert('" + message + "');") + ";parent.location='" + page + "'") + ";parent.ClosePop();" + "</script>";
            control.Page.RegisterStartupScript("", script);
        }

        public static void Alerts(Control control, string message)
        {
            control.Page.RegisterStartupScript("", string.Format("<script>javascript:alert(\"{0}\");</script>", EncodeJS(message)));
        }

        public static void BackHistory(int value)
        {
            string format = "<Script language='JavaScript'>history.go({0});</Script>";
            HttpContext.Current.Response.Write(string.Format(format, value));
            HttpContext.Current.Response.End();
        }

        public static void CloseWin(Control control, string returnValue)
        {
            string script = "<script language='JavaScript'>";
            script = (script + "window.parent.returnValue='" + returnValue + "';") + "window.close();" + "</script>";
            control.Page.RegisterStartupScript("", script);
        }

        public static string ConvertString(string strValue)
        {
            return strValue.Trim().Replace("'", "''");
        }

        public static string DateStringToString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.Now.ToShortDateString();
            }
            try
            {
                return DateTime.Parse(str).ToString("yyyy-MM-dd");
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public static string EncodeJS(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.Replace(@"\", @"\\").Replace("\n", @"\n").Replace("\r", @"\r").Replace("'", @"\'").Replace("\"", "\\\"");
        }

        public static string GetShortDate(DateTime? date)
        {
            string str = "";
            if (date.HasValue)
            {
                str = date.Value.ToString("yyyy-MM-dd");
            }
            return str;
        }

        public static void GoTo(string GoPage)
        {
            HttpContext.Current.Response.Redirect(GoPage);
        }

        public static bool IsFloat(string strValue)
        {
            return Regex.IsMatch(strValue, @"^(-?\d+)(\.\d+)?$");
        }

        public static bool IsNumerical(string strValue)
        {
            return Regex.IsMatch(strValue, "^[0-9]*$");
        }

        public static void Location(Control control, string page)
        {
            string script = "<script language='JavaScript'>";
            script = (script + "top.location='" + page + "'") + "</script>";
            control.Page.RegisterStartupScript("", script);
        }

        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            string s = string.Concat(new object[] { "<Script language='JavaScript'>window.open('", url, "','','height=", heigth, ",width=", width, ",top=", top, ",left=", left, ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>" });
            HttpContext.Current.Response.Write(s);
        }

        public static void RegisterScriptBlock(Page page, string scriptString)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock", "<script type='text/javascript'>" + scriptString + "</script>");
        }

        public static DateTime SafeConvertDate(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch
                {
                    return Convert.ToDateTime("1970-1-1");
                }
            }
            return Convert.ToDateTime("1970-1-1");
        }

        public static DateTime? SafeConvertDate2(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return new DateTime?(Convert.ToDateTime(value));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static decimal SafeConvertDecimal(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToDecimal(value);
                }
                catch
                {
                    return 0M;
                }
            }
            return 0M;
        }

        public static int SafeConvertInt32(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static void ShowConfirm(WebControl control, string message)
        {
            control.Attributes.Add("onclick", "return confirm('" + EncodeJS(message) + "');");
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            return ("<script language=javascript>\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tshowModalDialog('" + webFormUrl + "','','" + features + "');</script>");
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string s = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(s);
        }

        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px;dialogHeight:" + height.ToString() + "px;dialogLeft:" + left.ToString() + "px;dialogTop:" + top.ToString() + "px;center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
        }
    }
}

