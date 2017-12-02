using CNB.Common;
using OM.Api;
using OM.AppServer.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OM.AppServer
{
    public static class OMHandler
    {

        private static Biz.CDRBiz Biz = new AppServer.Biz.CDRBiz();

        public static void Init()
        {
            ApiClient.Init(new ApiClientOption()
            {
                BaseUri = ConfigurationManager.AppSettings.Get("OMServerUrl"),
                Pwd = ConfigurationManager.AppSettings.Get("OMServerPwd")
            });
            ApiClient.OnReceiveCDR += ApiClient_OnReceiveCDR;
            ApiClient.OnReceiveEvent += ApiClient_OnReceiveEvent;
        }

        private static void ApiClient_OnReceiveEvent(object sender, OMEventEventArgs e)
        {
            // Menu外呼，外部电话回铃 没有分机号
            if (e.Data is IExtNotify en && en.ExtID != null)
            {
                OMHubHelper.Send(en.ExtID, en);
            }

            if (e.Data is IAdminNotify an)
            {
                OMHubHelper.Send(an);
            }
        }

        private static void ApiClient_OnReceiveCDR(object sender, OMCDREventArgs e)
        {
            OMHubHelper.Send(e.Data);
            Task.Run(async () =>
            {
                await Biz.AddCDR(new Entity.CDR()
                {
                    CallID = e.Data.CallID,
                    Duration = e.Data.Duration,
                    From = e.Data.From,
                    ID = e.Data.ID,
                    OuterID = e.Data.Outer?.ID.ToInt(0) ?? 0,
                    RecCodec = e.Data.RecCodec,
                    Recording = e.Data.Recording,
                    RecoredOn = DateTime.Now,
                    Route = (byte)e.Data.Route,
                    TimeEnd = e.Data.TimeEnd,
                    TimeStar = e.Data.TimeStar,
                    To = e.Data.To,
                    TrunkNumber = e.Data.TrunkNumber,
                    Type = (byte)e.Data.Type,
                    VisitorID = e.Data.Visitor?.ID.ToInt(0) ?? 0
                });
            });
        }
    }
}