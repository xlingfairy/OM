using AsNum.FluentXml;
using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Methods.Controls
{
    /// <summary>
    /// 查询指定来电的相关信息，如：来电的属性参数(编号、原始主叫、原始被叫、通话状态、相对唯一标识符)、来电的通话方、呼叫状态
    /// </summary>
    public class GetVisitorCallInfo : BaseMethod<VisitorCallInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 来电的编号,值为空时列举所有来电
        /// </summary>
        public int? ID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Query".AsAttribute(),
                visitor = new
                {
                    id = this.ID.AsAttribute()
                }
            };
        }

        protected override Task<VisitorCallInfo> ParseResult(string result)
        {
            result = result.Replace("<Status>", "").Replace("</Status>", "");
            return base.ParseResult(result);
        }
    }
}
