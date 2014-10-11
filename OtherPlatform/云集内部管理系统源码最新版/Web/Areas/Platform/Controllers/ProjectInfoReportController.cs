using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ProjectInfoReportController : Controller
    {
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUserInfo _iUserInfo;

        public ProjectInfoReportController(IProjectInfoService iProjectInfoService, IUserInfo iUserInfo)
        {
            _iProjectInfoService = iProjectInfoService;
            _iUserInfo = iUserInfo;
        }

        // 项目信息统计报表
        // GET: /Platform/ProjectInfoReport/

        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            IQueryable<ProjectInfo> model =
                _iProjectInfoService.GetAll(a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow));
            if (!string.IsNullOrEmpty(keyword))
            {
                model =
                    model.Where(
                        a =>
                            a.ProjectName.Contains(keyword) || a.ProjectObjective.Contains(keyword) ||
                            a.Tag.Contains(keyword) || a.ProjectUsers.Any(b => b.SysUser.UserName == keyword));
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