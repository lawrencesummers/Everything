using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class CustomerBusinessChanceService : RepositoryBase<CustomerBusinessChance>, ICustomerBusinessChanceService
    {
        public CustomerBusinessChanceService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override void Delete(CustomerBusinessChance item)
        {
            base.Remove(item);
        }
    }
}