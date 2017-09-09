using Caliburn.Micro;
using Notifications.Wpf;
using OM.App.Attributes;
using OM.App.Models;
using OM.AppClient.SignalR;
using OM.AppServer.Api.Client.Methods;
using OM.Moq.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
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

        private NotificationManager NM = new NotificationManager();

        public BindableCollection<EventLog> Logs { get; }
            = new BindableCollection<EventLog>();


        public BindableCollection<DebtInfo> Debts { get; }
            = new BindableCollection<DebtInfo>();


        public ICollectionView CV { get; private set; }

        private string _filterStr = null;
        /// <summary>
        /// 
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

        public ExtViewModel()
        {
            Execute.OnUIThread(() =>
            {
                this.CV = CollectionViewSource.GetDefaultView(this.Logs);
                this.CV.Filter = new Predicate<object>(this.Filter);
            });

            OMExtHubProxy.Instance.OnAlert += Instance_OnAlert;

            Task.Run(async () =>
            {
                await this.LoadDebts(0, 20);
            });
        }

        private async Task LoadDebts(int page, int pageSize)
        {
            var mth = new GetDebts()
            {
                Page = page,
                PageSize = pageSize
            };
            var debts = await SApiClient.ExecuteAsync(mth);
            this.Debts.Clear();
            this.Debts.AddRange(debts.Result);
        }

        public void LoadingRowDetails(DataGridRowDetailsEventArgs e)
        {
            try
            {
                var detail = e.DetailsElement.FindName("Detail") as ContentControl;
                var vm = new DebtDetailViewModel((e.Row.DataContext as DebtInfo));
                View.SetModel(detail, vm);
            }
            catch
            {
            }
        }

        private void Instance_OnAlert(object sender, NotifyArgs<Api.Models.Events.Alert> e)
        {
            var content = new NotificationContent()
            {
                Message = "对方已振铃",
                Title = $"您呼叫的号码：{e.Event.ToNO} 已振铃",
                Type = NotificationType.Information
            };
            this.NM.Show(content, expirationTime: TimeSpan.FromSeconds(5));

            Execute.OnUIThread(() =>
            {
                this.Logs.Insert(0, new EventLog()
                {
                    CreateOn = DateTime.Now,
                    Event = e.Event,
                    Tip = $"您呼叫的号码：{e.Event.ToNO} 已振铃"
                });
            });
        }
    }
}
