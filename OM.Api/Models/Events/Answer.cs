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
    /// 呼叫应答事件
    /// 当分机对呼叫应答时，OM向应用服务器推送该事件报告。
    /// </summary>
    [XmlRoot("Event")]
    public class Answer : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.ANSWER;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public List<IDAttribute> _ext { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public IDAttribute Ext => this._ext.First();

        /// <summary>
        /// 分机呼分机，被叫分机应答：
        /// </summary>
        public IDAttribute FromExt => this._ext?.ElementAt(1);


        /// <summary>
        /// 来电呼分机，分机应答：
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }
    }
}
