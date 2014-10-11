using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Web.Helper
{
    public class CultureInfoAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        ///     站点支持的语言列表
        /// </summary>
        public static readonly List<string> AvailableCultures = new List<string>
        {
            "en-US",
            "zh-CN"
        };

        #region IActionFilter Members

        public void OnActionExecuting(ActionExecutingContext
            filterContext)
        {
            string cultureCode = GetBrowserCulture(filterContext);
            if (string.IsNullOrEmpty(cultureCode) ||
                !AvailableCultures.Any(o => o.Equals(cultureCode, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        #endregion

        /// <summary>
        ///     获取浏览器的语言设置
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private string GetBrowserCulture(ActionExecutingContext filterContext)
        {// 下载于www.51aspx.com
            string[] browerCulture = filterContext.HttpContext.Request.UserLanguages;
            if (browerCulture == null)
            {
                return null;
            }
            return
                browerCulture.FirstOrDefault(
                    item => AvailableCultures.Any(o => o.Equals(item, StringComparison.OrdinalIgnoreCase)));
        }
    }
}