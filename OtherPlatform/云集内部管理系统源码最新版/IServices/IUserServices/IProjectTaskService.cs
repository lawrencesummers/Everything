using System;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IProjectTaskService :  IRepository<ProjectTask>
    {
        void Finish(Guid id, bool finish);

        void Accept(Guid id, bool accept);
    }
}