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
    /// 通话结束事件
    /// </summary>
    [XmlRoot("Event")]
    public class Bye : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.BYE;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

        /// <summary>
        /// 来电和分机的通话结束，来电挂断
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }


        /// <summary>
        /// 分机和去电的通话结束，分机挂断：
        /// </summary>
        [XmlElement("outer")]
        public OutCallInfo Outer { get; set; }

        //TODO 来电转去电的通话结束，来电挂断：
        //TODO 双向外呼的通话结束，两个去电分别各有一个BYE事件：
    }
}
