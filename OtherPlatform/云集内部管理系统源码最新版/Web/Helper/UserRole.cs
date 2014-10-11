using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IServices.ISysServices;

namespace Web.Helper
{
    public interface IUserRole
    {
        bool Check(string area, string action, string controller);
    }

    public class UserRole : IUserRole
    {

        private readonly ISysRoleService _iSysRoleService;
        private readonly IUserInfo _iUserInfo;

        public UserRole(ISysRoleService iSysRoleService, IUserInfo iUserInfo)
        {
            _iSysRoleService = iSysRoleService;
            _iUserInfo = iUserInfo;
        }


        public bool Check(string area, string action, string controller)
        {
            return _iSysRoleService.CheckSysUserSysRoleSysControllerSysActions(_iUserInfo.EnterpriseId, _iUserInfo.UserId, area, action, controller);
        }
    }
}