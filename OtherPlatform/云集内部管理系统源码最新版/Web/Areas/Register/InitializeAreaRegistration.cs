using System.Web.Mvc;

namespace Web.Areas.Register
{
    public class InitializeAreaRegistration : AreaRegistration
    {
        //系统初始化模块
        public override string AreaName
        {
            get
            {
                return "Register";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Register_default",
                "Register/{controller}/{action}/{id}",
                new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                new[] { "Web.Areas.Register.Controllers" }
            );
        }
    }
}
