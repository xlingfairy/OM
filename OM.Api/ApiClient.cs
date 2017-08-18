using OM.Api.Models;
using System;
using System.Threading.Tasks;

namespace OM.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiClient
    {

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
            return $"{this.Option.BaseUri}/Hotel/{mth.Attribute}.asmx";
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

        public static T Execute<T>(BaseMethod<T> mth) where T: BaseResponse
        {
            return mth.Execute(Instance.Value).Result;
        }
    }
}
