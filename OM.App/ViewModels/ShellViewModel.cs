using Caliburn.Micro;
using OM.App.Attributes;
using OM.AppClient.SignalR;
using System;
using System.Linq;
using SClient = OM.AppServer.Api.Client.ApiClient;
using Notifications.Wpf;
using OM.App.Models;
using System.ComponentModel;
using System.Windows.Data;
using OM.Api.Models.Events;
using OM.AppServer.Api.Client;
using System.Windows.Input;

namespace OM.App.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton, ForType = typeof(IShell))]
    public class ShellViewModel : BaseVM, IShell
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Title => "OM Client";

        /// <summary>
        /// 
        /// </summary>
        //public NotificationManager NM => new NotificationManager();
        private INotificationManager NM { get; }


        #region 页签
        /// <summary>
        /// 页签数据源
        /// </summary>
        public IObservableCollection<BaseVM> Tabs { get; }

        /// <summary>
        /// 选中的页签
        /// </summary>
        public BaseVM SelectedTab { get; set; }

        #endregion


        #region 实时收到的通知
        /// <summary>
        /// 实时收到的通知,数据源
        /// </summary>
        public BindableCollection<EventLog> Logs { get; }
            = new BindableCollection<EventLog>();



        /// <summary>
        /// 实时通知的可过滤源
        /// </summary>
        public ICollectionView CV { get; private set; }



        private string _filterStr = null;
        /// <summary>
        /// 过滤字符串
        /// </summary>
        public string FilterStr
        {
            get
            {
                return this._filterStr;
            }
            set
            {
                this._filterStr = value;
                if (this.CV != null)
                    this.CV.Refresh();
            }
        }



        /// <summary>
        /// 实时事件过滤时的回调
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool Filter(object o)
        {
            if (string.IsNullOrWhiteSpace((this.FilterStr)))
            {
                return true;
            }
            else
            {
                var e = (EventLog)o;
                return e.Tip.IndexOf(this.FilterStr, StringComparison.OrdinalIgnoreCase) > -1;
            }
        }

        #endregion


        public ICommand SettingCmd { get; }

        /// <summary>
        /// 
        /// </summary>
        public ShellViewModel(INotificationManager nm)
        {
            this.NM = nm;

            this.Tabs = new BindableCollection<BaseVM>();

            //在 UI 线程上执行, CollectionView 不支持多线程操作
            Execute.OnUIThread(() =>
            {
                this.CV = CollectionViewSource.GetDefaultView(this.Logs);
                this.CV.Filter = new Predicate<object>(this.Filter);
            });

            #region  SignalR 事件注册
            if (!SClient.IsAdmin)
            {
                OMExtHubProxy.Instance.OnAlert += Instance_OnAlert;
                OMExtHubProxy.Instance.OnAnswered += Instance_OnAnswered;
                OMExtHubProxy.Instance.OnBye += Instance_OnBye;

                OMExtHubProxy.Instance.OnRing += Instance_OnRing;
                OMExtHubProxy.Instance.OnDivert += Instance_OnDivert;
            }

            OMExtHubProxy.Instance.ConnectionClosed += Instance_ConnectionClosed;
            #endregion

            #region CMD 注册
            this.SettingCmd = new Command(() =>
            {
                this.ShowTab<SettingViewModel>();
            });
            #endregion
        }

        #region SignalR 事件处理
        private void Instance_ConnectionClosed(object sender, EventArgs e)
        {
            var vm = IoC.Get<LostConnectionViewModel>();
            vm.Reset();
            this.NM.Show(vm, expirationTime: TimeSpan.FromSeconds(60));
        }

        /// <summary>
        /// 添加 Log 到侧边栏
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="tip"></param>
        private void AddLog(BaseEvent evt, string tip)
        {
            //在 UI 线程上执行
            Execute.OnUIThread(() =>
            {
                this.Logs.Insert(0, new EventLog()
                {
                    CreateOn = DateTime.Now,
                    Event = evt,
                    Tip = tip
                });
            });
        }

        //对方回铃
        private void Instance_OnAlert(object sender, NotifyArgs<Alert> e)
        {
            var content = new NotificationContent()
            {
                Message = "对方已回铃",
                Title = $"您呼叫的号码：{e.Data.ToNO} 已回铃",
                Type = NotificationType.Information
            };
            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            this.AddLog(e.Data, $"您呼叫的号码：{e.Data.ToNO} 已回铃");
        }


        //对方应答
        private void Instance_OnAnswered(object sender, NotifyArgs<Answered> e)
        {
            var content = new NotificationContent()
            {
                Message = "对方已应答",
                Title = $"您呼叫的号码：{e.Data.ToNO} 已应答",
                Type = NotificationType.Information
            };

            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            this.AddLog(e.Data, $"您呼叫的号码：{e.Data.ToNO} 已应答");
        }

        //通话结束
        private void Instance_OnBye(object sender, NotifyArgs<Bye> e)
        {
            var content = new NotificationContent()
            {
                Message = "通话结束",
                Title = $"与 {e.Data.ToNO} 的通话结束",
                Type = NotificationType.Information
            };
            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            this.AddLog(e.Data, $"与 {e.Data.ToNO} 的通话结束");
        }


        //本机响铃
        private void Instance_OnRing(object sender, NotifyArgs<Ring> e)
        {
            if (e.Data.RingFromType != Api.Models.Enums.RingFromTypes.OM)
            {
                var content = new VisitorNotificationViewModel()
                {
                    Msg = $"收到来自: {e.Data.RingFromType} {e.Data.FromNO} 的来电",
                    VisitorNO = e.Data.FromNO
                };

                this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(60));

                this.AddLog(e.Data, $"收到来自: {e.Data.RingFromType} {e.Data.FromNO} 的来电");
            }
        }


        //呼叫转移
        private void Instance_OnDivert(object sender, NotifyArgs<Divert> e)
        {
            if (e.Data.Visitor != null)
            {
                var vm = new VisitorNotificationViewModel()
                {
                    VisitorNO = e.Data.Visitor.From,
                    Msg = $"收到 {e.Data.Visitor.From} 的来电, 呼叫转移自:{e.Data.DivertInfo?.From}",
                };

                this.NM.Show(vm, expirationTime: TimeSpan.FromSeconds(60));

                this.AddLog(e.Data, $"收到 {e.Data.Visitor.From} 的来电, 呼叫转移自:{e.Data.DivertInfo?.From}");
            }
        }

        #endregion


        #region 显示登陆框
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
                {
                    this.ShowTab<DashboardViewModel>();
                    this.ShowTab<CDRViewModel>();
                }
                else
                    this.ShowTab<ExtViewModel>();
            };
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="allowMuti"></param>
        public void ShowTab<T>(bool allowMuti = false) where T : BaseVM
        {
            var vm = IoC.Get<T>();
            this.ShowTab(vm, allowMuti);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="allowMuti"></param>
        public void ShowTab<T>(T vm, bool allowMuti = false) where T : BaseVM
        {
            //if (allowMuti || !this.Tabs.Contains(vm))
            if (allowMuti || !this.Tabs.OfType<T>().Any(t => t.TabIdentifier == vm.TabIdentifier))
                this.Tabs.Add(vm);

            this.SelectedTab = vm;
            this.NotifyOfPropertyChange(() => this.SelectedTab);
        }


        /// <summary>
        /// 添加 Log 到侧边栏
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="tip"></param>
        public void AddLog(string tip)
        {
            //在 UI 线程上执行
            Execute.OnUIThread(() =>
            {
                this.Logs.Insert(0, new EventLog()
                {
                    CreateOn = DateTime.Now,
                    Tip = tip
                });
            });
        }

    }
}
