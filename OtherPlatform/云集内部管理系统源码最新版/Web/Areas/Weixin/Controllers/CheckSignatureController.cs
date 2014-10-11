using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Areas.Weixin.Controllers
{
    public class CheckSignatureController : Controller
    {
        //
        // GET: /Weixin/checkSignature/
        public ActionResult Index(string echoStr, string signature, string timestamp, string nonce)
        {
            const string token = "wjw1weixin";

            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);     //字典排序  
            var tmpStr = string.Join("", arrTmp);

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");

            if (tmpStr == null) return null;

            tmpStr = tmpStr.ToLower();

            return tmpStr == signature ? Content(echoStr) : null;
        }
    }
}