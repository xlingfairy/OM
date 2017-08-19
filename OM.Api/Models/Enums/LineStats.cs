using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 线路状态
    /// </summary>
    public enum LineStats
    {
        /// <summary>
        /// 空闲可用
        /// </summary>
        [XmlEnum("ready")]
        空闲可用,

        /// <summary>
        /// 振铃、回铃或通话中
        /// </summary>
        [XmlEnum("active")]
        振铃_回铃或通话中,

        /// <summary>
        /// 模拟分机摘机后等待拨号以及拨号过程中
        /// </summary>
        [XmlEnum("progress")]
        Progress,

        /// <summary>
        /// IP分机离线
        /// </summary>
        [XmlEnum("offline")]
        IP分机离线,

        /// <summary>
        /// 模拟分机听催挂音时的状态
        /// </summary>
        [XmlEnum("offhook")]
        Offhook
    }
}
