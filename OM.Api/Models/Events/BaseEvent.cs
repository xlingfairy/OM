using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{

    public abstract class BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("attribute")]
        public EventTypes Attribute { get; set; }

    }
}
