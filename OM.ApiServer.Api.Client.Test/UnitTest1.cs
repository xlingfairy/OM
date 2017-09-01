using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OM.AppServer.Api.Client.Methods;

namespace OM.AppServer.Api.Client.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            ApiClient.Init(new ApiClientOption()
            {
                BaseUri = "http://localhost:52537/api/"
            });
        }

        [TestMethod]
        public void TestMethod1()
        {
            var mth = new Login()
            {
                User = "6678",
                Pwd = "6678"
            };
            var a = ApiClient.Execute(mth);
        }
    }
}
