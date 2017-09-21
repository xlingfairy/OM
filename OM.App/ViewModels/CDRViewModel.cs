using Caliburn.Micro;
using OM.Api.Models;
using OM.App.Attributes;
using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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

        public ICollectionView CV { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public CDRViewModel()
        {
            OMExtHubProxy.Instance.OnCDR += Instance_OnCDR;

            Execute.OnUIThread(() =>
            {
                this.CV = CollectionViewSource.GetDefaultView(this.Datas);
                this.CV.Filter = new Predicate<object>(this.Filter);
            });
        }

        /// <summary>
        /// CDR过滤时的回调
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool Filter(object o)
        {
            if (string.IsNullOrWhiteSpace((this.From))
                && string.IsNullOrWhiteSpace(this.To))
            {
                return true;
            }
            else
            {
                var e = (CDR)o;
                return (!string.IsNullOrWhiteSpace(this.From) && e.From.IndexOf(this.From, StringComparison.OrdinalIgnoreCase) > -1)
                    || (!string.IsNullOrWhiteSpace(this.To) && e.To.IndexOf(this.To, StringComparison.OrdinalIgnoreCase) > -1);
            }
        }

        private void Instance_OnCDR(object sender, NotifyArgs<CDR> e)
        {
            this.Datas.Add(e.Data);
        }

        public void Search()
        {
            this.NotifyOfPropertyChange(() => this.From, () => this.To);
            this.CV.Refresh();
        }
    }
}
