using System;
using System.Timers;
using IServices.Infrastructure;
using IServices.ISysServices;
using Models.SysModels;

namespace Web.Helper
{
    public interface IOnTimedEvent
    {
        void Run(object source, ElapsedEventArgs elapsedEventArgs);
    }

    public class OnTimedEvent : IOnTimedEvent
    {
        private readonly ISysMailService _iSysMailService;
        private readonly ISysLogService _sysLogService;
        private readonly ISysUserLogService _sysUserLogService;
        private readonly IUnitOfWork _unitOfWork;

        public OnTimedEvent(IUnitOfWork unitOfWork, ISysUserLogService sysUserLogService, ISysLogService sysLogService,
            ISysMailService iSysMailService)
        {
            _unitOfWork = unitOfWork;
            _sysUserLogService = sysUserLogService;
            _sysLogService = sysLogService;
            _iSysMailService = iSysMailService;
        }

        public void Run(object source, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                _sysUserLogService.DeleteExpiredData();
                _unitOfWork.CommitAsync();

                _sysLogService.Add(new SysLog {Level = "Info", Message = "成功清理过期用户日志。"});
            }
            catch (Exception e)
            {
                _sysLogService.Add(new SysLog {Level = "Error", Message = e.Message});
            }

            try
            {
                _sysLogService.DeleteExpiredData();
                _sysLogService.Add(new SysLog {Level = "Info", Message = "成功清理过期系统日志。"});
            }
            catch (Exception e)
            {
                _sysLogService.Add(new SysLog {Level = "Error", Message = e.Message});
            }

            try
            {
                int sent = _iSysMailService.SendMail();
                _unitOfWork.Commit();

                _sysLogService.Add(new SysLog {Level = "Info", Message = "邮件发送成功" + sent + "封。"});
            }
            catch (Exception e)
            {
                _sysLogService.Add(new SysLog {Level = "Error", Message = e.Message});
            }
        }
    }
}