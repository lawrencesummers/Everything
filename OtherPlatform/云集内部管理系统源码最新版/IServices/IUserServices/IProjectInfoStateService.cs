using System.Web.Mvc;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IProjectInfoStateService :  IRepository<ProjectInfoState>
    {
        SelectList SelectList(object selectedValue);
    }
}