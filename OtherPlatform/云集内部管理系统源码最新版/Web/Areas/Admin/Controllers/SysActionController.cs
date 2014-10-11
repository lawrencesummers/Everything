using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.ISysServices;
using IServices.Infrastructure;
using Models.SysModels;
using Web.Helper;

namespace Web.Areas.Admin.Controllers
{
    public class SysActionController : Controller
    {
        private readonly ISysActionService _sysActionService;
        private readonly IUnitOfWork _unitOfWork;

        public SysActionController(ISysActionService sysActionService, IUnitOfWork unitOfWork)
        {
            _sysActionService = sysActionService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Platform/SysAction/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model =
                _sysActionService.GetAllEnt()
                                 .Select(
                                     a =>
                                     new { a.ActionDisplayName, a.ActionName, a.SystemId, a.CreatedDate, a.Remark, a.Id }).Search(keyword);

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
        // GET: /Platform/SysAction/Details/5

        public ActionResult Details(Guid id)
        {
            SysAction item = _sysActionService.GetById(id);
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysAction/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new SysAction();
            if (id.HasValue)
            {
                item = _sysActionService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysAction/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysAction collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _sysActionService.Save(id, collection);

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysAction/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _sysActionService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}