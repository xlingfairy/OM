using OM.Api;
using OM.AppServer.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OM.Moq.AppServer
{
    public static class OMHandler
    {

        public static void Regist()
        {
            ApiClient.OnReceiveCDR += ApiClient_OnReceiveCDR;
            ApiClient.OnReceiveEvent += ApiClient_OnReceiveEvent;
        }

        private static void ApiClient_OnReceiveEvent(object sender, OMEventEventArgs e)
        {
            if(e.Data is IExtNotify en)
            {
                OMHubHelper.Send(en.ExtID, en);
            }
        }

        private static void ApiClient_OnReceiveCDR(object sender, OMCDREventArgs e)
        {
            
        }
    }
}