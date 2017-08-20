using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    /// <summary>
    /// 分机组的相关信息，如：配置参数（分机成员、呼叫排队时播放的背景音乐、呼叫分配规则）、正在该分机组队列中等待的来电。
    /// </summary>
    [XmlRoot("group")]
    public class GroupInfo
    {

        /// <summary>
        /// 分机组的序号
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }


        /// <summary>
        /// 语音文件，支持dat和pcm两种格式，这里为呼叫等待时播放的音乐
        /// </summary>
        [XmlElement("voicefile")]
        public string VoiceFile { get; set; }

        /// <summary>
        /// 呼叫分配规则, 默认值：circular
        /// </summary>
        [XmlElement("distribution")]
        public GroupDistributionTypes DistributionType { get; set; }

        /// <summary>
        /// 分机组成员
        /// </summary>
        [XmlElement("ext")]
        public List<ExtDeviceItem> Exts { get; set; }

        /// <summary>
        /// 正处于该分机组等待队列中的来电
        /// </summary>
        [XmlElement("visitor")]
        public List<VisitorCallInfo> Visitors { get; set; }
    }
}
