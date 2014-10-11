using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
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

        public ActionResult Index(Guid itemId, int pageIndex=1, int pageSize = 10)
        {
            IQueryable<KnowledgeReply> model = _iKnowledgeReplyService.GetAll(a => a.KnowledgeId == itemId);
            var result = model.Select(a=> new
            {
                a.Id,
                a.UserId,
                a.KnowledgeId,
                a.KnowledgeReplyObjective
            }).ToPagedList(pageIndex, pageSize);
            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult Create(string entity)
        {
            var item = (KnowledgeReply)JsonConvert.DeserializeObject(entity, typeof(KnowledgeReply));
            _iKnowledgeReplyService.Save(null, item);
            _unitOfWork.Commit();
            return Content("True");
        }

        public ActionResult Delete(Guid id)
        {
            _iKnowledgeReplyService.Delete(id);
            _unitOfWork.Commit();
            return Content("True");
        }
    }
}