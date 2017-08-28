using Microsoft.AspNet.SignalR;
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

        public static void Configuration(this IAppBuilder app)
        {
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UCUserIDProvider());
            //app.UseCors(CorsOptions.AllowAll);
            var cfg = new HubConfiguration()
            {
                EnableJSONP = true,
                EnableDetailedErrors = true
            };
            //app.MapSignalR("/Msg", cfg);
            app.MapSignalR(cfg);
        }

    }
}
