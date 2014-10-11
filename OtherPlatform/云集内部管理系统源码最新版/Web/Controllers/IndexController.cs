using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices.ISysServices;
using Models;

namespace Web.Controllers
{
    public class IndexController : Controller
    {
        
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Index", new {area = "Platform"});
        }

    }
}
