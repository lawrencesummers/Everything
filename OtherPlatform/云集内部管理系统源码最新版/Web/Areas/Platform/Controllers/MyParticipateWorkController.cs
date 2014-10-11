using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class MyParticipateWorkController : Controller
    {
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;


        public MyParticipateWorkController(IProjectInfoService iProjectInfoService, IUserInfo iUserInfo,
            IUnitOfWork unitOfWork)
        {
            _iProjectInfoService = iProjectInfoService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
        }

        // 我参与的项目
        // GET: /Platform/MyParticipateWork/

        public ActionResult Index(string keyword, bool? finish, int pageIndex = 1)
        {
            IQueryable<ProjectInfo> model =
                _iProjectInfoService.GetAll(a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow));

            //为了显示子项目准备
            ViewBag.ProjectInfo = model;

            if (!string.IsNullOrEmpty(keyword))
            {
                model =
                    model.Where(
                        a =>
                            a.ProjectName.Contains(keyword) || a.ProjectObjective.Contains(keyword) ||
                            a.Tag.Contains(keyword) || a.ProjectUsers.Any(b => b.SysUser.UserName == keyword));
            }
            else
            {
                //筛选主项目
                model = model.Where(a => a.LastProjectInfoId == null);
            }

            ViewBag.CountAll = model.Count();
            ViewBag.unfinish = model.Count(a => !a.Finish);
            ViewBag.finish = model.Count(a => a.Finish);

            if (finish.HasValue)
            {
                model = model.Where(a => a.Finish == finish);
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