using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string CPF { get; set; }

        public string Senha { get; set; }

        public string Imagem { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Status { get; set; }

    }
}