using System;
using System.Linq;
using System.Web.Mvc;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class KnowledgeReplyController : Controller
    {
        private readonly IKnowledgeReplyService _iKnowledgeReplyService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public KnowledgeReplyController(IKnowledgeReplyService iKnowledgeReplyService, IUnitOfWork unitOfWork,
            IUserInfo iUserInfo)
        {
            _iKnowledgeReplyService = iKnowledgeReplyService;
            _unitOfWork = unitOfWork;
            _iUserInfo = iUserInfo;
        }

        public ActionResult Index(Guid KnowledgeId)
        {
            IQueryable<KnowledgeReply> model = _iKnowledgeReplyService.GetAll(a => a.KnowledgeId == KnowledgeId);
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.KnowledgeId = KnowledgeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Guid KnowledgeId, KnowledgeReply item)
        {
            _iKnowledgeReplyService.Save(null, item);
            _unitOfWork.Commit();

            return RedirectToAction("Index", new {KnowledgeId});
        }

        [HttpDelete]
        public ActionResult Delete(Guid id, Guid KnowledgeId)
        {
            _iKnowledgeReplyService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new {KnowledgeId});
        }
    }
}