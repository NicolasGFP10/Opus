using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Opus.Controller
{
    public class UsuarioController
    {

        public int ValidarCadastro(string nome, string email, string telefone, string cpf, string senha, HttpPostedFile imagem)
        {

            UsuarioDAO dao = new UsuarioDAO();

            if (dao.UsuarioExiste(email, telefone, cpf))
            {
                return 409;
            }

            if (string.IsNullOrEmpty(nome) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(telefone) ||
            string.IsNullOrEmpty(cpf) ||
            string.IsNullOrEmpty(senha) ||
            imagem == null ||
            imagem.ContentLength == 0 ||
            imagem.ContentLength > 5 * 1024 * 1024)
            {

                return 400;

            }

            string extensao = Path.GetExtension(imagem.FileName).ToLower();

            if (extensao != ".jpg" &&
            extensao != ".jpeg" &&
            extensao != ".png")
            {
                return 400;
            }

            Usuario usuario = new Usuario();

            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Telefone = telefone;
            usuario.CPF = cpf;
            usuario.Senha = senha;
            usuario.Imagem = imagem.FileName;
            usuario.DataCadastro = DateTime.Now;
            usuario.Status = true;

            return dao.CadastrarUsuario(usuario, imagem);
        }

        public int ValidarLogin(string email, string senha)
        {
            UsuarioDAO dao = new UsuarioDAO();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {

                return 400;

            }

            return dao.EntrarUsuario(email, senha);
        }

        public int EditarDados(string nome, string email, string telefone, string senha)
        {
            UsuarioDAO dao = new UsuarioDAO();

            if (string.IsNullOrEmpty(nome) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(telefone) ||
            string.IsNullOrEmpty(senha))
            {
                return 400;
            }

            Usuario usuario = new Usuario();
            usuario.Id = (int)HttpContext.Current.Session["usu_id"];
            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Telefone = telefone;
            usuario.Senha = senha;

            return dao.EditarUsuario(usuario);
        }
    }
}