using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eflowDirektLinkService
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new SelfHost();
            api.Baslat();
            Console.WriteLine("Sunucu Aktif");
            Console.ReadLine();
            api.Durdur();
            Environment.Exit(0);
        }
    }
}
