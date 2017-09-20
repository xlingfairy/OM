﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseHubProxy : IDisposable
    {

        public event EventHandler Connected = null;

        /// <summary>
        /// 连接被关闭
        /// </summary>
        public event EventHandler ConnectionClosed = null;

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
            this.Connection.StateChanged += Connection_StateChanged;
            this.Connection.Closed += Connection_Closed;

            this.Connection.Headers.Add("Authorization", $"Bearer {authorizationToken}");
            this.Proxy = this.Connection.CreateHubProxy(this.HubName);

            this.BeforeStart();

            await this.Connection.Start();
        }

        /// <summary>
        /// 重连(只在断开连接时使用)
        /// </summary>
        /// <returns></returns>
        public async Task Restart()
        {
            await this.Connection.Start();
        }

        private void Connection_Closed()
        {
            this.ConnectionClosed?.BeginInvoke(null, new EventArgs(), ConnectedCallback, null);
        }

        private void Connection_StateChanged(StateChange obj)
        {
            if (obj.NewState == ConnectionState.Connected)
            {
                Task.Run(() =>
                {
                    this.Connected?.DynamicInvoke(null, new EventArgs());
                });
            }
        }


        private void ConnectedCallback(IAsyncResult result)
        {
            var ar = (AsyncResult)result;
            var mth = (EventHandler)ar.AsyncDelegate;
            mth.EndInvoke(result);
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
