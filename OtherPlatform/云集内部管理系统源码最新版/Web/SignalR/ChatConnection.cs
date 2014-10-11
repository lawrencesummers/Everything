using Common;
using IServices.ISysServices;
using Microsoft.AspNet.SignalR;
using Microsoft.Security.Application;
using Models.SysModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.SignalR
{
    public class ChatHubConnection : PersistentConnection
    {
        private readonly IUserInfo _iUserInfo = DependencyResolver.Current.GetService<IUserInfo>();
        private readonly ISysSignalROnlineService _iSysSignalROnlineService = DependencyResolver.Current.GetService<ISysSignalROnlineService>();
        private const string GroupId = "chat";
        private readonly Guid _userId;
        private readonly Guid _enterpriseId;
        private readonly string _groupName;

        private readonly ISysUserService _iSysUserService = DependencyResolver.Current.GetService<ISysUserService>();
        private readonly ISysSignalRService _iSysSignalRService = DependencyResolver.Current.GetService<ISysSignalRService>();

        public ChatHubConnection()
        {
            _userId = _iUserInfo.UserId;
            _enterpriseId = _iUserInfo.EnterpriseId;
            _groupName = _iUserInfo.EnterpriseId + GroupId;
        }


        protected override Task OnConnected(IRequest request, string connectionId)
        {
            Groups.Add(connectionId, _groupName);

            _iSysSignalROnlineService.Remove(a => a.GroupId == GroupId && a.UserId == _iUserInfo.UserId && a.EnterpriseId == _iUserInfo.EnterpriseId);

            //添加新的登录信息
            _iSysSignalROnlineService.Add(new SysSignalROnline() { ConnectionId = connectionId, GroupId = GroupId, UserId = _iUserInfo.UserId, EnterpriseId = _iUserInfo.EnterpriseId });

            return base.OnConnected(request, connectionId);
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            Groups.Add(connectionId, _groupName);

            _iSysSignalROnlineService.Remove(a => a.GroupId == GroupId && a.UserId == _iUserInfo.UserId && a.EnterpriseId == _iUserInfo.EnterpriseId);
            //添加新的登录信息
            _iSysSignalROnlineService.Add(new SysSignalROnline() { ConnectionId = connectionId, GroupId = GroupId, UserId = _iUserInfo.UserId, EnterpriseId = _iUserInfo.EnterpriseId });

            return base.OnReconnected(request, connectionId);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId)
        {
            Groups.Remove(connectionId, _groupName);

            //清除之前未退出的登录信息
            _iSysSignalROnlineService.Remove(a => a.ConnectionId == connectionId);

            return base.OnDisconnected(request, connectionId);
        }

        private class DataMessage
        {
            public Guid? UserId { get; set; }

            public string Message { get; set; }

        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var item = JsonConvert.DeserializeObject<DataMessage>(data);

            //message = Sanitizer.GetSafeHtmlFragment(message); 
            item.Message = Encoder.HtmlEncode(item.Message);
            //message = Encoder.JavaScriptEncode(message);

            ////替换表情成图片链接
            item.Message = item.Message.Replace("[biaoqing]", "<img src='");
            item.Message = item.Message.Replace("[/biaoqing]", "' />");

            if (string.IsNullOrEmpty(item.Message)) return null;

            if (item.UserId.HasValue)
            {
                var sysSignalR = new SysSignalR
                {
                    GroupId = GroupId,
                    EnterpriseId = _enterpriseId,
                    GroupName = _groupName,
                    Message = item.Message,
                    UserId = _userId,
                    UserId1 = item.UserId.Value
                };
                sysSignalR.UserName = _iSysUserService.GetDisplayName(sysSignalR.UserId);
                sysSignalR.UserName1 = _iSysUserService.GetDisplayName(item.UserId.Value);

                _iSysSignalRService.Add(sysSignalR);


                //收件人
                //查询用户的connectionid
                var onlineconnectionId =
                    _iSysSignalROnlineService.GetAll()
                        .Where(a => a.UserId == item.UserId.Value)
                        .OrderByDescending(a => a.CreatedDate)
                        .Select(a => a.ConnectionId)
                        .FirstOrDefault();

                if (!string.IsNullOrEmpty(onlineconnectionId))
                {
                    sysSignalR.UserName = "<a href=\"javascript:;\" onclick=\"$('#UserId').val('" + sysSignalR.UserId + "');\">" +
                          sysSignalR.UserName + "</a>" +
                          " 对 <a href=\"javascript:;\" onclick=\"$('#UserId').val('" + sysSignalR.UserId1 + "');\">" + sysSignalR.UserName1 + "</a> 说";
                    Connection.Send(onlineconnectionId, JsonConvert.SerializeObject(sysSignalR));
                }
                else
                {
                    var user = _iSysUserService.GetById(item.UserId.Value);
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        Email.SendEmail(user.Email, "您在云集中收到 " + sysSignalR.UserName + " 发来的消息", sysSignalR.Message);
                        sysSignalR.Message = sysSignalR.Message + " （对方不在线，直接发送到对方邮箱中。）";
                    }
                    else
                    {
                        sysSignalR.Message = sysSignalR.Message + " （对方不在线）";
                    }
                }

                sysSignalR.UserName = "<a href=\"javascript:;\" onclick=\"$('#UserId').val('" + sysSignalR.UserId + "');\">" +
                          sysSignalR.UserName + "</a>" +
                          " 对 <a href=\"javascript:;\" onclick=\"$('#UserId').val('" + sysSignalR.UserId1 + "');\">" + sysSignalR.UserName1 + "</a> 说";
                //发件人
                Connection.Send(connectionId, JsonConvert.SerializeObject(sysSignalR));

            }
            else
            {
                //保存沟通记录
                var sysSignalR = new SysSignalR
              {
                  GroupId = GroupId,
                  EnterpriseId = _enterpriseId,
                  GroupName = _groupName,
                  Message = item.Message,
                  UserId = _userId
              };

                sysSignalR.UserName = _iSysUserService.GetDisplayName(sysSignalR.UserId);

                _iSysSignalRService.Add(sysSignalR);

                sysSignalR.UserName = "<a href=\"javascript:;\" onclick=\"$('#UserId').val('" + sysSignalR.UserId + "');\">" +
                               sysSignalR.UserName + "</a>" +
                               " 对 <a href=\"javascript:;\" onclick=\"$('#UserId').val('');\">大家</a> 说";
                Groups.Send(_groupName, JsonConvert.SerializeObject(sysSignalR));
            }

            return base.OnReceived(request, connectionId, data);
        }


    }
}