using Opus.DAO;
using Opus.Model;
using System.Collections.Generic;

namespace Opus.Controller
{
    public class CidadeController
    {
        CidadeDAO dao = new CidadeDAO();

        // =====================================================
        // LISTAR CIDADES DE UM ESTADO
        // Usado pelo Autônomo
        // =====================================================

        public List<Cidade> ListarCidades(int estado)
        {
            return dao.ListarCidades(estado);
        }

        // =====================================================
        // LISTAR TODAS AS CIDADES
        // Usado pelo Moderador
        // =====================================================

        public List<CidadeView> ListarTodasCidades()
        {
            return dao.ListarTodasCidades();
        }

        // =====================================================
        // VALIDAR E CADASTRAR CIDADE
        // =====================================================

        public int ValidarCidade(string nome, int estado)
        {
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

        // =====================================================
        // EXCLUIR CIDADE
        // =====================================================

        public int ExcluirCidade(int id)
        {
            return dao.ExcluirCidade(id);
        }
    }
}