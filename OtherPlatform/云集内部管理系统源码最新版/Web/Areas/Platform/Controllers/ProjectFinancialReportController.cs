using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ProjectFinancialReportController : Controller
    {
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUserInfo _iUserInfo;

        public ProjectFinancialReportController(IProjectInfoService projectInfoService, IUserInfo iUserInfo)
        {
            _iProjectInfoService = projectInfoService;
            _iUserInfo = iUserInfo;
        }

        // 知识库
        // GET: /Platform/ProjectFinancial/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            //筛选项目成员中包含我的
            var model =
                _iProjectInfoService.GetAll(a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow))
                    .Select(
                        a =>
                            new
                            {
                                a.ProjectName,
                                Count = a.ProjectFinancials.Count(b => !b.Deleted),
                                AccountReceivable =
                                    a.ProjectFinancials.Any(b => !b.Deleted)
                                        ? a.ProjectFinancials.Where(b => !b.Deleted).Sum(b => b.AccountReceivable)
                                        : 0,
                                PaidIn = a.ProjectFinancials.Where(b => !b.Deleted).Sum(b => b.PaidIn),
                                Difference =
                                    a.ProjectFinancials.Where(b => !b.Deleted).Sum(b => b.AccountReceivable - b.PaidIn),
                                Finish = a.ProjectFinancials.All(b => b.Finish)
                            }).Where(a => a.Count > 0).Search(keyword);

            model = model.OrderBy(a => a.Finish).ThenByDescending(a => a.Difference);

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

        public ActionResult Details(Guid id)
        {
            ProjectInfo item = _iProjectInfoService.GetById(id);
            return View(item);
        }
    }
}