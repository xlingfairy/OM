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
            Indent = true
        };

        /// <summary>
        /// 
        /// </summary>
        public abstract ActionCategories ActionCategory { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
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
                return this.ResultCode != ResultCodes.成功;
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
    public abstract class BaseMethod<T> : BaseMethod
    {


        internal async Task<T> Execute(ApiClient client)
        {
            //重置
            this.ResultCode = ResultCodes.成功;
            this.ErrorMessage = null;

            string result = null;
            try
            {
                result = await this.GetResult(client);
            }
            catch (Exception ex) when (ex is WebException wex && ((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.Unauthorized)
            {
                this.ResultCode = ResultCodes.授权失败;
                this.ErrorMessage = ex.GetBaseException().Message;
            }
            catch (Exception ex)
            {
                this.ResultCode = ResultCodes.请求错误;
                this.ErrorMessage = ex.GetBaseException().Message;
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                result = this.Fix(result);
                var msg = this.GetErrorMsg(result);

                try
                {
                    var rst = await this.ParseResult(result);
                    return rst;
                }
                catch (Exception ex)
                {
                    /*
                     * 这种不规范的xml,不应算作失败
                     * <?xml version="1.0" encoding="utf-8" ?>
                     * <!-- Error, can't find extension:6604000 -->
                     */
                    if (!string.IsNullOrEmpty(msg))
                    {
                        this.ErrorMessage = msg;
                        this.ResultCode = ResultCodes.预定义的错误;
                    }
                    else
                    {
                        if (!this.HasError)
                        {
                            this.ResultCode = ResultCodes.数据解析错误;
                        }
                        this.ErrorMessage = string.Format("{0}; {1}", this.ErrorMessage, ex.GetBaseException().Message);
                    }
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
            xml = xml.Replace("<Request>", "").Replace("</Request>", "");

            var req = new RequestHelper();
            //req.RequestHeader.Add("Accept-Encoding", "gzip, deflate");

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
            xml = this.Fix(xml);
            return this.ParseResult(xml);
        }

        private static readonly Regex ErrorOrEmptyMsgReg = new Regex(@"^<?[\s\S]*?>\s*<!--(?<msg>[\s\S]*?)-->");

        /// <summary>
        /// 提取返回消息中的错误描述
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string GetErrorMsg(string result)
        {
            var ma = ErrorOrEmptyMsgReg.Match(result);
            if (ma.Success)
            {
                return ma.Groups["msg"].Value;
            }
            return null;
        }

        /// <summary>
        /// 对不规整的数据进行修整，使得反序列化可以顺利进行
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual string Fix(string result)
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
            nonce = Guid.NewGuid().ToString().To16bitMD5();
            timestamp = (UInt64)DateTime.Now.ToUnixTimestamp() * 1000;
            var a = $"{opt.Pwd}{nonce}{timestamp}".ToMD5();
            return a;
        }
    }
}
