using System.Linq;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysActionService : RepositoryBase<SysAction>, ISysActionService
    {
        public SysActionService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysAction> GetAllEnt()
        {
            return base.GetAllEnt().OrderBy(a => a.SystemId);
        }

        /// <summary>
        ///     物理删除
        /// </summary>
        /// <param name="item"></param>
        public override void Delete(SysAction item)
        {
            base.Remove(item);
        }
    }
}