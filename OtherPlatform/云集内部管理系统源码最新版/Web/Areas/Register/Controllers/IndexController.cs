using BootstrapSupport;
using Common;
using Models.SysModels;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace Web.Areas.Register.Controllers
{
    public class IndexController : Controller
    {
        private readonly ApplicationDb _db = new ApplicationDb();


        // 企业注册模块
        // GET: /Initialize/Index/

        public ActionResult Index()
        {
            ////判断系统内企业是否有有效用户
            //if (_db.SysEnterprises.Any(a => !a.Deleted))
            //{
            //    return null;
            //}

            //if (_db.SysUsers.Any(a => !a.Deleted))
            //{
            //    return null;
            //}

            ////显示系统初始化界面 企业及管理员 


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InitializeModel item)
        {
            //检测数据完整性
            if (_db.SysEnterprises.Any(u => !u.Deleted && u.EnterpriseName == item.EnterpriseName))
            {
                ModelState.AddModelError("EnterpriseName", @"该企业名称已存在");
            }

            if (_db.SysEnterprises.Any(u => !u.Deleted && u.EnterpriseShortName == item.EnterpriseShortName))
            {
                ModelState.AddModelError("EnterpriseShortName", @"该企业简称已存在");
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            try
            {
                #region SysEnterprise

                var host = Spell.MakeSpellCode(item.EnterpriseShortName, SpellOptions.FirstLetterOnly);

                //检查Host是否已经被占用

                while (_db.SysEnterprises.Any(a => a.Host == host))
                {
                    host = host + new Random().Next(11, 99);
                }

                host = host + ".wjw1.com";

                var sysEnterprise = new SysEnterprise { EnterpriseName = item.EnterpriseName, EnterpriseShortName = item.EnterpriseShortName, Host = host };

                _db.SysEnterprises.Add(sysEnterprise);

                #endregion

                #region SysRole

                var sysRole = new SysRole { RoleName = "管理员", EnterpriseId = sysEnterprise.Id };

                _db.SysRoles.Add(sysRole);

                #endregion

                #region SysUser

                var sysUser = new SysUser
                {
                    EnterpriseId = sysEnterprise.Id,
                    UserName = item.UserName,
                    DisplayName = "系统管理员",
                    Email = item.Email,
                    Password = HashPassword.GetHashPassword(item.Password),
                    OldPassword = HashPassword.GetHashPassword(item.Password),
                };

                _db.SysUsers.Add(sysUser);

                #endregion

                #region SysRoleSysUser

                var sysRoleSysUser = new SysRoleSysUser
                {
                    EnterpriseId = sysEnterprise.Id,
                    SysUserId = sysUser.Id,
                    SysRoleId = sysRole.Id
                };

                _db.SysRoleSysUsers.Add(sysRoleSysUser);

                #endregion

                #region SysRoleSysControllerSysAction

                var sysRoleSysControllerSysAction = new List<SysRoleSysControllerSysAction>();

                foreach (var sysControllerSysAction in _db.SysControllerSysActions)
                {
                    sysRoleSysControllerSysAction.Add(new SysRoleSysControllerSysAction
                    {
                        EnterpriseId = sysEnterprise.Id,
                        SysControllerSysActionId = sysControllerSysAction.Id,
                        SysRoleId = sysRole.Id
                    });
                }

                sysRoleSysControllerSysAction.ForEach(a => _db.SysRoleSysControllerSysActions.Add(a));

                #endregion

                _db.SaveChanges();

                TempData[Alerts.SUCCESS] = "注册成功！";

                return RedirectToAction("Index", "Succes", new { Id = sysEnterprise.Id });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(item);
            }
        }

        public class InitializeModel
        {
            [Required(ErrorMessage = @"企业名称不能为空")]
            [StringLength(50, MinimumLength = 4, ErrorMessage = @"{0}长度{2}~{1}")]
            [Remote("CheckUserAccountExists", "Index", ErrorMessage = @"该企业名称已存在")] // 远程验证（Ajax）
            public string EnterpriseName { get; set; }


            //公司简称
            [Required(ErrorMessage = @"企业简称不能为空")]
            [StringLength(50, MinimumLength = 4, ErrorMessage = @"{0}长度{2}~{1}")]
            [Remote("CheckEnterpriseShortNameExists", "Index", ErrorMessage = @"该企业简称已存在")] // 远程验证（Ajax）
            public string EnterpriseShortName { get; set; }


            [Required(ErrorMessage = @"用户名不能为空")]
            [StringLength(50, MinimumLength = 4, ErrorMessage = @"{0}长度{2}~{1}")]
            public string UserName { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = @"密码不能为空")]
            [StringLength(50, MinimumLength = 5, ErrorMessage = @"{0}长度{2}~{1}")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Required(ErrorMessage = @"密码不能为空")]
            [System.ComponentModel.DataAnnotations.Compare("Password")]
            public string ConfirmPassword { get; set; }


            [EmailAddress(ErrorMessage = @"请输入正确的邮箱")]
            public string Email { get; set; }
        }

        [HttpGet] // 只能用GET ！！！
        public ActionResult CheckUserAccountExists(string enterpriseName)
        {
            var exists = _db.SysEnterprises.Any(u => !u.Deleted && u.EnterpriseName == enterpriseName);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        [HttpGet] // 只能用GET ！！！
        public ActionResult CheckEnterpriseShortNameExists(string enterpriseShortName)
        {
            var exists = _db.SysEnterprises.Any(u => !u.Deleted && u.EnterpriseShortName == enterpriseShortName);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}