using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IServices.ISysServices;
using LanguagePack;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISysUserService _sysUserService;
        private readonly IUserInfo _iUserInfo;

        public LoginController(ISysUserService sysUserService, IUserInfo iUserInfo)
        {
            _sysUserService = sysUserService;
            _iUserInfo = iUserInfo;
        }

        //
        // GET: /WebApi/Login/

        public ActionResult Index(string userName, string passWord, Guid enterpriseId, bool remember=true)
        {
            var sysUser = _sysUserService.GetByUserNamePassword(enterpriseId, userName, passWord);

            if (sysUser == null) return Content("UserName Or Password Error");

            if (!sysUser.Enabled) return Content("User disable");

            FormsAuthentication.SetAuthCookie(sysUser.EnterpriseId + "," + sysUser.Id, remember);

            return Content("True");
        }

//        public ActionResult GetUserInfo()
//        {
//            return Json(_iUserInfo, JsonRequestBehavior.AllowGet);
//        }

    }
}
