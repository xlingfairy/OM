using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models;
using System.ComponentModel.DataAnnotations;
using AsNum.FluentXml;
using OM.Api.Models.Events;

namespace OM.Api.Methods.Transfer
{

    /// <summary>
    /// 分机呼外部电话
    /// </summary>
    public class CallOuter : BaseMethod<Transient>
    {
        public override ActionCategories ActionCategory => ActionCategories.Transfer;

        /// <summary>
        /// 分机号码
        /// </summary>
        [RegularExpression(@"\d+"), Required]
        public string ExtID { get; set; }

        /// <summary>
        /// 中继号码，可以为IP中继或者模拟中继号码, 可空
        /// </summary>
        [RegularExpression(@"\d+")]
        public string TrunkID { get; set; }

        /// <summary>
        /// 外部号码
        /// </summary>
        [RegularExpression(@"\d+"), Required]
        public string OuterNumber { get; set; }

        /// <summary>
        /// 加前缀外呼，指定从模拟外线或IP外线呼出，此时需配置设备的拨号规则为“外线加前缀，内线直拨”。
        /// 可空
        /// </summary>
        [RegularExpression(@"\d+")]
        public string Prefix { get; set; }

        /// <summary>
        /// 二次拨号时，“,,1”表示呼叫接通后，主叫自动按键“1”，
        /// “,,”后可再加逗号来延迟按键时间，
        /// 一个逗号延迟1秒（目前版本V114，只有通过IP外线外呼支持，通过模拟外线外呼不支持，待后续修改）
        /// </summary>
        [RegularExpression(@"\d+")]
        public string Number2 { get; set; }

        /// <summary>
        /// 二次拔号延时（秒），默认2秒
        /// </summary>
        public int Number2DelaySeconds { get; set; } = 2;

        internal override object GetRequestData(ApiClientOption opt)
        {
            var lst = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.Prefix))
                lst.Add(this.Prefix);
            lst.Add(this.OuterNumber);
            if (!string.IsNullOrWhiteSpace(this.Number2))
                lst.Add(Number2);

            var to = string.Join(",", lst.ToArray());

            return new
            {
                attribute = "Connect".AsAttribute(),
                ext = new
                {
                    id = this.ExtID.AsAttribute()
                },
                outer = new
                {
                    to = to.AsAttribute()
                },
                trunk = !string.IsNullOrWhiteSpace(this.TrunkID) ? new
                {
                    id = this.TrunkID.AsAttribute()
                } : null
            };
        }
    }
}
