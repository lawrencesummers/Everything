using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using IServices.Infrastructure;
using Models.SysModels;
using Models.UserModels;
using Newtonsoft.Json;
using Web.Helper;

namespace Web.Areas.WebApi.Controllers
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

        public ActionResult Index(string keywords, int pageIndex=1, int pageSize = 10)
        {
            var model = _iKnowledgeService.GetAll(a => a.UserId==_iUserInfo.UserId || a.Public);
            if (!string.IsNullOrEmpty(keywords))
                model = model.Where(a => a.KnowledgeTitle.Contains(keywords) || a.KnowledgeContent.Contains(keywords));

            var result = model.Select(a => new 
            {
                a.Id,
                a.KnowledgeTitle,
                a.Public,
                a.CreatedDate,
                a.SysUser.DisplayName
            }).ToPagedList(pageIndex, pageSize);

            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Details(Guid id)
        {
            var item = _iKnowledgeService.GetById(id);
            //过滤内容中的背景、字体大小、行高样式以及控制符
            const string pattern = "background-color|font-size|line-height|\r|\n|\t";
            var result = new
            {
                item.Id,
                item.UserId,
                item.KnowledgeTitle,
                item.Public,
                item.CreatedDate,
                item.SysUser.DisplayName,
                KnowledgeContent = Regex.Replace(item.KnowledgeContent, pattern, string.Empty, RegexOptions.IgnoreCase),
                item.Remark
            };
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Create(string entity)
        {
            var item = (Knowledge)JsonConvert.DeserializeObject(entity, typeof(Knowledge));
            _iKnowledgeService.Save(null, item);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        //
        // GET: /Platform/SysDepartment/Edit/5

        public ActionResult Edit(string entity)
        {
            var item = (Knowledge)JsonConvert.DeserializeObject(entity, typeof(Knowledge));
            _iKnowledgeService.Save(item.Id, item);
            _unitOfWork.Commit();
            return Content("True"); 
        }

        //
        // POST: /Platform/SysDepartment/Delete/5
        public ActionResult Delete(Guid id)
        {
            _iKnowledgeService.Delete(id);
            _unitOfWork.Commit();
            return Content("True"); 
        }

    }
}
