using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OM.Api.Models;
using AsNum.FluentXml;

namespace OM.Api.Methods.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class GetDeviceInfo : BaseMethod<DeviceInfo>
    {
        public override ActionCategories ActionCategory => ActionCategories.Control;

        internal override object GetRequestData(ApiClientOption opt)
        {
            object o = null;
            return new
            {
                attribute = "Query".AsAttribute(),
                DeviceInfo = o.AsElement().SetNullVisible(true)
            };
        }
    }
}
