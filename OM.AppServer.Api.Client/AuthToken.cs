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
        public string LoginedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }

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
    }
}
