using OM.Api.Models;
using OM.Api.Models.Events;
using OM.Api.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Parser
{
    /// <summary>
    /// 
    /// </summary>
    internal class CDRParser : IInputParser
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IInput Parse(string xml)
        {
            var bytes = Encoding.UTF8.GetBytes(xml);
            var ser = new XmlSerializer(typeof(CDR));
            using (var msm = new MemoryStream(bytes))
            {
                return (CDR)ser.Deserialize(msm);
            }
        }
    }
}
