using OM.Api.Models.Enums;
using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class OMEventEventArgs : EventArgs
    {

        /// <summary>
        /// 
        /// </summary>
        public EventTypes Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BaseEvent Data { get; set; }
    }
}
