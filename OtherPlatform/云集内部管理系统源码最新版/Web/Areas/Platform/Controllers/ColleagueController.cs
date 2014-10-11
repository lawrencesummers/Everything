using System;
using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using Models.SysModels;

namespace Web.Areas.Platform.Controllers
{
    public class ColleagueController : Controller
    {
        private readonly ISysUserService _ISysUserService;

        public ColleagueController(ISysUserService iSysUserService)
        {
            _ISysUserService = iSysUserService;
        }

        //
        // GET: /Platform/MyColleague/

        public ActionResult Index(string sysDepartmentId, bool? Enabled, Guid? sysRoleId, string keyword)
        {
            IQueryable<SysUser> model = _ISysUserService.GetAll();

            if (!string.IsNullOrEmpty(sysDepartmentId))
                model =
                    model.Where(
                        a => a.SysDepartmentSysUsers.Any(b => b.SysDepartment.SystemId.StartsWith(sysDepartmentId)));

            if (Enabled.HasValue)
                model = model.Where(a => a.Enabled == Enabled);

            if (sysRoleId.HasValue)
                model = model.Where(a => a.SysRoleSysUsers.Any(b => b.SysRoleId == sysRoleId));

            if (!string.IsNullOrEmpty(keyword))
                model = model.Where(a => a.UserName.Contains(keyword) || a.DisplayName.Contains(keyword));

            return View(model);
        }


        public ActionResult Details(Guid Id)
        {
            SysUser item = _ISysUserService.GetById(Id);
            return View(item);
        }
    }
}