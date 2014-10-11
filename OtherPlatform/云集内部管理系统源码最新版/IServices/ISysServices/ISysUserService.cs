using System;
using System.Web.Mvc;
using IServices.Infrastructure;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysUserService :  IRepository<SysUser>
    {
        SysUser GetByUserNamePassword(Guid enterpriseId, string userName, string password);
        SysUser GetByUserNamePassword(string enterpriseShortName, string userName, string password);
        SysUser GetByUserNamePassword(Guid userId, string password);
        SelectList SelectList(object selectedValue);
        string GetDisplayName(Guid id);
    }
}