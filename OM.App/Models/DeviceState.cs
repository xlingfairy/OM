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

        public DeviceStatus Status { get; set; }

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
