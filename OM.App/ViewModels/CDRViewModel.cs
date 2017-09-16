using Caliburn.Micro;
using OM.Api.Models;
using OM.App.Attributes;
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
    public class CDRViewModel : BaseVM
    {
        public override string Title => "实时通话记录报告";

        public BindableCollection<CDR> Datas { get; }
            = new BindableCollection<CDR>();

        public CDRViewModel()
        {
            OMExtHubProxy.Instance.OnCDR += Instance_OnCDR;
        }

        private void Instance_OnCDR(object sender, NotifyArgs<CDR> e)
        {
            this.Datas.Insert(0, e.Data);
        }
    }
}
