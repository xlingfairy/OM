using Caliburn.Micro;
using Notifications.Wpf;
using OM.App.Attributes;
using OM.App.Models;
using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ExtViewModel()
        {
            OMExtHubProxy.Instance.OnAlert += Instance_OnAlert;
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
            this.Logs.Insert(0, new EventLog()
            {
                CreateOn = DateTime.Now,
                Event = e.Event,
                Tip = $"您呼叫的号码：{e.Event.ToNO} 已振铃"
            });
        }
    }
}
