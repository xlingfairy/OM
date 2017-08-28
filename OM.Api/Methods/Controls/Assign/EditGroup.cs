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
    /// 配置分机组
    /// 特别注意：
    /// 1) OM系统重启后，分机组内的分机被清除，须重新配置分机组。
    /// 系统重启时，OM会向应用服务器推送BOOTUP事件，可根据该事件判断系统是否重启。
    /// 2) 配置分机组时，只会累加分机不会删除组内之前的分机，建议配置分机组前先清空该分机组
    /// 执行成后后，会返回当前分组的所有分机；如果没有指定 voiceFile ，不会有默认值 
    /// </summary>
    public class EditGroup : BaseMethod<GroupInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 分机组的编号	1~50
        /// </summary>
        [Range(1, 50)]
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VoiceFile { get; set; }

        /// <summary>
        /// 呼叫分配规则 默认值：circular 轮选
        /// </summary>
        public GroupDistributionTypes? DistributionType { get; set; }

        /// <summary>
        /// 分机号码，这里为分机组成员
        /// </summary>
        public IEnumerable<string> Exts { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Assign".AsAttribute(),
                group = new
                {
                    id = this.ID.AsAttribute(),
                    voicefile = this.VoiceFile.AsElement(),
                    distribution = this.DistributionType.AsElement(),
                    exts = this.Exts?.AsElementArray("ext")
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override string Fix(string result)
        {
            return result.Replace("<Status>", "").Replace("</Status>", "");
        }
    }
}