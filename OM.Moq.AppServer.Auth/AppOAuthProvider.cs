using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OM.Moq.AppServer.Auth
{

    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string PublicClientId;

        public AppOAuthProvider(string publicClientId)
        {
            this.PublicClientId = publicClientId ?? throw new ArgumentNullException("publicClientId");
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<AppUserManager>();
            //var rep = ServiceHelper.Login(customerNO, oper, context.Password);

            var rep = MoqLogin.Login(context.UserName, context.Password);

            //必须，要不然AJAX登陆时，信息不会返回
            context.Response.Headers.Add("Access-Control-Allow-Headers", new string[] { "*" });
            context.Response.Headers.Add("Access-Control-Allow-Origin", new string[] { "*" });
            context.Response.Headers.Add("Access-Control-Request-Method", new string[] { "*" });

            if (rep == null)
            {
                context.SetError("invalid_grant", "用户名密码不正确");
                return;
            }
            else
            {
                var user = new AppUser(rep);

                var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

                var properties = CreateProperties(user);
                var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // 资源所有者密码凭据未提供客户端 ID。
            if (string.IsNullOrEmpty(context.ClientId))
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId.Equals(this.PublicClientId))
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(AppUser user)
        {
            //客户端用到的 Token 附加内容
            var data = new Dictionary<string, string>
            {
                {"RealName", user.UserInfo.RealName ??"" },
                {"ExID", user.UserInfo.ExtID??""},
                //这样安全不?
                {"IsAdmin", user.UserInfo.IsAdmin.ToString() }
            };
            return new AuthenticationProperties(data);
        }


        /// <summary>
        /// https://stackoverflow.com/questions/26614771/owin-token-authentication-400-bad-request-on-options-from-browser
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }
    }
}
