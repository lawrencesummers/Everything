using System;
using System.Configuration;
using System.Linq;
using IServices.ISysServices;
using Models.SysModels;

namespace Services.SysServices
{
    public class SysLogService : ISysLogService
    {
        public void DeleteExpiredData()
        {
            using (var db = new ApplicationDb())
            {
                //只保留一定数量的日志,根据web.config中的设置值，默认单位：天。
                if (ConfigurationManager.AppSettings["LogValidity"] != null)
                {
                    double logValidity = Convert.ToDouble(ConfigurationManager.AppSettings["LogValidity"]);
                    DateTime createddatetime = DateTime.Now.AddDays(-logValidity);
                    foreach (SysLog item in db.SysLogs.Where(a => a.CreatedDate < createddatetime))
                    {
                        db.SysLogs.Remove(item);
                    }
                }
                db.SaveChangesAsync().Wait();
            }
        }

        public void Add(SysLog item)
        {
            using (var db = new ApplicationDb())
            {
                db.SysLogs.Add(item);
                db.SaveChangesAsync().Wait();
            }
        }

        public IQueryable<SysLog> GetAll()
        {
            var db = new ApplicationDb();
            return db.SysLogs.OrderByDescending(a => a.CreatedDate);
        }
    }
}