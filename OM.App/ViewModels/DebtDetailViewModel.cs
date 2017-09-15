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
    /// </summary>
    [Regist(InstanceMode.PreRequest)]
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

        public DebtDetailViewModel()
        {
        }

        public void OpenInNewTab()
        {
            var vm = IoC.Get<CallViewModel>();
            vm.Data = this.Data;
            IoC.Get<IShell>().ShowTab(vm, true);
        }

    }
}
