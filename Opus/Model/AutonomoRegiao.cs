using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class AutonomoRegiao
    {
        public int ID { get; set; }
        public int AutonomoID { get; set; }
        public int RegiaoID { get; set; }
        public string NomeRegiao { get; set; } // Opção para não ter que chamar o objeto região inteiro

    }
}