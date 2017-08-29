using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        private static readonly JsonSerializerSettings JSONSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        public static void ConfigurationOMHub(this IAppBuilder app)
        {
            //GlobalHost.HubPipeline.AddModule(new HubExceptionModule());
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
            var str = JsonConvert.SerializeObject(input, JSONSetting);

            var a = GlobalHost.ConnectionManager
                .GetHubContext<OMHub>()
                .Clients
                //.All
                .User(extID)
                .OnReceiveInput(str);
        }
    }
}
