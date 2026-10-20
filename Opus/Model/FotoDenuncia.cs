using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class FotoDenuncia
    {
        public int ID { get; set; }
        public int DenunciaID { get; set; }
        public string CaminhoFoto { get; set; }
    }
}