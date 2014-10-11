using System;
using IServices.Infrastructure;

namespace IServices.ISysServices
{
    public interface IUserInfo 
    {
        Guid UserId { get; }
        Guid EnterpriseId { get; }
    }
}