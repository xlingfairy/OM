using CNB.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OM.AppServer.Api.Client
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseMethod
    {

        /// <summary>
        /// 
        /// </summary>
        public abstract string Model { get; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public abstract HttpMethod HttpMethod
        {
            get;
        }


        /// <summary>
        /// 超时时间（秒）
        /// </summary>
        public int? Timeout { get; set; }


        /// <summary>
        /// 验证输入
        /// </summary>
        protected virtual IEnumerable<ValidationResult> Validate()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(this, context, results, true);
            return results;
        }

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

        /// <summary>
        /// 参数验证结果
        /// </summary>
        public IEnumerable<ValidationResult> ValidationErrors { get; protected set; }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMethod<T> : BaseMethod where T : BaseResponse
    {

        internal Task<T> Execute(ApiClient client)
        {
            var rs = this.Validate();
            if (rs == null || rs.Count() == 0)
            {
                return this.InnerExecute(client);
            }
            else
            {
                this.ResultCode = ResultCodes.参数验证失败;
                this.ValidationErrors = rs;
                this.ErrorMessage = string.Join(";", rs.Select(r => $"{string.Join(".", r.MemberNames)}: {r.ErrorMessage}"));
                return Task.FromResult<T>(default(T));
            }
        }

        private async Task<T> InnerExecute(ApiClient client)
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
                try
                {
                    var rst = this.ParseResult(result);

                    if (!string.IsNullOrEmpty(rst.ErrorCode))
                    {
                        this.ResultCode = ResultCodes.预定义的错误;
                        this.ErrorMessage = $"{rst.ErrorCode}:{rst.ErrorMsg}";
                    }

                    return rst;
                }
                catch (Exception ex)
                {
                    if (!this.HasError)
                    {
                        this.ResultCode = ResultCodes.数据解析错误;
                    }
                    this.ErrorMessage = string.Format("{0}; {1}", this.ErrorMessage, ex.GetBaseException().Message);
                }
            }

            return default(T);
        }



        protected virtual async Task<string> GetResult(ApiClient client)
        {
            var url = client.GetModelUrl(this);
            using (var hc = new HttpClient())
            {
                //设置超时
                if (this.Timeout.HasValue)
                    hc.Timeout = TimeSpan.FromSeconds(this.Timeout.Value);

                var request = new HttpRequestMessage(this.HttpMethod, url);
                if (client.AuthToken != null)
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", client.AuthToken?.AccessToken);

                var ps = ParamHelper.GetParams(this);
                var content = new FormUrlEncodedContent(ps);
                request.Content = content;

                string rst = null;
                var sw = Stopwatch.StartNew();
                try
                {

                    //发送请求
                    var rep = await hc.SendAsync(request);

                    //获取请求返回的数据
                    rst = await rep.Content.ReadAsStringAsync();
                    sw.Stop();
                    return rst;
                }
                finally
                {
                    this.Diagnose = new WebDiagnoseInfo()
                    {
                        RequestHeader = request.Headers.ToString(),
                        ResponseBody = rst,
                        ResponseTime = sw.ElapsedMilliseconds,
                        SendDatas = JsonConvert.SerializeObject(ps),
                        Url = url
                    };
                }
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="client"></param>
        ///// <returns></returns>
        //protected virtual async Task<string> GetResult(ApiClient client)
        //{
        //    var req = new RequestHelper();
        //    req.RequestHeader.Add("Accept-Encoding", "gzip, deflate");

        //    if (client.Option.UseProxy)
        //        req.Proxy = new WebProxy(client.Option.ProxyAddress);

        //    var ps = ParamHelper.GetParams(this);
        //    var url = client.GetModelUrl(this);

        //    var sw = Stopwatch.StartNew();
        //    string result = null;
        //    try
        //    {
        //        result = await req.PostAsync(url, ps);
        //    }
        //    finally
        //    {
        //        sw.Stop();
        //        this.Diagnose = new WebDiagnoseInfo()
        //        {
        //            SendDatas = JsonConvert.SerializeObject(ps),
        //            Url = url,
        //            RequestHeader = JsonConvert.SerializeObject(req.RequestHeader),
        //            ResponseBody = result,
        //            ResponseTime = sw.ElapsedMilliseconds
        //        };
        //    }

        //    return result;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual T ParseResult(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }

    }
}
