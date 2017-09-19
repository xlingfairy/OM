using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    public class DivertInfo
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("from")]
        public string From { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("to")]
        public string To { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("reason")]
        public string Reason { get; set; }
    }
}
