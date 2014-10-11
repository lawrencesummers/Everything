using System;
using System.Linq;
using System.Linq.Expressions;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysSignalROnlineService
    {
        IQueryable<SysSignalROnline> GetAll();

        SysSignalROnline GetById(Guid id);

        void Add(SysSignalROnline item);

        void Remove(Expression<Func<SysSignalROnline, bool>> where);
    }
}