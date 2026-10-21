using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class DenunciaView
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Status { get; set; }

        public int DenuncianteID { get; set; }
        public string NomeDenunciante { get; set; }
        public string ImagemDenunciante { get; set; }

        public int DenunciadoID { get; set; }
        public string NomeDenunciado { get; set; }
        public string ImagemDenunciado { get; set; }

        public DateTime DataEnvio { get; set; }

        public List<FotoDenuncia> Fotos { get; set; } = new List<FotoDenuncia>();
    }
}