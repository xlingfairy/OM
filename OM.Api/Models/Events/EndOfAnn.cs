using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models.Enums;
using System.Xml.Serialization;

namespace OM.Api.Models.Events
{
    /// <summary>
    /// 语音文件播放完毕事件
    /// 呼叫转接到语音菜单（menu）后，OM播放menu的语音文件(voicefile)，当语音文件按照指定播放次数播放完毕后，OM向应用服务器推送该事件。
    /// 语音播放过程中，若用户按键，语音停止播放，且OM不推送EndOfAnn事件。
    /// </summary>
    [XmlRoot("Event")]
    public class EndOfAnn : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override EventTypes Attribute => EventTypes.EndOfAnn;

        /// <summary>
        /// 来电转menu
        /// </summary>
        [XmlElement("visitor")]
        public VisitorCallInfo Visitor { get; set; }

        /// <summary>
        /// 去电转menu
        /// </summary>
        [XmlElement("outer")]
        public OutCallInfo Outer { get; set; }

        /// <summary>
        /// 分机转menu
        /// </summary>
        [XmlElement("ext")]
        public IDAttribute Ext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("menu")]
        public IDAttribute Menu { get; set; }
    }
}
