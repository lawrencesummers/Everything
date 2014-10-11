using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common;
using IServices.ISysServices;
using IServices.IUserServices;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Models.SysModels;
using Models.UserModels;


namespace Web.SignalR
{

    [HubName("messenger")]
    public class MessengerHub : Hub
    {
        private readonly IUserInfo _iUserInfo = DependencyResolver.Current.GetService<IUserInfo>();
        private readonly IMessageService _iMessageService = DependencyResolver.Current.GetService<IMessageService>();
        private readonly ISysSignalROnlineService _iSysSignalROnlineService = DependencyResolver.Current.GetService<ISysSignalROnlineService>();
        private const string GroupId = "Messageer";

        public override Task OnConnected()
        {
            var item = _iMessageService.GetAll(a => a.SysUserId == _iUserInfo.UserId).Count(a => !a.Read);

            if (item > 0)
                Clients.Client(Context.ConnectionId).add("您有" + item + "条新信息");

            var date = DateTime.Now.AddDays(-1);
            _iSysSignalROnlineService.Remove(a =>a.CreatedDate<date && a.GroupId==GroupId && a.UserId==_iUserInfo.UserId && a.EnterpriseId==_iUserInfo.EnterpriseId);

            _iSysSignalROnlineService.Add(new SysSignalROnline { ConnectionId = Context.ConnectionId, GroupId = GroupId, UserId = _iUserInfo.UserId, EnterpriseId = _iUserInfo.EnterpriseId });
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            _iSysSignalROnlineService.Remove(a => a.ConnectionId == Context.ConnectionId);
            return base.OnDisconnected();
        }
    }

    public interface IMessenger
    {
        void SendMessage(Guid userId);
        void SendMessage(Guid userId, string message);
    }

    public class Messenger : IMessenger
    {

        private readonly ISysSignalROnlineService _iSysSignalROnlineService = DependencyResolver.Current.GetService<ISysSignalROnlineService>();
        private const string GroupId = "Messageer";
        private readonly IMessageService _iMessageService = DependencyResolver.Current.GetService<IMessageService>();


        public void SendMessage(Guid userId, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();

            foreach (var item in _iSysSignalROnlineService.GetAll().Where(a => a.GroupId == GroupId && a.UserId == userId))
            {
                context.Clients.Client(item.ConnectionId).add(message);
            }

            _iMessageService.Save(null, new Message
            {
                SysUserId = userId,
                MessageTitle = message,
                MessageContent ="",
            });

        }

        public void SendMessage(Guid userId)
        {
            SendMessage(userId, "");
        }
    }
}