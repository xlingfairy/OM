using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{

    [XmlRoot("Event")]
    public class TransientEvent : BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("outer")]
        public OutCallInfo Outer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public ExtDeviceItem Ext { get; set; }
    }
}
