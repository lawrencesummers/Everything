using System;
using System.Linq;
using IServices.ISysServices;
using Models.SysModels;

namespace Services.SysServices
{
    public class SysSignalRService : ISysSignalRService
    {
        private IUserInfo _IUserInfo;

        public SysSignalRService(IUserInfo iUserInfo)
        {
            _IUserInfo = iUserInfo;
        }

        public void Add(SysSignalR item)
        {
            using (var db = new ApplicationDb())
            {
                db.SysSignalRs.Add(item);
                db.SaveChanges();
            }
        }

        public IQueryable<SysSignalR> GetAll(string groupId)
        {
            var db = new ApplicationDb();
            return db.SysSignalRs.Where(a => a.EnterpriseId == _IUserInfo.EnterpriseId && a.GroupId.Equals(groupId) && (a.UserId == _IUserInfo.UserId || !a.UserId1.HasValue || a.UserId1 == _IUserInfo.UserId)).OrderByDescending(a => a.CreatedDate);
        }
    }
}