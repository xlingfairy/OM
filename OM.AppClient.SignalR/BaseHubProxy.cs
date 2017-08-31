﻿using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
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
    public abstract class BaseHubProxy
    {
        /// <summary>
        /// 
        /// </summary>
        protected IHubProxy Proxy { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected HubConnection Connection { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        protected abstract string HubName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Start(string signalRUrl, string authorizationToken)
        {
            this.Connection = new HubConnection(signalRUrl);
            this.Connection.Error += Connection_Error;

            this.Connection.Headers.Add("Authorization", $"Bearer {authorizationToken}");
            this.Proxy = this.Connection.CreateHubProxy(this.HubName);

            this.BeforeStart();

            await this.Connection.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void BeforeStart()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void Connection_Error(Exception obj)
        {
            this.OnError(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnError(Exception e)
        {

        }

        #region dispose
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~BaseHubProxy()
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