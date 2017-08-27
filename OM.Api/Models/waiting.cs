using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Waiting
    {

        /// <summary>
        /// 分机组的唯一标识，不同型号设备起始值不同，最多能添加50个分机组（注意跟分机组的编号区分）
        /// </summary>
        [XmlAttribute("group")]
        public int GroupID { get; set; }

        /// <summary>
        /// 在队列中排队等待的个数，取值范围1~30，超过30个后不再排队自动挂断
        /// </summary>
        [XmlAttribute("count")]
        public int Count { get; set; }

        /// <summary>
        /// offline：表示组内分机全离线；full：表示组内分机全忙
        /// </summary>
        [XmlAttribute("reason")]
        public string Reason { get; set; }
    }
}
