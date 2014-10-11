using System.Linq;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysAreaService : RepositoryBase<SysArea>, ISysAreaService
    {
        public SysAreaService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysArea> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId);
        }


        /// <summary>
        ///     物理删除
        /// </summary>
        /// <param name="item"></param>
        public override void Delete(SysArea item)
        {
            base.Remove(item);
        }
    }
}