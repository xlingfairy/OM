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
    public enum RingFromTypes
    {

        /// <summary>
        /// 分机呼叫
        /// </summary>
        Ext,

        /// <summary>
        /// 外部呼叫
        /// </summary>
        Visitor,

        /// <summary>
        /// 呼叫电话,然后OM系统回拔
        /// </summary>
        OM,

        /// <summary>
        /// 
        /// </summary>
        Menu
    }
}
