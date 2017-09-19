using Caliburn.Micro;
using Notifications.Wpf;
using OM.Api.Models.Events;
using OM.App.Attributes;
using OM.App.Models;
using OM.AppClient.SignalR;
using OM.AppServer.Api.Client.Methods;
using OM.Moq.Entity;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SApiClient = OM.AppServer.Api.Client.ApiClient;

namespace OM.App.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class ExtViewModel : BaseVM
    {
        public override string Title => "操作台";


        #region 催收数据
        /// <summary>
        /// 催收数据,从 WebApi 处获取
        /// </summary>
        public BindableCollection<DebtInfo> Debts { get; }
            = new BindableCollection<DebtInfo>();


        /// <summary>
        /// 催收数据总数
        /// </summary>
        public long Total { get; set; }


        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }


        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; } = 50;


        /// <summary>
        /// 分页命令
        /// </summary>
        public ICommand PageChandCmd { get; }
        #endregion


        #region 查询条件
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        #endregion


        public ExtViewModel()
        {
            this.PageChandCmd = new Command<int>(async page =>
            {
                await this.LoadDebts(page, this.PageSize);
            });

            //加载催收数据
            Task.Run(async () =>
            {
                await this.LoadDebts(0, this.PageSize);
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task LoadDebts(int page, int pageSize)
        {
            var mth = new GetDebts()
            {
                Page = page,
                PageSize = pageSize,
                Name = this.Name,
                Phone = this.Phone
            };
            var debts = await SApiClient.ExecuteAsync(mth);
            this.Debts.Clear();
            this.Debts.AddRange(debts.Result);

            this.Total = debts.Total;
            this.NotifyOfPropertyChange(() => this.Total);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Search()
        {
            this.Page = 0;
            this.NotifyOfPropertyChange(() => this.Page);
            await this.LoadDebts(0, this.PageSize);
        }

        /// <summary>
        /// 加载催收详情面板(CM 事件处理)
        /// 注意：如果已经加载，不会触发第二次
        /// </summary>
        /// <param name="e"></param>
        public void LoadingRowDetails(DataGridRowDetailsEventArgs e)
        {
            try
            {
                var detail = e.DetailsElement.FindName("Detail") as ContentControl;
                var vm = IoC.Get<DebtDetailViewModel>();
                vm.Data = e.Row.DataContext as DebtInfo;
                View.SetModel(detail, vm);
            }
            catch
            {
            }
        }
    }
}
