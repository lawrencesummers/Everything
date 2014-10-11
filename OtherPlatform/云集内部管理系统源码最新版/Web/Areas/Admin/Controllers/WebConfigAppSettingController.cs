using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Mvc;
using Common;

namespace Web.Areas.Admin.Controllers
{
    public class WebConfigAppSettingController : Controller
    {
        //
        // GET: /Platform/WebConfigAppSetting/

        public ActionResult Index()
        {
            NameValueCollection model = ConfigurationManager.AppSettings;
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.id = id;
            ViewBag.value = ConfigurationManager.AppSettings[id];
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id, string value)
        {
            try
            {
                var webConfig = new WebAppSetting();
                webConfig.Modify(id, value);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            Edit(id);
            return View();
        }
    }
}