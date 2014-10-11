using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Web.SignalR
{
    public class ServerDateTime : Hub
    {
        private bool _send;

        public override Task OnConnected()
        {
            _send = true;
            var t = new Task(() =>
            {
                while (_send)
                {
                    Clients.Client(Context.ConnectionId).GetDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Thread.Sleep(1000);
                }
            });
            t.Start();
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            _send = true;
            var t = new Task(() =>
            {
                while (_send)
                {
                    Clients.Client(Context.ConnectionId).GetDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Thread.Sleep(1000);
                }
            });
            t.Start();
            return base.OnReconnected();
        }

        public override Task OnDisconnected()
        {
            _send = false;
            return base.OnDisconnected();
        }


    }
}