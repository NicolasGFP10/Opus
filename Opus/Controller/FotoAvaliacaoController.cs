using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class FotoAvaliacaoController
    {
        private FotoAvaliacaoDAO dao = new FotoAvaliacaoDAO();

        public void Cadastrar(FotoAvaliacao foto)
        {
            dao.Cadastrar(foto);
        }
    }
}