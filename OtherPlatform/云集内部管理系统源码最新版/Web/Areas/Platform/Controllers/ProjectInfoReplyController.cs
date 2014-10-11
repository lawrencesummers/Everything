using System;
using System.Linq;
using System.Web.Mvc;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ProjectInfoReplyController : Controller
    {
        private readonly IProjectInfoReplyService _iProjectInfoReplyService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectInfoReplyController(IProjectInfoReplyService iProjectInfoReplyService, IUnitOfWork unitOfWork,
            IUserInfo iUserInfo)
        {
            _iProjectInfoReplyService = iProjectInfoReplyService;
            _unitOfWork = unitOfWork;
            _iUserInfo = iUserInfo;
        }

        public ActionResult Index(Guid projectInfoId)
        {
            IQueryable<ProjectInfoReply> model = _iProjectInfoReplyService.GetAll(a => a.ProjectInfoId == projectInfoId);
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.projectInfoId = projectInfoId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Guid projectInfoId, ProjectInfoReply item)
        {
            _iProjectInfoReplyService.Save(null, item);
            _unitOfWork.Commit();

            return RedirectToAction("Index", new {projectInfoId});
        }

        [HttpDelete]
        public ActionResult Delete(Guid id, Guid projectInfoId)
        {
            _iProjectInfoReplyService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new {projectInfoId});
        }
    }
}