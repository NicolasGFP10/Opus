using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Web;

namespace Opus.Controller
{
    public class AutonomoCidadeController
    {
        private AutonomoCidadeDAO dao = new AutonomoCidadeDAO();

        //=========================
        // Cadastrar cidade
        //=========================

        public int CadastrarCidadeAutonomo(int cidadeID)
        {
            int autID = Convert.ToInt32(HttpContext.Current.Session["aut_ID"]);

            if (dao.CidadeJaCadastrada(cidadeID, autID))
                return 409;

            AutonomoCidade cidade = new AutonomoCidade();

            cidade.CidadeID = cidadeID;
            cidade.AutonomoID = autID;

            return dao.AdicionarCidade(cidade);
        }

        //=========================
        // Listar cidades
        //=========================

        public List<CidadeView> ListarCidadesAutonomo()
        {
            int autID = Convert.ToInt32(HttpContext.Current.Session["aut_ID"]);

            return dao.ListarCidades(autID);
        }

        //=========================
        // Excluir cidade
        //=========================

        public int ExcluirCidade(int id)
        {
            return dao.ExcluirCidade(id);
        }
    }
}