using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.Infrastructure;
using Models.SysModels;
using Web.Helper;

namespace Web.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        private readonly ISysControllerService _sysControllerService;
        private readonly ISysUserService _sysUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfo _userInfo;


        public IndexController(ISysControllerService sysControllerService, ISysUserService sysUserService, IUnitOfWork unitOfWork, IUserInfo userInfo)
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
            ViewBag.UserName = _sysUserService.GetById(_userInfo.UserId).UserName;
            return View(model);
        }

        //用户信息
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ChangePassword item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            //验证原密码是否正确

            var user = _sysUserService.GetByUserNamePassword(_userInfo.UserId, item.OldPassword);
            if (user != null)
            {
                user.Password = item.NewPassword;
                _sysUserService.Update(user);
                _unitOfWork.Commit();
                ModelState.AddModelError("", "修改成功！");
            }
            else
            {
                ModelState.AddModelError("OldPassword", "原密码错误！");
            }

            return View();
        }



    }
}