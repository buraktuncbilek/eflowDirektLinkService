using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace eflowDirektLinkService
{
    public class SelfHost
    {
        private HttpSelfHostServer server;
        public void Baslat()
        {
            var adres = ConfigurationManager.AppSettings["ServisAdresi"] ?? "http://127.0.0.1:4321";
            var cfg = new HttpSelfHostConfiguration(adres);

            cfg.Routes.MapHttpRoute(
                "API", "api/{controller}/{ciid}",
                new { ciid = RouteParameter.Optional });

            server = new HttpSelfHostServer(cfg);
            server.OpenAsync().Wait();
        }

        public void Durdur()
        {
            server?.CloseAsync().Wait();
        }
    }
}
