using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Opus.DAO;
using Opus.Model;

namespace Opus.Controller
{
    public class DenunciaController
    {

        public int ValidarDenuncia(string titulo, string descricao, int categoria, int status, int denunciante, int denunciado)
        {
            if (string.IsNullOrWhiteSpace(titulo)) return 400; // Título vazio
            if (string.IsNullOrWhiteSpace(descricao)) return 400; // Descrição vazia
            if (categoria <= 0) return 400; // Categoria inválida
            if (denunciante <= 0) return 400; // Denunciante inválido
            if (denunciado <= 0) return 400; // Denunciado inválido

            Denuncia denuncia = new Denuncia();

            denuncia.Titulo = titulo;
            denuncia.Descricao = descricao;
            denuncia.Categoria = categoria;
            denuncia.Status = status;
            denuncia.Denunciante = denunciante;
            denuncia.Denunciado = denunciado;
            denuncia.Data = DateTime.Now;

            DenunciaDAO dao = new DenunciaDAO();

            return dao.CadastrarDenuncia(denuncia);
        }

    }
}