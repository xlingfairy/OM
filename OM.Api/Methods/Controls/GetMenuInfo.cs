﻿using AsNum.FluentXml;
using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Methods.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMenuInfo : BaseMethod<MenuInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 语音菜单的编号	1~50，值为空表示列举所有语音菜单
        /// </summary>
        public int? ID { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Query".AsAttribute(),
                menu = new
                {
                    id = this.ID.AsAttribute()
                }
            };
        }

        protected override Task<MenuInfo> ParseResult(string result)
        {
            result = result.Replace("<Status>", "").Replace("</Status>", "");
            return base.ParseResult(result);
        }
    }
}
