using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class AvaliacaoController
    {

        public int GerarCodigoAvaliacao()
        {
            
            CodigoAvaliacaoDAO dao = new CodigoAvaliacaoDAO();

            return dao.GerarCodigoAvaliacao();

        }

    }
}