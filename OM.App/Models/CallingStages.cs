using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App.Models
{

    /// <summary>
    /// 呼叫状态
    /// </summary>
    public enum CallingStages
    {
        None = 0,
        /// <summary>
        /// 拔号中
        /// </summary>
        Dailing = 1,

        /// <summary>
        /// 对方已回铃
        /// </summary>
        Alert = 2,

        /// <summary>
        /// 对方已应答
        /// </summary>
        Answered = 3,

        /// <summary>
        /// 通话结束
        /// </summary>
        Bye = 4,

        /// <summary>
        /// 通话中，不知道具体的通话时间，半路获取到的状态。
        /// </summary>
        InTalk = 5,
    }
}
