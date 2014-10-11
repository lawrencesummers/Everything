using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class ProjectFinancialController : Controller
    {
        private readonly IProjectFinancialService _iProjectFinancialService;
        private readonly IProjectInfoService _iProjectInfoService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectFinancialController(IProjectFinancialService iProjectFinancialService,
            IProjectInfoService projectInfoService, IUserInfo iUserInfo, IUnitOfWork unitOfWork)
        {
            _iProjectFinancialService = iProjectFinancialService;
            _iProjectInfoService = projectInfoService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
        }

        // 财务信息
        // GET: /Platform/ProjectFinancial/

        public ActionResult Index(Guid? projectInfoId, string keywords, string ordering, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<ProjectFinancial> models =
                _iProjectFinancialService.GetAll(
                    a => a.ProjectInfo.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow));

            if (projectInfoId.HasValue)
            {
                models = models.Where(a => a.ProjectInfoId == projectInfoId);
            }

            //筛选项目成员中包含我的
            var model = models.Select(a => new
            {
                a.Raty,
                a.ProjectInfo.ProjectName,
                a.AccountReceivable,
                a.AccountReceivableDate,
                a.PaidIn,
                a.PaidInDate,
                a.Invoice,
                a.InvoiceType,
                a.Finish,
                a.CreatedDate,
                a.Remark,
                a.Id,
                a.UserId
            }).Search(keywords);

            model = model.OrderBy(a => a.Finish).ThenBy(a => a.AccountReceivableDate);
            var pageResult = model.ToPagedList(pageIndex, pageSize);

            return Content(JsonConvert.SerializeObject(pageResult));
        }

        public ActionResult Details(Guid id)
        {
            ProjectFinancial item = _iProjectFinancialService.GetById(id);
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.ProjectInfoId = item.ProjectInfo.ProjectName;
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/ProjectFinancial/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new ProjectFinancial();
            if (id.HasValue)
            {
                item = _iProjectFinancialService.GetById(id.Value);
            }
            ViewBag.ProjectInfoId =
                new SelectList(
                    _iProjectInfoService.GetAll(
                        a => a.ProjectUsers.Any(b => b.SysUserId == _iUserInfo.UserId && !b.Follow)), "Id",
                    "ProjectName", item.ProjectInfoId);
            return View(item);
        }

        //
        // POST: /Platform/ProjectFinancial/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, ProjectFinancial collection)
        {
            if (!ModelState.IsValid)
            {
                Edit(id);
                return View(collection);
            }

            _iProjectFinancialService.Save(id, collection);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/ProjectFinancial/Delete/5

        public ActionResult Delete(Guid id)
        {
            _iProjectFinancialService.Delete(id);
            _unitOfWork.Commit();
            return Content("True");
        }
    }
}