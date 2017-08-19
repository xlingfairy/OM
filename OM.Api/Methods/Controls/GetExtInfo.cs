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
    /// 查询指定分机的相关信息
    /// </summary>
    public class GetExtInfo : BaseMethod<ExtInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 分机号
        /// </summary>
        public string ID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Query".AsAttribute(),
                ext = new
                {
                    id = this.ID.AsAttribute()
                }
            };
        }

        protected override Task<ExtInfo> ParseResult(string result)
        {
            result = result.Replace("<Status>", "").Replace("</Status>", "");
            return base.ParseResult(result);
        }
    }
}
