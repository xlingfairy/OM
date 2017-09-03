using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using OM.App.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SApiClient = OM.AppServer.Api.Client.ApiClient;

namespace OM.App.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class LoginViewModel : BaseVM
    {
        public override string Title => "用户登陆";

        public string User { get; set; }

        public string Pwd { get; set; }

        public string Msg { get; set; }

        public bool HasError { get; set; }

        public async Task<bool> Login()
        {
            var (isSuccess, msg) = await SApiClient.Login(this.User, this.Pwd);
            if (!isSuccess)
            {
                this.Msg = msg;
                this.HasError = true;
            }
            else
            {
                this.Msg = null;
                this.HasError = false;
            }


            // 不能使用 await,
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, (System.Action)delegate () { this.Notify(); });
            // 无效
            //Dispatcher.CurrentDispatcher.InvokeAsync(() => this.Notify(), DispatcherPriority.Send);

            return isSuccess;
        }

        private void Notify()
        {
            //this.NotifyOfPropertyChange(() => this.HasError);
            //this.NotifyOfPropertyChange(() => this.Msg);

            this.NotifyOfPropertyChange(() => this.Msg, ()=>this.HasError);
        }
    }

}
