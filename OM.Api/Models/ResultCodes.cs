using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Models
{
    public enum ResultCodes
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 
        /// </summary>
        Fail,

        /// <summary>
        /// 
        /// </summary>
        Error,

        /// <summary>
        /// 请求错误
        /// </summary>
        RequestError,

        /// <summary>
        /// 数据解析错误
        /// </summary>
        ParseError,


        /// <summary>
        /// 返回的结果中包含对方定义的错误
        /// </summary>
        ResponseWithError,

        /// <summary>
        /// 未知错误
        /// </summary>
        Unknow
    }
}
