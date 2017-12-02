using OM.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OM.AppServer.Controllers
{
    public class InputController : ApiController
    {
        private log4net.ILog Log = log4net.LogManager.GetLogger(typeof(InputController));

        [HttpPost]
        public async Task Post()
        {
            var input = await Request.Content.ReadAsStringAsync();
            var flag = ApiClient.Execute(input);
            if (!flag)
            {
                this.Log.Error($"POST 输入解析失败: {input}");
            }
            this.Log.Info(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task Get()
        {
            var bytes = await this.Request.Content.ReadAsByteArrayAsync();
            var input = Encoding.UTF8.GetString(bytes);
            var flag = ApiClient.Execute(input);
            if (!flag)
            {
                this.Log.Error($"GET 输入解析失败: {input}");
            }
            this.Log.Info(input);
        }

    }
}
