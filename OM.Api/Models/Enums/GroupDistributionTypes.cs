using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 分机组呼叫分配规则
    /// </summary>
    public enum GroupDistributionTypes
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("circular")]
        轮选 = 0,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("sequential")]
        顺选,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("group")]
        群振

    }
}
