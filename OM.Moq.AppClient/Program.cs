using OM.AppClient.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Moq.AppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Begin();
            Console.Read();
        }

        public static async void Begin()
        {
            await OMHubProxy.Start();
        }
    }
}
