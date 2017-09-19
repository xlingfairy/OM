using CNB.Common;
using OM.Api.Models.Enums;
using OM.Api.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    /// <summary>
    /// 通话记录报告
    /// 通话记录报告简称话单，是指OM对一路通话从开始到结束的记录和统计的报告。当一路通话释放后，OM向应用服务器实时推送该报告。
    /// </summary>
    [XmlRoot("Cdr")]
    public class CDR : IAdminNotify, IInput
    {

        /// <summary>
        /// 话单id
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// 通话的相对唯一标识符
        /// </summary>
        [XmlElement("callid")]
        public int CallID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [XmlElement("visitor")]
        public IDAttribute Visitor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("outer")]
        public IDAttribute Outer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Type")]
        public CDRTypes Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Route")]
        public CDRRouteTypes Route { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("TimeStart")]
        public string _timestar { get; set; }

        /// <summary>
        /// 呼叫起始时间，即发送或收到呼叫请求的时间
        /// </summary>
        [XmlIgnore]
        public DateTime? TimeStar => this._timestar.ToDateTimeOrNull("yyyyMMddHHmmss");

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("TimeEnd")]
        public string _timeend { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public DateTime? TimeEnd => this._timeend.ToDateTimeOrNull("yyyyMMddHHmmss");

        /// <summary>
        /// 主叫号码
        /// </summary>
        [XmlElement("CPN")]
        public string From { get; set; }

        /// <summary>
        /// 被叫号码
        /// </summary>
        [XmlElement("CDPN")]
        public string To { get; set; }

        /// <summary>
        /// 通话时长，值为0说明未接通。
        /// </summary>
        [XmlElement("Duration")]
        public int Duration { get; set; }

        /// <summary>
        /// 该路通话所经过的中继号码
        /// </summary>
        [XmlElement("TrunkNumber")]
        public string TrunkNumber { get; set; }

        //TODO
        /// <summary>
        /// 录音文件的相对保存路径
        /// </summary>
        [XmlElement("Recording")]
        public string Recording { get; set; }

        /// <summary>
        /// 编码方式，决定录音文件格式，值为：G729、G711(PCMA、PCMU)。 
        /// </summary>
        [XmlElement("RecCodec")]
        public string RecCodec { get; set; }
    }
}
