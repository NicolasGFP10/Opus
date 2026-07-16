using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class AutonomoController
    {

        public static bool IsCnpj(string cnpj)
        {
            // Remove caracteres especiais e valida tamanho
            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14) return false;

            // Cálculo dos dígitos verificadores (Regra Receita Federal)
            int[] m1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] m2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp = cnpj.Substring(0, 12);
            int soma = 0;
            for (int i = 0; i < 12; i++) soma += int.Parse(temp[i].ToString()) * m1[i];
            int resto = (soma % 11);
            int d1 = (resto < 2) ? 0 : 11 - resto;

            temp += d1;
            soma = 0;
            for (int i = 0; i < 13; i++) soma += int.Parse(temp[i].ToString()) * m2[i];
            resto = (soma % 11);
            int d2 = (resto < 2) ? 0 : 11 - resto;

            return cnpj.EndsWith(d1.ToString() + d2.ToString());
        }

        public int ValidarCadastro(string cnpj, string email, string telefone, string descricao)
        {
            AutonomoDAO aut = new AutonomoDAO();

            if (string.IsNullOrEmpty(cnpj) ||
                string.IsNullOrEmpty(descricao))
            {
                return 400;
            }

            if (!IsCnpj(cnpj))
            {
                return 406;
            }

            if (aut.AutonomoExiste(cnpj, email, telefone))
            {
                return 409;
            }

            Autonomo autonomo = new Autonomo();

            autonomo.CNPJ = cnpj;
            autonomo.Descricao = descricao;
            autonomo.EmailCorp = null;
            autonomo.TelefoneCorp = null;
            autonomo.DataCadastro = DateTime.Now;
            autonomo.Status = true;
            autonomo.usu_ID = (int)HttpContext.Current.Session["usu_ID"];

            if (email != null && email.Length > 0)
            {
                autonomo.EmailCorp = email;
            }

            if (telefone != null && telefone.Length > 0)
            {
                autonomo.TelefoneCorp = telefone;
            }

            return aut.CadastrarAutonomo(autonomo);
        }

        public int EditarDados(string telefoneCorp, string emailCorp, string descricao)
        {

            AutonomoDAO aut = new AutonomoDAO();

            if (string.IsNullOrEmpty(descricao))
            {
                return 400;
            }

            Autonomo autonomo = new Autonomo();

            autonomo.Descricao = descricao;
            autonomo.EmailCorp = null;
            autonomo.TelefoneCorp = null;
            autonomo.ID = (int)HttpContext.Current.Session["aut_ID"];

            if (emailCorp != null && emailCorp.Length > 0)
            {
                autonomo.EmailCorp = emailCorp;
            }

            if (telefoneCorp != null && telefoneCorp.Length > 0)
            {
                autonomo.TelefoneCorp = telefoneCorp;
            }

            return aut.EditarAutonomo(autonomo);
        }

    }
}