using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OM.Api.Models.Enums;

namespace OM.Api.Models.Events
{

    /// <summary>
    /// 来电呼叫请求事件
    /// 在中继的“来电应答前控制”开关打开的情况下，当来电呼叫该中继时，OM会向应用服务器推送INVITE事件。
    /// </summary>
    [XmlRoot("Event")]
    public class Invite : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.INVITE;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("trunk")]
        public IDAttribute Trunk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }
    }
}
