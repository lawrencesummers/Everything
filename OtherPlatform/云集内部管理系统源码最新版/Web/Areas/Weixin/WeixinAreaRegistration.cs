using System.Web.Mvc;

namespace Web.Areas.Weixin
{
    public class WeixinAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Weixin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Weixin_default",
                "Weixin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                     new[] { "Web.Areas.Weixin.Controllers" }
            );
        }
    }
}