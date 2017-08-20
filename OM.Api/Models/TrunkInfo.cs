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
    /// 继（又称为外线）的相关信息
    /// </summary>
    [XmlRoot("trunk")]
    public class TrunkInfo : BaseResponse
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// 中继的线路编号，是中继的唯一固定标识
        /// </summary>
        [XmlElement("lineid")]
        public string LineID { get; set; }

        /// <summary>
        /// 中继的线路状态
        /// </summary>
        [XmlElement("state")]
        public TrunkStats State { get; set; }


        /// <summary>
        /// 通话信息
        /// </summary>
        [XmlElement("outer", typeof(OutCallInfo))]
        [XmlElement("visitor", typeof(VisitorCallInfo))]
        public CallInfo CallInfo { get; set; }
    }
}
