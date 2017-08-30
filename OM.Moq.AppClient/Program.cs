using CNB.Common;
using Newtonsoft.Json;
using OM.AppClient.SignalR;
using OM.Moq.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Moq.AppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("请输入分机号：");
                var extID = Console.ReadLine().Trim();
                var token = Login(extID);
                if (token != null)
                {
                    Begin(token.AccessToken);
                    break;
                }
            }
            Console.WriteLine("连接成功");
            Console.Read();
        }

        public static async void Begin(string token)
        {
            await OMHubProxy.Start(
                "/signalr".FixUrl(ConfigurationManager.AppSettings.Get("AppServerUrl")),
                token
                );

            try
            {
                var info = await OMHubProxy.GetExtInfo();
                Console.WriteLine($"ID:{info.ID} StaffID:{info.StaffID} State:{info.State}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static Token Login(string extID)
        {
            var url = "/api/Token".FixUrl(ConfigurationManager.AppSettings.Get("AppServerUrl"));
            var rh = new RequestHelper();
            try
            {
                var ctx = rh.Post(
                    url,
                    new Dictionary<string, string>()
                        {
                            {"userName", extID },
                            { "password", extID},
                            {"grant_type","password" }
                        }
                );
                var token = JsonConvert.DeserializeObject<Token>(ctx);
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}
