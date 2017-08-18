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
        public bool HasError { get; internal set; }
        public string ErrorMsg { get; internal set; }
    }
}
