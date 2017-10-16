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
using Notifications.Wpf;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace OM.App
{
    public class AppBootstrapper : BootstrapperBase
    {

        private log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AppBootstrapper));

        private static readonly string BASE_URI = ConfigurationManager.AppSettings.Get("AppServerUrl");

        /// <summary>
        /// 
        /// </summary>
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();

            // ע�� json ����
            JsonConfig.Regist<CallConfig>();

            //��ʼ�� AppServer API Client
            SApiClient.Init(new SApiClientOption()
            {
                BaseUri = "api".FixUrl(BASE_URI)// "http://localhost:52537/api/"
            });

            //�����û���½�¼�
            SApiClient.OnLogined += SApiClient_OnLogined;

            //
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void SApiClient_OnLogined(object sender, AppServer.Api.Client.LoginedEventArgs e)
        {
            var tsk = Task.Run(async () =>
            {
                //��ʼ�� SignalR �ͻ���
                await OMExtHubProxy.Instance.Start("signalr".FixUrl(BASE_URI), e.Token.AccessToken);
            });
            Task.WaitAll(tsk);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.Log.Error(e.ExceptionObject);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            this.Log.Error(e.Exception.Message, e.Exception);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            this.Log.Error(e.Exception.Message, e.Exception);
            e.SetObserved();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<INotificationManager, NotificationManager>();

            //ע�� ViewModel
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