using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OM.Api;
using OM.Api.Models;
using OM.Api.Models.Events;
using OM.Api.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{

    /// <summary>
    /// 
    /// </summary>
    public class OMHubProxy : IDisposable
    {


        private static string HubUrl { get; set; }

        /// <summary>
        /// 用户登陆的认证信息
        /// </summary>
        private static string AuthorizationToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static Lazy<OMHubProxy> Instance = new Lazy<OMHubProxy>(() => new OMHubProxy(HubUrl));


        /// <summary>
        /// 
        /// </summary>
        private IHubProxy Proxy { get; }

        /// <summary>
        /// 
        /// </summary>
        private HubConnection Connection { get; }


        /// <summary>
        /// 
        /// </summary>
        private static readonly JsonSerializerSettings JSONSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };


        private OMHubProxy(string hubUrl)
        {
            this.Connection = new HubConnection(hubUrl);
            this.Connection.Error += Connection_Error;
            this.Connection.Headers.Add("Authorization", AuthorizationToken);
            this.Proxy = this.Connection.CreateHubProxy("OMHub");

            #region
            this.Proxy.On<string>("OnReceiveInput", d =>
            {
                var evt = (BaseEvent)JsonConvert.DeserializeObject<IExtNotify>(d, JSONSetting);
                Console.WriteLine(evt.Attribute);
            });
            #endregion
        }


        private void Connection_Error(Exception obj)
        {

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubUrl">OMHub Url</param>
        /// <param name="authorizationToken">用户认证信息</param>
        /// <returns></returns>
        public static async Task Start(string hubUrl, string authorizationToken)
        {
            HubUrl = hubUrl;
            AuthorizationToken = $"Bearer {authorizationToken}";
            try
            {
                await OMHubProxy.Instance.Value.Connection.Start();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 远程执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public static async Task<T> Execute<T>(BaseMethod<T> mth)
        {
            var mthJson = JsonConvert.SerializeObject(mth, JSONSetting);
            var rst = await Instance.Value.Proxy.Invoke<string>("Execute", mthJson);
            return JsonConvert.DeserializeObject<T>(rst, JSONSetting);
        }


        #region dispose
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~OMHubProxy()
        {
            this.Dispose(false);
        }


        private bool isDisposed = false;
        private void Dispose(bool flag)
        {
            if (!isDisposed)
            {
                if (flag)
                {
                    if (this.Connection != null)
                    {
                        this.Connection.Stop();
                        this.Connection.Dispose();
                    }
                }
                isDisposed = true;
            }
        }
        #endregion
    }
}
