using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Opus.Controller
{
    public class RegiaoController
    {
        public int ValidarRegiao(string estado, string cidade)
        {
            if (string.IsNullOrEmpty(estado) || string.IsNullOrEmpty(cidade))
            {
                return 400; // Bad Request
            }

            Regiao regiao = new Regiao();
            
            regiao.Estado = estado;
            regiao.Cidade = cidade;

            RegiaoDAO reg = new RegiaoDAO();

            return reg.CadastrarRegiao(regiao);
        }

        public List<Regiao> ListarRegioes()
        {
            RegiaoDAO dao = new RegiaoDAO();

            return dao.ListarRegioes();
        }

        public int EditarRegiao(int id, string estado, string cidade)
        {

            if (string.IsNullOrEmpty(estado) || string.IsNullOrEmpty(cidade))
            {
                return 400;
            }

            Model.Regiao regiao = new Model.Regiao();

            regiao.ID = id;
            regiao.Estado = estado;
            regiao.Cidade = cidade;

            RegiaoDAO dao = new RegiaoDAO();

            return dao.EditarRegiao(regiao);

        }

        public void ExcluirRegiao(int id)
        {
            RegiaoDAO dao = new RegiaoDAO();

            dao.ExcluirRegiao(id);
        }
    }
}