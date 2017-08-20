using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 通话状态
    /// </summary>
    public enum CallingStats
    {
        /// <summary>
        /// 未设定，API 中未返回
        /// </summary>
        Unknow = 0,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("talk")]
        通话进行中,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("progress")]
        呼叫处理过程中,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("wait")]
        呼叫等待中
    }
}
