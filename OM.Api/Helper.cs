using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OM.Api
{
    public static class Helper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToYesOrNo(this bool? value)
        {
            if (value == null)
                return null;
            else if (value.Value)
                return "yes";
            else
                return "no";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToOnOrOff(this bool? value)
        {
            if (value == null)
                return null;
            else if (value.Value)
                return "on";
            else
                return "off";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int ToInt(this APIFunctions? func)
        {
            var v = 0;
            if ((func & APIFunctions.状态监控) == APIFunctions.状态监控)
                v += 7;
            if ((func & APIFunctions.来电应答前控制) == APIFunctions.来电应答前控制)
                v += 10;
            if ((func & APIFunctions.来电应答后控制) == APIFunctions.来电应答后控制)
                v += 20;
            return v;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        public static APIFunctions ToApiFunctions(int? api)
        {
            if (api == 0)
                return APIFunctions.关闭;
            else if (api == 7)
                return APIFunctions.状态监控;
            else if (api == 10)
                return APIFunctions.来电应答前控制;
            else if (api == 20)
                return APIFunctions.来电应答后控制;
            else if (api == 17)
                return APIFunctions.状态监控 | APIFunctions.来电应答前控制;
            else if (api == 27)
                return APIFunctions.状态监控 | APIFunctions.来电应答后控制;
            else if (api == 37)
                return APIFunctions.状态监控 | APIFunctions.来电应答前控制 | APIFunctions.来电应答后控制;
            else
                return APIFunctions.关闭;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string str)
        {
            var regex = new Regex(@"_?(?<a>\w)(?<b>[^_]+)");
            return regex.Replace(str, ma =>
            {
                return $"{ma.Groups["a"].Value.ToUpper()}{ma.Groups["b"].Value.ToLower()}";
            });
        }
    }
}
