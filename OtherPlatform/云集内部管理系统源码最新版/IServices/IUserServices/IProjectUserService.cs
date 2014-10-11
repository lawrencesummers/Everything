using System;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IProjectUserService :  IRepository<ProjectUser>
    {
        void Follow(Guid id, Guid userId);
    }
}