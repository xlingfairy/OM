using OM.Api.Models;
using OM.Api.Models.Events;
using OM.Api.Parser;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace OM.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiClient
    {

        /// <summary>
        /// 当接收到事件推送时
        /// </summary>
        public static event EventHandler<OMEventEventArgs> OnReceiveEvent;

        /// <summary>
        /// 当接收到CDR推送时
        /// </summary>
        public static event EventHandler<OMCDREventArgs> OnReceiveCDR;


        private static Lazy<ApiClient> Instance = new Lazy<ApiClient>(() => new ApiClient());


        /// <summary>
        /// 是否已经初始化，只会初始化一次
        /// </summary>
        private static bool HasInitilized = false;


        internal ApiClientOption Option
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="opt"></param>
        public static void Init(ApiClientOption opt)
        {
            if (opt == null)
                throw new ArgumentNullException("opt");

            if (!HasInitilized)
            {
                Instance.Value.Option = opt;
                HasInitilized = true;
            }
        }

        /// <summary>
        /// 不允许直接实例
        /// </summary>
        private ApiClient()
        {

        }


        /// <summary>
        /// 获取最终请求地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public string GetModelUrl<T>(BaseMethod<T> mth)
        {
            return $"{this.Option.BaseUri}";
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(BaseMethod<T> mth)
        {
            return await mth.Execute(Instance.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public static T Execute<T>(BaseMethod<T> mth)
        {
            return mth.Execute(Instance.Value).Result;
        }


        /// <summary>
        /// 执行方法， For SignalR
        /// </summary>
        /// <param name="mth"></param>
        /// <returns></returns>
        public static async Task<object> ExecuteAsync(BaseMethod mth)
        {
            return await mth.Execute2(Instance.Value);
        }

        /// <summary>
        /// 执行输入
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static void Execute(string xml)
        {
            var input = InputParser.Parse(xml);
            if (input != null)
            {
                if (OnReceiveEvent != null && input is BaseEvent)
                {
                    var evt = (BaseEvent)input;
                    OnReceiveEvent.BeginInvoke(
                        null,
                        new OMEventEventArgs()
                        {
                            Data = (BaseEvent)input,
                            Type = evt.Attribute
                        },
                        OMEventInvokeCallback,
                        null
                    );
                }
                else if (OnReceiveCDR != null && input is CDR)
                {
                    OnReceiveCDR.BeginInvoke(
                        null,
                        new OMCDREventArgs() { Data = (CDR)input },
                        OMCDRInvokeCallback,
                        null
                    );
                }
            }
        }

        private static void OMEventInvokeCallback(IAsyncResult result)
        {
            var ar = (AsyncResult)result;
            var mth = (EventHandler<OMEventEventArgs>)ar.AsyncDelegate;
            mth.EndInvoke(result);
        }

        private static void OMCDRInvokeCallback(IAsyncResult result)
        {
            var ar = (AsyncResult)result;
            var mth = (EventHandler<OMCDREventArgs>)ar.AsyncDelegate;
            mth.EndInvoke(result);
        }
    }
}
