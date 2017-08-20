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
    /// 查询指定去电的相关信息，如：去电的编号、主叫方、被叫方、通过的中继号码、通话状态以及通话的相对唯一标识符。
    /// </summary>
    public class GetOutCallInfo : BaseMethod<OutCallInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;


        /// <summary>
        /// 去电的编号,值为空时列举所有去电
        /// </summary>
        public int? ID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Query".AsAttribute(),
                outer = new
                {
                    id = this.ID.AsAttribute()
                }
            };
        }

        protected override Task<OutCallInfo> ParseResult(string result)
        {
            result = result.Replace("<Status>", "").Replace("</Status>", "");
            return base.ParseResult(result);
        }
    }
}
