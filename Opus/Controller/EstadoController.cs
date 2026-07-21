using Opus.DAO;
using Opus.Model;
using System.Collections.Generic;

namespace Opus.Controller
{
    public class EstadoController
    {

        public int ValidarEstado(string nome)
        {
            EstadoDAO dao = new EstadoDAO();

            if (string.IsNullOrWhiteSpace(nome))
                return 400;

            if (dao.EstadoExiste(nome))
                return 409;

            Estado estado = new Estado();

            estado.Nome = nome;

            return dao.CadastrarEstado(estado);
        }

        public List<Estado> ListarEstados()
        {
            EstadoDAO dao = new EstadoDAO();

            return dao.ListarEstados();
        }

        public int ExcluirEstado(int id)
        {
            EstadoDAO dao = new EstadoDAO();

            return dao.ExcluirEstado(id);
        }

    }
}