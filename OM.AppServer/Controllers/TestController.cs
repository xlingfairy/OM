using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OM.Moq.AppServer.Controllers
{

    [Authorize(Roles = "admin")]
    public class TestController : ApiController
    {

        public string Get()
        {
            return "aaa";
        }

    }
}
