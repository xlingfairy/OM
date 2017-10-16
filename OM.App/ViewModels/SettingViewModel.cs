using Caliburn.Micro;
using OM.App.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OM.App.ViewModels
{

    [Regist(InstanceMode.Singleton)]
    public class SettingViewModel : BaseVM
    {
        public override string Title => "设置";

        private IEnumerable<ISetting> Settings { get; }

        public Dictionary<string, List<ISetting>> Groups { get; }

        public SettingViewModel()
        {
            var ss = IoC.GetAll<ISetting>();
            this.Settings = ss;
            this.Groups = ss.GroupBy(s => s.Group.ToUpper()).ToDictionary(g => g.Key, g => g.ToList());

            foreach (var s in ss)
                s.Load();
        }

        public void Save()
        {
            foreach (var s in this.Settings)
            {
                s.Save();
            }
        }
    }
}
