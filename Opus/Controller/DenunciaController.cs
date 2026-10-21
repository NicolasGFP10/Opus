using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Opus.DAO;
using Opus.Model;
using System.Reflection.Emit;

namespace Opus.Controller
{
    public class DenunciaController
    {

        public int ValidarDenuncia(string titulo, string descricao, int categoria, int denunciante, int denunciado)
        {
            if (string.IsNullOrWhiteSpace(titulo)) return -400;
            if (string.IsNullOrWhiteSpace(descricao)) return -400;
            if (categoria <= 0) return -400;
            if (denunciante <= 0) return -400;
            if (denunciado <= 0) return -400;

            Denuncia denuncia = new Denuncia();

            denuncia.Titulo = titulo;
            denuncia.Descricao = descricao;
            denuncia.Categoria = categoria;
            denuncia.Status = 1;
            denuncia.Denunciante = denunciante;
            denuncia.Denunciado = denunciado;
            denuncia.Data = DateTime.Now;

            DenunciaDAO dao = new DenunciaDAO();

            int result = dao.CadastrarDenuncia(denuncia);

            return result;
        }

        public List<DenunciaView> ListarDenuncias(int filtro)
        {
            if (filtro < 0 || filtro > 2) return null;

            DenunciaDAO dao = new DenunciaDAO();

            return dao.ListarDenuncias(filtro);
        }

        public int AlterarStatus(int id, string status)
        {
            if (id < 0) return 400;

            DenunciaDAO dao = new DenunciaDAO();

            int valor;

            if (status == "Pendente") valor = 1;
            else valor = 0;

            return dao.AlterarStatus(id, valor);
        }


    }
}