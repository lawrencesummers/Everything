using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BootstrapSupport;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Web.SignalR;

namespace Web.Areas.Platform.Controllers
{
    public class MyProjectTaskController : Controller
    {
        private readonly ISysDepartmentService _iSysDepartmentService;
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IProjectTaskService _iProjectTaskService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessenger _iMessenger;


        public MyProjectTaskController(IUserInfo iUserInfo, IProjectTaskService iProjectTaskService,
            IProjectInfoService iProjectInfoService, IUnitOfWork unitOfWork,
            ISysDepartmentService iSysDepartmentService, IMessenger iMessenger)
        {
            _iUserInfo = iUserInfo;
            _iProjectTaskService = iProjectTaskService;
            _iProjectInfoService = iProjectInfoService;
            _unitOfWork = unitOfWork;
            _iSysDepartmentService = iSysDepartmentService;
            _iMessenger = iMessenger;
        }

        // 我的任务
        // GET: /Platform/MyProjectTask/

        public ActionResult Index(string keyword, string displaytype, bool? finish, int pageIndex = 1)
        {
            ViewBag.UserId = _iUserInfo.UserId;
            IQueryable<ProjectTask> model =
                _iProjectTaskService.GetAll(
                    a =>
                        (a.ProjectInfo == null || !a.ProjectInfo.Deleted) && !a.Deleted &&
                        (a.SysUserId == _iUserInfo.UserId || a.UserId == _iUserInfo.UserId));
            if (!string.IsNullOrEmpty(keyword))
            {
                model =
                    model.Where(
                        a =>
                            a.ProjectTaskName.Contains(keyword) || a.ProjectTaskObjective.Contains(keyword) ||
                            a.ProjectInfo.ProjectName.Contains(keyword));
            }

            ViewBag.unfinished = model.Count(a => !a.Finish);
            ViewBag.finished = model.Count(a => a.Finish);

            //显示数量用
            ViewBag.CountAll = model.Count();
            ViewBag.CountMy = model.Count(a => a.SysUserId == _iUserInfo.UserId);
            ViewBag.CountTo = model.Count(a => a.UserId == _iUserInfo.UserId);

            if (finish.HasValue)
            {
                model = model.Where(a => a.Finish == finish);
            }

            if (displaytype == "0")
            {
                model = model.Where(a => a.SysUserId == _iUserInfo.UserId);
            }

            if (displaytype == "1")
            {
                model = model.Where(a => a.UserId == _iUserInfo.UserId);
            }

            model = model.OrderBy(a => a.Finish).ThenByDescending(a => a.CreatedDate);

            return View(model.ToPagedList(pageIndex));
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }
        public ActionResult Edit(Guid? id)
        {
            ViewBag.Id = id;

            var item = new ProjectTask();



            if (id.HasValue)
                item = _iProjectTaskService.GetById(id.Value);



            ViewBag.ProjectInfoId =
                new SelectList(
                    _iProjectInfoService.GetAll()
                        .Where(a => !a.Finish && a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow))
                        .Select(a => new { a.Id, a.ProjectName }), "Id", "ProjectName", item.ProjectInfoId);
            ViewBag.SysDepartment = _iSysDepartmentService.GetAll();

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Guid? id, ProjectTask item, Guid[] sysUserId)
        {
            if (sysUserId == null)
            {
                ModelState.AddModelError("SysUserId", "请选择用户");
            }

            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    if (item.SysUserId != null)
                        _iMessenger.SendMessage(item.SysUserId.Value, "任务更新：<a href='" + Url.Action("Details", new { item.Id }) + "'  data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#Main\" >" + item.ProjectTaskName + "</a>");

                    //编辑任务 针对单人
                    _iProjectTaskService.Save(id, item);
                }
                else
                {
                    //添加任务 多人
                    if (sysUserId != null)
                    {
                        foreach (var userid in sysUserId)
                        {
                            var projectTask = new ProjectTask
                            {
                                SysUserId = userid,
                                ProjectTaskName = item.ProjectTaskName,
                                ProjectInfoId = item.ProjectInfoId,
                                ProjectTaskObjective = item.ProjectTaskObjective,
                                FileUrl = item.FileUrl,
                                BeginTime = item.BeginTime,
                                EndTime = item.EndTime
                            };

                            _iProjectTaskService.Save(null, projectTask);

                            _iMessenger.SendMessage(userid, "新任务：<a href='" + Url.Action("Details", new { projectTask.Id }) + "'  data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#Main\" >" + item.ProjectTaskName + "</a>");
                        }
                    }
                }

                _unitOfWork.Commit();



                if (id.HasValue)
                {
                    TempData[Alerts.SUCCESS] = "编辑任务成功！";
                }

                else
                {
                    TempData[Alerts.SUCCESS] = "发布任务成功！";
                }
            }

            Edit(id);

            return View(item);
        }

        public ActionResult Details(Guid id, bool? finish, string displaytype)
        {
            ProjectTask item = _iProjectTaskService.GetById(id);
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.finish = finish;
            ViewBag.displaytype = displaytype;
            return View(item);
        }

        [HttpDelete]
        public ActionResult Delete(Guid id, string type, bool? finish, bool? accept, string displaytype,
            int pageIndex = 1)
        {
            switch (type)
            {
                case "Delete":
                    _iProjectTaskService.Delete(id);
                    break;
                case "Accept":

                    _iProjectTaskService.Accept(id, accept != null && accept.Value);

                    break;
                case "Finish":
                    _iProjectTaskService.Finish(id, finish != null && finish.Value);
                    finish = !finish;
                    break;
                default:

                    break;
            }

            _unitOfWork.Commit();
            return RedirectToAction("Index", new { displaytype, finish, pageIndex });
        }
    }
}