using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace eflowDirektLinkService.Controllers
{
    public class DokumanController : ApiController
    {
        [Route("api/Dokuman/{ciid}/{did}")]
        public async Task<HttpResponseMessage> GetByCIID(int ciid, int did)
        {
            // return await SQL.DokumanVer(ciid, did);
            try
            {
                var belge = await SQL.DokumanVer(ciid, did);
                if (belge is null)
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("CIID ve DID ile herhangi bir belgeye ulaşılamadı.", Encoding.UTF8)
                    };

                var cevap = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(belge.VALUE)
                };
                cevap.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = belge.FILENAME
                    };
                cevap.Content.Headers.ContentType = new MediaTypeHeaderValue(belge.MIMETYPE);
                //cevap.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                //return Request.CreateResponse(HttpStatusCode.OK, cevap);
                return await Task.FromResult(cevap);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message + " " + e.InnerException?.Message, Encoding.UTF8)
                };
            }
        }
    }
}
