using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysUserResetPasswordService : RepositoryBase<SysUserResetPassword>, ISysUserResetPasswordService
    {
        public SysUserResetPasswordService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }
    }
}