using BootstrapSupport;
using IServices.ISysServices;
using LanguagePack;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Web.Areas.Login.Models;

namespace Web.Areas.Login.Controllers
{
    [AllowAnonymous]
    public class IndexController : Controller
    {
        private readonly ISysUserService _sysUserService;
        private readonly ISysEnterpriseService _sysEnterpriseService;

        public IndexController(ISysUserService sysUserService, ISysEnterpriseService sysEnterpriseService)
        {
            _sysUserService = sysUserService;
            _sysEnterpriseService = sysEnterpriseService;
        }

        //
        // GET: /Login/Index/

        public ActionResult Index()
        {
            // if (!_sysEnterpriseService.GetAllEnt().Any())
            //    return RedirectToAction("Index", "Index", new { area = "Initialize" });

            //ViewBag.EnterpriseId = new SelectList(_sysEnterpriseService.GetAllEnt().Where(a => a.Enabled && a.Validity > DateTime.Now).Select(a => new { a.Id, a.EnterpriseName }), "Id", "EnterpriseName");


            //根据url选定当前企业

            if (Request.Url == null) return null;

            var host = Request.Url.Host;

            var item = _sysEnterpriseService.GetAllEnt().FirstOrDefault(a => a.Host.Equals(host));

            //如果该域名未绑定，直接跳转到注册页面
            if (item == null)
            {
                return RedirectToAction("Index", "Index", new { area = "Register" });
            }

            ViewBag.Enterprise = item;
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel item)
        {
            var sysUser = _sysUserService.GetByUserNamePassword(item.EnterpriseId, item.UserName, item.Password);

            if (sysUser != null)
            {
                if (sysUser.Enabled)
                {
                    FormsAuthentication.SetAuthCookie(sysUser.EnterpriseId + "," + sysUser.Id, item.Remember);
                    return Redirect(FormsAuthentication.DefaultUrl);
                }
                TempData[Alerts.Warning] = lang.UserDisabled;
            }
            else
            {
                TempData[Alerts.Warning] = lang.UserNamePasswordError;
            }

            return RedirectToAction("Index");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }
    }
}
