using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class FlowService : RepositoryBase<Flow>, IFlowService
    {
        public FlowService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}