using OM.App.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OM.App.ViewModels
{

    [Regist(InstanceMode.Singleton, ForType = typeof(ISetting))]
    public class CallPrefixSettingViewModel : BaseVM, ISetting
    {
        public string Group => "拔号规则";

        public override string Title => "拔号规则";

        /// <summary>
        /// 拔号前缀(拔号前,先拔哪个号)
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 指定中继
        /// </summary>
        public string Trunk { get; set; }

        public async Task Load()
        {
            await Task.Run(() =>
            {
                var setting = JsonConfig.Get<CallConfig>();
                this.Prefix = setting.Prefix;
                this.Trunk = setting.Trunk;
            });
            this.NotifyOfPropertyChange(() => this.Prefix);
        }

        public async Task Save()
        {
            //if (!string.IsNullOrEmpty(this.Prefix) && Regex.IsMatch(this.Prefix, @"\d+"))
            //{
            var cp = JsonConfig.Get<CallConfig>();
            cp.Prefix = this.Prefix?.Trim();
            cp.Trunk = this.Trunk?.Trim();
            await JsonConfig.Save(cp);
            //}
        }
    }
}
