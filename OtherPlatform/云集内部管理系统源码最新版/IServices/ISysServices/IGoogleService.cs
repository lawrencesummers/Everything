using System;
using System.Threading.Tasks;
using IServices.Infrastructure;
using Models.SysModels;

namespace IServices.ISysServices
{
    public interface IGoogleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>EventId</returns>
        Task<string> Insert(Guid userId, string title, string content, DateTime start, DateTime end);

        Task Delete(Guid userId, string eventId);
    }
}