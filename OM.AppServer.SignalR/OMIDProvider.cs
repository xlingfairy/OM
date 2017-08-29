using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.SignalR
{
    public class OMIDProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.Headers.Get("ExtID");
        }
    }
}
