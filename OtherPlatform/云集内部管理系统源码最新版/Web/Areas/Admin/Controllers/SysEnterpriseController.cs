using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DoddleReport;
using DoddleReport.Web;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using Models.SysModels;
using Web.Helper;

namespace Web.Areas.Admin.Controllers
{
    public class SysEnterpriseController : Controller
    {
        private readonly ISysEnterpriseService _sysEnterpriseService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUserService _sysUserService;
        private readonly ISysControllerSysActionService _sysControllerSysActionService;

        public SysEnterpriseController(ISysEnterpriseService sysEnterpriseService, IUnitOfWork unitOfWork, ISysUserService sysUserService, ISysControllerSysActionService sysControllerSysActionService)
        {
            _sysEnterpriseService = sysEnterpriseService;
            _unitOfWork = unitOfWork;
            _sysUserService = sysUserService;
            _sysControllerSysActionService = sysControllerSysActionService;
        }

        //
        // GET: /Platform/SysEnterprise/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model = _sysEnterpriseService.GetAllEnt().Select(a => new { a.EnterpriseName, a.EnterpriseShortName, User = a.SysUser.Count(b=>!b.Deleted), a.MaxUser, a.Host, a.Validity, a.Enabled, a.CreatedDate, a.Remark, a.Id }).Search(keyword);

            if (!string.IsNullOrEmpty(ordering))
            {
                model = model.OrderBy(ordering, null);
            }

            if (!string.IsNullOrEmpty(Request["report"]))
            {
                var reportModel = new Report(model.ToReportSource());
                return new ReportResult(reportModel);
            }

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysEnterprise/Details/5

        public ActionResult Details(Guid id)
        {
            var item = _sysEnterpriseService.GetById(id);
            return View(item);
        }

        //
        // GET: /Platform/SysEnterprise/Create

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysEnterprise/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new SysEnterprise();
            if (id.HasValue)
            {
                item = _sysEnterpriseService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysEnterprise/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, SysEnterprise collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _sysEnterpriseService.Save(id, collection);

            if (!id.HasValue)
            {
                //添加企业的时候需要给该企业初始化 角色和管理员账户

                //新建角色

                var sysRoleSysControllerSysActions = new List<SysRoleSysControllerSysAction>();

                foreach (var aa in _sysControllerSysActionService.GetAllEnt().Where(a => a.SysController.SysArea.AreaName == "Platform"))
                {
                    sysRoleSysControllerSysActions.Add(new SysRoleSysControllerSysAction
                    {
                        EnterpriseId = collection.Id,
                        SysControllerSysAction = aa
                    });
                }

                var role = new SysRole
                               {
                                   EnterpriseId = collection.Id,
                                   RoleName = "管理员",
                                   SysRoleSysControllerSysActions = sysRoleSysControllerSysActions,

                               };
                //新建管理员
                var user = new SysUser
                               {
                                   EnterpriseId = collection.Id,
                                   UserName = "admin",
                                   Password = "admin",
                                   OldPassword = "admin",
                                   DisplayName = "管理员",
                                   SysRoleSysUsers = new List<SysRoleSysUser>
                                                       {
                                                           new SysRoleSysUser
                                                               {
                                                                   EnterpriseId = collection.Id,
                                                                   SysRole=role,
                                                               }
                                                       }
                               };

                _sysUserService.Add(user);

            }

            _unitOfWork.Commit();

            return RedirectToAction("Index");

        }


        //
        // POST: /Platform/SysEnterprise/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _sysEnterpriseService.Delete(id);

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}
