using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client
{
    public class BaseResponse
    {

        //public virtual string ErrorCode { get; set; }

        [JsonProperty("Msg")]
        public virtual string Message { get; set; }

        [JsonProperty("IsSuccess")]
        public virtual bool IsSuccess { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Result
        {
            get;
            set;
        }
    }

    public class DataResponse<T> : BaseResponse<T>
    {
    }

    public class ListResponse<T> : BaseResponse<IEnumerable<T>>
    {
        public long Total
        {
            get;
            set;
        }
    }
}
