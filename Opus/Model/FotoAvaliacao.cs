using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class FotoAvaliacao
    {
        public int ID { get; set; }

        public string Imagem { get; set; }

        public int AvaliacaoID { get; set; }
    }
}