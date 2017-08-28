using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models;
using AsNum.FluentXml;
using System.ComponentModel.DataAnnotations;

namespace OM.Api.Methods.Transfer
{
    /// <summary>
    /// 
    /// </summary>
    public class CallExt : BaseMethod<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public override ActionCategories ActionCategory => ActionCategories.Transfer;

        /// <summary>
        /// 主叫分机
        /// </summary>
        [RegularExpression(@"\d+"), Required]
        public string FromID { get; set; }

        /// <summary>
        /// 被叫分机
        /// </summary>
        [RegularExpression(@"\d+"), Required]
        public string ToID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Connect".AsAttribute(),
                ext = new
                {
                    id = this.FromID.AsAttribute(),
                },
                ext2 = new
                {
                    id = this.ToID.AsAttribute()
                }.AsElement("ext")
            };
        }
    }
}
