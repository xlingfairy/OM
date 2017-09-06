using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client
{
    public class AuthToken : BaseResponse
    {

        public string User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        //public string token_type { get; set; }
        //public int expires_in { get; set; }
        public string RealName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(".issued")]
        public DateTime LoginedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(".expires")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("error")]
        public override string ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("error_description")]
        public override string ErrorMsg { get; set; }


        public bool IsAdmin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(this.AccessToken)
                                && this.ExpiresAt > DateTime.Now;
    }
}
