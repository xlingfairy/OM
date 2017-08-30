using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using OM.Api;
using OM.Api.Methods.Controls.Query;
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


        /// <summary>
        /// 获取分机号
        /// </summary>
        /// <returns></returns>
        private string GetExtID()
        {
            return GlobalHost.DependencyResolver.Resolve<IUserIdProvider>().GetUserId(Context.Request);
        }

        /// <summary>
        /// 把分机按所在分机组进行分组
        /// </summary>
        /// <returns></returns>
        private async Task JoinGroup()
        {
            var extID = this.GetExtID();
            var mth = new GetExtInfo()
            {
                ID = extID
            };
            var rst = await ApiClient.ExecuteAsync(mth);
            if (!mth.HasError)
            {
                foreach (var g in rst.Groups)
                {
                    await Groups.Add(Context.ConnectionId, g.ID.ToString());
                    this.Log.Debug($"分机 {extID} 加入Hub分组: {g.ID}");
                }
            }
        }


        public async override Task OnConnected()
        {
            //对应为 HubProxy["ExtID"]
            // TODO 这里获取不到？
            //var tmp = this.Clients.Caller.ExtID;

            var extID = this.GetExtID();
            await JoinGroup();
            this.Log.Debug($"Client {Context.ConnectionId} 分机：{extID} 上线");
            await base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var extID = this.GetExtID();
            this.Log.Debug($"Client {Context.ConnectionId} 分机：{extID} 下线");
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var extID = this.GetExtID();
            this.Log.Debug($"Client {Context.ConnectionId} 分机：{extID} 重连");
            return base.OnReconnected();
        }
    }
}
