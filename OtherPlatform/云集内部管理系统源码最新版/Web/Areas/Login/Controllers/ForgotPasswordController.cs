using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices;
using IServices.ISysServices;
using Models;
using Models.SysModels;
using Services;
using Services.SysServices;

namespace Web.Areas.Login.Controllers
{
    [AllowAnonymous]
    public class ForgotPasswordController : Controller
    {
        private readonly ISysUserResetPasswordService _sysUserResetPasswordService ;
        private readonly ISysUserService _sysUserService;

        public ForgotPasswordController(ISysUserService sysUserService, ISysUserResetPasswordService sysUserResetPasswordService)
        {
            _sysUserService = sysUserService;
            _sysUserResetPasswordService = sysUserResetPasswordService;
        }

        // 找回密码
        // GET: /Login/ForgotPassword/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(string userName, string email)
        {
            //检测邮箱和密码是否相符
            var item =
                _sysUserService.GetAll().SingleOrDefault(
                    a => a.UserName.Equals(userName) && a.Email.Equals(email));
            if (item == null)
            {
                ModelState.AddModelError("", "您输入的用户名或邮箱错误！");
                return View();
            }

            //发送加密验证邮件 将加密字符串保存 到SysUserResetPassword
            var sysUserResetPassword = new SysUserResetPassword
                                           {
                                               SysUserId = item.Id
                                           };

            var key = HashPassword.GetHashPassword(item.Id + item.Password + sysUserResetPassword.CreatedDate);
            sysUserResetPassword.Key = key;

            _sysUserResetPasswordService.Add(sysUserResetPassword);

            var url = Url.Action("Reset", "ForgotPassword", new { area = "Login", id = item.Id, key }, "http");

            url = "<a href='" + url + "'>点此找回密码</a>";

            Email.SendEmail(email, "密码找回", url);

            ModelState.AddModelError("", string.Format("找回密码邮件已经发送到您的{0}邮箱中！", email));

            return View();
        }


        public ActionResult Reset(Guid id, string key)
        {
            var datetime = DateTime.Now.AddDays(-1);
            var item = _sysUserResetPasswordService.GetAll().SingleOrDefault(a => !a.Used && a.SysUserId.Equals(id) && a.Key.Equals(key) && a.CreatedDate > datetime);

            if (item == null)
            {
                ViewBag.invalid = true;
                ModelState.AddModelError("", "找回密码操作无效！");
            }
            else
            {
                ViewBag.invalid = false;
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Reset(Guid id, string key, string password, string repassword)
        {
            var datetime = DateTime.Now.AddDays(-1);
            var reset = _sysUserResetPasswordService.GetAll().SingleOrDefault(
                    a => !a.Used && a.SysUserId.Equals(id) && a.Key.Equals(key) && a.CreatedDate > datetime);

            if (reset == null)
            {
                ViewBag.invalid = true;
                ModelState.AddModelError("", "找回密码操作无效！");
            }
            else
            {
                ViewBag.invalid = false;
            }

            if (string.IsNullOrEmpty("password"))
            {
                ModelState.AddModelError("password", "请输入新密码！");
            }

            if (!password.Equals(repassword))
            {
                ModelState.AddModelError("repassword", "两次输入的密码不同！");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var item = _sysUserService.GetById(id);
            if (item != null)
            {
                item.Password = password;
                _sysUserService.Update(item);
                reset.Used = true;
                _sysUserResetPasswordService.Update(reset);
            }

            ModelState.AddModelError("", "修改成功！");

            return View();
        }
    }
}