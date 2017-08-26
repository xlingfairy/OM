using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace TestServer
{
    public class Global : System.Web.HttpApplication
    {

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(Global));

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            Dictionary<string, object> forms = new Dictionary<string, object>();
            var q = ToDictionary(Request.QueryString);
            var f = ToDictionary(Request.Form);
            var h = ToDictionary(Request.Headers);
            var s = "";
            using (var sr = new StreamReader(Request.InputStream))
            {
                s = sr.ReadToEnd();
            }
            var obj = new
            {
                Q = q,
                F = f,
                H = h,
                s = s,
                U = Request.RawUrl
            };

            log.Info(JsonConvert.SerializeObject(obj));
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


        public IDictionary<string, string> ToDictionary(NameValueCollection col)
        {
            IDictionary<string, string> myDictionary = new Dictionary<string, string>();
            if (col != null)
            {
                myDictionary =
                    col.Cast<string>()
                        .Select(s => new { Key = s, Value = col[s] })
                        .ToDictionary(p => p.Key, p => p.Value);
            }
            return myDictionary;
        }
    }
}