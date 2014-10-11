using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.SysModels;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class PlanReportController : Controller
    {
        private readonly ISysUserService _ISysUserService;
        private readonly IUserInfo _IUserInfo;
        private readonly IPlanService _iPlanService;
        private readonly ISysDepartmentService _iSysDepartmentService;

        public PlanReportController(ISysDepartmentService iSysDepartmentService, IPlanService iPlanService,
            ISysUserService iSysUserService, IUserInfo iUserInfo)
        {
            _iSysDepartmentService = iSysDepartmentService;
            _iPlanService = iPlanService;
            _ISysUserService = iSysUserService;
            _IUserInfo = iUserInfo;
        }

        // 工作总结
        // GET: /Platform/PlanReport/

        public ActionResult Index()
        {
            SysUser sysUser = _ISysUserService.GetById(_IUserInfo.UserId);

            IEnumerable<string> systemIds = sysUser.SysDepartmentSysUsers.Select(a => a.SysDepartment.SystemId);

            IQueryable<SysDepartment> model = _iSysDepartmentService.GetAll();

            ParameterExpression c = Expression.Parameter(typeof(SysDepartment), "c");
            Expression condition = Expression.Constant(false);
            foreach (string s in systemIds)
            {
                string systemId = s.Length > 3 ? s.Substring(0, s.Length - 3) : s;
                Expression con = Expression.Call(
                    Expression.Property(c, typeof(SysDepartment).GetProperty("SystemId")),
                    typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                    Expression.Constant(systemId));
                condition = Expression.Or(con, condition);
            }
            Expression<Func<SysDepartment, bool>> end = Expression.Lambda<Func<SysDepartment, bool>>(condition,
                new[] { c });

            model = model.Where(end);

            return View(model);
        }

        public ActionResult Details(string departmentId, Guid? userId, DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                //默认显示当前周
                DateTime thisdtWeekStart;
                DateTime thisdtWeekeEnd;
                DateTimeExtensions.GetWeek(DateTime.Now.Year, DateTime.Now.WeekOfYear(), out thisdtWeekStart,
                    out thisdtWeekeEnd);
                startDate = thisdtWeekStart;
                endDate = thisdtWeekeEnd;
            }

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            //默认显示本周工作总结 其他显示本月 本年总结
            IQueryable<Plan> model = _iPlanService.GetAll();

            if (!string.IsNullOrEmpty(departmentId))
            {
                model =
                    model.Where(
                        a => a.SysUser.SysDepartmentSysUsers.Any(b => b.SysDepartment.SystemId.StartsWith(departmentId)));
            }

            if (userId.HasValue)
            {
                model = model.Where(a => a.UserId == userId);
            }

            model =
                model.Where(
                    a =>
                        !((a.StartDate >= endDate) || (a.EndDate <= startDate)));

            return View(model);
        }
    }
}