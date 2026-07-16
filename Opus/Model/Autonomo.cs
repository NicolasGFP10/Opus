using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class Autonomo
    {

        public int ID { get; set; }

        public string CNPJ { get; set; }

        public string EmailCorp { get; set; }

        public string TelefoneCorp { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Status { get; set; }

        public int usu_ID { get; set; }

    }
}