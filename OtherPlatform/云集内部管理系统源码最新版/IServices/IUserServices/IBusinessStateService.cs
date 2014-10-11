using System.Web.Mvc;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IBusinessStateService :  IRepository<BusinessState>
    {
        SelectList SelectList(object selectedValue);
    }
}