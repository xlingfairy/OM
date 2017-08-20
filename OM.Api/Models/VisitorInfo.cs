using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{

    /// <summary>
    /// 来电的相关信息
    /// </summary>
    [XmlRoot("visitor")]
    public class VisitorInfo : BaseResponse
    {

        /// <summary>
        /// 来电的编号，可依据该参数对来电进行转接、查询、挂断等操作
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }

        /// <summary>
        /// 原始主叫号码
        /// </summary>
        [XmlAttribute("from")]
        public string From { get; set; }

        /// <summary>
        /// 原始被叫号码（对于visitor而言，原始被叫为来电呼入的中继号码）
        /// </summary>
        [XmlAttribute("to")]
        public string To { get; set; }

        /// <summary>
        /// 通话的相对唯一标识符
        /// </summary>
        [XmlAttribute("callid")]
        public int CallID { get; set; }


        // TODO [ext | menu | outer], 没有具体的数据，不知道这个是什么意思
        /// <summary>
        /// 来电的通话方，可能为分机、语音菜单、呼叫寄存区、广播区、去电，为空时表明来电呼入OM后尚未被转接
        /// </summary>
        [XmlElement("ext")]
        public ExtDeviceItem Ext { get; set; }

        /// <summary>
        /// 通话状态
        /// </summary>
        [XmlElement("state")]
        public CallingStats State { get; set; }
    }
}
