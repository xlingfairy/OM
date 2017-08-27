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
    /// 系统启动事件
    /// 当OM系统启动完成后，OM会向应用服务器推送该报告。
    /// </summary>
    [XmlRoot("Event")]
    public class BootUp : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.BOOTUP;

    }
}
