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
    public class KnowledgeController : Controller
    {
        private readonly IKnowledgeService _iKnowledgeService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public KnowledgeController(IKnowledgeService iKnowledgeService, IUserInfo iUserInfo, IUnitOfWork unitOfWork)
        {
            _iKnowledgeService = iKnowledgeService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
        }

        // 知识库
        // GET: /Platform/Knowledge/

        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            IQueryable<Knowledge> model = _iKnowledgeService.GetAll(a => a.UserId == _iUserInfo.UserId || a.Public);
            if (!string.IsNullOrEmpty(keyword))
                model = model.Where(a => a.KnowledgeTitle.Contains(keyword) || a.KnowledgeContent.Contains(keyword));

            ViewBag.UserId = _iUserInfo.UserId;
            return View(model.ToPagedList(pageIndex));
        }

        public ActionResult Details(Guid id)
        {
            Knowledge item = _iKnowledgeService.GetById(id);
            ViewBag.UserId = _iUserInfo.UserId;
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
            var item = new Knowledge();
            if (id.HasValue)
            {
                item = _iKnowledgeService.GetById(id.Value);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, Knowledge collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _iKnowledgeService.Save(id, collection);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iKnowledgeService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}