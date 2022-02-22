using System;
using System.Configuration;
using System.Reflection;
using Topshelf;

namespace eflowDirektLinkService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cfg = ConfigurationManager.AppSettings;
            var rc = HostFactory.Run(x =>
            {
                x.Service<SelfHost>(s =>
                {
                    s.ConstructUsing(name => new SelfHost());
                    s.WhenStarted(tc => tc.Baslat());
                    s.WhenStopped(tc => tc.Durdur());
                    
                    s.AfterStartingService(() =>
                        Log.Ekle(new string[] {"Servis başlatıldı!", "Adres:" + cfg["ServisAdresi"]}));

                    s.AfterStoppingService(() => Log.Ekle(new[] {"Servis durduruldu!"}));
                });

                switch (cfg["KullaniciTuru"])
                {
                    case "LocalSystem":
                        x.RunAsLocalSystem();
                        break;
                    case "LocalService":
                        x.RunAsLocalService();
                        break;
                    case "NetworkService":
                        x.RunAsNetworkService();
                        break;
                    case "VirtualServiceAccount":
                        x.RunAsVirtualServiceAccount();
                        break;
                    case "Prompt":
                        x.RunAsPrompt();
                        break;
                    case "User":
                        x.RunAs(cfg["Kullanici"], cfg["Parola"]);
                        break;
                    default:
                        x.RunAsNetworkService();
                        break;
                }

                x.SetHelpTextPrefix(
                    "Eflow süreç verisindeki dokümanlara direkt link olarak erişim sağlayan windows servisidir. Detaylı bilgi için: https://github.com/buraktuncbilek/eflowDirektLinkService");
                x.UseAssemblyInfoForServiceInfo(Assembly.GetExecutingAssembly());

                x.SetDescription("E-Flow Doküman direkt link servisi");
                x.SetDisplayName("E-Flow Doküman Servisi");
                x.SetServiceName("EFlowDokumanServisi");

                //Gecikmeli başlat
                if (cfg["GecikmeliBaslat"] == "E")
                    x.StartAutomaticallyDelayed();
                else
                    x.StartAutomatically();

                x.BeforeInstall(() => Log.Kontrol());
                x.OnException(ex =>
                {
                    Console.WriteLine(ex);
                    Log.Ekle(new[]
                    {
                        "Servis Hatası!",
                        ex.InnerException?.Message,
                        ex.Message,
                    });
                });
            });
        }
    }
}