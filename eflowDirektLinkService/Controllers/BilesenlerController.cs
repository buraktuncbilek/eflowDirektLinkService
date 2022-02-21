using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace eflowDirektLinkService.Controllers
{
    public class BilesenlerController : ApiController
    {
        //ciid ye göre did listesi
        public async Task<object> Get(int ciid)
        {
            try
            {
                return await SQL.ListeVer<Tipler.DidTip>(
                    $"Select DID,DISPLAYNAME,NAME from TMPL_DATA_DICTIONARY with(nolock) where CID=(select top(1) CID from INST_COURSE with(nolock) where CIID={ciid}) and TYPE=4");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(new
                {
                    Basarili = false,
                    Mesaj = e.Message + " " + e.InnerException?.Message
                });
            }
        }
    }
}
