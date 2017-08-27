using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models.Enums;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{

    // TODO
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("Event")]
    public class DTMF : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.DTMF;
    }
}
