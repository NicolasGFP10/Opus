using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class Mensagem
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string Texto { get; set; }

        public DateTime DataEnvio { get; set; }
    }
}