using System;
using IServices.Infrastructure;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysRoleService :  IRepository<SysRole>
    {
        bool CheckSysUserSysRoleSysControllerSysActions(Guid enterpriseId, Guid userid, string area, string action,
            string controller);

    }
}