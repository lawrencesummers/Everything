using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class ActivityUserController : Controller
    {
        private readonly IActivityUserService _iActivityUserService;
        private readonly IUnitOfWork _unitOfWork;
        private IUserInfo _IUserInfo;


        public ActivityUserController(IActivityUserService iActivityUserService,
            IUnitOfWork unitOfWork, IUserInfo iUserInfo)
        {
            _iActivityUserService = iActivityUserService;
            _unitOfWork = unitOfWork;
            _IUserInfo = iUserInfo;
        }



        public ActionResult Create(Guid id)
        {
            var item = new ActivityUser {ActivityId = id};

            _iActivityUserService.Save(null, item);
            _unitOfWork.Commit();
            return RedirectToAction("Index", "Activity");
        }


        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iActivityUserService.Delete(a => a.ActivityId == id && a.UserId == _IUserInfo.UserId);
            _unitOfWork.Commit();
            return RedirectToAction("Index", "Activity");
        }
    }
}