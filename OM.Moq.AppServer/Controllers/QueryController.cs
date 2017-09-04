using OM.Api;
using OM.Api.Entity;
using OM.Api.Methods.Controls.Query;
using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OM.Moq.AppServer.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Query")]
    //[Authorize]
    public class QueryController : ApiController
    {

        // 太慢 
        //[Route("Exts")]
        //public async Task<ListResult<ExtInfo>> Get()
        //{
        //    var lst = new List<ExtInfo>();

        //    var mth = new GetDeviceInfo();
        //    var info = await ApiClient.ExecuteAsync(mth);
        //    foreach (var e in info.Exts)
        //    {
        //        var mth2 = new GetExtInfo()
        //        {
        //            ID = e.ID
        //        };
        //        var ext = await ApiClient.ExecuteAsync(mth2);
        //        lst.Add(ext);
        //    }

        //    return BaseResult.CreateListResult(lst);
        //}


        [Route("Device")]
        public async Task<DataResult<DeviceInfo>> GetDeviceInfo()
        {
            var mth = new GetDeviceInfo();
            return BaseResult.CreateDataResult(await ApiClient.ExecuteAsync(mth));
        }

        [Route("Ext")]
        public async Task<DataResult<ExtInfo>> GetExtInfo(string id)
        {
            var mth = new GetExtInfo()
            {
                ID = id
            };
            return BaseResult.CreateDataResult(await ApiClient.ExecuteAsync(mth));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Trunk")]
        public async Task<DataResult<TrunkInfo>> GetTrunkInfo(string id)
        {
            var mth = new GetTrunkInfo()
            {
                ID = id
            };

            return BaseResult.CreateDataResult(await ApiClient.ExecuteAsync(mth));
        }
    }
}