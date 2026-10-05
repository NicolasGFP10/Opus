using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Model
{
    public class CodigoAvaliacao
    {
        public int ID { get; set; }
        public int Token { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public int AutonomoID { get; set; }
    }
}