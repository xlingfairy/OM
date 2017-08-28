using AsNum.FluentXml;
using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Methods.Controls.Assign
{
    /// <summary>
    /// 清空分机组
    /// </summary>
    public class ClearGroup : BaseMethod<GroupInfo>
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

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Assign".AsAttribute(),
                group = new
                {
                    id = this.ID.AsAttribute()
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
