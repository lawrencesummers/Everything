using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class DesktopController : Controller
    {
        private readonly IMessageService _iMessageService;
        private readonly IPlanService _iPlanService;
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IProjectTaskService _iProjectTaskService;
        private readonly IUserInfo _iUserInfo;
        private ICustomerService _ICustomerService;

        public DesktopController(IUserInfo iUserInfo, IProjectTaskService iProjectTaskService,
            IProjectInfoService iProjectInfoService, IMessageService iMessageService, IPlanService iPlanService, ICustomerService iCustomerService)
        {
            _iUserInfo = iUserInfo;
            _iProjectTaskService = iProjectTaskService;
            _iProjectInfoService = iProjectInfoService;
            _iMessageService = iMessageService;
            _iPlanService = iPlanService;
            _ICustomerService = iCustomerService;
        }

        //
        // GET: /Admin/Desktop/

        public ActionResult Index()
        {
            ViewBag.UserId = _iUserInfo.UserId;
            IOrderedQueryable<ProjectTask> model =
                _iProjectTaskService.GetAll(a => (a.ProjectInfo == null || !a.ProjectInfo.Deleted) && !a.Finish)
                    .OrderBy(a => a.EndTime);

            ViewBag.ProjectInfo = _iProjectInfoService.GetAll(a => !a.Finish);

            ViewBag.Message = _iMessageService.GetAll(a => a.SysUserId == _iUserInfo.UserId).Take(5);

            ViewBag.Plan =
                _iPlanService.GetAll(a => !a.Finish && a.UserId == _iUserInfo.UserId && a.StartDate < DateTime.Now);

            ViewBag.CustomerCount = _ICustomerService.GetAll().Count();

            return View(model);
        }

        public ActionResult Details(DateTime? date)
        {
            //今天任务列表
            if (!date.HasValue)
                date = DateTime.Now;

            ViewBag.date = date;

            //计算哪天有未完成的日计划
            ViewBag.PlatCalender =
                _iPlanService.GetAll(
                    a => a.UserId == _iUserInfo.UserId && !a.Finish && DbFunctions.DiffDays(a.StartDate, a.EndDate) <= 1)
                    .Select(a => a.StartDate)
                    .ToList();

            return View();
        }
    }
}