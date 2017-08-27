using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ForEventAttribute : Attribute
    {

        /// <summary>
        /// 
        /// </summary>
        public Type EventType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        public ForEventAttribute(Type eventType)
        {
            this.EventType = eventType;
        }

    }
}
