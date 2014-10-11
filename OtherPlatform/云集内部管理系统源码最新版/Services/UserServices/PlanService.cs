using System;
using System.Linq;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class PlanService : RepositoryBase<Plan>, IPlanService
    {
        private readonly IGoogleService _iGoogleService;

        public PlanService(IDatabaseFactory databaseFactory, IUserInfo userInfo, IGoogleService iGoogleService)
            : base(databaseFactory, userInfo)
        {
            _iGoogleService = iGoogleService;
        }

        public override IQueryable<Plan> GetAll()
        {
            return base.GetAll().OrderBy(a => a.Finish).ThenByDescending(a => a.Raty).ThenBy(a => a.EndDate);
        }

        public void Finish(Guid id)
        {
            Plan item = GetById(id);
            item.Finish = !item.Finish;
            item.FinishTime = DateTime.Now;
        }

        public override void Add(Plan entity)
        {
            //if (entity.UserId.HasValue)
            //    _iGoogleService.Insert(entity.UserId.Value, entity.PlanTitle, entity.Remark,
            //         entity.StartDate, entity.EndDate);
            base.Add(entity);
        }

        public override void Delete(Plan item)
        {
            //if (item.UserId.HasValue && !string.IsNullOrEmpty(item.GoogleEventEntryId))
            //    _iGoogleService.Delete(item.UserId.Value, item.GoogleEventEntryId);
            base.Delete(item);
        }
    }
}