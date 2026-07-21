using Opus.DAO;
using Opus.Model;
using System.Collections.Generic;
using System.Web;

namespace Opus.Controller
{
    public class AutonomoCidadeController
    {

        public int AdicionarCidade(int cidade)
        {
            AutonomoCidadeDAO dao = new AutonomoCidadeDAO();

            if (cidade <= 0)
                return 400;

            int idAutonomo =
                (int)HttpContext.Current.Session["aut_ID"];

            if (dao.CidadeJaCadastrada(cidade, idAutonomo))
                return 409;

            AutonomoCidade cadastro = new AutonomoCidade();

            cadastro.CidadeID = cidade;
            cadastro.AutonomoID = idAutonomo;

            return dao.AdicionarCidade(cadastro);
        }

        public List<CidadeView> ListarCidades()
        {
            AutonomoCidadeDAO dao = new AutonomoCidadeDAO();

            int idAutonomo =
                (int)HttpContext.Current.Session["aut_ID"];

            return dao.ListarCidades(idAutonomo);
        }

        public int ExcluirCidade(int id)
        {
            AutonomoCidadeDAO dao = new AutonomoCidadeDAO();

            return dao.ExcluirCidade(id);
        }

    }
}