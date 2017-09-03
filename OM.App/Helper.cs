using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using OM.App.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OM.App
{
    public static class Helper
    {

        /// <summary>
        /// 注册ViewModel
        /// </summary>
        /// <param name="_container"></param>
        public static void RegistInstances(this SimpleContainer container, Assembly asm)
        {
            var types = asm.DefinedTypes
                .Select(t =>
                {
                    var attr = t.GetCustomAttribute<RegistAttribute>();
                    return new
                    {
                        T = t,
                        Mode = attr?.Mode,
                        TargetType = attr?.ForType
                    };
                })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types)
            {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton)
                {
                    container.RegisterSingleton(t.TargetType ?? type, null, type);
                }
                else if (t.Mode == InstanceMode.PreRequest)
                {
                    container.RegisterPerRequest(t.TargetType ?? type, null, type);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="asms"></param>
        public static void RegistInstances(this SimpleContainer container, IEnumerable<Assembly> asms)
        {
            foreach (var asm in asms)
                container.RegistInstances(asm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async static void ShowAsDialog<T>(this T vm, Func<T, bool> canClose = null) where T : BaseVM
        {
            var view = ViewLocator.LocateForModel(vm, null, null);
            ViewModelBinder.Bind(vm, view, null);

            //事件处理器，如果使用异步， 
            DialogClosingEventHandler close = (sender, args) =>
            {
                if (canClose != null && !canClose.Invoke(vm))
                {
                    args.Cancel();
                }
            };

            await DialogHost.Show(view, close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="canClose"></param>
        public async static void ShowAsDialog<T>(this T vm, Func<T, Task<bool>> canClose = null) where T : BaseVM
        {
            var view = ViewLocator.LocateForModel(vm, null, null);
            ViewModelBinder.Bind(vm, view, null);

            //DialogClosingEventHandler close = async (sender, args) =>
            //{
            //    args.Cancel();
            //    if (canClose != null && !await canClose.Invoke(vm))
            //    {
            //        args.Cancel();
            //    }
            //    else
            //    {
            //        //Session.Close 调用 DialogHost 实例的 Close 方法，实现的 Close 方法里面又调用 Session.Close 方法，
            //        //造成了死循环
            //        args.Session.Close();
            //    }
            //};

            DialogClosingEventHandler close = (sender, args) =>
            {
                if (canClose != null)
                {
                    // 不能直接使用 async / await
                    var task = Task.Run(() => canClose.Invoke(vm));
                    Task.WaitAll(task);
                    if (!task.Result)
                        args.Cancel();
                }
            };

            await DialogHost.Show(view, close);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="obj"></param>
        /// <param name="ps"></param>
        public static void NotifyOfPropertyChange(this PropertyChangedBase obj, params Expression<Func<object>>[] ps)
        {
            foreach (var p in ps)
            {
                obj.NotifyOfPropertyChange(p);
            }
        }
    }
}
