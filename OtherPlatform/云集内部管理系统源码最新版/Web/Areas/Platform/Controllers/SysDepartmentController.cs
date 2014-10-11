using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.Infrastructure;
using IServices.ISysServices;
using Models.SysModels;

namespace Web.Areas.Platform.Controllers
{
    public class SysDepartmentController : Controller
    {
        private readonly ISysDepartmentService _sysDepartmentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfo _userInfo;

        public SysDepartmentController(ISysDepartmentService sysDepartmentService, IUnitOfWork unitOfWork,
            IUserInfo userInfo)
        {
            _sysDepartmentService = sysDepartmentService;
            _unitOfWork = unitOfWork;
            _userInfo = userInfo;
        }

        //
        // GET: /Platform/SysDepartment/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model =
                _sysDepartmentService.GetAll()
                    .Select(
                        a =>
                            new
                            {
                                a.DepartmentName,
                                a.DepartmentCode,
                                a.SystemId,
                                Population = a.SysDepartmentSysUsers.Count(),
                                a.Ico,
                                a.Enabled,
                                a.CreatedDate,
                                a.Remark,
                                a.Id
                            }).Search(keyword);


            if (!string.IsNullOrEmpty(ordering))
            {
                model = model.OrderBy(ordering, null);
            }

            if (!string.IsNullOrEmpty(Request["report"]))
            {
                //导出
                var reportModel = new Report(model.ToReportSource());
                return new ReportResult(reportModel);
            }

            return View(model.ToPagedList(pageIndex));
        }


        //
        // GET: /Platform/SysDepartment/Details/5

        public ActionResult Details(Guid id)
        {
            SysDepartment item = _sysDepartmentService.GetById(id);
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
            var item = new SysDepartment();
            if (id.HasValue)
            {
                item = _sysDepartmentService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysDepartment collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _sysDepartmentService.Save(id, collection);

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _sysDepartmentService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}