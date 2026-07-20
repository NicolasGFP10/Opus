using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class AutonomoServico
    {
        public int ID { get; set; }

        public int AutonomoID { get; set; }

        public int ServicoID { get; set; }

        public string NomeServico { get; set; } // Opção para não ter que chamar o objeto serviço inteiro
    }
}