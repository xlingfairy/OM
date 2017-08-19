using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OM.Api.Methods.Controls;

namespace OM.Api.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void Init()
        {
            var opt = new ApiClientOption()
            {
                BaseUri = "http://xxx",
                Pwd = "xxx"
            };
            ApiClient.Init(opt);
        }

        [TestMethod]
        public void GetDeviceInfoTest()
        {
            var mth = new GetDeviceInfo();
            var a = ApiClient.Execute(mth);
        }

        [TestMethod]
        public void GetExtInfoTest()
        {
            var mth = new GetExtInfo()
            {
                ID = "6604"
            };
            var a = ApiClient.Execute(mth);
        }
    }
}
