using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class Denuncia
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int Categoria { get; set; }

        public int Status { get; set; }

        public int Denunciante { get; set; }

        public int Denunciado { get; set; }

        public DateTime Data { get; set; }
    }
}