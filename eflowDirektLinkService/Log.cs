using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eflowDirektLinkService
{
    public static class Log
    {
        private static readonly string Klasör = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

        public static void Kontrol()
        {
            if (!Directory.Exists(Klasör))
                Directory.CreateDirectory(Klasör);
        }

        private static object LockObj = new object();
        public static void Ekle(string[] Mesaj)
        {
            lock (LockObj)
            {
                var yol = Path.Combine(Klasör, $"{DateTime.Now:yyyyMMdd}.txt");
                if (!File.Exists(yol))
                    File.WriteAllLines(yol, new[] {DateTime.Now.ToString("O"), "Log Dosyası Oluşturuldu.", ""},
                        Encoding.UTF8);
                var temp = new List<string>()
                {
                    "--------------------------------------",
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff")
                };
                temp.AddRange(Mesaj);
                File.AppendAllLines(yol, temp, Encoding.UTF8);
            }
        }

    }
}
