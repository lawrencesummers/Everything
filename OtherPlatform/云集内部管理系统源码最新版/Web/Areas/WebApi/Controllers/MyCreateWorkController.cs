using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapSupport;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class MyCreateWorkController : Controller
    {
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfo _iUserInfo;
        private readonly IProjectTaskService _iProjectTaskService;
        private readonly ISysUserService _iSysUserService;
        private readonly IProjectUserService _iProjectUserService;
        private readonly ISysDepartmentService _iSysDepartmentService;
        private readonly IProjectInfoStateService _iProjectInfoStateService;
        private ICustomerService _iCustomerService;

        public MyCreateWorkController(IProjectInfoService iProjectInfoService, IUserInfo iUserInfo, IUnitOfWork unitOfWork, IProjectTaskService iProjectTaskService, ISysUserService iSysUserService, IProjectUserService iProjectUserService, ISysDepartmentService iSysDepartmentService, IProjectInfoStateService iProjectInfoStateService, ICustomerService iCustomerService)
        {
            _iProjectInfoService = iProjectInfoService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
            _iProjectTaskService = iProjectTaskService;
            _iSysUserService = iSysUserService;
            _iProjectUserService = iProjectUserService;
            _iSysDepartmentService = iSysDepartmentService;
            _iProjectInfoStateService = iProjectInfoStateService;
            _iCustomerService = iCustomerService;
        }

        public ActionResult Index(string keywords, bool? finish, int pageIndex = 1, int pageSize = 10)
        {
            var model = _iProjectInfoService.GetAll(a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && b.Leader));

            if (!string.IsNullOrEmpty(keywords))
            {
                model =
                    model.Where(
                        a =>
                        a.ProjectName.Contains(keywords) || a.ProjectObjective.Contains(keywords) ||
                        a.Tag.Contains(keywords) || a.ProjectUsers.Any(b => b.SysUser.UserName == keywords));
            }

            if (finish.HasValue)
            {
                model = model.Where(a => a.Finish == finish);
            }

            //子项目
            var subModel = model.Where(a => a.LastProjectInfoId != null);
            //主项目
            var mainModel = model.Where(a => a.LastProjectInfoId == null).ToPagedList(pageIndex, pageSize);

            var mainList = new List<ProjectInfo>() { };
            foreach (var item in mainModel)
            {
                mainList.Add(item);
                mainList.AddRange((subModel as IQueryable<ProjectInfo>).Where(a => a.LastProjectInfoId == item.Id));
            }

            var result = mainList.Select(
                a => new
                {
                    a.Id,
                    a.UserId,
                    Leader = a.ProjectUsers != null ? a.ProjectUsers.Where(b => b.Leader).Select(b => b.SysUser.DisplayName) : null,
                    ProjectInfoState = a.ProjectInfoState != null ? a.ProjectInfoState.ProjectInfoStateName : null,
                    Follow = a.Public && (a.ProjectUsers != null ? a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && b.Follow) : false),
                    a.LastProjectInfoId,
                    a.Raty,
                    a.Public,
                    PlanCount = a.Plans.Count(p => !p.Deleted),
                    TaskCount = a.ProjectTasks.Count(t => !t.Deleted),
                    ReplyCount = a.ProjectInfoReplys.Count(r => !r.Deleted),
                    MemberCount = a.ProjectUsers != null ? a.ProjectUsers.Count(b => !b.Follow) : 0,
                    a.CustomerId,
                    a.Tag,
                    a.StarTime,
                    a.EndTime,
                    a.Finish,
                    a.ProjectName,
                    a.ProjectObjective,
                    a.CreatedDate
                });
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Create(string entity)
        {
            var item = (ProjectInfo)JsonConvert.DeserializeObject(entity, typeof(ProjectInfo));
            _iProjectInfoService.Save(null, item);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Edit(string entity)
        {
            var item = (ProjectInfo)JsonConvert.DeserializeObject(entity, typeof(ProjectInfo));
            _iProjectInfoService.Save(item.Id, item);

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
                }
            }

            _iProjectUserService.Save(null, new ProjectUser
            {
                ProjectInfoId = item.Id,
                SysUserId = _iUserInfo.UserId,
                Leader = true
            });

            _unitOfWork.Commit();
            return Content("True");
        }

    }
}
