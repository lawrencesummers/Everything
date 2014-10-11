using System;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IPlanService :  IRepository<Plan>
    {
        void Finish(Guid id);
    }
}