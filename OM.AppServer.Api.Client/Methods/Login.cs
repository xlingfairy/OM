using CNB.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client.Methods
{
    public class Login : BaseMethod<AuthToken>
    {
        public override HttpMethod HttpMethod => HttpMethod.Post;

        public override string Model => "Token";

        [Param("userName"), Required]
        public string User { get; set; }

        [Param("password"), Required]
        public string Pwd { get; set; }

        [Param("grant_type")]
        private string GrantType => "password";
    }
}
