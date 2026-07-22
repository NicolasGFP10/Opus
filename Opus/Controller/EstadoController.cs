using Opus.DAO;
using Opus.Model;
using System.Collections.Generic;

namespace Opus.Controller
{
    public class EstadoController
    {
        EstadoDAO dao = new EstadoDAO();

        public List<Estado> ListarEstados()
        {
            return dao.ListarEstados();
        }

        public int ValidarEstado(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return 400;

            if (dao.EstadoExiste(nome))
                return 409;

            Estado estado = new Estado();

            estado.Nome = nome;

            return dao.CadastrarEstado(estado);
        }

        public int ExcluirEstado(int id)
        {
            return dao.ExcluirEstado(id);
        }
    }
}