using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{

    /// <summary>
    /// OM配置变化事件
    /// 当OM的web页面配置发生变化时，OM向应用服务器推送该报告，以便于应用服务器及时更新和同步OM的相关配置。
    /// </summary>
    [XmlRoot("Event")]
    public class ConfigChange : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.CONFIG_CHANGE;

    }
}
