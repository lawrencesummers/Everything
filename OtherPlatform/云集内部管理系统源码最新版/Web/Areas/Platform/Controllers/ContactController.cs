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
    public class ContactController : Controller
    {
        private readonly IContactService _iContactService;
        private readonly ICustomerService _iCustomerService;
        private readonly ITagService _iTagService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;

        public ContactController(IContactService iContactService, IUnitOfWork unitOfWork, IUserInfo iUserInfo,
            ITagService iTagService, ICustomerService iCustomerService)
        {
            _iContactService = iContactService;
            _unitOfWork = unitOfWork;
            _iUserInfo = iUserInfo;
            _iTagService = iTagService;
            _iCustomerService = iCustomerService;
        }

        public ActionResult Index(string keyword, Guid? CustomerId, int pageIndex = 1)
        {
            ViewBag.Tag =
                _iTagService.GetAll(a => a.TagType == "Contect")
                    .GroupBy(a => a.TagName)
                    .Select(a => new {key = a.Key, count = a.Count()})
                    .OrderByDescending(a => a.count)
                    .Take(5)
                    .Select(a => a.key)
                    .ToList();

            IQueryable<Contact> model = _iContactService.GetAll().Search(keyword);

            if (CustomerId.HasValue)
                model = model.Where(a => a.CustomerId == CustomerId);

            ViewBag.UserId = _iUserInfo.UserId;

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysDepartment/Details/5

        public ActionResult Details(Guid id)
        {
            Contact item = _iContactService.GetById(id);
            ViewBag.CustomerId = item.Customer.CustomerName;
            return View(item);
        }

        public ActionResult Create(Guid? CustomerId)
        {
            return RedirectToAction("Edit", new {CustomerId});
        }

        //
        // GET: /Platform/SysDepartment/Edit/5

        public ActionResult Edit(Guid? id, Guid? CustomerId)
        {
            var item = new Contact();
            if (id.HasValue)
            {
                item = _iContactService.GetById(id.Value);
                CustomerId = item.CustomerId;
            }
            ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll().Select(a => new {a.Id, a.CustomerName}), "Id",
                "CustomerName", CustomerId);
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, Contact collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll().Select(a => new {a.Id, a.CustomerName}),
                    "Id", "CustomerName", collection.CustomerId);
                return View(collection);
            }

            _iContactService.Save(id, collection);

            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iContactService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}