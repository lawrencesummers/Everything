using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using Models.SysModels;
using Newtonsoft.Json;

namespace Web.Areas.WebApi.Controllers
{
    public class ChatController : Controller
    {
        private readonly ISysDepartmentService _iSysDepartmentService;
        private readonly ISysSignalRService _iSysSignalRService;
        private readonly IUserInfo _iUserInfo;

        public ChatController(IUserInfo iUserInfo, ISysSignalRService iSysSignalRService,
            ISysDepartmentService iSysDepartmentService)
        {
            _iUserInfo = iUserInfo;
            _iSysSignalRService = iSysSignalRService;
            _iSysDepartmentService = iSysDepartmentService;
        }

        public ActionResult Index(Guid anotherUserId)
        {
            var userId = _iUserInfo.UserId;
            var model = _iSysSignalRService.GetAll("chat");

            //筛选当前用户
            var result = model.Where(b=>(b.UserId==userId&&b.UserId1==anotherUserId)||(b.UserId1==userId&&b.UserId==anotherUserId)).Select(a => new
            {
                a.Id,
                a.CreatedDate,
                a.EnterpriseId,
                a.UserId,
                a.UserName,
                a.UserId1,
                a.UserName1,
                a.Message,
                a.GroupId,
                a.GroupName
            });

            return Content(JsonConvert.SerializeObject(result), "text/json");
        }

        public ActionResult Details(string id, string keyword, int pageIndex = 1)
        {
            var model = _iSysSignalRService.GetAll("chat");

            //筛选当前用户


            if (!string.IsNullOrEmpty(keyword))
                model =
                    model.Where(
                        a =>
                            a.UserName.Contains(keyword) || a.UserName1.Contains(keyword) || a.Message.Contains(keyword));


            return View(model.ToPagedList(pageIndex, 10));
        }
    }
}