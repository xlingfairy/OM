using AsNum.FluentXml;
using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Methods.Controls.Query
{

    /// <summary>
    /// 查询指定中继（又称为外线）的相关信息
    /// </summary>
    public class GetTrunkInfo : BaseMethod<TrunkInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 中继/外线ID
        /// </summary>
        public string ID { get; set; }


        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Query".AsAttribute(),
                trunk = new
                {
                    id = this.ID.AsAttribute()
                }
            };
        }


        protected override string Fix(string result)
        {
            return result.Replace("<Status>", "").Replace("</Status>", "");
        }
    }
}
