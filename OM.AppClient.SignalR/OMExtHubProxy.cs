using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using OM.Api;
using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{
    public class OMExtHubProxy : BaseHubProxy
    {


        #region
        public event EventHandler<NotifyArgs<Alert>> OnAlert = null;
        public event EventHandler<NotifyArgs<Answer>> OnAnswer = null;
        public event EventHandler<NotifyArgs<Answered>> OnAnswered = null;
        public event EventHandler<NotifyArgs<BootUp>> OnBootup = null;

        public event EventHandler<NotifyArgs<Online>> OnOnline = null;
        public event EventHandler<NotifyArgs<Offline>> OnOffline = null;
        #endregion


        /// <summary>
        /// 
        /// </summary>
        private static Lazy<OMExtHubProxy> _Instance = new Lazy<OMExtHubProxy>(() => new OMExtHubProxy());

        /// <summary>
        /// 
        /// </summary>
        public static OMExtHubProxy Instance => _Instance.Value;

        /// <summary>
        /// 
        /// </summary>
        protected override string HubName => "OMHub";

        /// <summary>
        /// 
        /// </summary>
        private static readonly JsonSerializerSettings JSONSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        /// <summary>
        /// 不允许实例
        /// </summary>
        private OMExtHubProxy()
        {

        }

        protected override void BeforeStart()
        {
            base.BeforeStart();

            //OMHubHelper.Send 方法里定义
            this.Proxy.On<string>("OnReceiveInput", d =>
            {
                var notify = JsonConvert.DeserializeObject<INotify>(d, JSONSetting);
                if (this.OnOnline != null && notify is Online online)
                    this.OnOnline?.BeginInvoke(null, new NotifyArgs<Online>() { Event = online }, null, null);
                if (this.OnOffline != null && notify is Offline offline)
                    this.OnOffline?.BeginInvoke(null, new NotifyArgs<Offline>() { Event = offline }, null, null);
            });
        }


        /// <summary>
        /// 远程执行方法
        /// OMHub.Execute 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public async Task<T> Execute<T>(BaseMethod<T> mth)
        {
            var mthJson = JsonConvert.SerializeObject(mth, JSONSetting);
            var rst = await this.Proxy.Invoke<string>("Execute", mthJson);
            return JsonConvert.DeserializeObject<T>(rst, JSONSetting);
        }
    }
}
