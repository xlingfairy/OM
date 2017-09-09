using Caliburn.Micro;
using OM.App.Attributes;
using OM.Moq.Entity;
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
    [Regist(InstanceMode.None)]
    public class DebtDetailViewModel : PropertyChangedBase
    {
        public DebtInfo Data { get; }

        public bool IsCalling { get; set; }

        public DebtDetailViewModel(DebtInfo data)
        {
            this.Data = data;
        }

        public void Call()
        {
            this.IsCalling = !this.IsCalling;
            this.NotifyOfPropertyChange(() => this.IsCalling);
        }
    }
}
