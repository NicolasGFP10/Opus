using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class PortifolioController
    {

        public int CadastrarPortifolio(int autID, string descricao)
        {
            if (autID <= 0 || string.IsNullOrEmpty(descricao))
            {
                return -400;
            }

            Portifolio portifolio = new Portifolio();

            portifolio.AutonomoID = autID;
            portifolio.Descricao = descricao;

            PortifolioDAO dao = new PortifolioDAO();

            return dao.CadastrarPortifolio(portifolio);
        }

        public List<PortifolioView> ListarPortifolios(int autonomoID)
        {
            PortifolioDAO portifolioDAO = new PortifolioDAO();
            FotoPortifolioDAO fotoDAO = new FotoPortifolioDAO();

            List<PortifolioView> lista =
                portifolioDAO.ListarPortifolios(autonomoID);

            foreach (PortifolioView portifolio in lista)
            {
                portifolio.Fotos = fotoDAO.ListarFotos(portifolio.ID);
            }

            return lista;
        }

        public bool ExcluirPortifolio(int portifolioID, int autonomoID)
        {
            if (portifolioID <= 0 || autonomoID <= 0)
                return false;

            PortifolioDAO dao = new PortifolioDAO();

            return dao.ExcluirPortifolio(portifolioID, autonomoID);
        }

    }
}