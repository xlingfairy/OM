using AsNum.FluentXml;
using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OM.Api.Methods
{

    /// <summary>
    /// 强拆
    /// </summary>
    public class Clear : BaseMethod<bool>
    {

        /// <summary>
        /// 
        /// </summary>
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 对于来去电，强拆的判断依据是：visitor/outer id；
        /// 双方通话时，不管强拆其中哪一方，该路通话都会被释放。
        /// </summary>
        public int? VisitorID { get; set; }

        /// <summary>
        /// 对于来去电，强拆的判断依据是：visitor/outer id；
        /// 双方通话时，不管强拆其中哪一方，该路通话都会被释放。
        /// </summary>
        public int? OuterID { get; set; }

        /// <summary>
        /// 对于分机，强拆的判断依据是：ext id；
        /// 双方通话时，不管强拆其中哪一方，该路通话都会被释放。
        /// </summary>
        [RegularExpression(@"\d+")]
        public string ExtID { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<ValidationResult> Validate()
        {
            if (string.IsNullOrWhiteSpace(this.ExtID)
                && this.VisitorID == null
                && this.OuterID == null)
                return new List<ValidationResult>() {
                    new ValidationResult("ExtID/OuterID/VisitorID 必须填写其中一个")
                };
            else
                return base.Validate();
        }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Clear".AsAttribute(),
                ext = !string.IsNullOrWhiteSpace(this.ExtID) ? new
                {
                    id = this.ExtID.AsAttribute(),
                } : null,
                visitor = this.VisitorID.HasValue ? new
                {
                    id = this.VisitorID.AsAttribute(),
                } : null,
                outer = this.OuterID.HasValue ? new
                {
                    id = this.OuterID.AsAttribute()
                } : null
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected override async Task<string> GetResult(ApiClient client)
        {
            //该方法返回的 Cotnent-length = 0, 如果执行没有报异常,就算执行成功
            try
            {
                await base.GetResult(client);
            }
            catch (Exception ex)
            {

            }
            return "true";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override Task<bool> ParseResult(string result)
        {
            return Task.FromResult(true);
        }
    }
}
