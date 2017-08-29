using Microsoft.AspNet.SignalR;
using OM.Api.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.SignalR
{

    /// <summary>
    /// 
    /// </summary>
    public class OMHub : Hub
    {

        private log4net.ILog Log = log4net.LogManager.GetLogger(typeof(OMHub));

        //public void NotifyInput(string extID, IInput input)
        //{
        //    Clients.User(extID)
        //        .notify(input);
        //}

        public override Task OnConnected()
        {
            this.Log.Debug("Client connected: " + Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            this.Log.Debug("Client connected: " + Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            this.Log.Debug("Client connected: " + Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}
