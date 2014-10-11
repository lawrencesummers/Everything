using System.Web.Mvc;

namespace Web.Areas.Platform.Controllers
{
    public class MyFileController : Controller
    {
        // 我的文档
        // GET: /Platform/MyFile/

        public ActionResult Index()
        {
            return View();
        }
    }
}