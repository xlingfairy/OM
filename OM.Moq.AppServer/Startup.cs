using log4net.Config;
using Microsoft.Owin;
using OM.AppServer;
using OM.AppServer.SignalR;
using OM.Moq.AppServer;
using OM.Moq.AppServer.Auth;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

//不起作用
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
[assembly: OwinStartup(typeof(Startup))]
namespace OM.AppServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //TODO assembly 方式的不起作用...原因未知
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            var c = XmlConfigurator.Configure(new FileInfo(path));

            GlobalConfiguration.Configure(WebApiConfig.Register);

            OMHandler.Init();

            app.UseMoqAuth();
            app.ConfigurationOMHub();
        }

    }
}