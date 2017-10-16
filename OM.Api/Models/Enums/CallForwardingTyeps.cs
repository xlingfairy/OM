﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    //TODO 还有其它值 ？？
    /// <summary>
    /// 呼叫转移方式
    /// 0: 关闭，1: 全转，2: 遇忙或无应答转
    /// </summary>
    public enum CallForwardingTyeps
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("0")]
        关闭 = 0,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("1")]
        全部转移 = 1,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("2")]
        遇忙或无应答时转移 = 2,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("4")]
        未知 = 4
    }
}
