using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using OM.App.Attributes;
using System;
using System.Collections.Generic;
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
                IoC.Get<LoginViewModel>().ShowAsDialog(vm => vm.Login());
                ((Views.ShellView)sender).Activated -= V_Activated;
            }
        }
    }
}
