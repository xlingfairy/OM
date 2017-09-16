﻿using Caliburn.Micro;
using OM.Api.Methods.Controls.Query;
using OM.Api.Models;
using OM.App.Attributes;
using OM.App.Models;
using OM.AppClient.SignalR;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OM.App.ViewModels
{

    [Regist(InstanceMode.Singleton)]
    public class DashboardViewModel : BaseVM
    {
        public override string Title => "监控";


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

        public DeviceInfo Data { get; private set; }

        public IObservableCollection<DeviceState> Exts { get; }
                = new BindableCollection<DeviceState>();

        public ICollectionView CV { get; set; }

        public DashboardViewModel()
        {
            Execute.OnUIThread(() => {
                this.CV = CollectionViewSource.GetDefaultView(this.Exts);
                this.CV.Filter = new Predicate<object>(this.Filter);
            });
            OMExtHubProxy.Instance.OnOffline += Instance_OnOffline;
            OMExtHubProxy.Instance.OnOnline += Instance_OnOnline;

            this.LoadDeviceInfo();
        }

        private bool Filter(object o)
        {
            if (string.IsNullOrWhiteSpace((this.FilterStr)))
            {
                return true;
            }
            else
            {
                var e = (DeviceState)o;
                return e.ID.IndexOf(this.FilterStr) > -1;
            }
        }

        private void Instance_OnOnline(object sender, NotifyArgs<Api.Models.Events.Online> e)
        {
            this.SetExtStatus(e.Data.Ext.ID, DeviceStatus.Connected);
        }

        private void Instance_OnOffline(object sender, NotifyArgs<Api.Models.Events.Offline> e)
        {
            this.SetExtStatus(e.Data.Ext.ID, DeviceStatus.Offline);
        }


        private void SetExtStatus(string extID, DeviceStatus status)
        {
            var ext = this.Exts.First(i => i.ID == extID);
            if (ext != null)
            {
                ext.Status = status;
                ext.NotifyOfPropertyChange("Status");
            }
        }

        private async void LoadDeviceInfo()
        {
            var mth = new GetDeviceInfo();
            var info = await OMExtHubProxy.Instance.Execute(mth);
            this.Data = info;
            this.NotifyOfPropertyChange(() => this.Data);

            var exts = this.Data.Exts.Select(e => new DeviceState()
            {
                ID = e.ID
            }).OrderBy(e => e.ID);

            Execute.OnUIThread(() => {
                this.Exts.AddRange(exts);
            });

            this.LoadExtStats();
        }

        public void LoadExtStats()
        {
            foreach (var e in this.Data?.Exts)
            {
                Task.Run(async () =>
                {
                    var mth = new GetExtInfo()
                    {
                        ID = e.ID
                    };
                    var info = await OMExtHubProxy.Instance.Execute(mth);
                    if (info != null)
                    {
                        var ext = this.Exts.First(i => i.ID == e.ID);
                        ext.Data = info;
                        ext.Status = info.ToStatus();
                        ext.NotifyOfPropertyChange("Status");
                    }
                });
            }
        }
    }
}
