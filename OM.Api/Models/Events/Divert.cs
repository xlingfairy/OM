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
    /// 呼叫转移事件
    /// </summary>
    [XmlRoot("Event")]
    public class Divert : BaseEvent, IExtNotify
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.DIVERT;

        /// <summary>
        /// 因分机设置了呼叫转移等原因，导致该呼叫被转移
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }

        ///// <summary>
        ///// 因被叫分机设置了呼叫转移等原因，导致该呼叫被转移
        ///// </summary>
        //[XmlElement("ext")]
        //public IDAttribute Ext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("divert")]
        public DivertInfo DivertInfo { get; set; }

        string IExtNotify.ExtID => this.DivertInfo?.To;
    }
}
