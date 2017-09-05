using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OM.AppServer.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OM.Moq.AppServer.Auth
{

    public class AppUser : IUser<int>
    {
        public int Id
        {
            get;
        } = 0;

        public User UserInfo { get; }

        public string UserName { get; set; }

        public AppUser(User user)
        {
            this.UserInfo = user;
            this.UserName = user.UserName;
        }

        public AppUser(int v1, string userName)
        {
            this.Id = Id;
            this.UserName = userName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, string authenticationType)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            //服务器端用到的
            userIdentity.AddClaim(new Claim("User", JsonConvert.SerializeObject(this.UserInfo)));
            //
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, this.UserInfo.IsAdmin ? "admin" : "ext"));
            return userIdentity;
        }
    }
}
