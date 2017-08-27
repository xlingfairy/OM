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
    /// 呼叫临时事件
    /// 该事件通常作为转接（Transfer）请求的响应消息，特殊情况下，OM也会推送该事件消息。
    /// </summary>
    [XmlRoot("Event")]
    public class Transient : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.TRANSIENT;


        /// <summary>
        /// 
        /// </summary>
        [XmlElement("outer")]
        public OutCallInfo Outer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }
    }
}
