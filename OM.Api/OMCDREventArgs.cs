using OM.Api.Models;
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
    public class OMCDREventArgs : EventArgs
    {

        /// <summary>
        /// 
        /// </summary>
        public CDR Data { get; set; }

    }
}
