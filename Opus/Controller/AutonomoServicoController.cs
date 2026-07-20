using Opus.DAO;
using Opus.Model;
using System.Collections.Generic;
using System.Web;

namespace Opus.Controller
{
    public class AutonomoServicoController
    {
        AutonomoServicoDAO dao = new AutonomoServicoDAO();

        public List<Servico> ListarServicos()
        {
            return dao.ListarServicos();
        }

        public List<AutonomoServico> ListarServicosAutonomo()
        {
            int autID = (int)HttpContext.Current.Session["aut_ID"];

            return dao.ListarServicosAutonomo(autID);
        }

        public int AdicionarServico(int servicoID)
        {

            int autID = (int)HttpContext.Current.Session["aut_ID"];

            if (dao.ServicoJaExiste(autID, servicoID))
                return 409;

            return dao.CadastrarServico(autID, servicoID);
        }

        public int ExcluirServico(int ausID)
        {
            return dao.ExcluirServico(ausID);
        }
    }
}