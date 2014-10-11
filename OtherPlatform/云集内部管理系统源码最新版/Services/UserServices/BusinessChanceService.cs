using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class BusinessChanceService : RepositoryBase<BusinessChance>, IBusinessChanceService
    {
        public BusinessChanceService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<BusinessChance> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId).ThenBy(a => a.BusinessChanceName);
        }

        public SelectList SelectList(object selectedValue)
        {
            return new SelectList(GetAll(), "Id", "BusinessChanceName", selectedValue);
        }
    }
}