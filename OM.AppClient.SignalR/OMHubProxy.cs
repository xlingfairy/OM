using Microsoft.AspNet.SignalR.Client;
using OM.Api;
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

        private OMHubProxy(string hubUrl)
        {
            this.Connection = new HubConnection(hubUrl);
            this.Connection.Error += Connection_Error;

            //无法指定 ConnectionId
            //this.Connection.ConnectionId = "200";
            //this.Connection.Credentials = new NetworkCredential("200", "");
            //this.Connection.Headers.Add("ExtID", "8073");

            this.Connection.Headers.Add("Authorization", AuthorizationToken);
            this.Proxy = this.Connection.CreateHubProxy("OMHub");

            //对应为 Clients.Caller.ExtID dynamic
            //this.Proxy["ExtID"] = "200";

            this.Proxy.On<string>("OnReceiveInput", d =>
            {
                Console.WriteLine(d);
            });
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
            await OMHubProxy.Instance.Value.Connection.Start();
        }

        public void Send()
        {

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
