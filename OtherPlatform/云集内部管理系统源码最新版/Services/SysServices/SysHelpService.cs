using System.Linq;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysHelpService : RepositoryBase<SysHelp>, ISysHelpService
    {
        public SysHelpService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysHelp> GetAllEnt()
        {
            return base.GetAllEnt().OrderBy(a => a.Sort);
        }
    }
}