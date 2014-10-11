using System.Web.Mvc;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface ICustomerLevelService :  IRepository<CustomerLevel>
    {
        SelectList SelectList(object selectedValue);
    }
}