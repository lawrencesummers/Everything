using IServices.Infrastructure;
using Models.UserModels;

namespace IServices.IUserServices
{
    public interface IMessageService :  IRepository<Message>
    {
    }
}