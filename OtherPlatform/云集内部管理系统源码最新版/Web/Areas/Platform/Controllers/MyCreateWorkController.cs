using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Web.SignalR;

namespace Web.Areas.Platform.Controllers
{
    public class MyCreateWorkController : Controller
    {
        private readonly ICustomerService _iCustomerService;
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IProjectInfoStateService _iProjectInfoStateService;
        private readonly IProjectTaskService _iProjectTaskService;
        private readonly IProjectUserService _iProjectUserService;
        private readonly ISysDepartmentService _iSysDepartmentService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessenger _iMessenger;

        public MyCreateWorkController(IProjectInfoService iProjectInfoService, IUserInfo iUserInfo,
            IUnitOfWork unitOfWork, IProjectTaskService iProjectTaskService, IProjectUserService iProjectUserService,
            ISysDepartmentService iSysDepartmentService, IProjectInfoStateService iProjectInfoStateService,
            ICustomerService iCustomerService, IMessenger iMessenger)
        {
            _iProjectInfoService = iProjectInfoService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
            _iProjectTaskService = iProjectTaskService;
            _iProjectUserService = iProjectUserService;
            _iSysDepartmentService = iSysDepartmentService;
            _iProjectInfoStateService = iProjectInfoStateService;
            _iCustomerService = iCustomerService;
            _iMessenger = iMessenger;
        }

        //
        // GET: /Platform/MyCreateWork/

        public ActionResult Index(string keyword, bool? finish, int pageIndex = 1)
        {
            IQueryable<ProjectInfo> model =
                _iProjectInfoService.GetAll(a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && b.Leader));

            //为了显示子项目准备
            ViewBag.ProjectInfo = model.Where(a => a.LastProjectInfoId != null);


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

        public ActionResult Create(Guid? LastProjectInfoId)
        {
            return RedirectToAction("Edit", new {LastProjectInfoId});
        }

        public ActionResult Edit(Guid? id)
        {
            var item = new ProjectInfo();
            if (id.HasValue)
            {
                item = _iProjectInfoService.GetById(id.Value);
            }
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.SysDepartment = _iSysDepartmentService.GetAll();
            ViewBag.ProjectInfoStateId = _iProjectInfoStateService.SelectList(item.ProjectInfoStateId);
            ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll().Select(a => new {a.Id, a.CustomerName}), "Id",
                "CustomerName", item.CustomerId);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Guid? id, ProjectInfo item)
        {
            if (!ModelState.IsValid)
            {
                Edit(id);
                return View(item);
            }

            _iProjectInfoService.Save(id, item);

            //清除原有用户数据关联
            _iProjectUserService.Delete(a => a.ProjectInfoId.Equals(item.Id) && !a.Follow);

            if (item.ProjectUsersId != null)
            {
                foreach (var sysUserId in item.ProjectUsersId)
                {
                    _iProjectUserService.Save(null, new ProjectUser
                    {
                        ProjectInfoId = item.Id,
                        Leader = item.LeaderUserId.Any(a => a == sysUserId),
                        SysUserId = sysUserId
                    });

                    _iMessenger.SendMessage(sysUserId, "加入项目：<a href='" + Url.Action("Details", "MyParticipateWork", new { item.Id }) + "'  data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#Main\" >" + item.ProjectName + "</a>");
                }
            }

            _iProjectUserService.Save(null, new ProjectUser
            {
                ProjectInfoId = item.Id,
                SysUserId = _iUserInfo.UserId,
                Leader = true
            });

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            ProjectInfo item = _iProjectInfoService.GetById(id);
            ViewBag.UserId = _iUserInfo.UserId;
            return View(item);
        }

        [HttpDelete]
        public ActionResult Delete(string table, Guid id)
        {
            //删除项目
            if (table == "ProjectInfo")
            {
                _iProjectInfoService.Delete(id);
                _unitOfWork.Commit();
            }

            //删除任务
            if (table == "ProjectTask")
            {
                Guid? rid = _iProjectTaskService.GetById(id).ProjectInfoId;
                _iProjectTaskService.Delete(id);
                _unitOfWork.Commit();
                return RedirectToAction("Details", new {id = rid});
            }

            return RedirectToAction("Index");
        }
    }
}