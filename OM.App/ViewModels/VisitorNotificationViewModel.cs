using Caliburn.Micro;
using OM.App.Attributes;
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
    public class VisitorNotificationViewModel : PropertyChangedBase
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public override string Title => "来电通知";

        private string _visitorNO;
        /// <summary>
        /// 
        /// </summary>
        public string VisitorNO
        {
            get
            {
                return this._visitorNO;
            }
            set
            {
                this._visitorNO = value;
            }
        }

        private string _msg;
        /// <summary>
        /// 
        /// </summary>
        public string Msg
        {
            get
            {
                return this._msg;
            }
            set
            {
                this._msg = value;
            }
        }

        public async Task Find()
        {
            var vm = IoC.Get<ExtViewModel>();
            IoC.Get<IShell>().ShowTab(vm);
            await vm.Search("", this.VisitorNO);
        }
    }
}
