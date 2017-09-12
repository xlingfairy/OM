using OM.Api;
using OM.Api.Models.Events;
using System;

namespace OM.AppClient.SignalR
{
    public static class Helper
    {
        public static void InvokeEvent<T>(this INotify notify, EventHandler<NotifyArgs<T>> handler) where T : BaseEvent
        {
            if (handler != null && notify is T t)
                handler?.BeginInvoke(null, new NotifyArgs<T>() { Event = t }, null, null);
        }
    }
}
