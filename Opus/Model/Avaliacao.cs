using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class Avaliacao
    {
        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int Nota { get; set; }

        public int UsuarioID { get; set; }

        public int AutonomoID { get; set; }

        public int CodigoID { get; set; }

        public List<FotoAvaliacao> Fotos { get; set; } = new List<FotoAvaliacao>();
    }
}