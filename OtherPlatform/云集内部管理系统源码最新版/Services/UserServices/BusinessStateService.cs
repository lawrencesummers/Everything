using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class BusinessStateService : RepositoryBase<BusinessState>, IBusinessStateService
    {
        public BusinessStateService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<BusinessState> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId).ThenBy(a => a.BusinessStateName);
        }

        public SelectList SelectList(object selectedValue)
        {
            return new SelectList(GetAll(), "Id", "BusinessStateName", selectedValue);
        }
    }
}