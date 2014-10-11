using System;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ProjectTaskService : RepositoryBase<ProjectTask>, IProjectTaskService
    {
        private readonly IGoogleService _iGoogleService;
        private readonly IMessageService _iMessageService;


        public ProjectTaskService(IDatabaseFactory databaseFactory, IUserInfo userInfo, IMessageService iMessageService,
            IGoogleService iGoogleService)
            : base(databaseFactory, userInfo)
        {
            _iMessageService = iMessageService;
            _iGoogleService = iGoogleService;
        }


        /// <summary>
        ///     是否接受任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accept"></param>
        public void Accept(Guid id, bool accept)
        {
            ProjectTask item = GetById(id);

            item.Accept = accept;

            item.AcceptTime = DateTime.Now;

            Finish(id, !item.Accept.Value);
          
        }


        /// <summary>
        ///     改变任务完成状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="finish"></param>
        public void Finish(Guid id, bool finish)
        {
            ProjectTask item = GetById(id);

            item.Finish = finish;

            item.FinishTime = DateTime.Now;
            
        }

        public override void Add(ProjectTask entity)
        {
         

            //if (entity.SysUserId.HasValue)
            //    _iGoogleService.Insert(entity.SysUserId.Value, entity.ProjectTaskName,
            //        entity.ProjectTaskObjective, entity.BeginTime, entity.EndTime);

            base.Add(entity);
        }

        public override void Update(ProjectTask entity)
        {
           
            base.Update(entity);
        }

        public override void Delete(ProjectTask item)
        {
            //if (item.SysUserId.HasValue && !string.IsNullOrEmpty(item.GoogleEventEntryId))
            //    _iGoogleService.Delete(item.SysUserId.Value, item.GoogleEventEntryId);
            base.Delete(item);
        }
    }
}