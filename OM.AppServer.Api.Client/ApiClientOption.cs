using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client
{
    public class ApiClientOption
    {

        public string BaseUri { get; set; }


        public bool UseProxy { get; internal set; }


        public string ProxyAddress { get; internal set; }
    }
}
