using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using Models.SysModels;

namespace Web.Areas.Platform.Controllers
{
    public class HelpController : Controller
    {
        private readonly ISysHelpService _iSysHelpService;

        public HelpController(ISysHelpService iSysHelpService)
        {
            _iSysHelpService = iSysHelpService;
        }

        //
        // GET: /Admin/Desktop/

        public ActionResult Index(string keyword)
        {
            IQueryable<SysHelp> model = _iSysHelpService.GetAllEnt();

            if (!string.IsNullOrEmpty(keyword))
            {
                model = model.Where(a => a.Title.Contains(keyword) || a.Content.Contains(keyword));
            }

            return View(model);
        }
    }
}