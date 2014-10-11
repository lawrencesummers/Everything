using System.ComponentModel.DataAnnotations;
using BootstrapSupport;
using IServices.Infrastructure;
using IServices.ISysServices;
using System.Linq;
using System.Web.Mvc;

namespace Web.Areas.Platform.Controllers
{
    public class IndexController : Controller
    {
        private readonly ISysControllerService _sysControllerService;
        private readonly ISysUserService _sysUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfo _userInfo;

        public IndexController(ISysControllerService sysControllerService, ISysUserService sysUserService,
            IUnitOfWork unitOfWork, IUserInfo userInfo)
        {
            _sysControllerService = sysControllerService;
            _sysUserService = sysUserService;
            _unitOfWork = unitOfWork;
            _userInfo = userInfo;
        }

        //
        // GET: /Platform/Index/

        public ActionResult Index()
        {
            var area = (string)RouteData.DataTokens["area"];
            var model = _sysControllerService.GetAllEnt()
                .Where(
                    a =>
                        a.Display && a.Enabled &&
                        a.SysControllerSysActions.Any(
                            b =>
                                b.SysRoleSysControllerSysActions.Any(
                                    c =>
                                        c.SysRole.SysRoleSysUsers.Any(
                                            d => d.SysUserId.Equals(_userInfo.UserId)))) &&
                        a.SysArea.AreaName.Equals(area)).ToList();
            ViewBag.UserName = _sysUserService.GetDisplayName(_userInfo.UserId);
            ViewBag.EnterpriseName = _sysUserService.GetById(_userInfo.UserId).SysEnterprise.EnterpriseName;

            ViewBag.UserPicture = _sysUserService.GetById(_userInfo.UserId).Picture;

            return View(model);
        }

        //用户信息
        public ActionResult Edit()
        {
            var item = _sysUserService.GetById(_userInfo.UserId);
            return View(item);
        }

        public class SysUserEdit
        {
            [Required]
            public string DisplayName { get; set; }

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
            public string Picture { get; set; }

            public string MobilePhone { get; set; }
            public string GoogleUserName { get; set; }
            public string GooglePassword { get; set; }
        }

        [HttpPost]
        public ActionResult Edit(SysUserEdit collection)
        {

            if (!ModelState.IsValid)
            {
                var item = _sysUserService.GetById(_userInfo.UserId);
                return View(item);
            }

            var user = _sysUserService.GetById(_userInfo.UserId);
            if (user != null)
            {
                user.DisplayName = collection.DisplayName;
                user.Password = collection.Password;
                user.Email = collection.Email;
                user.MobilePhone = collection.MobilePhone;
                user.Picture = collection.Picture;
                user.GooglePassword = collection.GooglePassword;
                user.GoogleUserName = collection.GoogleUserName;
                _sysUserService.Update(user);
                _unitOfWork.Commit();
                TempData[Alerts.ATTENTION] = "修改成功！";
            }
            else
            {
                TempData[Alerts.ATTENTION] = "修改失败！";
            }

            return RedirectToAction("Edit");
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}