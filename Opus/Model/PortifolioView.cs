using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace Opus.Model
{
    public class PortifolioView
    {
        public int ID { get; set; }

        public string Descricao { get; set; }

        public int AutonomoID { get; set; }

        public List<FotoPortifolio> Fotos { get; set; }
            = new List<FotoPortifolio>();
    }
}