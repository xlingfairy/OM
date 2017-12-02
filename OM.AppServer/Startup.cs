using log4net;
using log4net.Config;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using OM.AppServer;
using OM.AppServer.SignalR;
using OM.Moq.AppServer;
using OM.Moq.AppServer.Auth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
[assembly: OwinStartup(typeof(Startup))]
namespace OM.AppServer
{
    public class Startup
    {
        private ILog Log = LogManager.GetLogger(typeof(Startup));

        public void Configuration(IAppBuilder app)
        {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            GlobalConfiguration.Configure(WebApiConfig.Register);

            OMHandler.Init();

            app.UseMoqAuth();


            var redisConnStr = ConfigurationManager.AppSettings.Get("SignalR:Redis");
            RedisScaleoutConfiguration redisConfig = null;
            if (!string.IsNullOrWhiteSpace(redisConnStr))
            {
                redisConfig = new RedisScaleoutConfiguration(redisConnStr, "_signalR");
            }

            app.ConfigurationOMHub(redisConfig);

            Log.Debug("here");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.Log.Error(e.ExceptionObject);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            this.Log.Error(e.Exception);
        }
    }
}