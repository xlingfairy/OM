using Caliburn.Micro;
using OM.Api.Models;
using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App.Models
{
    public class DeviceState : PropertyChangedBase
    {

        public string ID { get; set; }

        public DeviceStatus Status
        {
            get
            {
                if (this.Data != null)
                    switch (this.Data?.State)
                    {
                        case ExtStats.IP分机离线:
                            return DeviceStatus.Offline;
                        case ExtStats.Offhook:
                        case ExtStats.Progress:
                        case ExtStats.振铃_回铃或通话中:
                            return DeviceStatus.Busy;
                        default:
                            return DeviceStatus.Connected;
                    }
                else
                    return DeviceStatus.Unknow;
            }
        }

        public ExtInfo Data { get; set; }

    }

    public enum DeviceStatus
    {
        Unknow = 0,
        Connected,
        Offline,
        Busy
    }
}
