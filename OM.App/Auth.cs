using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App
{
    public static class Auth
    {

        public static string CurrentUser { get; private set; }

        public static Token Token { get; private set; }

        //public static async Task<bool> Login(string user, string pwd)
        //{

        //}
    }
}
