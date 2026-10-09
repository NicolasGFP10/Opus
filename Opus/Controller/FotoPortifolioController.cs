using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class FotoPortifolioController
    {

        private FotoPortifolioDAO dao = new FotoPortifolioDAO();

        public void Cadastrar(FotoPortifolio foto)
        {
            dao.Cadastrar(foto);
        }

    }
}