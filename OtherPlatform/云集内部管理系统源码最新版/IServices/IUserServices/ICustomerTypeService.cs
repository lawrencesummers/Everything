using System.Web.Mvc;
using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface ICustomerTypeService :  IRepository<CustomerType>
    {
        SelectList SelectList(object selectedValue);
    }
}