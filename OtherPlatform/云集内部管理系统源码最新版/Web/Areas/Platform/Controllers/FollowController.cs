using System.Web.Mvc;

namespace Web.Areas.Platform.Controllers
{
    public class FollowController : Controller
    {
        // 我关注的
        // GET: /Platform/Follow/

        public ActionResult Index()
        {
            return View();
        }
    }
}