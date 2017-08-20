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
    /// 通话信息
    /// </summary>
    public abstract class CallInfo
    {

        /// <summary>
        /// 去电/来电的编号,可依据该参数进行呼叫转接
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }

        /// <summary>
        /// 原始主叫号码
        /// </summary>
        [XmlAttribute("from")]
        public string From { get; set; }

        /// <summary>
        /// 原始被叫号码（对于visitor而言，原始被叫为来电呼入中继号码）
        /// </summary>
        [XmlAttribute("to")]
        public string To { get; set; }

        /// <summary>
        /// 通话的相对唯一标识符
        /// </summary>
        [XmlAttribute("callid")]
        public int CallID { get; set; }

        /// <summary>
        /// 通话状态
        /// </summary>
        [XmlElement("state")]
        public CallingStats State { get; set; }
    }

    /// <summary>
    /// 去电，这里作为该查询分机的通话方
    /// </summary>
    [XmlRoot("outer")]
    public class OutCallInfo : CallInfo
    {

        /// <summary>
        /// 中继号，即该去电从该中继呼出
        /// </summary>
        [XmlAttribute("trunk")]
        public string Trunk { get; set; }

    }

    /// <summary>
    /// 来电，这里作为该查询分机的通话方
    /// </summary>
    [XmlRoot("visitor")]
    public class VisitorCallInfo : CallInfo
    {
        //TODO 没有实际数据，不知道是什么东东
        /// <summary>
        /// [ext | menu | outer]	
        /// 来电的通话方，可能为分机、语音菜单、呼叫寄存区、广播区、去电，为空时表明来电呼入OM后尚未被转接
        /// </summary>
        [XmlElement("ext", typeof(ExtDeviceItem))]
        public DeviceItem Device { get; set; }
    }
}
