using Caliburn.Micro;
using OM.App.Attributes;
using OM.AppClient.SignalR;
using System;
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

        /// <summary>
        /// 页签数据源
        /// </summary>
        public IObservableCollection<BaseVM> Tabs { get; }

        /// <summary>
        /// 选中的页签
        /// </summary>
        public BaseVM SelectedTab { get; set; }

        public ShellViewModel()
        {
            this.Tabs = new BindableCollection<BaseVM>();
        }

        /// <summary>
        /// OnViewLoaded 的时候, DialogHost 还没有加载
        /// 所以要继续监听 ShellView 的 Activated 事件
        /// </summary>
        /// <param name="view"></param>
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
                //弹出登陆框
                this.ShowLogin();
                //卸载事件,避免错误
                ((Views.ShellView)sender).Activated -= V_Activated;
            }
        }

        private async void ShowLogin()
        {
            var o = await IoC.Get<LoginViewModel>().ShowAsDialog2(vm => vm.Login());

            //当 SignalR 连接上时,才显示
            OMExtHubProxy.Instance.Connected += (sender, args) =>
            {
                if (SClient.IsAdmin)
                    this.ShowTab<DashboardViewModel>();
                else
                    this.ShowTab<ExtViewModel>();
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="allowMuti"></param>
        public void ShowTab<T>(bool show = true, bool allowMuti = false) where T : BaseVM
        {
            var vm = IoC.Get<T>();
            this.ShowTab(vm, show, allowMuti);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="allowMuti"></param>
        public void ShowTab<T>(T vm, bool show = true, bool allowMuti = false) where T : BaseVM
        {
            if (allowMuti || !this.Tabs.Contains(vm))
                this.Tabs.Add(vm);

            this.SelectedTab = vm;
            this.NotifyOfPropertyChange(() => this.SelectedTab);
        }
    }
}
