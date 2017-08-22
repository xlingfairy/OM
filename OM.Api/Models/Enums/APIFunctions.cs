using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Models.Enums
{

    [Flags]
    public enum APIFunctions : byte
    {
        关闭 = 0,
        状态监控 = 1,
        来电应答前控制 = 2,
        来电应答后控制 = 4,

        All = 关闭 | 状态监控 | 来电应答前控制 | 来电应答后控制
    }
}
