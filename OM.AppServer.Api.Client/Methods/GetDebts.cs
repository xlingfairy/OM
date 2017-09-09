using OM.Moq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using CNB.Common.Attributes;

namespace OM.AppServer.Api.Client.Methods
{
    public class GetDebts : BaseMethod<ListResponse<DebtInfo>>
    {
        public override string Model => "Debts/Search";

        public override HttpMethod HttpMethod => HttpMethod.Post;

        [Param]
        public int Page { get; set; }

        [Param]
        public int PageSize { get; set; } = 20;
    }
}
