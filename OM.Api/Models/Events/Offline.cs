using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{

    /// <summary>
    /// 分机离线事件
    /// 当IP分机由上线变为离线时， OM设备向应用服务器推送该报告。
    /// 只有IP分机会有该事件，模拟分机没有。
    /// </summary>
    [XmlRoot("Event")]
    public class Offline : BaseEvent, IExtNotify
    {


        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.OFFLINE;

        /// <summary>
        /// 分机离线事件
        /// 当IP分机由上线变为离线时， OM设备向应用服务器推送该报告
        /// 只有IP分机会有该事件，模拟分机没有。
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

        string IExtNotify.ExtID => this.Ext.ID;
    }
}
