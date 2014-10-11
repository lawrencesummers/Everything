using System;
using System.Collections.Generic;
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
    public class SysRoleController : Controller
    {
        private readonly ISysControllerService _sysControllerService;
        private readonly ISysRoleService _sysRoleService;
        private readonly ISysRoleSysControllerSysActionService _sysRoleSysControllerSysActionService;
        private readonly IUnitOfWork _unitOfWork;

        public SysRoleController(ISysControllerService sysControllerService, ISysRoleService sysRoleService,
            ISysRoleSysControllerSysActionService sysRoleSysControllerSysActionService, IUnitOfWork unitOfWork)
        {
            _sysControllerService = sysControllerService;
            _sysRoleService = sysRoleService;
            _sysRoleSysControllerSysActionService = sysRoleSysControllerSysActionService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Platform/SysRole/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model =
                _sysRoleService.GetAll()
                    .Select(
                        a =>
                            new
                            {
                                a.RoleName,
                                a.SystemId,
                                Population = a.SysRoleSysUsers.Count(b => !b.Deleted && !b.SysUser.Deleted),
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
        // GET: /Platform/SysRole/Details/5

        public ActionResult Details(Guid id)
        {
            SysRole item = _sysRoleService.GetById(id);
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysRole/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var area = (string) Request.RequestContext.RouteData.DataTokens["area"];
            var item = new SysRole();
            if (id.HasValue)
            {
                item = _sysRoleService.GetById(id.Value);
            }
            ViewBag.SysControllers = _sysControllerService.GetAllEnt().Where(a => a.SysArea.AreaName.Equals(area));
            return View(item);
        }

        //
        // POST: /Platform/SysRole/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysRole collection, IEnumerable<Guid> sysControllerSysActionsId)
        {
            var area = (string) Request.RequestContext.RouteData.DataTokens["area"];

            if (!ModelState.IsValid)
            {
                Edit(id);
                return View(collection);
            }

            if (id.HasValue)
            {
                //清除原有数据
                _sysRoleSysControllerSysActionService.Delete(
                    a =>
                        a.SysRoleId.Equals(id.Value) &&
                        a.SysControllerSysAction.SysController.SysArea.AreaName.Equals(area));
            }

            _sysRoleService.Save(id, collection);

            if (sysControllerSysActionsId != null)
            {
                foreach (Guid sysControllerSysActionId in sysControllerSysActionsId)
                {
                    _sysRoleSysControllerSysActionService.Save(null, new SysRoleSysControllerSysAction
                    {
                        SysRoleId = collection.Id,
                        SysControllerSysActionId = sysControllerSysActionId
                    });
                }
            }

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysRole/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _sysRoleService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}