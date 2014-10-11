using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ProjectFileController : Controller
    {
        private readonly IProjectFileService _iProjectFileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfo _userInfo;

        public ProjectFileController(IProjectFileService iProjectFileService, IUnitOfWork unitOfWork, IUserInfo userInfo)
        {
            _iProjectFileService = iProjectFileService;
            _unitOfWork = unitOfWork;
            _userInfo = userInfo;
        }

        public ActionResult Index(Guid projectInfoId)
        {
            var model = _iProjectFileService.GetAll(a => a.ProjectInfoId == projectInfoId);
            ViewBag.projectInfoId = projectInfoId;
            ViewBag.UserId = _userInfo.UserId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Guid? id, ProjectFile collection)
        {
            _iProjectFileService.Save(id, collection);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new { collection.ProjectInfoId });
        }


        [HttpDelete]
        public ActionResult Delete(Guid id, Guid projectInfoId)
        {
            _iProjectFileService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new {  projectInfoId });
        }
    }
}