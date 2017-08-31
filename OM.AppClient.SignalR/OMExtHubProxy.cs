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

            this.Proxy.On<string>("OnReceiveInput", d =>
            {
                var evt = (BaseEvent)JsonConvert.DeserializeObject<IExtNotify>(d, JSONSetting);
                Console.WriteLine(evt.Attribute);
            });
        }


        /// <summary>
        /// 远程执行方法
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
