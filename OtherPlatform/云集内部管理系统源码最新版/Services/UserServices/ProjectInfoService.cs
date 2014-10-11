using System;
using System.Linq;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ProjectInfoService : RepositoryBase<ProjectInfo>, IProjectInfoService
    {
        private readonly IUserInfo _IUserInfo;
        private readonly IMessageService _iMessageService;

        public ProjectInfoService(IDatabaseFactory databaseFactory, IUserInfo userInfo, IMessageService iMessageService)
            : base(databaseFactory, userInfo)
        {
            _IUserInfo = userInfo;
            _iMessageService = iMessageService;
        }

        public void Finish(Guid id)
        {
            ProjectInfo item = GetById(id);
            item.Finish = !item.Finish;
        }

        public override IQueryable<ProjectInfo> GetAll()
        {
            return base.GetAll().OrderBy(a => a.Finish).ThenByDescending(a => a.Raty).ThenBy(a => a.EndTime);
        }

        public override void Add(ProjectInfo entity)
        {
            ////发送通知
            //if (entity.ProjectUsersId != null)
            //    foreach (Guid userId in entity.ProjectUsersId)
            //        _iMessageService.Add(new Message
            //        {
            //            SysUserId = userId,
            //            MessageTitle = "创建新项目:" + entity.ProjectName,
            //            MessageContent = entity.ProjectObjective,
            //            EnterpriseId = _IUserInfo.EnterpriseId
            //        });
            base.Add(entity);
        }

        public override void Update(ProjectInfo entity)
        {
            //    //发送通知
            //    if (entity.ProjectUsersId != null)
            //        foreach (var userId in entity.ProjectUsersId)
            //            _iMessageService.Add(new Message { SysUserId = userId, MessageTitle = "更新项目信息:" + entity.ProjectName, MessageContent = entity.ProjectObjective, EnterpriseId = _IUserInfo.EnterpriseId });
            base.Update(entity);
        }
    }
}