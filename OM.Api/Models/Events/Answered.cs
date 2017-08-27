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
    /// 呼叫被应答事件
    /// 当主叫检查到被叫应答时，OM向应用服务器推送该事件报告
    /// </summary>
    [XmlRoot("Event")]
    public class Answered : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.ANSWERED;

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
        /// 分机呼分机，主叫分机检查到被叫分机应答：
        /// </summary>
        public IDAttribute ToExt => this._ext?.ElementAt(1);

        /// <summary>
        /// 分机呼外部电话，分机检查到外部电话应答：
        /// </summary>
        [XmlElement("outer")]
        public OutCallInfo Outer { get; set; }

    }
}
