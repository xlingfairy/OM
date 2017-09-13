using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using OM.Api;
using OM.Api.Methods.Controls.Query;
using OM.Api.Models;
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
    [Authorize]
    public class OMHub : Hub
    {

        private log4net.ILog Log = log4net.LogManager.GetLogger(typeof(OMHub));

        private static readonly JsonSerializerSettings JSONSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };


        /// <summary>
        /// SignalR 对泛型，基类/接口的处理还没有弄明白，
        /// 这样处理只是代替方案
        /// </summary>
        /// <param name="mthJson">Method 的 json 序列化字符串</param>
        /// <returns>Method 执行结果的 json 序列化字符串</returns>
        public async Task<(string, bool, ResultCodes, string)> Execute(string mthJson)
        {
            try
            {
                var mth = JsonConvert.DeserializeObject<BaseMethod>(mthJson, JSONSetting);
                if (mth != null)
                {
                    var rst = await ApiClient.ExecuteAsync(mth);
                    return (JsonConvert.SerializeObject(rst, JSONSetting), !mth.HasError, mth.ResultCode, mth.ErrorMessage);
                }
                return (null, true, ResultCodes.错误, "反序列化 method 失败");
            }
            catch (Exception e)
            {
                throw new HubException(e.Message);
            }
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="mth"></param>
        ///// <returns></returns>
        //public async Task<string> GetDeviceInfo()
        //{
        //    var mth = new GetDeviceInfo();
        //    var info = await ApiClient.ExecuteAsync(mth);
        //    // info 含有抽象类，没有找到解决方法
        //    return JsonConvert.SerializeObject(info, JSONSetting);
        //}


        //public async Task<ExtInfo> GetExtInfo()
        //{
        //    var extID = this.GetExtID();
        //    var mth = new GetExtInfo()
        //    {
        //        ID = extID
        //    };
        //    var info = await ApiClient.ExecuteAsync(mth);
        //    if (!mth.HasError)
        //    {
        //        return info;
        //    }
        //    else
        //    {
        //        throw new HubException(mth.ErrorMessage);
        //    }
        //}



        #region 
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
            // AppUser.cs GenerateUserIdentityAsync 自定义 Role admin
            if (Context.User.IsInRole("admin"))
            {
                await Groups.Add(Context.ConnectionId, "admin");
            }
            else
            {
                var name = this.Context.User.Identity.Name;
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
        }
        #endregion


        #region 事件
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
        #endregion
    }
}
