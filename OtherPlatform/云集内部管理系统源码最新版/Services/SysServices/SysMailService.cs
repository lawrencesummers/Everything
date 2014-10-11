using System.Linq;
using Common;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysMailService : RepositoryBase<SysMail>, ISysMailService
    {
        public SysMailService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }


        public int SendMail()
        {
            int i = 0;

            foreach (SysMail item in GetAllEnt().Where(a => !a.Sent))
            {
                try
                {
                    Email.SendEmail(item.To, item.Subject, item.Body);
                    i++;
                    item.Sent = true;
                }
                catch
                {
                }
            }

            return i;
        }
    }
}