using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class CustomerTypeService : RepositoryBase<CustomerType>, ICustomerTypeService
    {
        public CustomerTypeService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<CustomerType> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId).ThenBy(a => a.CustomerTypeName);
        }

        public SelectList SelectList(object selectedValue)
        {
            return new SelectList(GetAll(), "Id", "CustomerTypeName", selectedValue);
        }
    }
}