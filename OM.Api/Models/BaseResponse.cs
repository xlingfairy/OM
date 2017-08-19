using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{
    [XmlRoot("Response")]
    public class BaseResponse
    {

        [XmlIgnore]
        public bool HasError { get; set; }

        [XmlIgnore]
        public string ErrorMsg { get; set; }
    }
}
