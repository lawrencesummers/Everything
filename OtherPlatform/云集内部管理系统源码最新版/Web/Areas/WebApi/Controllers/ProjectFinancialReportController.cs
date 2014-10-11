using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
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

        public ActionResult Index(string keywords, int pageIndex = 1, int pageSize = 10)
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
                            }).Where(a => a.Count > 0).Search(keywords);

            model = model.OrderBy(a => a.Finish).ThenByDescending(a => a.Difference);

            var result = model.Select(a => new
            {
                a.ProjectName,
                a.Count,
                a.AccountReceivable,
                a.PaidIn,
                a.Difference,
                a.Finish
            }).ToPagedList(pageIndex, pageSize);

            return Content(JsonConvert.SerializeObject(result));
        }

    }
}