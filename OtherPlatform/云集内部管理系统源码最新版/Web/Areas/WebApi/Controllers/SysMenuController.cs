using System.Collections;
using IServices.ISysServices;
using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class SysMenuController : Controller
    {

        private readonly ISysControllerService _sysControllerService;
        private readonly ISysUserService _sysUserService;
        private readonly IUserInfo _iUserInfo;

        public SysMenuController(ISysControllerService sysControllerService,ISysUserService sysUserService, IUserInfo iUserInfo)
        {
            _sysControllerService = sysControllerService;
            _sysUserService = sysUserService;
            _iUserInfo = iUserInfo;
        }
        //
        // GET: /WebApi/SysMenu/

        public ActionResult Index()
        {
            var userId = _iUserInfo.UserId;

            var sysUser = _sysUserService.GetById(userId);

            //用户信息
            var userInfo = new
            {
                UserId = userId,
                sysUser.UserName,
                sysUser.DisplayName,
                sysUser.Email,
                DepartmentName = sysUser.SysDepartmentSysUsers.Select(a => a.SysDepartment.DepartmentName),
                sysUser.Picture,
                sysUser.SysEnterprise.EnterpriseName,
                RoleName = sysUser.SysRoleSysUsers.Select(a => a.SysRole.RoleName),
                sysUser.GoogleUserName,
                sysUser.GooglePassword,
                sysUser.CreatedDate,
                sysUser.Remark
            };

            //菜单信息
            var sysMenu = _sysControllerService.GetAllEnt()
                         .Where(
                             a =>
                             a.Display && a.Enabled &&
                             a.SysControllerSysActions.Any(
                                 b =>
                                 b.SysRoleSysControllerSysActions.Any(
                                     c =>
                                     c.SysRole.SysRoleSysUsers.Any(
                                         d => d.SysUserId.Equals(userId)))) &&
                             a.SysArea.AreaName.Equals("platform")).Select(a => new { a.ControllerDisplayName, a.ControllerName, a.SystemId });

            //过滤掉部分菜单，包括字典、管理、流程、日历
            sysMenu = sysMenu.Where(a => String.Compare(a.SystemId.Substring(0, 3), "650", StringComparison.Ordinal) < 0 && !a.SystemId.Substring(0, 3).Equals("550") && !a.SystemId.Equals("200300"));

            var result = new { userInfo, sysMenu };

            return Content(JsonConvert.SerializeObject(result));
        }

    }
}
