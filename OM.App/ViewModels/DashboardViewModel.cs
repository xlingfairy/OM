using Caliburn.Micro;
using OM.Api.Methods.Controls.Query;
using OM.Api.Models;
using OM.App.Attributes;
using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OM.App.ViewModels
{

    [Regist(InstanceMode.Singleton)]
    public class DashboardViewModel : BaseVM
    {
        public override string Title => "监控";

        public DeviceInfo Data { get; private set; }


        public DashboardViewModel()
        {
            this.LoadDeviceInfo();
        }

        ////未执行，原因未知
        //protected override void OnActivate()
        //{
        //    base.OnActivate();

        //    this.LoadDeviceInfo();
        //}

        private async void LoadDeviceInfo()
        {
            var mth = new GetDeviceInfo();
            var info = await OMExtHubProxy.Instance.Execute(mth);
            this.Data = info;
            this.NotifyOfPropertyChange(() => this.Data);
        }

        private void Notify()
        {
            this.NotifyOfPropertyChange(() => this.Data);
        }
    }
}
