using System;
using System.Collections.Generic;
using Caliburn.Micro;
using OM.App.ViewModels;
using System.Reflection;
using SApiClient = OM.AppServer.Api.Client.ApiClient;
using SApiClientOption = OM.AppServer.Api.Client.ApiClientOption;

using System.Threading.Tasks;
using OM.AppClient.SignalR;
using System.Configuration;
using CNB.Common;

namespace OM.App
{
    public class AppBootstrapper : BootstrapperBase
    {

        private static readonly string BASE_URI = ConfigurationManager.AppSettings.Get("AppServerUrl");

        /// <summary>
        /// 
        /// </summary>
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();

            //初始化 AppServer API Client
            SApiClient.Init(new SApiClientOption()
            {
                BaseUri = "api".FixUrl(BASE_URI)// "http://localhost:52537/api/"
            });

            //挂载用户登陆事件
            SApiClient.OnLogined += SApiClient_OnLogined;

            //
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void SApiClient_OnLogined(object sender, AppServer.Api.Client.LoginedEventArgs e)
        {
            var tsk = Task.Run(async () =>
            {
                //初始化 SignalR 客户端
                await OMExtHubProxy.Instance.Start("signalr".FixUrl(BASE_URI), e.Token.AccessToken);
            });
            Task.WaitAll(tsk);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {

        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            //注册 ViewModel
            container.RegistInstances(Assembly.GetExecutingAssembly());
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}