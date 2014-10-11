using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace Web.Helper
{
    /// <summary>
    ///     异常捕获
    /// </summary>
    public class LogExceptionFilterAttribute : HandleErrorAttribute
    {
        /// <summary>
        ///     触发异常时调用的方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //错误类型
            var exceptiontype = filterContext.Exception.GetType().Name;
            var message = string.Empty;
            if (exceptiontype.Equals("DbEntityValidationException"))
            {
                var exc = filterContext.Exception as DbEntityValidationException;
                if (exc != null)
                    foreach (var item in exc.EntityValidationErrors)
                    {
                        foreach (var item2 in item.ValidationErrors)
                        {
                            var errorMessage = item2.ErrorMessage;
                            var propertyName = item2.PropertyName;
                            message += string.Format("<br>错误消息：{0}<br>错误字段：{1}", errorMessage, propertyName);
                        }
                    }
            }

            var mainmessage = string.Format("消息类型：{0}<br>消息内容：{1}<br>引发异常的方法：{2}<br>引发异常的对象：{3}<br>异常目录：{4}<br>异常文件：{5}"
                    , exceptiontype
                    , filterContext.Exception.Message + message
                    , filterContext.Exception.TargetSite
                    , filterContext.Exception.Source
                    , filterContext.RouteData.GetRequiredString("controller")
                    , filterContext.RouteData.GetRequiredString("action"));

            Console.WriteLine(mainmessage);
        }
    }
}