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
    public class SysMailController : Controller
    {
        private readonly ISysMailService _SysMailService;
        private readonly IUnitOfWork _unitOfWork;

        public SysMailController(ISysMailService SysMailService, IUnitOfWork unitOfWork)
        {
            _SysMailService = SysMailService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Platform/SysMail/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model =
                _SysMailService.GetAllEnt()
                                 .Select(
                                     a =>
                                     new { a.To, a.Subject, a.Sent, a.CreatedDate, a.Remark, a.Id }).Search(keyword);


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
        // GET: /Platform/SysMail/Details/5

        public ActionResult Details(Guid id)
        {
            SysMail item = _SysMailService.GetById(id);
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysMail/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new SysMail();
            if (id.HasValue)
            {
                item = _SysMailService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysMail/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysMail collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _SysMailService.Save(id, collection);

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysMail/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _SysMailService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}