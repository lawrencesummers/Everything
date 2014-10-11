using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.SysModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class ColleagueController : Controller
    {
        private readonly ISysUserService _iSysUserService;
        private readonly IProjectInfoService _iProjectInfoService;

        public ColleagueController(ISysUserService iSysUserService, IProjectInfoService iProjectInfoService)
        {
            _iSysUserService = iSysUserService;
            _iProjectInfoService = iProjectInfoService;
        }

        //
        // GET: /Platform/MyColleague/

        public ActionResult Index(string sysDepartmentId, bool? enabled, Guid? sysRoleId, string keywords, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<SysUser> model = _iSysUserService.GetAll();

            if (!string.IsNullOrEmpty(sysDepartmentId))
                model =
                    model.Where(
                        a => a.SysDepartmentSysUsers.Any(b => b.SysDepartment.SystemId.StartsWith(sysDepartmentId)));

            if (enabled.HasValue)
                model = model.Where(a => a.Enabled == enabled);

            if (sysRoleId.HasValue)
                model = model.Where(a => a.SysRoleSysUsers.Any(b => b.SysRoleId == sysRoleId));

            if (!string.IsNullOrEmpty(keywords))
                model = model.Where(a => a.UserName.Contains(keywords) || a.DisplayName.Contains(keywords));

            return GetModelDetail(model, pageIndex, pageSize);
        }

        //根据项目id查询成员
        public ActionResult InProject(Guid itemId, int pageIndex = 1, int pageSize = 10)
        {
            var model = _iProjectInfoService.GetById(itemId).ProjectUsers.Where(a => !a.Follow).Select(a => a.SysUser).AsQueryable();
            return GetModelDetail(model,pageIndex, pageSize);

        }

        private ActionResult GetModelDetail(IQueryable<SysUser> model, int pageIndex, int pageSize)
        {
            var result = model.Select(a => new
            {
                a.Id,
                a.UserId,
                a.CreatedDate,
                a.DisplayName,
                a.MobilePhone,
                a.Picture,
                a.Enabled,
                a.Email,
                a.UserName,
                DepartmentName = a.SysDepartmentSysUsers.Select(b => b.SysDepartment.DepartmentName),
                RoleName = a.SysRoleSysUsers.Select(b => b.SysRole.RoleName),
                a.Remark
            }).ToPagedList(pageIndex, pageSize);

            return Content(JsonConvert.SerializeObject(result));
        }

    }
}