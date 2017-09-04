using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using OM.Api.Entity;

namespace OM.AppServer.Api.Client.Methods
{
    public class GetExts : BaseMethod<ListResult<ExtInfo>>
    {
        public override string Model => "Query/Exts";

        public override HttpMethod HttpMethod => HttpMethod.Get;
    }
}
