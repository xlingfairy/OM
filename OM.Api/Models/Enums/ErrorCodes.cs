using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 
    /// </summary>
    public enum ErrorCodes
    {

        /// <summary>
        /// 
        /// </summary>
        Unknow = 0,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("1")]
        失败 = 1,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("2")]
        主叫的呼叫权限受限 = 2,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("3")]
        被叫分机不在线 = 3,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("4")]
        被叫分机当前正在和其他终端通话 = 4,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("5")]
        中继线资源不足_无法执行外呼 = 5,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("6")]
        被叫分机忙线 = 6,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("7")]
        分机不存在 = 7,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("8")]
        主叫分机自身不在线
    }
}
