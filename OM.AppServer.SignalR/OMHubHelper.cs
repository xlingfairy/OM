using Microsoft.AspNet.SignalR;
using OM.Api;
using OM.Api.Parser;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.SignalR
{
    public static class OMHubHelper
    {

        public static void ConfigurationOMHub(this IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new OMIDProvider());
            //app.UseCors(CorsOptions.AllowAll);
            var cfg = new HubConfiguration()
            {
                EnableJSONP = true,
                EnableDetailedErrors = true
            };
            //app.MapSignalR("/Msg", cfg);
            app.MapSignalR(cfg);
        }

        public static void Send(string extID, IExtNotify input)
        {
            GlobalHost.ConnectionManager
                .GetHubContext<OMHub>()
                .Clients
                .User(extID)
                .OnReceiveInput(input);
        }
    }
}
