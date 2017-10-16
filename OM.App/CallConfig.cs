using OM.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OM.App
{
    public class CallConfig : JsonConfigItem
    {
        public override string CfgFile => "Call.json";

        /// <summary>
        /// 拔号前缀
        /// </summary>
        public string Prefix
        {
            get; set;
        }

        /// <summary>
        /// 指定中继
        /// </summary>
        public string Trunk { get; set; }
    }
}
