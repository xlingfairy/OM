using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.SignalR
{
    public class OMUserIDProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            // AppUser.cs GenerateUserIdentityAsync 方法自定义 EXT_ID
            var claim = ((ClaimsPrincipal)request.User).Claims.FirstOrDefault(c => c.Type == "EXT_ID");
            return claim?.Value ?? request.User.Identity.Name;
        }
    }
}
