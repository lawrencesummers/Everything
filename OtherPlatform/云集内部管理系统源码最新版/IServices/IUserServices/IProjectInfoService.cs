using System;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IProjectInfoService :  IRepository<ProjectInfo>
    {
        void Finish(Guid id);
    }
}