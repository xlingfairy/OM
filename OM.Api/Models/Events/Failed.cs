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
    /// 呼叫失败事件
    /// 该事件通常作为API请求的响应消息出现，特殊情况下，OM也会推送该事件消息。
    /// 利用OM API发起呼叫且该呼叫执行过程中失败时，会触发该事件；
    /// 如果是手动拨号，只在极个别情况下会触发该事件。
    /// </summary>
    [XmlRoot("event")]
    public class Failed : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.FAILED;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("err")]
        public Error Error { get; set; }
    }
}
