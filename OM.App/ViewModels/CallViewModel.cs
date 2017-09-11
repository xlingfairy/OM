using OM.App.Attributes;
using OM.Moq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class CallViewModel : BaseVM
    {
        public override string Title => "呼叫";

        public DebtInfo Data { get; set; }


    }
}
