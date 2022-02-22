using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Exception = System.Exception;

namespace eflowDirektLinkService
{
    public class SelfHost
    {
        private HttpSelfHostServer server;
        public void Baslat()
        {
            try
            {
                var adres = ConfigurationManager.AppSettings["ServisAdresi"] ?? "http://127.0.0.1:4321";
                var cfg = new HttpSelfHostConfiguration(adres);

                cfg.Routes.MapHttpRoute(
                    "API", "api/{controller}/{ciid}",
                    new { ciid = RouteParameter.Optional });

                server = new HttpSelfHostServer(cfg);
                server.OpenAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log.Ekle(new[]
                {
                    "Servis başlatılırken hata oluştu:",
                    e.InnerException?.Message,
                    e.Message
                });
            }
        }

        public void Durdur()
        {
            try
            {
                server?.CloseAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log.Ekle(new[]
                {
                    "Servis durdurulurken hata oluştu:",
                    e.InnerException?.Message,
                    e.Message
                });
            }
        }
    }
}
