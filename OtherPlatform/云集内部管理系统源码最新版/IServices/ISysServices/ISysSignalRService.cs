using System.Linq;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysSignalRService
    {


        IQueryable<SysSignalR> GetAll(string groupId);
        void Add(SysSignalR item);
    }
}