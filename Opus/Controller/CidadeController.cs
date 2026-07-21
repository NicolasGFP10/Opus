using Opus.DAO;
using Opus.Model;
using System.Collections.Generic;

namespace Opus.Controller
{
    public class CidadeController
    {

        public int ValidarCidade(string nome, int estado)
        {
            CidadeDAO dao = new CidadeDAO();

            if (string.IsNullOrWhiteSpace(nome))
                return 400;

            if (estado <= 0)
                return 400;

            if (dao.CidadeExiste(nome, estado))
                return 409;

            Cidade cidade = new Cidade();

            cidade.Nome = nome;
            cidade.EstadoID = estado;

            return dao.CadastrarCidade(cidade);
        }

        public List<Cidade> ListarCidades(int estado)
        {
            CidadeDAO dao = new CidadeDAO();

            return dao.ListarCidades(estado);
        }

        public int ExcluirCidade(int id)
        {
            CidadeDAO dao = new CidadeDAO();

            return dao.ExcluirCidade(id);
        }
    }
}