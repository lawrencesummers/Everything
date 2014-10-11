using System.Linq;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysControllerService : RepositoryBase<SysController>, ISysControllerService
    {
        public SysControllerService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysController> GetAllEnt()
        {
            return base.GetAllEnt().OrderBy(a => a.SysArea.SystemId).ThenBy(a => a.SystemId);
        }

        /// <summary>
        ///     物理删除
        /// </summary>
        /// <param name="item"></param>
        public override void Delete(SysController item)
        {
            base.Remove(item);
        }
    }
}