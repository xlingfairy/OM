using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models;
using System.ComponentModel.DataAnnotations;
using AsNum.FluentXml;

namespace OM.Api.Methods.Controls
{

    // TODO 未调试通过
    /// <summary>
    /// 
    /// </summary>
    public class Hold : BaseMethod<bool>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;


        /// <summary>
        /// 分机号码
        /// </summary>
        [RegularExpression(@"\d+"), Required]
        public string ID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Hold".AsAttribute(),
                ext = new
                {
                    id = this.ID.AsAttribute()
                }
            };
        }
    }
}
