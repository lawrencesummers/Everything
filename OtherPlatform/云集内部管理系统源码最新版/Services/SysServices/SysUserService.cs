using System;
using System.Linq;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using Models.SysModels;
using Services.Infrastructure;

namespace Services.SysServices
{
    public class SysUserService : RepositoryBase<SysUser>, ISysUserService
    {
        public SysUserService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<SysUser> GetAll()
        {
            return base.GetAll().OrderBy(a => a.UserName);
        }

        public override void Add(SysUser entity)
        {
            entity.Password = HashPassword.GetHashPassword(entity.Password);
            entity.OldPassword = entity.Password;
            base.Add(entity);
        }

        public override void Update(SysUser entity)
        {
            if (entity.Password != entity.OldPassword)
            {
                entity.Password = HashPassword.GetHashPassword(entity.Password);
                entity.OldPassword = entity.Password;
            }
            base.Update(entity);
        }

        public SysUser GetByUserNamePassword(Guid enterpriseId, string userName, string password)
        {
            password = HashPassword.GetHashPassword(password);
            return
                base.GetAllEnt()
                    .FirstOrDefault(
                        a =>
                            a.EnterpriseId == enterpriseId && a.UserName.Equals(userName) && a.Password.Equals(password));
        }

        public SysUser GetByUserNamePassword(string enterpriseShortName, string userName, string password)
        {
            password = HashPassword.GetHashPassword(password);
            return
                base.GetAllEnt()
                    .FirstOrDefault(
                        a =>
                            a.SysEnterprise.EnterpriseShortName == enterpriseShortName && a.UserName.Equals(userName) &&
                            a.Password.Equals(password));
        }

        public SysUser GetByUserNamePassword(Guid userId, string password)
        {
            password = HashPassword.GetHashPassword(password);
            return base.GetAllEnt().FirstOrDefault(a => a.Id.Equals(userId) && a.Password.Equals(password));
        }

        public SelectList SelectList(object selectedValue)
        {
            return new SelectList(GetAll().Where(a => a.Enabled).Select(a => new {a.Id, a.DisplayName}), "Id",
                "DisplayName", selectedValue);
        }

        public string GetDisplayName(Guid id)
        {
            SysUser item = GetById(id);
            return string.IsNullOrEmpty(item.DisplayName) ? item.UserName : item.DisplayName;
        }
    }
}