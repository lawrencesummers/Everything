using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using Web.Helper;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // filters.Add(new HandleErrorAttribute());

            filters.Add(new LogExceptionFilterAttribute());

            filters.Add(new UserAuthorizeAttribute { Areas = new List<string> { "Admin", "Platform" } });
        }
    }
}