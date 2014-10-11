using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CachingListController : Controller
    {
        //
        // GET: /CachingList/

        public ActionResult Index()
        {
            ObjectCache cache = MemoryCache.Default;

            Response.Write(cache.GetCount());

            foreach (var item in cache)
            {
                Response.Write("<hr>");
                Response.Write(item.Key);
            }

            return null;
        }

    }
}
