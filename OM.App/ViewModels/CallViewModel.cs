using Caliburn.Micro;
using Notifications.Wpf;
using OM.Api.Models.Events;
using OM.App.Attributes;
using OM.App.Models;
using OM.AppClient.SignalR;
using OM.AppServer.Api.Client.Methods;
using OM.Moq.Entity;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using SApiClient = OM.AppServer.Api.Client.ApiClient;

namespace OM.App.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.PreRequest)]
    public class CallViewModel : BaseVM
    {
        public override string Title => this.Data?.DebtorName ?? "呼叫";

        public override string TabIdentifier => this.Data?.DebtorName;

        private DebtInfo _data = null;
        public DebtInfo Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
                this.NotifyOfPropertyChange(() => this.Data, () => this.Title);
                Task.Run(() => this.LoadNotes(value.ID));
            }
        }

        public BindableCollection<DebtNote> Notes { get; }
            = new BindableCollection<DebtNote>();

        private CallingStages _status = CallingStages.None;
        /// <summary>
        /// 呼叫状态
        /// </summary>
        public CallingStages Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
                this.NotifyOfPropertyChange(() => this.Status);
                if (value == CallingStages.Answered)
                {
                    this.Span = TimeSpan.FromSeconds(0);
                    Execute.OnUIThread(() => this.Timer.Start());
                }
                else
                {
                    Execute.OnUIThread(() => this.Timer.Stop());
                }
            }
        }

        /// <summary>
        /// 通话时长
        /// </summary>
        public TimeSpan Span { get; set; }


        private DispatcherTimer Timer;


        public CallViewModel()
        {

            OMExtHubProxy.Instance.OnAlert += Instance_OnAlert;
            OMExtHubProxy.Instance.OnAnswered += Instance_OnAnswered;
            OMExtHubProxy.Instance.OnBye += Instance_OnBye;

            Execute.OnUIThread(() =>
            {
                this.Timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(1),
                };
                this.Timer.Tick += Timer_Tick;
            });

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Span = this.Span.Add(TimeSpan.FromSeconds(1));
            this.NotifyOfPropertyChange(() => this.Span);
        }


        private async Task LoadNotes(long debtID)
        {
            var mth = new GetDebtNotes()
            {
                DebtID = debtID
            };
            var rst = await SApiClient.ExecuteAsync(mth);
            if (!mth.HasError)
            {
                this.Notes.Clear();
                this.Notes.AddRange(rst.Result);
            }
        }

        public void Call()
        {
            if (this.Status == CallingStages.None)
                this.Status = CallingStages.Dailing;
            else
                this.Status = CallingStages.None;
        }


        #region
        //对方回铃
        private void Instance_OnAlert(object sender, NotifyArgs<Alert> e)
        {
            if (string.Equals(e.Data.ToNO, this.Data.DebtorPhone))
                this.Status = CallingStages.Alert;
        }

        //对方应答
        private void Instance_OnAnswered(object sender, NotifyArgs<Answered> e)
        {
            if (string.Equals(e.Data.ToNO, this.Data.DebtorPhone))
                this.Status = CallingStages.Answered;
        }

        //通话结束
        private void Instance_OnBye(object sender, NotifyArgs<Bye> e)
        {
            if (string.Equals(e.Data.ToNO, this.Data.DebtorPhone))
                this.Status = CallingStages.Bye;
        }
        #endregion
    }
}
