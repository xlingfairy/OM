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
    /// 分机组队列事件
    /// 通过设备页面配置分机组，且组内分机全忙时，若有分机或来电呼入分机组，OM会向应用服务器汇报该事件。
    /// 排队等待的电话个数最多为30个，超过30个后来电自动挂断，不再排队；
    /// Waiting group的值为分机组的相对唯一标识符，注意与分机组的组号区分；
    /// OM只提供进入队列的事件（包括当前队列长度）， 出队列需API应用服务器自己维护。
    /// </summary>
    [XmlRoot("Event")]
    public class Queue : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.QUEUE;

        /// <summary>
        /// 分机呼入分机组：
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

        /// <summary>
        /// 来电呼入分机组：
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("waiting")]
        public Waiting Waiting { get; set; }
    }
}
