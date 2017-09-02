using CNB.Common;
using Newtonsoft.Json;
using OM.AppClient.SignalR;
using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AppServerApiClient = OM.AppServer.Api.Client.ApiClient;
using ApppServerApiClientOption = OM.AppServer.Api.Client.ApiClientOption;

namespace OM.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {

        private NotifyIcon Icon = null;

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    this.SetTray();

        //    AppServerApiClient.Init(new ApppServerApiClientOption()
        //    {
        //        BaseUri = "http://localhost:52537/api/"
        //    });
        //}


        private void SetTray()
        {
            this.Icon = new NotifyIcon()
            {
                Text = "OM",
                Icon = OM.App.Properties.Resources.ico
            };
            var m = new ContextMenuStrip();
            var t1 = new ToolStripMenuItem("设置");
            var t2 = new ToolStripMenuItem("我的信息");
            var t3 = new ToolStripMenuItem("关于");
            var t4 = new ToolStripSeparator();
            var t5 = new ToolStripMenuItem("退出");
            t1.Click += T1_Click;
            t2.Click += T2_Click;
            t3.Click += T3_Click;
            t5.Click += T5_Click;
            m.Items.AddRange(new ToolStripItem[] { t1, t2, t3, t4, t5 });
            this.Icon.ContextMenuStrip = m;
            this.Icon.Visible = true;
        }

        private void T5_Click(object sender, EventArgs e)
        {
            this.Shutdown();
        }

        private void T3_Click(object sender, EventArgs e)
        {

        }

        private void T2_Click(object sender, EventArgs e)
        {

        }

        private void T1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (this.Icon != null)
            {
                this.Icon.Visible = false;
                this.Icon.Dispose();
                this.Icon = null;
            }
            base.OnExit(e);
        }
    }
}
