using System.Web.Mvc;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IBusinessChanceService :  IRepository<BusinessChance>
    {
        SelectList SelectList(object selectedValue);
    }
}