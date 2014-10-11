using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class CustomerCommunicationService : RepositoryBase<CustomerCommunication>, ICustomerCommunicationService
    {
        public CustomerCommunicationService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}