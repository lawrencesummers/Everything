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
    public class SysControllerController : Controller
    {
        private readonly ISysActionService _sysActionService;
        private readonly ISysAreaService _sysAreaService;
        private readonly ISysControllerService _sysControllerService;
        private readonly ISysControllerSysActionService _sysControllerSysActionService;
        private readonly IUnitOfWork _unitOfWork;

        public SysControllerController(ISysActionService sysActionService, ISysAreaService sysAreaService,
                                       IUnitOfWork unitOfWork, ISysControllerService sysControllerService,
                                       ISysControllerSysActionService sysControllerSysActionService)
        {
            _sysActionService = sysActionService;
            _sysAreaService = sysAreaService;
            _unitOfWork = unitOfWork;
            _sysControllerService = sysControllerService;
            _sysControllerSysActionService = sysControllerSysActionService;
        }

        //
        // GET: /Platform/SysController/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model =
                _sysControllerService.GetAllEnt()
                                     .Select(
                                         a =>
                                         new
                                             {
                                                 SysArea = a.SysArea.AreaDisplayName,
                                                 a.ControllerDisplayName,
                                                 a.ControllerName,
                                                 a.ActionName,
                                                 a.Parameter,
                                                 a.SystemId,
                                                 a.Display,
                                                 a.Ico,
                                                 a.Enabled,
                                                 a.TargetBlank,
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
        // GET: /Platform/SysController/Details/5

        public ActionResult Details(Guid id)
        {
            SysController item = _sysControllerService.GetById(id);
            ViewBag.SysAreaId = item.SysArea.AreaDisplayName;
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysController/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new SysController();
            if (id.HasValue)
            {
                item = _sysControllerService.GetById(id.Value);
            }
            ViewBag.SysAreaId = new SelectList(_sysAreaService.GetAllEnt(), "Id", "AreaDisplayName", item.SysAreaId);
            ViewBag.SysActionsId = new MultiSelectList(_sysActionService.GetAllEnt(), "Id", "ActionDisplayName",
                                                       item.SysControllerSysActions != null
                                                           ? item.SysControllerSysActions.Where(a => !a.Deleted).Select(a => a.SysActionId) : null);
            return View(item);
        }

        //
        // POST: /Platform/SysController/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysController collection)
        {
            if (!ModelState.IsValid)
            {
                Edit(id);
                return View(collection);
            }

            if (id.HasValue)
            {
                //清除原有数据
                _sysControllerSysActionService.Delete(a => a.SysControllerId.Equals(id.Value) && !collection.SysActionsId.Contains(a.SysActionId));
            }

            _sysControllerService.Save(id, collection);

            if (collection.SysActionsId != null)
            {
                foreach (
                    var actionid in
                        collection.SysActionsId.Where(
                            actionid =>
                            !_sysControllerSysActionService.GetAll()
                                                           .Where(b => b.SysControllerId.Equals(id.Value))
                                                           .Select(b => b.SysActionId)
                                                           .Contains(actionid)))
                {
                    _sysControllerSysActionService.Save(null, new SysControllerSysAction
                                                           {
                                                               SysControllerId = collection.Id,
                                                               SysActionId = actionid
                                                           });
                }
            }

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysController/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _sysControllerService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}