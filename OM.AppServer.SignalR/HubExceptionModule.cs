using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.SignalR
{
    public class HubExceptionModule : HubPipelineModule
    {

        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            base.OnIncomingError(exceptionContext, invokerContext);
        }

        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            return base.OnBeforeIncoming(context);
        }
    }
}
