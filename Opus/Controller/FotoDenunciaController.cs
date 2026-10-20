using Opus.Model;
using Opus.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class FotoDenunciaController
    {
        public void Cadastrar(FotoDenuncia foto)
        {
            FotoDenunciaDAO dao = new FotoDenunciaDAO();

            dao.Cadastrar(foto);
        }
    }
}