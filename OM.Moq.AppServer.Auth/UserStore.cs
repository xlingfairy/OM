using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace OM.Moq.AppServer.Auth
{

    class UserStore : IUserStore<AppUser, int>, IUserPasswordStore<AppUser, int>
    {

        #region IUserStore
        public Task CreateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Task<AppUser> FindByIdAsync(int userId)
        {
            return Task.FromResult<AppUser>(null);
        }


        public Task<AppUser> FindByNameAsync(string userName)
        {
            //return Task.FromResult<AppUser>(null);
            return Task.FromResult<AppUser>(new AppUser(0, userName));
        }

        public Task<string> GetPasswordHashAsync(AppUser user)
        {
            return Task.FromResult<string>(null);
        }

        public Task<bool> HasPasswordAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
