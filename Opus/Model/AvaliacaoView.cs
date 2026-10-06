using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace Opus.Model
{
    public class AvaliacaoView
    {
        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int Nota { get; set; }

        public int UsuarioID { get; set; }

        public string NomeUsuario { get; set; }

        public int AutonomoID { get; set; }

        public List<FotoAvaliacao> Fotos { get; set; }
            = new List<FotoAvaliacao>();
    }
}