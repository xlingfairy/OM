using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OM.Moq.AppServer.Auth
{
    public static class MoqLogin
    {

        public static User Login(string userName, string pwd)
        {
            if (!string.IsNullOrWhiteSpace(userName)
                && Regex.IsMatch(userName, @"\d+")
                && userName == pwd)
            {
                return new User()
                {
                    ExtID = userName,
                    RealName = "",
                    UserName = userName,
                    IsAdmin = false
                };
            }
            else if (userName == "admin" && pwd == "admin123456789")
            {
                return new User()
                {
                    RealName = "Admin",
                    UserName = "Admin",
                    IsAdmin = true
                };
            }

            return null;
        }

    }
}
