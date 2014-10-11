using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class MyParticipateWorkController : Controller
    {
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUserInfo _iUserInfo;
        private readonly ISysUserService _iSysUserService;
        //
        // GET: /WebApi/MyParticipateWork/
        public MyParticipateWorkController(IProjectInfoService iProjectInfoService, IUserInfo iUserInfo, ISysUserService iSysUserService)
        {
            _iProjectInfoService = iProjectInfoService;
            _iUserInfo = iUserInfo;
            _iSysUserService = iSysUserService;
        }

        public ActionResult Index(string keywords, bool? finish, int pageIndex = 1, int pageSize = 10)
        {
            var model = _iProjectInfoService.GetAll(a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow));

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

            var m = mainList.Select(
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

            return Content(JsonConvert.SerializeObject(m));
        }

    }
}
