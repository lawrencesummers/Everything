using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Web.SignalR;

namespace Web.Areas.Platform.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _iMessageService;
        private readonly IUserInfo _iUserInfo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessenger _iMessenger;

        public MessageController(IMessageService iMessageService, IUserInfo iUserInfo, IUnitOfWork unitOfWork, IMessenger iMessenger)
        {
            _iMessageService = iMessageService;
            _iUserInfo = iUserInfo;
            _unitOfWork = unitOfWork;
            _iMessenger = iMessenger;
        }

        // 系统通知消息
        // GET: /Platform/Message/

        public ActionResult Index(string keyword, bool? read, int pageIndex = 1)
        {
            IQueryable<Message> model = _iMessageService.GetAll(a => a.SysUserId == _iUserInfo.UserId);
            if (!string.IsNullOrEmpty(keyword))
            {
                model = model.Where(a => a.MessageTitle.Contains(keyword));
            }

            ViewBag.CountAll = model.Count();
            ViewBag.unread = model.Count(a => !a.Read);
            ViewBag.read = model.Count(a => a.Read);

            if (read.HasValue)
            {
                model = model.Where(a => a.Read == read);
            }
            return View(model.ToPagedList(pageIndex));
        }

        public ActionResult Details(Guid id)
        {
            Message item = _iMessageService.GetById(id);
            item.Read = true;
            _unitOfWork.Commit();
            return View(item);
        }

        //将未读消息全部标记成已读
        [HttpPost]
        public ActionResult Edit(int pageIndex = 1)
        {
            foreach (Message item in _iMessageService.GetAll(a => a.SysUserId == _iUserInfo.UserId && !a.Read))
            {
                item.Read = true;
            }

            _unitOfWork.Commit();

            _iMessenger.SendMessage(_iUserInfo.UserId);

            return RedirectToAction("Index", new {pageIndex});
        }

        public ActionResult Edit(Guid id, bool read, int pageIndex = 1)
        {
            Message item = _iMessageService.GetById(id);
            item.Read = read;
            _unitOfWork.Commit();
            _iMessenger.SendMessage(_iUserInfo.UserId);
            return RedirectToAction("Index", new {pageIndex});
        }

        [HttpPost]
        public JsonResult Details()
        {
            int item = _iMessageService.GetAll(a => a.SysUserId == _iUserInfo.UserId).Count(a => !a.Read);
            return Json(item);
        }

        [HttpDelete]
        public ActionResult Delete(Guid id, bool? read)
        {
            _iMessageService.Delete(id);
            _unitOfWork.Commit();

            return RedirectToAction("Index", new {read});
        }
    }

   
}