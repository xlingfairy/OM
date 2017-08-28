using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputParser
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IInput Parse(string input);

    }
}
