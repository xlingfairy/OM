using Newtonsoft.Json;
using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace OM.Moq.AppServer
{
    [Authorize]
    public class BaseController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected User GetCurrentUser()
        {
            var json = this.GetClaimValue("User");
            if (!string.IsNullOrWhiteSpace(json))
            {
                var user = JsonConvert.DeserializeObject<User>(json);
                return user;
            }
            throw new Exception();
        }

        [NonAction]
        private string GetClaimValue(string claimKey)
        {
            if ((this.ActionContext.ControllerContext.RequestContext.Principal.Identity is ClaimsIdentity))
            {
                var identity = (ClaimsIdentity)this.ActionContext.ControllerContext.RequestContext.Principal.Identity;
                var claim = identity.FindFirst(claimKey);
                if (claim != null)
                {
                    return claim.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取验证错误
        /// </summary>
        /// <returns></returns>
        protected string ValidationErrors
        {
            get
            {
                return string.Join(";", this.ModelState.SelectMany(e => e.Value.Errors.Select(eE => eE.ErrorMessage)));
            }
        }
    }
}