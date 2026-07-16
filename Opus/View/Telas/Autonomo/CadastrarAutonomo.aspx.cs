using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Autonomo
{
    public partial class CadastrarAutonomo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usu_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                }
            }
        }

        protected void btnCadastro(object sender, EventArgs e)
        {

            string cnpj = Regex.Replace(tbxCNPJ.Text, @"\D", "");
            string email = tbxEmail.Text.ToString();
            string telefone = Regex.Replace(tbxTelefone.Text, @"\D", "");
            string descricao = tbxDescricao.Text.ToString();

            AutonomoController aut = new AutonomoController();

            int resultado = aut.ValidarCadastro(cnpj, email, telefone, descricao);

            switch (resultado)
            {
                case 200:

                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Cadastro realizado com sucesso!');", true);

                    Response.Redirect("Carreira.aspx");

                    break;

                case 400:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Preencha todos os dados corretamente!');", true);

                break;

                case 406:

                    ClientScript.RegisterStartupScript(this.GetType(), "CNPJ inválido", "alert('Insira um CNPJ válido!');", true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(this.GetType(), "Conta já existente", "alert('Está conta já está cadastrada!');", true);

                    break;

                case 500:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro no Sistema", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro no Sistema", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;
            }

        }
    }
}