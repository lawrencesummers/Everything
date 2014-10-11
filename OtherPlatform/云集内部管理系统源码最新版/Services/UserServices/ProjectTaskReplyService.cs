using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ProjectTaskReplyService : RepositoryBase<ProjectTaskReply>, IProjectTaskReplyService
    {
        public ProjectTaskReplyService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

    }
}