using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using CNB.Common;
using OM.Api.Attributes;
using OM.Api.Parser;

namespace OM.Api.Models.Events
{

    /// <summary>
    /// 
    /// </summary>
    [XmlInclude(typeof(Alert))]
    [XmlInclude(typeof(Answer))]
    [XmlInclude(typeof(Answered))]
    [XmlInclude(typeof(BootUp))]
    [XmlInclude(typeof(Busy))]
    [XmlInclude(typeof(Bye))]
    [XmlInclude(typeof(ConfigChange))]
    [XmlInclude(typeof(Divert))]
    [XmlInclude(typeof(Failed))]
    [XmlInclude(typeof(Idle))]
    [XmlInclude(typeof(Offline))]
    [XmlInclude(typeof(Online))]
    [XmlInclude(typeof(Ring))]
    [XmlInclude(typeof(Transient))]
    [XmlInclude(typeof(Invite))]
    [XmlInclude(typeof(Incomming))]
    [XmlInclude(typeof(DTMF))]
    [XmlInclude(typeof(EndOfAnn))]
    [XmlInclude(typeof(Queue))]
    [XmlRoot("Event")]
    public abstract class BaseEvent : IInput, IAdminNotify
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("attribute")]
        public abstract EventTypes Attribute { get; }

    }
}
