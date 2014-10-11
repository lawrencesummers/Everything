using System.Linq;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysEnterpriseService : RepositoryBase<SysEnterprise>, ISysEnterpriseService
    {
        public SysEnterpriseService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysEnterprise> GetAll()
        {
            return base.GetAll().OrderBy(a => a.EnterpriseName);
        }
    }
}