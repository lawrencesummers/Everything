using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysRoleSysUserService : RepositoryBase<SysRoleSysUser>, ISysRoleSysUserService
    {
        public SysRoleSysUserService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override void Delete(SysRoleSysUser item)
        {
            base.Remove(item);
        }
    }
}