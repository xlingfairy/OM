using OM.AppServer.Api.Client;
using System.Net.Http;

namespace OM.AppServer.Api.Client {

    /// <summary>
    /// 
    /// </summary>
    public interface IContentHandler {

        string ContentType { get; }

        HttpContent GetContent(object data);


        T Parse<T>(ApiClient client, byte[] bytes);

        /// <summary>
        /// 对 HttpClient 进行处理，比如添加 Accept Header 等，以供请求使用
        /// </summary>
        /// <param name="client"></param>
        void SetRequestHttpClient(HttpClient client);
    }
}
