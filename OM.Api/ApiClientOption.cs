namespace OM.Api
{

    /// <summary>
    /// 
    /// </summary>
    public class ApiClientOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string BaseUri { get; set; }


        /// <summary>
        /// API Password
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 是否使用代理
        /// </summary>
        public bool UseProxy { get; set; }

        /// <summary>
        /// 代理地址
        /// </summary>
        public string ProxyAddress { get; set; }
    }
}
