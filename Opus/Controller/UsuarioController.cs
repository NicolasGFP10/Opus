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

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }


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

            if (IsCpf(cpf) == false)
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

        public int EditarDados(string nome, string email, string telefone, string senha, HttpPostedFile imagem)
        {
            UsuarioDAO dao = new UsuarioDAO();

            if (string.IsNullOrEmpty(nome) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(telefone) ||
                string.IsNullOrEmpty(senha))
            {
                return 400;
            }

            if (imagem != null)
            {
                if (imagem.ContentLength > 5 * 1024 * 1024)
                    return 413;

                string extensao = Path.GetExtension(imagem.FileName).ToLower();

                if (extensao != ".jpg" &&
                    extensao != ".jpeg" &&
                    extensao != ".png")
                {
                    return 415;
                }
            }

            Usuario usuario = new Usuario();

            usuario.Id = (int)HttpContext.Current.Session["usu_ID"];
            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Telefone = telefone;
            usuario.Senha = senha;

            if (imagem != null)
            {
                usuario.Imagem = imagem.FileName;
            }

            return dao.EditarUsuario(usuario, imagem);
        }

        public List<Usuario> PesquisarUsuario(string pesquisa, int filtro)
        {
            if (string.IsNullOrWhiteSpace(pesquisa))
                return new List<Usuario>();

            if (filtro < 0 || filtro > 2)
                return new List<Usuario>();

            UsuarioDAO dao = new UsuarioDAO();

            return dao.PesquisarUsuario(pesquisa.Trim(), filtro);
        }

        public Usuario DadosUsuario(int id)
        {
            UsuarioDAO dao = new UsuarioDAO();

            return dao.DadosUsuario(id);
        }

        public int AlterarStatus(int id, int status)
        {
            if (id == 0 || status < 0 || status > 1)
            {
                return 400;
            }

            // Alterar status
            if (status == 1)
            {
                status = 0;
            } 
            else
            {
                status = 1;
            }

            UsuarioDAO dao = new UsuarioDAO();

            return dao.AlterarStatus(id, status);
        }
    }
}