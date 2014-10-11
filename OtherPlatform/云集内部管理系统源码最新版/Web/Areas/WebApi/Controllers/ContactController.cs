using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using DoddleReport;
using DoddleReport.Web;
using IServices.ISysServices;
using IServices.IUserServices;
using IServices.Infrastructure;
using Models.SysModels;
using Models.UserModels;
using Newtonsoft.Json;
using Web.Helper;


namespace Web.Areas.WebApi.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _iContactService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfo _iUserInfo;
        private readonly ITagService _iTagService;
        private readonly ICustomerService _iCustomerService;

        public ContactController(IContactService iContactService, IUnitOfWork unitOfWork, IUserInfo iUserInfo, ITagService iTagService, ICustomerService iCustomerService)
        {
            _iContactService = iContactService;
            _unitOfWork = unitOfWork;
            _iUserInfo = iUserInfo;
            _iTagService = iTagService;
            _iCustomerService = iCustomerService;
        }

        public ActionResult Index(string keywords, Guid? customerId, int pageIndex = 1, int pageSize = 10)
        {

            var model = _iContactService.GetAll();

            if (customerId.HasValue)
                model = model.Where(a => a.CustomerId == customerId);

            if (!string.IsNullOrEmpty(keywords))
                model = model.Where(a => a.ContactName.Contains(keywords) || a.Pinyin.Contains(keywords) || a.Tag.Contains(keywords) || a.Customer.CustomerName.Contains(keywords) || a.Email.Contains(keywords) || a.Telephony.Contains(keywords) || a.Extension.Contains(keywords));

            var result = model.Select(a => new
            {
                a.Id,
                a.UserId,
                a.Customer.CustomerName,
                a.ContactName,
                a.Tag,
                a.Position,
                a.Telephony,
                a.MobilePhone,
                a.Email,
                a.Remark,
                a.Extension,
                a.CreatedDate
            }).ToPagedList(pageIndex, pageSize);
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Create(Guid? customerId)
        {
            return RedirectToAction("Edit", new { customerId });
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
            ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll().Select(a => new { a.Id, a.CustomerName }), "Id", "CustomerName", CustomerId);
            return Content("True");
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, Contact collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll().Select(a => new { a.Id, a.CustomerName }), "Id", "CustomerName", collection.CustomerId);
                return View(collection);
            }

            _iContactService.Save(id, collection);

            _unitOfWork.Commit();

            return Content("True");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        public ActionResult Delete(Guid id)
        {
            _iContactService.Delete(id);
            _unitOfWork.Commit();
            return Content("True");
        }
    }
}
