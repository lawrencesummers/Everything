using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ProjectInfoReplyService : RepositoryBase<ProjectInfoReply>, IProjectInfoReplyService
    {
        public ProjectInfoReplyService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }


    }
}