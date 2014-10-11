using System.Linq;
using System.Web.Mvc;
using IServices.ISysServices;
using IServices.IUserServices;
using Models.UserModels;
using Services.Infrastructure;

namespace Services.UserServices
{
    public class ProjectInfoStateService : RepositoryBase<ProjectInfoState>, IProjectInfoStateService
    {
        public ProjectInfoStateService(IDatabaseFactory databaseFactory, IUserInfo userInfo)
            : base(databaseFactory, userInfo)
        {
        }

        public override IQueryable<ProjectInfoState> GetAll()
        {
            return base.GetAll().OrderBy(a => a.SystemId).ThenBy(a => a.ProjectInfoStateName);
        }

        public SelectList SelectList(object selectedValue)
        {
            return new SelectList(GetAll(), "Id", "ProjectInfoStateName", selectedValue);
        }
    }
}