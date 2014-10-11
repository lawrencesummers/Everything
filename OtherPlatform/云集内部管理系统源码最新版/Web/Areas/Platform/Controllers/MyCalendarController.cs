using System;
using System.Linq;
using System.Web.Mvc;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Newtonsoft.Json;

namespace Web.Areas.Platform.Controllers
{
    public class MyCalendarController : Controller
    {
        private readonly IPlanService _iPlanService;
        private readonly IProjectTaskService _iProjectTaskService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public MyCalendarController(IProjectTaskService iProjectTaskService, IUserInfo iUserInfo,
            IPlanService iPlanService, IUnitOfWork unitOfWork)
        {
            _iProjectTaskService = iProjectTaskService;
            _iUserInfo = iUserInfo;
            _iPlanService = iPlanService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Platform/MyPlan/

        public ActionResult Index()
        {
            var model =
                _iProjectTaskService.GetAll(a => !a.Deleted && !a.Finish && a.SysUserId == _iUserInfo.UserId)
                    .Select(a => new
                    {
                        id = a.Id,
                        text = "（任务）" + a.ProjectTaskName,
                        start_date = a.BeginTime,
                        end_date = a.EndTime,
                        custom = 1
                    }).Concat(
                        _iPlanService.GetAll(b => !b.Deleted && !b.Finish && b.UserId == _iUserInfo.UserId)
                            .Select(
                                b =>
                                    new
                                    {
                                        id = b.Id,
                                        text = b.PlanTitle,
                                        start_date = b.StartDate,
                                        end_date = b.EndDate,
                                        custom = 0
                                    }));

            var setting = new JsonSerializerSettings {DateFormatString = "MM-dd-yyyy HH:mm"};

            ViewBag.JsonData = JsonConvert.SerializeObject(model, Formatting.None, setting);

            return View();
        }

        public ActionResult Edit(FormCollection item)
        {
            string nativeeditor_status = item[4];

            switch (nativeeditor_status)
            {
                case "inserted":
                    _iPlanService.Save(null,
                        new Plan
                        {
                            PlanTitle = item[2],
                            StartDate = DateTime.Parse(item[0]),
                            EndDate = DateTime.Parse(item[1])
                        });
                    break;
            }

            nativeeditor_status = item[5];

            switch (nativeeditor_status)
            {
                case "deleted":
                    _iPlanService.Delete(Guid.Parse(item[0]));
                    break;

                case "updated":

                    Plan plan = _iPlanService.GetById(Guid.Parse(item[0]));

                    plan.PlanTitle = item[1];
                    plan.StartDate = DateTime.Parse(item[2]);
                    plan.EndDate = DateTime.Parse(item[3]);

                    _iPlanService.Save(Guid.Parse(item[0]), plan);
                    break;
            }

            _unitOfWork.CommitAsync();

            return null;
        }
    }
}