using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.IUserServices;
using Models.UserModels;

namespace Web.Areas.Platform.Controllers
{
    public class CustomerCommunicationController : Controller
    {
        private readonly ICustomerCommunicationService _iCustomerCommunicationService;
        private readonly ICustomerService _iCustomerService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerCommunicationController(ICustomerCommunicationService iCustomerCommunicationService,
            IUnitOfWork unitOfWork, ICustomerService iCustomerService)
        {
            _iCustomerCommunicationService = iCustomerCommunicationService;
            _unitOfWork = unitOfWork;
            _iCustomerService = iCustomerService;
        }

        //
        // GET: /Platform/BusinessCommunication/

        public ActionResult Index(string keyword, int pageIndex = 1)
        {
            IQueryable<CustomerCommunication> model = _iCustomerCommunicationService.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                model =
                    model.Where(
                        a => a.CommunicationContent.Contains(keyword) || a.Customer.CustomerName.Contains(keyword));
            return View(model.ToPagedList(pageIndex));
        }


        public ActionResult Details(Guid id)
        {
            CustomerCommunication item = _iCustomerCommunicationService.GetById(id);
            ViewBag.CustomerId = item.Customer.CustomerName;
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
            var item = new CustomerCommunication();
            if (id.HasValue)
            {
                item = _iCustomerCommunicationService.GetById(id.Value);
            }
            ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll(), "Id", "CustomerName", item.CustomerId);
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid? id, CustomerCommunication collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CustomerId = new SelectList(_iCustomerService.GetAll(), "Id", "CustomerName",
                    collection.CustomerId);
                return View(collection);
            }

            _iCustomerCommunicationService.Save(id, collection);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _iCustomerCommunicationService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}