using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class FlowController : Controller
    {
        private readonly IFlowService _iFlowService;
        private readonly IUnitOfWork _unitOfWork;

        public FlowController(IFlowService iFlowService, IUnitOfWork unitOfWork)
        {
            _iFlowService = iFlowService;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Platform/Flow/

        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            IQueryable<Flow> model = _iFlowService.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                model = model.Where(a => a.FlowName.Contains(keyword));
            return View(model.ToPagedList(pageIndex));
        }


        public ActionResult Details(Guid id)
        {
            Flow item = _iFlowService.GetById(id);
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
            var item = new Flow();
            if (id.HasValue)
            {
                item = _iFlowService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, Flow collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _iFlowService.Save(id, collection);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iFlowService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}