namespace OM.App
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using OM.App.ViewModels;
    using System.Reflection;
    using SApiClient = OM.AppServer.Api.Client.ApiClient;
    using SApiClientOption = OM.AppServer.Api.Client.ApiClientOption;
    using System.Threading.Tasks;

    public class AppBootstrapper : BootstrapperBase
    {
        SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();

            SApiClient.Init(new SApiClientOption()
            {
                BaseUri = "http://localhost:52537/api/"
            });

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            //зЂВс ViewModel
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