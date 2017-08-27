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
    public enum CDRRouteTypes
    {
        //IP（IP中继）/XO（模拟中继）/IC（内部）/OP（总机

        /// <summary>
        /// IP
        /// </summary>
        [XmlEnum("IP")]
        IP中继,

        /// <summary>
        /// XO
        /// </summary>
        [XmlEnum("XO")]
        模拟中继,

        /// <summary>
        /// IC
        /// </summary>
        [XmlEnum("IC")]
        内部,

        /// <summary>
        /// OP
        /// </summary>
        [XmlEnum("OP")]
        总机
    }
}
