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
    public class ActivityController : Controller
    {
        private readonly IActivityService _iActivityService;
        private readonly IUnitOfWork _unitOfWork;
        private IUserInfo _IUserInfo;


        public ActivityController(IActivityService iActivityService,
            IUnitOfWork unitOfWork, IUserInfo iUserInfo)
        {
            _iActivityService = iActivityService;
            _unitOfWork = unitOfWork;
            _IUserInfo = iUserInfo;
        }

        //
        // GET: /Platform/BusinessCommunication/

        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            var model = _iActivityService.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                model =
                    model.Where(
                        a => a.ActivityTitle.Contains(keyword));

            ViewBag.UserId = _IUserInfo.UserId;
            return View(model.ToPagedList(pageIndex));
        }


        public ActionResult Details(Guid id)
        {
            var item = _iActivityService.GetById(id);
         
            return View(item);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysDepartment/Edit/5

        public ActionResult Edit(Guid? id)
        {
            var item = new Activity();
            if (id.HasValue)
            {
                item = _iActivityService.GetById(id.Value);
            }
       
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, Activity collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _iActivityService.Save(id, collection);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iActivityService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}