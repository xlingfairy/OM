using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OM.Api.Methods.Controls;
using System.IO;

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

        private string GetTestXml(string file)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestXml", $"{file}.xml");
            if (File.Exists(path))
                return File.ReadAllText(path);
            else
            {
                Assert.Fail($"文件：{file} 失败不存在");
                return null;
            }
        }

        [TestMethod]
        public void GetDeviceInfoTest()
        {
            var xml = this.GetTestXml("DeviceInfo");

            var mth = new GetDeviceInfo();

            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }

        [TestMethod]
        public void GetExtInfoTest()
        {
            var xml = this.GetTestXml("ExtInfo");
            var mth = new GetExtInfo()
            {
                ID = "6604000"
            };

            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }

        [TestMethod]
        public void GetTrunkInfoTest()
        {
            var xml = this.GetTestXml("TrunkInfo");

            var mth = new GetTrunkInfo()
            {
                ID = "2000000"
            };

            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }


        [TestMethod]
        public void GetVisitorCallInfoTest()
        {
            var xml = this.GetTestXml("VisitorInfo");

            var mth = new GetVisitorCallInfo()
            {
            };

            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }

        [TestMethod]
        public void GetOutCallInfoTest()
        {
            var xml = this.GetTestXml("OutCallInfo");
            var mth = new GetOutCallInfo() { };

            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }


        [TestMethod]
        public void GetGroupInfo()
        {
            var xml = this.GetTestXml("GroupInfo");
            var mth = new GetGroupInfo();
            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }


        [TestMethod]
        public void GetMenuInfo()
        {
            var xml = this.GetTestXml("MenuInfo");
            var mth = new GetMenuInfo();
            var b = mth.TestXml(xml).Result;
            Assert.IsFalse(mth.HasError);

            var a = ApiClient.Execute(mth);
        }
    }
}
