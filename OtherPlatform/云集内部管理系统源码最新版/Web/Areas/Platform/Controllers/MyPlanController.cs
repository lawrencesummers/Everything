using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class MyPlanController : Controller
    {
        private readonly IPlanService _iPlanService;
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public MyPlanController(IUserInfo iUserInfo, IPlanService iPlanService, IUnitOfWork unitOfWork,
            IProjectInfoService iProjectInfoService)
        {
            _iUserInfo = iUserInfo;
            _iPlanService = iPlanService;
            _unitOfWork = unitOfWork;
            _iProjectInfoService = iProjectInfoService;
        }

        // 我的计划 参考tita计划
        // GET: /Platform/MyPlan/

        public ActionResult Index(DateTime? date, string keyword)
        {
            date = date.HasValue ? date : DateTime.Today;
            ViewBag.date = date;

            ViewBag.ProjectInfoId = new SelectList(
                    _iProjectInfoService.GetAll()
                        .Where(a => !a.Finish && a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow))
                        .Select(a => new { a.Id, a.ProjectName }), "Id", "ProjectName");

            var model = _iPlanService.GetAll(a => a.UserId == _iUserInfo.UserId).Search(keyword);
            return View(model);
        }

        public ActionResult Details(Guid id, string planType = "Day")
        {
            ViewBag.PlanType = planType;
            var item = _iPlanService.GetById(id);
            if (item.ProjectInfo != null)
                ViewBag.ProjectInfoId = item.ProjectInfo.ProjectName;
            return View(item);
        }


        [HttpPost]
        public ActionResult Create(Plan item)
        {
            _iPlanService.Save(null, item);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new { date = item.StartDate.Date });
        }

        public ActionResult Edit(Guid id)
        {
            var item = _iPlanService.GetById(id);

            ViewBag.ProjectInfoId =
                new SelectList(
                    _iProjectInfoService.GetAll()
                        .Where(a => !a.Finish && a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow))
                        .Select(a => new { a.Id, a.ProjectName }), "Id", "ProjectName", item.ProjectInfoId);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, Plan item)
        {
            if (!ModelState.IsValid)
            {
                Edit(id);
                return View(item);
            }

            _iPlanService.Save(id, item);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new { date = item.StartDate.Date });
        }

        [HttpDelete]
        public ActionResult Delete(string type, Guid id, DateTime date)
        {
            if (type == "Delete")
                _iPlanService.Delete(id);
            if (type == "Finish")
                _iPlanService.Finish(id);

            _unitOfWork.Commit();
            return RedirectToAction("Index", new { date });
        }
    }
}