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
    /// 分机忙事件
    /// 当分机由空闲变成忙碌时，OM设备会向应用服务器推送该报告。
    /// IP分机由空闲状态下摘机不会立即汇报BUSY事件，而是要等拨完号发起呼叫时才会汇报。
    /// </summary>
    [XmlRoot("Event")]
    public class Busy : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.BUSY;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

    }
}
