﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{
    /// <summary>
    /// 通话记录报告
    /// </summary>
    public enum CDRTypes
    {
        /// <summary>
        /// IN
        /// </summary>
        [XmlEnum("IN")]
        呼入 = 0,

        /// <summary>
        /// OU
        /// </summary>
        [XmlEnum("OU")]
        呼出 = 1,

        /// <summary>
        /// FI
        /// </summary>
        [XmlEnum("FI")]
        呼叫转移入 = 2,

        /// <summary>
        /// FW
        /// </summary>
        [XmlEnum("FW")]
        呼叫转移出 = 3,

        /// <summary>
        /// LO
        /// </summary>
        [XmlEnum("LO")]
        内部呼叫 = 4,

        /// <summary>
        /// CB
        /// </summary>
        [XmlEnum("CB")]
        双向外呼 = 5
    }
}
