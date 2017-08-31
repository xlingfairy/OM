﻿using MahApps.Metro.Controls;
using OM.Api.Methods.Controls.Query;
using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OM.App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.LoadData();
        }

        private async void LoadData()
        {
            var mth = new GetDeviceInfo();
            var devices = await OMExtHubProxy.Instance.Execute(mth);
        }
    }
}