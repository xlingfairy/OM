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
    /// 分机空闲事件
    /// 当分机由忙碌变成空闲时，OM设备会向应用服务器推送该报告
    /// </summary>
    [XmlRoot("Event")]
    public class Idle : BaseEvent, IExtNotify
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.IDLE;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

        string IExtNotify.ExtID => this.Ext.ID;
    }
}
