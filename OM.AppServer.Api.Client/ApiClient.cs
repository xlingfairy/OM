using CNB.Common;
using OM.AppServer.Api.Client.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Api.Client
{

    /// <summary>
    /// 
    /// </summary>
    public class ApiClient
    {

        /// <summary>
        /// 当登陆成功时
        /// </summary>
        public static event EventHandler<LoginedEventArgs> OnLogined = null;

        private static Lazy<ApiClient> Instance = new Lazy<ApiClient>(() => new ApiClient());


        /// <summary>
        /// 是否已经初始化，只会初始化一次
        /// </summary>
        private static bool HasInitilized = false;

        internal ApiClientOption Option
        {
            get;
            private set;
        }

        internal AuthToken AuthToken { get; private set; }

        /// <summary>
        /// 是否登陆
        /// </summary>
        public static bool IsLogined => Instance.Value.AuthToken?.IsValid ?? false;



        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="opt"></param>
        public static void Init(ApiClientOption opt)
        {
            if (opt == null)
                throw new ArgumentNullException("opt");

            if (!HasInitilized)
            {
                Instance.Value.Option = opt;
                HasInitilized = true;
            }
        }

        /// <summary>
        /// 不允许直接实例
        /// </summary>
        private ApiClient()
        {

        }


        /// <summary>
        /// 获取最终请求地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public string GetModelUrl<T>(BaseMethod<T> mth) where T : BaseResponse
        {
            return mth.Model.FixUrl(this.Option.BaseUri);
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(BaseMethod<T> mth) where T : BaseResponse
        {
            return await mth.Execute(Instance.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mth"></param>
        /// <returns></returns>
        public static T Execute<T>(BaseMethod<T> mth) where T : BaseResponse
        {
            return mth.Execute(Instance.Value).Result;
        }


        public static async Task<(bool, string)> Login(string user, string pwd)
        {
            var mth = new Login()
            {
                User = user,
                Pwd = pwd
            };
            var token = await mth.Execute(Instance.Value);
            if (!mth.HasError)
            {
                token.User = user;
                Instance.Value.AuthToken = token;

                if (OnLogined != null)
                {
                    OnLogined.BeginInvoke(null, new LoginedEventArgs()
                    {
                        Token = token
                    },
                    LoginedCallback,
                    null
                    );
                }

                return (true, null);
            }
            return (false, mth.ErrorMessage);
        }

        private static void LoginedCallback(IAsyncResult result)
        {
            var ar = (AsyncResult)result;
            var mth = (EventHandler<LoginedEventArgs>)ar.AsyncDelegate;
            mth.EndInvoke(result);
        }
    }

}
