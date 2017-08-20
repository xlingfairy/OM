using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    /// <summary>
    /// 语音菜单的相关信息，如：配置参数（语音文件、拨号检测长度、按键检查结束符）、转接到该菜单的呼叫信息等。
    /// </summary>
    [XmlRoot("menu")]
    public class MenuInfo
    {

        /// <summary>
        /// 语音菜单的编号，用于查询、配置、转接等操作的判断依据。
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }


        /// <summary>
        /// 语音文件，当呼叫转接到该菜单并接通后，OM会向通话方播放该文件。（只支持dat和pcm格式。）
        /// </summary>
        [XmlElement("voicefile")]
        public string VoiceFile { get; set; }


        /// TODO 非必填
        /// <summary>
        /// 语音文件的播放次数，取值范围 0~50，值为0时循环播放
        /// </summary>
        [XmlElement("repeat")]
        public int RepeatCount { get; set; }


        // TODO 非必填
        /// <summary>
        /// 拨号检测长度，当按键长度达到该长度时，
        /// OM则将已统计到的按键信息 （DTMF事件）推送给应用服务器，并重新开始统计。
        /// </summary>
        [XmlElement("infolength")]
        public int Infolength { get; set; }

        // TODO 非必填
        /// <summary>
        /// Xml 反序列化，不支持 char 类型
        /// </summary>
        [XmlElement("exit")]
        public string _ExitCode { get; set; }

        /// <summary>
        /// 按键检查结束符，当该菜单的通话方一旦拨了该字符后，
        /// OM会立刻将已统计到的按键信息 （DTMF事件）推送给应用服务器，并重新开始统计。
        /// </summary>
        [XmlIgnore]
        public char? ExitCode => this._ExitCode?.FirstOrDefault();


        // TODO 没有实际数据，不知道是不是一个列表
        /// <summary>
        /// 被转接到该语音菜单中的来电
        /// </summary>
        [XmlElement("visitor")]
        public List<VisitorCallInfo> Visitors { get; set; }


        // TODO 没有实际数据，不知道是不是一个列表
        /// <summary>
        /// 这里指被转接到该语音菜单中的去电
        /// </summary>
        [XmlElement("outer")]
        public List<OutCallInfo> Outers { get; set; }
    }
}
