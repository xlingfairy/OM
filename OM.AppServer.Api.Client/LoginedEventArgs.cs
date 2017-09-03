using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client
{

    /// <summary>
    /// 
    /// </summary>
    public class LoginedEventArgs : EventArgs
    {

        public AuthToken Token { get; set; }

    }
}
