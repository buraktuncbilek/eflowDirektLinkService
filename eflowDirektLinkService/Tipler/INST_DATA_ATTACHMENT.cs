using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eflowDirektLinkService.Tipler
{
    public class INST_DATA_ATTACHMENT
    {
        public int CIID { get; set; }
        public int DID { get; set; }
        //public System.Data.SqlTypes.SqlBinary VALUE { get; set; }
        public byte[] VALUE { get; set; }
        public int LENGTH { get; set; }
        public string MIMETYPE { get; set; }
        public string FILENAME { get; set; }
    }
}
