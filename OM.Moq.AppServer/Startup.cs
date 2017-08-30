using log4net.Config;
using Microsoft.Owin;
using OM.AppServer.SignalR;
using OM.Moq.AppServer;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace OM.Moq.AppServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //TODO
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            var c = XmlConfigurator.Configure(new FileInfo(path));

            GlobalConfiguration.Configure(WebApiConfig.Register);

            OMHandler.Init();

            //app.UseCors(CorsOptions.AllowAll);
            //app.MapSignalR();
            app.ConfigurationOMHub();
        }

    }
}