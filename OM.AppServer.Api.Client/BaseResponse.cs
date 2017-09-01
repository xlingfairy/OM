using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client
{
    public class BaseResponse
    {

        public virtual string ErrorCode { get; set; }

        public virtual string ErrorMsg { get; set; }
    }
}
