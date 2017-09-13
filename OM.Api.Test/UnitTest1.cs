using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OM.Api.Methods.Controls.Query;
using System.IO;
using OM.Api.Methods.Controls.Assign;
using System.Collections.Generic;
using OM.Api.Methods.Controls;
using OM.Api.Methods.Transfer;
using System.Text;
using System.Xml.Serialization;
using OM.Api.Models.Events;
using System.Xml.Linq;
using OM.Api.Parser;
using System.Diagnostics;

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
                BaseUri = "http://789zcgl.iask.in",
                Pwd = "admin"
            };

            ApiClient.Init(opt);
            ApiClient.OnReceiveEvent += ApiClient_OnReceiveEvent;
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
                ID = "8073"
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
                ID = "200"
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


        [TestMethod]
        public void EditGroupTest()
        {
            var mth = new EditGroup()
            {
                ID = 1,
                VoiceFile = "NewMorning",
                //Exts = new List<string>() { "6688", "6678" }
                Exts = new List<string>() { "8073" }
            };
            var a = ApiClient.Execute(mth);

            var mth2 = new ClearGroup()
            {
                ID = 1
            };
            var b = ApiClient.Execute(mth2);
        }


        [TestMethod]
        public void EditExtTest()
        {
            var mth = new EditExt()
            {
                LineID = "IPPhone 200",
                ID = "6678",
                AllowPickup = true,
                APIFunction = Models.Enums.APIFunctions.All,
                CallRestriction = Models.Enums.CallRestrictions.市话,
                ForwardingType = Models.Enums.CallForwardingTyeps.遇忙或无应答时转移,
                Groups = new List<int>() { 1 },
                IsAutoAnswer = true,
                IsNoDisturb = true,
                IsRealtimeRecord = true,
                Staffid = "0001",
                VoiceFile = "NewMorning",
                Mobile = "13799999999"
            };
            var a = ApiClient.Execute(mth);
        }




        [TestMethod]
        public void HoldTest()
        {
            //TODO 未完成
            var mth = new Hold()
            {
                ID = "6678"
            };
            ApiClient.Execute(mth);
        }



        [TestMethod]
        public void CallExtTest()
        {
            //TODO 未测试通过
            var mth = new CallExt()
            {
                FromID = "6600",
                ToID = "6601"
            };
            var a = ApiClient.Execute(mth);
        }


        [TestMethod]
        public void CallOuterTest()
        {
            var xml = this.GetTestXml("CallOuter");

            var mth = new CallOuter()
            {
                ExtID = "6601",
                Prefix = "9",
                OuterNumber = "15986627851"
            };

            var b = mth.TestXml(xml).Result;

            var f = new Models.Events.Failed();

            var a = ApiClient.Execute(mth);
        }


        [TestMethod]
        public void AA()
        {
            var ring = this.GetTestXml("RingExt2Ext");
            var cdr = this.GetTestXml("CDR");

            ApiClient.Execute(ring);
            ApiClient.Execute(cdr);
        }

        private void ApiClient_OnReceiveEvent(object sender, OMEventEventArgs e)
        {
            Debug.WriteLine("aaaaa");
        }
    }
}
