using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using OM.Api;
using OM.Api.Models;
using OM.Api.Models.Events;
using System;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{
    public class OMExtHubProxy : BaseHubProxy
    {


        #region 事件声明
        public event EventHandler<NotifyArgs<CDR>> OnCDR = null;

        public event EventHandler<NotifyArgs<Alert>> OnAlert = null;
        public event EventHandler<NotifyArgs<Answer>> OnAnswer = null;
        public event EventHandler<NotifyArgs<Answered>> OnAnswered = null;
        public event EventHandler<NotifyArgs<BootUp>> OnBootup = null;
        public event EventHandler<NotifyArgs<Busy>> OnBusy = null;
        public event EventHandler<NotifyArgs<Bye>> OnBye = null;
        public event EventHandler<NotifyArgs<ConfigChange>> OnConfigChange = null;
        public event EventHandler<NotifyArgs<Divert>> OnDivert = null;
        public event EventHandler<NotifyArgs<DTMF>> OnDTMF = null;
        public event EventHandler<NotifyArgs<EndOfAnn>> OnEndOfAnn = null;
        public event EventHandler<NotifyArgs<Failed>> OnFailed = null;
        public event EventHandler<NotifyArgs<Idle>> OnIdle = null;
        public event EventHandler<NotifyArgs<Incomming>> OnIncomming = null;
        public event EventHandler<NotifyArgs<Invite>> OnInvite = null;
        public event EventHandler<NotifyArgs<Online>> OnOnline = null;
        public event EventHandler<NotifyArgs<Offline>> OnOffline = null;
        public event EventHandler<NotifyArgs<Queue>> OnQueue = null;
        public event EventHandler<NotifyArgs<Ring>> OnRing = null;
        public event EventHandler<NotifyArgs<Transient>> OnTransient = null;
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
        /// 不允许直接实例
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
                notify.InvokeEvent(this.OnCDR);

                notify.InvokeEvent(this.OnAlert);
                notify.InvokeEvent(this.OnAnswer);
                notify.InvokeEvent(this.OnAnswered);
                notify.InvokeEvent(this.OnBootup);
                notify.InvokeEvent(this.OnBusy);
                notify.InvokeEvent(this.OnBye);
                notify.InvokeEvent(this.OnConfigChange);
                notify.InvokeEvent(this.OnDivert);
                notify.InvokeEvent(this.OnDTMF);
                notify.InvokeEvent(this.OnEndOfAnn);
                notify.InvokeEvent(this.OnFailed);
                notify.InvokeEvent(this.OnIdle);
                notify.InvokeEvent(this.OnIncomming);
                notify.InvokeEvent(this.OnInvite);
                notify.InvokeEvent(this.OnOnline);
                notify.InvokeEvent(this.OnOffline);
                notify.InvokeEvent(this.OnQueue);
                notify.InvokeEvent(this.OnRing);
                notify.InvokeEvent(this.OnTransient);
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
            var (rst, isSuccess, resultCode, errorMsg) = await this.Proxy.Invoke<(string, bool, ResultCodes, string)>("Execute", mthJson);
            if (isSuccess)
                return JsonConvert.DeserializeObject<T>(rst, JSONSetting);
            else
            {
                mth.ResultCode = resultCode;
                mth.ErrorMessage = errorMsg;
                return default(T);
            }
        }

    }
}
