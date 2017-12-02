using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using CNB.Common.Attributes;
using OM.AppServer.Entity;

namespace OM.AppServer.Api.Client.Methods
{
    public class GetDebtNotes : BaseMethod<ListResponse<DebtNote>>
    {
        public override string Model => "Debts/Notes";

        public override HttpMethod HttpMethod => HttpMethod.Get;

        [Param("ID")]
        public long DebtID { get; set; }
    }
}
