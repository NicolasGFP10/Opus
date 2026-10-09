using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class FotoPortifolio
    {
        public int ID { get; set; }
        public string Caminho { get; set; }
        public int PortifolioID { get; set; }
    }
}