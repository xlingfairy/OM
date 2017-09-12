using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App
{
    /// <summary>
    /// Base class of ViewModel
    /// </summary>
    public abstract class BaseVM : Screen
    {

        public abstract string Title
        {
            get;
        }


        private bool isBusy = false;
        /// <summary>
        /// Indicate currently whether this model is in processing.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            }
        }


        /// <summary>
        /// 对于注册为 PreRequest 的 VM， 用于判断是否在【主窗口的标签】中打开了这个VM的视图
        /// </summary>
        public virtual string TabIdentifier => null;

        public BaseVM()
        {
            this.DisplayName = this.Title;
        }
    }
}
