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

        public int Cadastrar(string titulo, string descricao, int nota, int usuario, int autonomo, int codigo)
        {
            AvaliacaoDAO dao = new AvaliacaoDAO();

            if (string.IsNullOrWhiteSpace(titulo))
                throw new Exception("Informe o título da avaliação.");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new Exception("Informe a descrição da avaliação.");

            if (titulo.Length > 100)
                throw new Exception("O título deve possuir no máximo 100 caracteres.");

            if (descricao.Length > 300)
                throw new Exception("A descrição deve possuir no máximo 300 caracteres.");

            if (nota < 1 || nota > 5)
                throw new Exception("Informe uma nota de 1 a 5.");

            Avaliacao avaliacao = new Avaliacao();

            avaliacao.Titulo = titulo;
            avaliacao.Descricao = descricao;
            avaliacao.Nota = nota;
            avaliacao.UsuarioID = usuario;
            avaliacao.AutonomoID = autonomo;
            avaliacao.CodigoID = codigo;

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