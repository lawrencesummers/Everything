using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class ProjectTaskReplyController : Controller
    {
        private readonly IProjectTaskReplyService _iProjectTaskReplyService;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTaskReplyController(IProjectTaskReplyService iProjectTaskReplyService, IUnitOfWork unitOfWork)
        {
            _iProjectTaskReplyService = iProjectTaskReplyService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(Guid itemId, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<ProjectTaskReply> model = _iProjectTaskReplyService.GetAll(a => a.ProjectTaskId == itemId);
            var result = model.Select(a => new
            {
                a.Id,
                a.UserId,
                a.ProjectTaskId,
                a.ProjectTaskReplyObjective
            }).ToPagedList(pageIndex, pageSize);
            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult Create(string entity)
        {
            var item = (ProjectTaskReply)JsonConvert.DeserializeObject(entity, typeof(ProjectTaskReply));
            _iProjectTaskReplyService.Save(null, item);
            _unitOfWork.Commit();
            return Content("True");
        }

        public ActionResult Delete(Guid id)
        {
            _iProjectTaskReplyService.Delete(id);
            _unitOfWork.Commit();
            return Content("True");
        }
    }
}