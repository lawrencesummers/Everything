using System.Linq;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.SysModels;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class MessageService : RepositoryBase<Message>, IMessageService
    {
        private readonly ISysUserService _iSysUserService;

        public MessageService(IDatabaseFactory databaseFactory, IUserInfo userInfo, ISysUserService iSysUserService)
            : base(databaseFactory, userInfo)
        {
            _iSysUserService = iSysUserService;
        }

        public override IQueryable<Message> GetAll()
        {
            return base.GetAll().OrderBy(a => a.Read).ThenByDescending(a => a.CreatedDate);
        }

        public override void Add(Message entity)
        {
            //发邮件
            if (!entity.SysUserId.HasValue) return;
            var item = _iSysUserService.GetById(entity.SysUserId.Value);
            if (!string.IsNullOrEmpty(item.Email))
                Email.SendEmail(item.Email, entity.MessageTitle.RemoveHtml(), entity.MessageContent);
            base.Add(entity);
        }
    }
}