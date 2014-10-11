using IServices.Infrastructure;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface ISysMailService :  IRepository<SysMail>
    {
        int SendMail();
    }
}