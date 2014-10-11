using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class KnowledgeReplyService : RepositoryBase<KnowledgeReply>, IKnowledgeReplyService
    {
        public KnowledgeReplyService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}