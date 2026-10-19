using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class AutonomoPesquisaView
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Descricao { get; set; }
        public decimal NotaMedia { get; set; }
        public int UsuarioID { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int QuantidadeAvaliacoes { get; set; }
    }
}