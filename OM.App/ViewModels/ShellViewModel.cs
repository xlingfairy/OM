using Caliburn.Micro;
using CNB.Common;
using MaterialDesignThemes.Wpf;
using OM.App.Attributes;
using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SClient = OM.AppServer.Api.Client.ApiClient;

namespace OM.App.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton, ForType = typeof(IShell))]
    public class ShellViewModel : BaseVM, IShell
    {
        public override string Title => "OM Client";

        public IObservableCollection<BaseVM> Tabs { get; }


        public ShellViewModel()
        {
            this.Tabs = new BindableCollection<BaseVM>();
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var v = ((Views.ShellView)view);
            v.Activated += V_Activated;
        }

        private void V_Activated(object sender, EventArgs e)
        {
            if (!SClient.IsLogined)
            {
                this.ShowLogin();
                ((Views.ShellView)sender).Activated -= V_Activated;
            }
        }

        private async void ShowLogin()
        {
            var o = await IoC.Get<LoginViewModel>().ShowAsDialog(vm => vm.Login());

            OMExtHubProxy.Instance.Connected += (sender, args) =>
            {
                BaseVM vm = null;
                if (SClient.IsAdmin)
                    vm = IoC.Get<DashboardViewModel>();
                else
                    vm = IoC.Get<ExtViewModel>();

                if (!this.Tabs.Contains(vm))
                    this.Tabs.Add(vm);
            };
        }
    }
}
