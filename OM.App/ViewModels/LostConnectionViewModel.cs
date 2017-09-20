using Caliburn.Micro;
using Notifications.Wpf;
using OM.App.Attributes;
using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OM.App.ViewModels
{

    [Regist(InstanceMode.Singleton)]
    public class LostConnectionViewModel : PropertyChangedBase
    {
        /// <summary>
        /// 倒计时
        /// </summary>
        public int N { get; set; } = 60;

        /// <summary>
        /// 
        /// </summary>
        public bool Connected { get; set; }

        private DispatcherTimer Timer;

        public LostConnectionViewModel()
        {
            OMExtHubProxy.Instance.Connected += Instance_Connected;
            Execute.OnUIThread(() =>
            {
                this.Timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(1),
                };
                this.Timer.Tick += Timer_Tick;
            });
        }

        private void Instance_Connected(object sender, EventArgs e)
        {
            this.Connected = true;
            this.Timer.Stop();
            this.N = 60;
            this.NotifyOfPropertyChange(() => this.Connected, () => this.N);
        }

        public void Reset()
        {
            this.Connected = false;
            this.N = 60;
            this.Timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (--this.N <= 0)
            {
                Task.Run(async () =>
                {
                    await OMExtHubProxy.Instance.Restart();
                });
            }

            Execute.OnUIThread(() =>
            {
                this.NotifyOfPropertyChange(() => this.N);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ReConnect()
        {
            await OMExtHubProxy.Instance.Restart();
            this.N = 60;
            Execute.OnUIThread(() =>
            {
                this.NotifyOfPropertyChange(() => this.N);
            });
        }
    }
}
