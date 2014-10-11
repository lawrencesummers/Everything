using System;
using System.Linq;
using System.Web.Mvc;
using BootstrapSupport;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.ISysServices;
using IServices.Infrastructure;
using Models.SysModels;
using Web.Helper;

namespace Web.Areas.Admin.Controllers
{
    public class SysAreaController : Controller
    {
        private readonly ISysAreaService _sysAreaService;
        private readonly IUnitOfWork _unitOfWork;

        public SysAreaController(IUnitOfWork unitOfWork, ISysAreaService sysAreaService)
        {
            _unitOfWork = unitOfWork;
            _sysAreaService = sysAreaService;
        }

        //
        // GET: /Platform/SysArea/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model = _sysAreaService.GetAllEnt().Select(a => new { a.AreaDisplayName, a.AreaName, a.SystemId, a.CreatedDate, a.Remark, a.Id }).Search(keyword);

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
        // GET: /Platform/SysArea/Details/5

        public ActionResult Details(Guid id)
        {
            SysArea item = _sysAreaService.GetById(id);
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysArea/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new SysArea();
            if (id.HasValue)
            {
                item = _sysAreaService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysArea/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysArea collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _sysAreaService.Save(id, collection);

            _unitOfWork.Commit();

            return RedirectToAction("Index");

        }

        //
        // POST: /Platform/SysArea/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _sysAreaService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}