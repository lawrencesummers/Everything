using System;
using System.Linq;
using Common;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysRoleService : RepositoryBase<SysRole>, ISysRoleService
    {
        public SysRoleService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysRole> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId);
        }

        public bool CheckSysUserSysRoleSysControllerSysActions(Guid enterpriseId, Guid userid, string area,
            string action,
            string controller)
        {
            return
                base.GetAll().Where(
                    a =>
                        a.SysRoleSysUsers.Any(b => b.SysUserId.Equals(userid) && b.EnterpriseId.Equals(enterpriseId)) &&
                        a.SysRoleSysControllerSysActions.Any(
                            b =>
                                b.SysControllerSysAction.SysController.SysArea.AreaName.Equals(area) &&
                                b.SysControllerSysAction.SysController.ControllerName.Equals(controller) &&
                                b.SysControllerSysAction.SysAction.ActionName.Equals(action))).Cache().Any();
        }
    }
}