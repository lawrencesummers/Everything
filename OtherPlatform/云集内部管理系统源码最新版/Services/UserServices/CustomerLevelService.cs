using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class CustomerLevelService : RepositoryBase<CustomerLevel>, ICustomerLevelService
    {
        public CustomerLevelService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<CustomerLevel> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId).ThenBy(a => a.CustomerLevelName);
        }

        public SelectList SelectList(object selectedValue)
        {
            return new SelectList(GetAll(), "Id", "CustomerLevelName", selectedValue);
        }
    }
}