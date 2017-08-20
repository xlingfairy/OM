using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{
    /// <summary>
    /// 中继的线路状态
    /// </summary>
    public enum TrunkStats
    {

        [XmlEnum("ready")]
        可用,
        [XmlEnum("active")]
        摘机振铃或通话中,

        [XmlEnum("unwired")]
        未接线,

        [XmlEnum("offline")]
        离线

    }
}
