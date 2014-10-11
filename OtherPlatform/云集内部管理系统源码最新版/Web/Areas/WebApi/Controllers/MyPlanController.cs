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
    public class MyPlanController :  AndroidBaseController<Plan>
    {
        //
        // GET: /WebApi/MyPlan/
        private readonly IUserInfo _iUserInfo;
        private readonly IPlanService _iPlanService;
        private readonly IUnitOfWork _unitOfWork;

        public MyPlanController(IUserInfo iUserInfo, IPlanService iPlanService, IUnitOfWork unitOfWork)
        {
            _iUserInfo = iUserInfo;
            _iPlanService = iPlanService;
            _unitOfWork = unitOfWork;
        }

        //根据起止时间、类型查询计划
        public ActionResult Index(DateTime? startTime, DateTime? endTime, string type, int pageIndex=1, int pageSize = 10)
        {
            var model = _iPlanService.GetAll().Where(a => a.UserId == _iUserInfo.UserId && a.EnterpriseId == _iUserInfo.EnterpriseId);
            if (string.IsNullOrEmpty(type) || !startTime.HasValue || !endTime.HasValue)
            {
                //不指定类型或没有起止时间，则获取所有未完成的计划
                model = model.Where(a => !a.Finish);
            }
            else
            {
                model = model.Where(a => a.PlanType == type && !((a.EndDate <= startTime) || (a.StartDate >= endTime)));
            }

            return GetModelDetail(model, pageIndex, pageSize);
        }

        //根据项目id查特定项目的所有计划
        public ActionResult InProject(Guid itemId, int pageIndex =1, int pageSize = 10)
        {
            var model = _iPlanService.GetAll().Where(a => !a.Deleted && a.ProjectInfoId == itemId);
            return GetModelDetail(model, pageIndex, pageSize); 
        }

        private ActionResult GetModelDetail(IQueryable<Plan> model, int pageIndex = 1, int pageSize = 10)
        {
            //排序
            model = model.OrderBy(a => a.Finish).ThenByDescending(a => a.CreatedDate);
            var result = model.Select(
            a =>
                new
                {
                    a.Id,
                    a.PlanType,
                    a.Raty,
                    a.UserId,
                    a.PlanTitle,
                    a.CreatedDate,
                    a.StartDate,
                    a.EndDate,
                    a.RemindTime,
                    a.Finish,
                    a.FinishTime,
                    a.Milestone,
                    a.SysUser.DisplayName,
                    a.ProjectInfoId,
                    a.ProjectInfo.ProjectName,
                    a.Remark
                }).ToPagedList(pageIndex, pageSize);

            return Content(JsonConvert.SerializeObject(result), "text/json");
        }

        public ActionResult Create(string entity)
        {
            var item = DeserializeObject(entity);
            _iPlanService.Save(null, item);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Edit(string entity)
        {
            var item = DeserializeObject(entity);
            _iPlanService.Save(item.Id, item);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        public ActionResult Delete(Guid id)
        {
            _iPlanService.Delete(id);
            _unitOfWork.Commit();
            return Content("True");
        }

        public ActionResult Finish(Guid id)
        {
            _iPlanService.Finish(id);
            _unitOfWork.Commit();
            return Content("True");
        }

    }
}
