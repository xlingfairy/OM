using Caliburn.Micro;
using OM.App.Attributes;
using OM.App.Models;
using OM.Moq.Entity;
using System;
using System.Windows.Threading;

namespace OM.App.ViewModels
{
    /// <summary>
    /// ExtView DataGrid 的RowDetailsTemplate
    /// 注意，这里为 Singleton ，所以在 ExtView DataGrid.SelectionMode="Single"
    /// 基类不能是 Screen / ViewAware , 否则的话, DataGrid.RowDetail 第二次不会正常显示,原因未知
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class DebtDetailViewModel : PropertyChangedBase
    {
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
                this.NotifyOfPropertyChange(() => this.Data);
            }
        }

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


        public DebtDetailViewModel()
        {
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

        public void Call()
        {
            if (this.Status == CallingStages.None)
                this.Status = CallingStages.Dailing;
            else
                this.Status = CallingStages.None;
        }

        public void OpenInNewTab()
        {
            var vm = IoC.Get<CallViewModel>();
            vm.Data = this.Data;
            vm.Status = this.Status;
            vm.Span = this.Span;

            IoC.Get<IShell>().ShowTab(vm);
        }

    }
}
