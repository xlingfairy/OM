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

        private static string AuthorizationToken { get; set; }

        private static Lazy<OMHubProxy> Instance = new Lazy<OMHubProxy>(() => new OMHubProxy(HubUrl));



        private IHubProxy Proxy { get; }

        private HubConnection Connection { get; }


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
            //不能设置这个，会有一系列报错
            //this.Connection.JsonSerializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;

            //无法指定 ConnectionId
            //this.Connection.ConnectionId = "200";
            //this.Connection.Credentials = new NetworkCredential("200", "");
            //this.Connection.Headers.Add("ExtID", "8073");

            this.Connection.Headers.Add("Authorization", AuthorizationToken);
            this.Proxy = this.Connection.CreateHubProxy("OMHub");

            //不能设置这个，会有一系列报错
            //this.Proxy.JsonSerializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;

            //对应为 Clients.Caller.ExtID dynamic
            //this.Proxy["ExtID"] = "200";

            #region
            //暂时没有找到序列化抽象类/接口，而又不引起报错的办法
            //只能在服务端发送 json 字符串，在客户端解开
            //this.Proxy.On<IExtNotify>("OnReceiveInput", d =>
            //{
            //    Console.WriteLine(d);
            //});

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


        public static async Task<ExtInfo> GetExtInfo()
        {
            return await Instance.Value.Proxy.Invoke<ExtInfo>("GetExtInfo");
        }

        public static async Task<DeviceInfo> GetDeviceInfo()
        {
            var json = await Instance.Value.Proxy.Invoke<string>("GetDeviceInfo");
            return JsonConvert.DeserializeObject<DeviceInfo>(json, JSONSetting);
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
