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

namespace Web.Areas.Platform.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IBusinessChanceService _iBusinessChanceService;
        private readonly IBusinessStateService _iBusinessStateService;
        private readonly ICustomerBusinessChanceService _iCustomerBusinessChanceService;
        private readonly ICustomerLevelService _iCustomerLevelService;
        private readonly ICustomerService _iCustomerService;
        private readonly ICustomerTypeService _iCustomerTypeService;
        private readonly ISysDepartmentService _iSysDepartmentService;
        private readonly ITagService _iTagService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerService iCustomerService, IUserInfo iUserInfo, IUnitOfWork unitOfWork,
            ICustomerLevelService iCustomerLevelService, ICustomerTypeService iCustomerTypeService,
            ITagService iTagService, IBusinessStateService iBusinessStateService,
            ISysDepartmentService iSysDepartmentService, IBusinessChanceService iBusinessChanceService,
            ICustomerBusinessChanceService iCustomerBusinessChanceService)
        {
            _iCustomerService = iCustomerService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
            _iCustomerLevelService = iCustomerLevelService;
            _iCustomerTypeService = iCustomerTypeService;
            _iTagService = iTagService;
            _iBusinessStateService = iBusinessStateService;
            _iSysDepartmentService = iSysDepartmentService;
            _iBusinessChanceService = iBusinessChanceService;
            _iCustomerBusinessChanceService = iCustomerBusinessChanceService;
        }

        // 知识库
        // GET: /Platform/Customer/

        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            ViewBag.Tag =
                _iTagService.GetAll(a => a.TagType == "Customer")
                    .GroupBy(a => a.TagName)
                    .Select(a => new {key = a.Key, count = a.Count()})
                    .OrderByDescending(a => a.count)
                    .Take(5)
                    .Select(a => a.key)
                    .ToList();

            IQueryable<Customer> model = _iCustomerService.GetAll().Search(keyword);

            if (!string.IsNullOrEmpty(Request["report"]))
            {
                //导出
                var reportModel = new Report(model.ToReportSource());
                return new ReportResult(reportModel);
            }
            ViewBag.UserId = _iUserInfo.UserId;
            return View(model.ToPagedList(pageIndex));
        }

        public ActionResult Details(Guid id)
        {
            Customer item = _iCustomerService.GetById(id);
            ViewBag.UserId = _iUserInfo.UserId;

            ViewBag.CustomerLevelId = item.CustomerLevel.CustomerLevelName;
            ViewBag.CustomerTypeId = item.CustomerType.CustomerTypeName;


            ViewBag.BusinessStateId = item.BusinessState.BusinessStateName;
            ViewBag.BusinessChancesId = String.Join(" ",
                item.CustomerBusinessChances.Select(a => a.BusinessChance.BusinessChanceName));
            ViewBag.LeaderId = item.Leader.DisplayName;


            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysDepartment/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new Customer();
            if (id.HasValue)
            {
                item = _iCustomerService.GetById(id.Value);
            }

            ViewBag.CustomerLevelId = _iCustomerLevelService.SelectList(item.CustomerLevelId);
            ViewBag.CustomerTypeId = _iCustomerTypeService.SelectList(item.CustomerTypeId);


            ViewBag.BusinessStateId = _iBusinessStateService.SelectList(item.BusinessStateId);

            ViewBag.SysDepartment = _iSysDepartmentService.GetAll();

            ViewBag.BusinessChances = _iBusinessChanceService.GetAll(a => !a.Disable);

            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, Customer collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CustomerLevelId = _iCustomerLevelService.SelectList(collection.CustomerLevelId);
                ViewBag.CustomerTypeId = _iCustomerTypeService.SelectList(collection.CustomerTypeId);

                ViewBag.BusinessStateId = _iBusinessStateService.SelectList(collection.BusinessStateId);

                ViewBag.SysDepartment = _iSysDepartmentService.GetAll();

                ViewBag.BusinessChances = _iBusinessChanceService.GetAll(a => !a.Disable);

                return View(collection);
            }

            _iCustomerService.Save(id, collection);

            //业务机会处理
            _iCustomerBusinessChanceService.Delete(a => a.CustomerId == collection.Id);

            if (collection.BusinessChancesId != null)
            {
                foreach (Guid businessChanceId in collection.BusinessChancesId)
                {
                    _iCustomerBusinessChanceService.Save(null,
                        new CustomerBusinessChance {CustomerId = collection.Id, BusinessChanceId = businessChanceId});
                }
            }

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iCustomerService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}