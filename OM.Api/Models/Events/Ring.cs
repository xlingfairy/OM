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
    /// 振铃事件
    /// 当分机振铃时，OM向应用服务器推送该事件报告。
    /// </summary>
    [XmlRoot("Event"), Serializable]
    public class Ring : BaseEvent, IExtNotify
    {

        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.RING;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ext")]
        public List<IDAttribute> _Ext { get; set; }

        /// <summary>
        /// 当前分机
        /// </summary>
        public IDAttribute Ext => this._Ext.First();

        /// <summary>
        /// 分机呼分机，分机振铃
        /// </summary>
        public IDAttribute FromExt => this._Ext?.ElementAt(1);

        /// <summary>
        /// 通过API实现分机外呼，且呼叫方式为 “先呼被叫，被叫回铃再呼主叫” 时，主叫分机振铃：
        /// </summary>
        [XmlElement("outer")]
        public OutCallInfo Outer { get; set; }

        /// <summary>
        /// 来电呼分机，分机振铃
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }


        /// <summary>
        /// menu呼分机，分机振铃：
        /// </summary>
        [XmlElement("menu")]
        public IDAttribute Menu { get; set; }

        string IExtNotify.ExtID => this.Ext.ID;
    }
}
