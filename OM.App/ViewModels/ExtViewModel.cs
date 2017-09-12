using Caliburn.Micro;
using Notifications.Wpf;
using OM.Api.Models.Events;
using OM.App.Attributes;
using OM.App.Models;
using OM.AppClient.SignalR;
using OM.AppServer.Api.Client.Methods;
using OM.Moq.Entity;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SApiClient = OM.AppServer.Api.Client.ApiClient;

namespace OM.App.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class ExtViewModel : BaseVM
    {
        public override string Title => "操作台";

        /// <summary>
        /// 通知管理器
        /// </summary>
        private NotificationManager NM = new NotificationManager();

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

        private bool Filter(object o)
        {
            if (string.IsNullOrWhiteSpace((this.FilterStr)))
            {
                return true;
            }
            else
            {
                var e = (EventLog)o;
                return e.Tip.IndexOf(this.FilterStr) > -1;
            }
        }

        #endregion

        #region 催收数据
        /// <summary>
        /// 催收数据(模拟)
        /// </summary>
        public BindableCollection<DebtInfo> Debts { get; }
            = new BindableCollection<DebtInfo>();

        private DebtInfo _selectedItem = null;
        public DebtInfo SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                this._selectedItem = value;
                this.DetailVM.Data = value;
            }
        }

        public DebtDetailViewModel DetailVM { get; } = IoC.Get<DebtDetailViewModel>();

        /// <summary>
        /// 催收数据总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; } = 50;

        public ICommand PageChandCmd { get; }
        #endregion

        #region 查询条件
        public string Name { get; set; }

        public string Phone { get; set; }
        #endregion

        public ExtViewModel()
        {
            this.PageChandCmd = new Command<int>(async page =>
            {
                await this.LoadDebts(page, this.PageSize);
            });

            //在 UI 线程上执行, CollectionView 不支持多线程操作
            Execute.OnUIThread(() =>
            {
                this.CV = CollectionViewSource.GetDefaultView(this.Logs);
                this.CV.Filter = new Predicate<object>(this.Filter);
            });

            OMExtHubProxy.Instance.OnAlert += Instance_OnAlert;
            OMExtHubProxy.Instance.OnRing += Instance_OnRing;
            OMExtHubProxy.Instance.OnAnswered += Instance_OnAnswered;
            OMExtHubProxy.Instance.OnBye += Instance_OnBye;

            //加载催收数据
            Task.Run(async () =>
            {
                await this.LoadDebts(0, this.PageSize);
            });
        }


        #region 事件处理
        //本机响铃
        private void Instance_OnRing(object sender, NotifyArgs<Ring> e)
        {
            if (e.Event.RingFromType != Api.Models.Enums.RingFromTypes.OM)
            {
                var content = new NotificationContent()
                {
                    Message = $"收到来自: {e.Event.RingFromType} {e.Event.FromNO} 的来电",
                    Title = $"来电",
                    Type = NotificationType.Warning
                };
                this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

                Execute.OnUIThread(() =>
                {
                    this.Logs.Insert(0, new EventLog()
                    {
                        CreateOn = DateTime.Now,
                        Event = e.Event,
                        Tip = $"收到来自: {e.Event.RingFromType} {e.Event.FromNO} 的来电",
                    });
                });
            }
        }

        //对方回铃
        private void Instance_OnAlert(object sender, NotifyArgs<Alert> e)
        {
            if (string.Equals(e.Event.ToNO, this.DetailVM.Data.DebtorPhone))
                this.DetailVM.Status = CallingStages.Alert;

            var content = new NotificationContent()
            {
                Message = "对方已回铃",
                Title = $"您呼叫的号码：{e.Event.ToNO} 已回铃",
                Type = NotificationType.Information
            };
            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            Execute.OnUIThread(() =>
            {
                this.Logs.Insert(0, new EventLog()
                {
                    CreateOn = DateTime.Now,
                    Event = e.Event,
                    Tip = $"您呼叫的号码：{e.Event.ToNO} 已回铃"
                });
            });
        }

        //对方应答
        private void Instance_OnAnswered(object sender, NotifyArgs<Answered> e)
        {
            if (string.Equals(e.Event.ToNO, this.DetailVM.Data.DebtorPhone))
                this.DetailVM.Status = CallingStages.Answered;

            var content = new NotificationContent()
            {
                Message = "对方已应答",
                Title = $"您呼叫的号码：{e.Event.ToNO} 已应答",
                Type = NotificationType.Information
            };
            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            Execute.OnUIThread(() =>
            {
                this.Logs.Insert(0, new EventLog()
                {
                    CreateOn = DateTime.Now,
                    Event = e.Event,
                    Tip = $"您呼叫的号码：{e.Event.ToNO} 已应答"
                });
            });
        }

        //通话结束
        private void Instance_OnBye(object sender, NotifyArgs<Bye> e)
        {
            var content = new NotificationContent()
            {
                Message = "通话结束",
                Title = $"与 {e.Event.ToNO} 的通话结束",
                Type = NotificationType.Information
            };
            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            //在UI线程上执行，避免 CollectionView 的跨线程问题
            Execute.OnUIThread(() =>
            {
                this.Logs.Insert(0, new EventLog()
                {
                    CreateOn = DateTime.Now,
                    Event = e.Event,
                    Tip = $"与 {e.Event.ToNO} 的通话结束"
                });
            });
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task LoadDebts(int page, int pageSize)
        {
            var mth = new GetDebts()
            {
                Page = page,
                PageSize = pageSize,
                Name = this.Name,
                Phone = this.Phone
            };
            var debts = await SApiClient.ExecuteAsync(mth);
            this.Debts.Clear();
            this.Debts.AddRange(debts.Result);

            this.Total = debts.Total;
            this.NotifyOfPropertyChange(() => this.Total);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Search()
        {
            this.Page = 0;
            this.NotifyOfPropertyChange(() => this.Page);
            await this.LoadDebts(0, this.PageSize);
        }

        /// <summary>
        /// 加载催收详情面板(CM 事件处理)
        /// 注意：如果已经加载，不会触发第二次
        /// </summary>
        /// <param name="e"></param>
        public void LoadingRowDetails(DataGridRowDetailsEventArgs e)
        {
            try
            {
                var detail = e.DetailsElement.FindName("Detail") as ContentControl;
                var vm = IoC.Get<DebtDetailViewModel>();
                vm.Data = e.Row.DataContext as DebtInfo;
                View.SetModel(detail, vm);
            }
            catch
            {
            }
        }
    }
}
