using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices.ISysServices;

namespace Web.Areas.Register.Controllers
{
    public class SuccesController : Controller
    {

        private readonly ISysEnterpriseService _iSysEnterpriseService;

        public SuccesController(ISysEnterpriseService iSysEnterpriseService)
        {
            _iSysEnterpriseService = iSysEnterpriseService;
        }

        //
        // GET: /Initialize/Succes/
        public ActionResult Index(Guid Id)
        {
            var item = _iSysEnterpriseService.GetById(Id);

            return View(item);
        }
	}
}