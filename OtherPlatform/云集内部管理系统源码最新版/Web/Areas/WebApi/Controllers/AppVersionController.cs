using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using IServices.ISysServices;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class AppVersionController : Controller
    {

        //
        // GET: /WebApi/AppUpdate/
        public ActionResult Index(Guid enterpriseId)
        {
            var result = new
            {
                VersionCode = 1,
                VersionName = "1.0.0",
                UpdateDescription = "更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧更新了各种新功能，赶紧下载吧",
                AppUrl = "http://192.168.1.144/down/CloudHub.apk"
            };
            return Content(JsonConvert.SerializeObject(result));
        }
	}


}