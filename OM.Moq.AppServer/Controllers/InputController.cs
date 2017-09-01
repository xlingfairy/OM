using OM.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OM.AppServer.Controllers
{
    public class InputController : ApiController
    {

        [HttpPost]
        public async Task Post()
        {
            ApiClient.Execute(await Request.Content.ReadAsStringAsync());
        }

    }
}
