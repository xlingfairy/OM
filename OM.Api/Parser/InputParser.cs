using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OM.Api.Parser
{
    /// <summary>
    /// 
    /// </summary>
    internal static class InputParser
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IInput Parse(string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                var parser = GetParser(xml);
                if (parser != null)
                    return parser.Parse(xml);
            }

            return null;
        }


        private static IInputParser GetParser(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException("xml");
            try
            {
                var ele = XElement.Parse(xml);

                var name = ele.Name.ToString().ToUpper();
                switch (name)
                {
                    case "EVENT":
                        return new EventParser();
                    case "CDR":
                        return new CDRParser();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}
