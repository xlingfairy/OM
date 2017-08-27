using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    public class Error
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("code")]
        public ErrorCodes Code { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [XmlElement("reason")]
        public string Reason { get; set; }
    }
}
