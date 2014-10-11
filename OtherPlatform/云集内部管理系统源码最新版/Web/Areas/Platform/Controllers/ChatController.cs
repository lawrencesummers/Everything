using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using Models.SysModels;

namespace Web.Areas.Platform.Controllers
{
    public class ChatController : Controller
    {
        private readonly ISysDepartmentService _iSysDepartmentService;
        private readonly ISysSignalRService _iSysSignalRService;
        private readonly IUserInfo _iUserInfo;

        public ChatController(IUserInfo iUserInfo, ISysSignalRService iSysSignalRService,
            ISysDepartmentService iSysDepartmentService)
        {
            _iUserInfo = iUserInfo;
            _iSysSignalRService = iSysSignalRService;
            _iSysDepartmentService = iSysDepartmentService;
        }

        public ActionResult Index()
        {
            ViewBag.EntId = _iUserInfo.EnterpriseId;
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.SysDepartments = _iSysDepartmentService.GetAll();

            var model = _iSysSignalRService.GetAll("chat");

            //筛选当前用户


            return View(model.Take(100).OrderBy(a => a.CreatedDate));
        }

        public ActionResult Details(string id, string keyword, int pageIndex = 1)
        {
            var model = _iSysSignalRService.GetAll("chat");

            //筛选当前用户


            if (!string.IsNullOrEmpty(keyword))
                model =
                    model.Where(
                        a =>
                            a.UserName.Contains(keyword) || a.UserName1.Contains(keyword) || a.Message.Contains(keyword));


            return View(model.ToPagedList(pageIndex, 10));
        }
    }
}