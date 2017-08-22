using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{

    [XmlRoot("ext")]
    public class ExtInfo
    {

        /// <summary>
        /// 分机号
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// 分机的线路编号，是分机的唯一固定标识
        /// </summary>
        [XmlElement("lineid")]
        public string LineID { get; set; }

        /// <summary>
        /// 工号，分机接通前会向来电方播放该工号
        /// </summary>
        [XmlElement("staffid")]
        public string StaffID { get; set; }

        /// <summary>
        /// 分机组，这里为该分机所属的分机组
        /// </summary>
        [XmlElement("group")]
        public List<Group> Groups { get; set; }

        /// <summary>
        /// 语音文件，这里为分机队列中排队等待时播放的语音文件
        /// 只支持.dat和.pcm格式
        /// </summary>
        [XmlElement("voicefile")]
        public string VoiceFile { get; set; }

        /// <summary>
        /// 员工的电子邮件地址(暂无对应功能)
        /// </summary>
        [XmlElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// 呼叫权限
        /// </summary>
        [XmlElement("Call_Restriction")]
        public CallRestrictions CallRestriction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Call_Pickup")]
        public string _Call_Pickup { get; set; }

        /// <summary>
        /// 代接权限，是否允许来电被其它分机代接
        /// </summary>
        [XmlIgnore]
        public bool AllowCallPickup => string.Equals("yes", this._Call_Pickup, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("No_Disturb")]
        public string _No_Disturb { get; set; }

        /// <summary>
        /// 是否开启了免打扰功能，开启免打扰后分机将屏蔽所有来电，但能主动发起呼叫
        /// </summary>
        [XmlIgnore]
        public bool IsNoDisturb => string.Equals(this._No_Disturb, "on", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 呼叫转移方式
        /// </summary>
        [XmlElement("Fwd_Type")]
        public CallForwardingTyeps ForwardingType { get; set; }

        /// <summary>
        /// 呼叫转移号码
        /// </summary>
        [XmlElement("Fwd_Number")]
        public string ForwardingNumber { get; set; }

        /// <summary>
        /// 同振号码, 值为空时关闭
        /// </summary>
        [XmlElement("fork")]
        public string ForkNumber { get; set; }

        /// <summary>
        /// 分机绑定的手机号，该手机号可作为呼叫转移、离线转移的缺省配置
        /// </summary>
        [XmlElement("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("record")]
        public string _record { get; set; }

        /// <summary>
        /// 是否实时录音
        /// </summary>
        public bool IsRealtimeRecord => string.Equals("on", this._record, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 线路状态
        /// </summary>
        [XmlElement("state")]
        public ExtStats State { get; set; }


        /// <summary>
        /// 通话信息
        /// </summary>
        [XmlElement("outer", typeof(OutCallInfo))]
        [XmlElement("visitor", typeof(VisitorCallInfo))]
        public CallInfo CallInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("api")]
        public int? _api { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public APIFunctions APIFunction => Helper.ToApiFunctions(this._api);
    }
}
