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
        成功 = 0,

        /// <summary>
        /// 
        /// </summary>
        参数验证失败,

        /// <summary>
        /// 
        /// </summary>
        失败,

        /// <summary>
        /// 
        /// </summary>
        错误,

        /// <summary>
        /// 请求错误
        /// </summary>
        请求错误,

        /// <summary>
        /// 数据解析错误
        /// </summary>
        数据解析错误,


        /// <summary>
        /// 返回的结果中包含对方定义的错误
        /// </summary>
        预定义的错误,

        /// <summary>
        /// 授权失败
        /// </summary>
        授权失败,

        /// <summary>
        /// 未知错误
        /// </summary>
        未知错误
    }
}
