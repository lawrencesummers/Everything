using System;
using System.Linq;
using System.Linq.Expressions;
using IServices.ISysServices;
using Models.SysModels;

namespace Services.SysServices
{
    public class SysSignalROnlineService : ISysSignalROnlineService
    {
        public void Add(SysSignalROnline item)
        {
            using (var db = new ApplicationDb())
            {
                db.SysSignalROnlines.Add(item);
                db.SaveChanges();
            }
        }

        public IQueryable<SysSignalROnline> GetAll()
        {
            var db = new ApplicationDb();
            return db.SysSignalROnlines;
        }

        public SysSignalROnline GetById(Guid id)
        {
            using (var db = new ApplicationDb())
            {
                return db.SysSignalROnlines.Find(id);
            }
        }

        public void Remove(Expression<Func<SysSignalROnline, bool>> where)
        {
            using (var db = new ApplicationDb())
            {
                foreach (var item in db.SysSignalROnlines.Where(where))
                {
                    db.SysSignalROnlines.Remove(item);
                }
                db.SaveChanges();
            }
        }
    }
}