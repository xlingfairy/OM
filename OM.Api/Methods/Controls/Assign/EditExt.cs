using AsNum.FluentXml;
using OM.Api.Models;
using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Methods.Controls.Assign
{

    /// <summary>
    /// 配置分机
    /// </summary>
    public class EditExt : BaseMethod<ExtInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 分机的线路编号 注：通过查询设备信息可获取线路编号
        /// </summary>
        [Required]
        public string LineID { get; set; }

        /// <summary>
        /// 分机号码 纯数字非空字符串
        /// </summary>
        [RegularExpression(@"\d+", ErrorMessage = "分机号码只能是纯数字非空字符串"), Required]
        public string ID { get; set; }

        /// <summary>
        /// 工号，分机接通前会向来电方播放该工号
        /// 纯数字字符串，值为空时不生效
        /// </summary>
        [RegularExpression(@"\d+", ErrorMessage = "工号只能是纯数字字符串")]
        public string Staffid { get; set; }

        /// <summary>
        /// 分机所属的分机组
        /// 同一分机可属多个分机组	1~50
        /// </summary>
        public List<int> Groups { get; set; }

        /// <summary>
        /// 语音文件，这里为分机队列中排队等待时播放的语音文件
        /// </summary>
        public string VoiceFile { get; set; }

        /// <summary>
        /// 员工的电子邮件地址(暂无对应功能)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 呼叫权限
        /// </summary>
        public CallRestrictions? CallRestriction { get; set; }

        /// <summary>
        /// 代接权限，是否允许来电被其它分机代接
        /// Yes:允许 no:不允许
        /// </summary>
        public bool? AllowPickup { get; set; }

        /// <summary>
        /// 免打扰功能开关（注：该分机为总机时无效），开启免打扰后分机将屏蔽所有来电，但能主动发起呼叫
        /// yes:开启 no:关闭
        /// </summary>
        public bool? IsNoDisturb { get; set; }

        /// <summary>
        /// 呼叫转移方式(注：该分机为总机时无效)
        /// </summary>
        public CallForwardingTyeps? ForwardingType { get; set; }

        /// <summary>
        /// 呼叫转移号码（注：该分机为总机时无效） 值为空时关闭
        /// </summary>
        public string ForwardingNumber { get; set; }

        /// <summary>
        /// 同振号码 值为空时关闭
        /// </summary>
        public string ForkNumber { get; set; }

        /// <summary>
        /// 分机绑定的手机号，该手机号可作为分机同振、或呼叫转移时的缺省配置  值为空时关闭
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 实时录音功能开关
        /// on: 开启，off: 关闭，默认值为off
        /// </summary>
        public bool? IsRealtimeRecord { get; set; }

        /// <summary>
        /// IP分机点击拨号是否自动应答
        /// 注：该参数仅对IP分机有效
        /// 且需要IP话机支持:如，迅时、方位等大部分品牌的话机
        /// yes:是，no:否 默认值为no
        /// </summary>
        public bool? IsAutoAnswer { get; set; }


        //TODO
        /// <summary>
        /// API的功能开关
        /// </summary>
        public APIFunctions? APIFunction { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Assign".AsAttribute(),
                ext = new
                {
                    lineid = this.LineID.AsAttribute(),
                    id = this.ID,
                    staffid = this.Staffid,
                    mobile = this.Mobile,
                    groups = this.Groups?.AsElementArray("group"),
                    voicefile = this.VoiceFile,
                    email = this.Email,
                    Call_Restriction = (int)this.CallRestriction,
                    Call_Pickup = this.AllowPickup.ToYesOrNo(),
                    No_Disturb = this.IsNoDisturb.ToYesOrNo(),
                    Fwd_Type = (int?)this.ForwardingType,
                    Fwd_Number = this.ForwardingNumber,
                    Fork = this.ForkNumber,
                    record = this.IsRealtimeRecord.ToOnOrOff(),
                    autoAnswer = this.IsAutoAnswer.ToYesOrNo(),
                    api = this.APIFunction.ToInt()
                }
            };
        }

        protected override string Fix(string result)
        {
            return result.Replace("<Status>", "").Replace("</Status>", "");
        }
    }
}
