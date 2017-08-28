using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum APIFunctions : byte
    {
        /// <summary>
        /// 
        /// </summary>
        关闭 = 0,
        /// <summary>
        /// 
        /// </summary>
        状态监控 = 1,
        /// <summary>
        /// 
        /// </summary>
        来电应答前控制 = 2,
        /// <summary>
        /// 
        /// </summary>
        来电应答后控制 = 4,

        /// <summary>
        /// 
        /// </summary>
        All = 关闭 | 状态监控 | 来电应答前控制 | 来电应答后控制
    }
}
