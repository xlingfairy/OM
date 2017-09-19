using OM.Api;
using OM.Api.Models;
using OM.Api.Models.Events;
using System;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{
    public static class Helper
    {
        public static void InvokeEvent<T>(this INotify notify, EventHandler<NotifyArgs<T>> handler)
        {
            if (handler != null && notify is T t)
            {
                var hs = handler.GetInvocationList();
                foreach (var h in hs)
                {
                    Task.Run(() =>
                    {
                        h.DynamicInvoke(null, new NotifyArgs<T>() { Data = t });
                    });
                }
                //handler?.BeginInvoke(null, new NotifyArgs<T>() { Event = t }, null, null);
            }
        }
    }
}
