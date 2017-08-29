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

        private static Lazy<OMHubProxy> Instance = new Lazy<OMHubProxy>(() => new OMHubProxy("http://localhost:52537/signalr"));

        private IHubProxy Proxy { get; }

        private HubConnection Connection { get; }

        private OMHubProxy(string hubUrl)
        {
            this.Connection = new HubConnection(hubUrl);
            //this.Connection.ConnectionId = "200";
            //this.Connection.Credentials = new NetworkCredential("200", "");
            this.Connection.Headers.Add("ExtID", "200");
            this.Proxy = this.Connection.CreateHubProxy("OMHub");

            this.Proxy.On<string>("OnReceiveInput", d =>
            {
                Console.WriteLine(d);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task Start()
        {
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
