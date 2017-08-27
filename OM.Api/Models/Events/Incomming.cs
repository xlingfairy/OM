using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models.Enums;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{
    /// <summary>
    /// 来电呼入事件
    /// 在中继的“来电应答后控制”开关打开的情况下，当来电呼叫该中继时，OM会在应答该来电后，向应用服务器推送INCOMING事件。
    /// </summary>
    [XmlRoot("Event")]
    public class Incomming : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.INCOMING;

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
