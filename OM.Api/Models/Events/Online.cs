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
    /// 分机上线事件
    /// 当IP分机由离线变为上线，或IP分机的地址发生变更时，OM设备向应用服务器推送该事件报告。
    /// </summary>
    [XmlRoot("Event")]
    public class Online : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.ONLINE;

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("RegisterIP")]
        public string RegisterIP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }
    }
}
