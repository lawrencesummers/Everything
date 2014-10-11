using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class MyProjectTaskController : Controller
    {

        private readonly IProjectTaskService _iProjectTaskService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public MyProjectTaskController(IProjectTaskService projectTaskService, IUserInfo userInfo, IUnitOfWork unitOfWork)
        {
            _iProjectTaskService = projectTaskService;
            _iUserInfo = userInfo;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /WebApi/MyProjectTask/

        public ActionResult Index(string keyword, bool? finish, int? type, int pageIndex = 1, int pageSize = 10)
        {

            var model =
                _iProjectTaskService.GetAll(
                    a => (a.ProjectInfo == null || !a.ProjectInfo.Deleted) && !a.Deleted && (a.SysUserId == _iUserInfo.UserId || a.UserId == _iUserInfo.UserId));

            if (!string.IsNullOrEmpty(keyword))
            {
                model = model.Where(
                        a =>
                        a.ProjectTaskName.Contains(keyword) || a.ProjectTaskObjective.Contains(keyword) ||
                        a.ProjectInfo.ProjectName.Contains(keyword));
            }

            if (finish.HasValue)
            {
                model = model.Where(a => a.Finish == finish);
            }

            if (type == 0)
            {
                model = model.Where(a => a.SysUserId == _iUserInfo.UserId);
            }

            if (type == 1)
            {
                model = model.Where(a => a.UserId == _iUserInfo.UserId);
            }

            return GetModelDetail(model, pageIndex, pageSize);
        }

        //根据项目id查询任务
        public ActionResult InProject(Guid itemId, int pageIndex = 1, int pageSize = 10)
        {
            var model = _iProjectTaskService.GetAll( a => (a.ProjectInfoId == itemId && !a.Deleted));
            return GetModelDetail(model, pageIndex, pageSize);
        }

        private ActionResult GetModelDetail(IQueryable<ProjectTask> model, int pageIndex = 1, int pageSize = 10)
        {
            //排序
            model = model.OrderBy(a => a.Finish).ThenBy(a => a.EndTime);
            var result = model.Select(
            a =>
                new
                {
                    a.Id,
                    a.UserId,
                    a.ProjectTaskName,
                    a.ProjectTaskObjective,
                    a.BeginTime,
                    a.EndTime,
                    a.Finish,
                    a.FinishTime,
                    a.Accept,
                    a.AcceptTime,
                    a.Milestone,
                    a.CreatedDate,
                    a.SysUserId,
                    OperatorName = a.SysUser.DisplayName,
                    CreatorName = a.SendUser.DisplayName,
                    a.SendUser.Picture,
                    a.ProjectInfoId,
                    a.ProjectInfo.ProjectName,
                    a.Remark
                }).ToPagedList(pageIndex, pageSize);

            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Create(string entity, string sysUsersId)
        {
            var item = (ProjectTask)JsonConvert.DeserializeObject(entity, typeof(ProjectTask));
            var usersId = (Guid[])JsonConvert.DeserializeObject(sysUsersId, typeof(Guid[]));
            foreach (var userId in usersId)
            {
                if(_iUserInfo.UserId.Equals(userId)) continue;
                var projectTask = new ProjectTask
                {
                    SysUserId = userId,
                    ProjectTaskName = item.ProjectTaskName,
                    ProjectInfoId = item.ProjectInfoId,
                    ProjectTaskObjective = item.ProjectTaskObjective,
                    FileUrl = item.FileUrl,
                    BeginTime = item.BeginTime,
                    EndTime = item.EndTime
                };
                //var projectTask = item;
                //projectTask.SysUserId = userId;
                _iProjectTaskService.Save(null, projectTask);
            }
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Edit(string entity)
        {
            var item = (ProjectTask)JsonConvert.DeserializeObject(entity, typeof(ProjectTask));
            _iProjectTaskService.Save(item.Id, item);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Accept(Guid id)
        {
            _iProjectTaskService.Accept(id, true);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Refuse(Guid id)
        {
            _iProjectTaskService.Accept(id, false);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Delete(Guid id)
        {
            _iProjectTaskService.Delete(id);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Finish(Guid id)
        {
            _iProjectTaskService.Finish(id, true);
            _unitOfWork.Commit();
            return Content("True"); 
        }

    }
}
