using System;
using System.Linq;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ProjectUserService : RepositoryBase<ProjectUser>, IProjectUserService
    {
        public ProjectUserService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override void Delete(ProjectUser item)
        {
            base.Remove(item);
        }

        public void Follow(Guid id, Guid userId)
        {
            //判断是否Follow
            if (GetAll(a => a.SysUserId.Equals(userId) && a.ProjectInfoId.Equals(id) && a.Follow).Any())
            {
                Delete(a => a.SysUserId.Equals(userId) && a.ProjectInfoId.Equals(id) && a.Follow);
                //移除
            }
            else
            {
                Save(null, new ProjectUser {Follow = true, SysUserId = userId, ProjectInfoId = id});
                //添加
            }
        }
    }
}