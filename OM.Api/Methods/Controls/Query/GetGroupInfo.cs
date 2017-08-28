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
    /// 查询分机组的相关信息，如：配置参数（分机成员、呼叫排队时播放的背景音乐、呼叫分配规则）、正在该分机组队列中等待的来电。
    /// </summary>
    public class GetGroupInfo : BaseMethod<GroupInfo>
    {

        /// <summary>
        /// 
        /// </summary>
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 分机组的编号	1~50，值为空时列举所有分机组
        /// </summary>
        public int? ID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Query".AsAttribute(),
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
