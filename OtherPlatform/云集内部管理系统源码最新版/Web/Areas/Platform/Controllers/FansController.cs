using System.Web.Mvc;

namespace Web.Areas.Platform.Controllers
{
    public class FansController : Controller
    {
        // 我的粉丝
        // GET: /Platform/Fans/

        public ActionResult Index()
        {
            return View();
        }
    }
}