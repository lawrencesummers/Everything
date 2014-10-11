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
    public class ProjectInfoStateController : Controller
    {
        private readonly IProjectInfoStateService _iProjectInfoStateService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectInfoStateController(IProjectInfoStateService iProjectInfoStateService, IUserInfo iUserInfo,
            IUnitOfWork unitOfWork)
        {
            _iProjectInfoStateService = iProjectInfoStateService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
        }

        // 知识库
        // GET: /Platform/ProjectInfoState/

        public ActionResult Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model =
                _iProjectInfoStateService.GetAll()
                    .Select(
                        a =>
                            new
                            {
                                a.ProjectInfoStateName,
                                a.SystemId,
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

        public ActionResult Details(Guid id)
        {
            ProjectInfoState item = _iProjectInfoStateService.GetById(id);
            ViewBag.UserId = _iUserInfo.UserId;
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
            var item = new ProjectInfoState();
            if (id.HasValue)
            {
                item = _iProjectInfoStateService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, ProjectInfoState collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _iProjectInfoStateService.Save(id, collection);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iProjectInfoStateService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}