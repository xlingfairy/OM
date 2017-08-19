using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{

    /// <summary>
    /// 分机组，这里为该分机所属的分机组
    /// </summary>
    public class Group
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }

    }
}
