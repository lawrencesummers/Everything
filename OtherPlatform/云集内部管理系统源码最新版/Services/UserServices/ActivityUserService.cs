using System;
using System.Linq;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ActivityUserService : RepositoryBase<ActivityUser>, IActivityUserService
    {

        public ActivityUserService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {

        }

    }
}