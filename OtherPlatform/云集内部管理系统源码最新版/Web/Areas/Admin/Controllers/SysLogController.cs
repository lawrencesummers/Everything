using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.ISysServices;
using Web.Helper;

namespace Web.Areas.Admin.Controllers
{
    public class SysLogController : Controller
    {
        private readonly ISysLogService _sysLogService;

        public SysLogController(ISysLogService sysLogService)
        {
            _sysLogService = sysLogService;
        }

        //
        // GET: /Platform/SysLog/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model = _sysLogService.GetAll().Select(a => new { a.Level, a.Message, a.CreatedDate }).Search(keyword);


            if (!string.IsNullOrEmpty(ordering))
            {
                model = model.OrderBy(ordering, null);
            }

            if (!string.IsNullOrEmpty(Request["report"]))
            {
                //导出
                var reportModel = new Report(model.ToReportSource());
                return new ReportResult(reportModel);
            }

            return View(model.ToPagedList(pageIndex));
        }
    }
}