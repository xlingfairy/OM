using System;

namespace OM.AppClient.SignalR
{
    public class NotifyArgs<T> : EventArgs
    {

        public T Data { get; set; }

    }
}
