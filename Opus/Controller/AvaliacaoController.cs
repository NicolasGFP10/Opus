using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class AvaliacaoController
    {

        public int Cadastrar(Avaliacao avaliacao)
        {
            AvaliacaoDAO dao = new AvaliacaoDAO();

            if (string.IsNullOrWhiteSpace(avaliacao.Titulo))
                throw new Exception("Informe o título da avaliação.");

            if (string.IsNullOrWhiteSpace(avaliacao.Descricao))
                throw new Exception("Informe a descrição da avaliação.");

            if (avaliacao.Titulo.Length > 100)
                throw new Exception("O título deve possuir no máximo 100 caracteres.");

            if (avaliacao.Descricao.Length > 300)
                throw new Exception("A descrição deve possuir no máximo 300 caracteres.");

            if (avaliacao.Nota < 1 || avaliacao.Nota > 5)
                throw new Exception("Informe uma nota de 1 a 5.");

            return dao.Cadastrar(avaliacao);
        }

        public int GerarCodigoAvaliacao()
        {

            CodigoAvaliacaoDAO dao = new CodigoAvaliacaoDAO();

            return dao.GerarCodigoAvaliacao();

        }

        public bool ValidarCodigo(int codigo)
        {
            CodigoAvaliacaoDAO dao = new CodigoAvaliacaoDAO();

            if (dao.ValidarCodigoAvaliacao(codigo) > 0 && dao.ValidarCodigoAutonomo(codigo) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}