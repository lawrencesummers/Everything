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

        public ActionResult Index(Guid itemId, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<ProjectInfoReply> model = _iProjectInfoReplyService.GetAll(a => a.ProjectInfoId == itemId);
            var result = model.Select(a => new
            {
                a.Id,
                a.UserId,
                a.ProjectInfoId,
                a.ProjectInfoReplyObjective
            }).ToPagedList(pageIndex, pageSize);
            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult Create(string entity)
        {
            var item = (ProjectInfoReply)JsonConvert.DeserializeObject(entity, typeof(ProjectInfoReply));
            _iProjectInfoReplyService.Save(null, item);
            _unitOfWork.Commit();
            return Content("True");
        }

        public ActionResult Delete(Guid id)
        {
            _iProjectInfoReplyService.Delete(id);
            _unitOfWork.Commit();
            return Content("True");
        }
    }
}