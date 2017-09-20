using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OM.Api;
using OM.Api.Models.Events;
using OM.Api.Parser;
using Owin;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.SignalR
{
    public static class OMHubHelper
    {

        private static Lazy<IHubContext> HubContext = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<OMHub>());

        private static readonly JsonSerializerSettings JSONSetting = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        public static void ConfigurationOMHub(this IAppBuilder app, RedisScaleoutConfiguration redis = null)
        {
            GlobalHost.HubPipeline.AddModule(new HubExceptionModule());
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new OMUserIDProvider());
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(10);

            if (redis != null)
            {
                //使用 Redis 订阅发布, 可扩展成多个站点
                //GlobalHost.DependencyResolver.UseRedis(new RedisScaleoutConfiguration("127.0.0.1:6379", "_signalR"));
                GlobalHost.DependencyResolver.UseRedis(redis);
            }

            //app.UseCors(CorsOptions.AllowAll);

            var cfg = new HubConfiguration()
            {
                EnableJSONP = true,
                EnableDetailedErrors = true
            };
            app.MapSignalR(cfg);
        }

        /// <summary>
        /// 发送分机事件
        /// </summary>
        /// <param name="extID"></param>
        /// <param name="input"></param>
        public static void Send(string extID, IExtNotify input)
        {
            var str = JsonConvert.SerializeObject(input, JSONSetting);

            HubContext.Value.Clients
                //.All
                .User(extID)
                //暂时没有找到序列化抽象类/接口，而又不引起报错的办法
                //.OnReceiveInput(input);
                //只能发送 json 字符串，然后在客户端解开
                .OnReceiveInput(str);
        }

        /// <summary>
        /// 发送管理员事件
        /// </summary>
        /// <param name="input"></param>
        public static void Send(IAdminNotify input)
        {
            var str = JsonConvert.SerializeObject(input, JSONSetting);

            HubContext.Value.Clients
                .Group("admin")
                .OnReceiveInput(str);
        }
    }
}
