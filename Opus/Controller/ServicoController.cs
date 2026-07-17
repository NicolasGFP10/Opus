using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class ServicoController
    {

        public int EnviarServico(string texto)
        {
            
            if (string.IsNullOrEmpty(texto))
            {
                return 400;
            }

            Servico servico = new Servico();

            servico.Nome = texto;

            ServicoDAO ser = new ServicoDAO();

            return ser.CadastrarServico(servico);
        }

        public List<Servico> ListarServicos()
        {
            ServicoDAO dao = new ServicoDAO();

            return dao.ListarServicos();
        }

        public int ExcluirServico(int id)
        {
            ServicoDAO dao = new ServicoDAO();

            return dao.ExcluirServico(id);
        }
    }
}