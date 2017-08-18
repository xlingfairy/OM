using AsNum.FluentXml;
using CNB.Common;
using Newtonsoft.Json;
using OM.Api.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OM.Api
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseMethod
    {
        private static XmlWriterSettings Setting = new XmlWriterSettings()
        {
            //禁止生成 BOM 字节序
            Encoding = new UTF8Encoding(false),
        };

        /// <summary>
        /// 
        /// </summary>
        public abstract ActionCategories ActionCategory { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string Attribute { get; }



        internal abstract object GetRequestData(ApiClientOption opt);

        #region
        /// <summary>
        /// 
        /// </summary>
        public ResultCodes ResultCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.ResultCode != ResultCodes.Success;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebDiagnoseInfo Diagnose
        {
            get;
            protected set;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public static readonly XNamespace NS = XNamespace.Get("http://www.opentravel.org/OTA/2003/05");


        #region
        protected string Nonce { get; set; }
        protected string Signature { get; set; }
        protected double Timestamp { get; set; }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        public byte[] BuildXmlRequestData(ApiClientOption opt = null)
        {
            var data = this.GetRequestData(opt);
            var root = new
            {
                Auth = new
                {
                    TimeStamp = this.Timestamp,
                    nonce = this.Nonce,
                    Signature = this.Signature
                },
                Data = data.AsElement(this.ActionCategory.ToString())
            };

            var ele = FluentXmlHelper.Build(root, "Request", null);

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), ele);
            using (var stm = new MemoryStream())
            using (var writter = XmlTextWriter.Create(stm, Setting))
            {
                doc.Save(writter);
                writter.Flush();
                return stm.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        public string BuildXmlRequestDataAsString(ApiClientOption opt = null)
        {
            return Encoding.UTF8.GetString(this.BuildXmlRequestData(opt));
        }

        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMethod<T> : BaseMethod where T : BaseResponse
    {


        internal async Task<T> Execute(ApiClient client)
        {
            //重置
            this.ResultCode = ResultCodes.Success;
            this.ErrorMessage = null;

            string result = null;
            try
            {
                result = await this.GetResult(client);
            }
            catch (Exception ex)
            {
                this.ResultCode = ResultCodes.RequestError;
                this.ErrorMessage = ex.GetBaseException().Message;
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                try
                {
                    var rst = await this.ParseResult(result);

                    //API返回的结果包含对方定义的错误
                    if (rst.HasError)
                    {
                        this.ResultCode = ResultCodes.ResponseWithError;
                        this.ErrorMessage = rst.ErrorMsg;
                    }

                    return rst;
                }
                catch (Exception ex)
                {
                    if (!this.HasError)
                    {
                        this.ResultCode = ResultCodes.ParseError;
                    }
                    this.ErrorMessage = string.Format("{0}; {1}", this.ErrorMessage, ex.GetBaseException().Message);
                }
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected virtual async Task<string> GetResult(ApiClient client)
        {
            this.Signature = this.SIG(client.Option, out string nonce, out double timestamp);
            this.Nonce = nonce;
            this.Timestamp = timestamp;

            var xml = this.BuildXmlRequestDataAsString(client.Option);

            var req = new RequestHelper();
            req.RequestHeader.Add("Accept-Encoding", "gzip, deflate");
            req.RequestHeader.Add("SOAPAction", "http://ctrip.com/Request");
            req.RequestHeader.Add("Content-Length", xml.Length.ToString());

            if (client.Option.UseProxy)
                req.Proxy = new WebProxy(client.Option.ProxyAddress);

            var sw = Stopwatch.StartNew();

            var url = client.GetModelUrl(this);
            var bytes = Encoding.UTF8.GetBytes(xml);

            string result = null;
            try
            {
                result = await req.PostAsync(url, origDatas: bytes, contentType: "text/xml; charset=utf-8");
            }
            catch
            {
                throw;
            }
            finally
            {
                sw.Stop();
                this.Diagnose = new WebDiagnoseInfo()
                {
                    SendDatas = xml,
                    Url = url,
                    RequestHeader = JsonConvert.SerializeObject(req.RequestHeader),
                    ResponseBody = result,
                    ResponseTime = sw.ElapsedMilliseconds
                };
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Task<T> TestXml(string xml)
        {
            return this.ParseResult(xml);
        }


        /// <summary>
        /// 对不规整的数据进行修整，使得反序列化可以顺利进行
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string Fix(string result)
        {
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual Task<T> ParseResult(string result)
        {
            result = this.Fix(result);

            var bytes = Encoding.UTF8.GetBytes(result);
            var ser = new XmlSerializer(typeof(T), "");
            using (var msm = new MemoryStream(bytes))
            {
                var o = (T)ser.Deserialize(msm);
                return Task.FromResult(o);
            }
        }

        private string SIG(ApiClientOption opt, out string nonce, out double timestamp)
        {
            nonce = Guid.NewGuid().ToString().ToMD5();
            timestamp = DateTime.Now.ToUnixTimestamp() / 1000;
            var a = $"{opt.Pwd}{nonce}{timestamp}".ToMD5(true);
            return a;
        }
    }
}
