using System.Linq;
using IServices.Infrastructure;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysLogService
    {
        IQueryable<SysLog> GetAll();
        void Add(SysLog item);
        void DeleteExpiredData();
    }
}