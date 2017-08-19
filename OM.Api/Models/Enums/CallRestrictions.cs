using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 呼叫权限
    /// 0: 内线，1: 市话，
    /// 2: 国内，3: 国际
    /// </summary>
    public enum CallRestrictions
    {

        [XmlEnum("0")]
        内线 = 0,

        [XmlEnum("1")]
        市话 = 1,

        [XmlEnum("2")]
        国内 = 2,

        [XmlEnum("3")]
        国际 = 3
    }
}
