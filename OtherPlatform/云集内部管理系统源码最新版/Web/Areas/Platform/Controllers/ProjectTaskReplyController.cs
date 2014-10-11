using System;
using System.Linq;
using System.Web.Mvc;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ProjectTaskReplyController : Controller
    {
        private readonly IProjectTaskReplyService _iProjectTaskReplyService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTaskReplyController(IProjectTaskReplyService iProjectTaskReplyService, IUnitOfWork unitOfWork,
            IUserInfo iUserInfo)
        {
            _iProjectTaskReplyService = iProjectTaskReplyService;
            _unitOfWork = unitOfWork;
            _iUserInfo = iUserInfo;
        }

        public ActionResult Index(Guid projectTaskId)
        {
            IQueryable<ProjectTaskReply> model = _iProjectTaskReplyService.GetAll(a => a.ProjectTaskId == projectTaskId);
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.projectTaskId = projectTaskId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Guid? id, Guid projectTaskId, ProjectTaskReply collection)
        {
            _iProjectTaskReplyService.Save(id, collection);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new {projectTaskId});
        }


        [HttpDelete]
        public ActionResult Delete(Guid id, Guid projectTaskId)
        {
            _iProjectTaskReplyService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new {projectTaskId});
        }
    }
}