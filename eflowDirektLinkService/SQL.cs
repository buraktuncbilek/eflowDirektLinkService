using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using eflowDirektLinkService.Tipler;

namespace eflowDirektLinkService
{
    public static class SQL
    {
        public static SqlConnection ConnectionVer()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["EFlowConnection"].ConnectionString ??
                                     "Initial Catalog=eflow;Data Source=.;Integrated Security=SSPI;");
        }

        public static async Task<IEnumerable<T>> ListeVer<T>(string qry)
        {
            using (var conn=ConnectionVer())
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<T>(qry);
            }
        }

        public static async Task<INST_DATA_ATTACHMENT> DokumanVer(int ciid, int did)
        {
            using (var conn=ConnectionVer())
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<INST_DATA_ATTACHMENT>(
                    "select CIID,DID,[VALUE],[LENGTH],MIMETYPE,[FILENAME] from INST_DATA_ATTACHMENT with(nolock) where CIID=@CIID and DID=@DID and LENGTH>0",
                    param: new
                    {
                        CIID = ciid,
                        DID = did
                    });
            }
        }
    }
}
