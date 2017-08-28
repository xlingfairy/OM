using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{

    /// <summary>
    /// 
    /// </summary>
    public class OMHubProxy : IDisposable
    {

        private IHubProxy Proxy { get; }

        private HubConnection Connection { get; }

        public OMHubProxy(string hubUrl)
        {
            this.Connection = new HubConnection(hubUrl);
            this.Proxy = this.Connection.CreateHubProxy("OM");
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
                        this.Connection.Dispose();
                    }
                }
                isDisposed = true;
            }
        }
        #endregion
    }
}
