using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Usuario
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usu_ID"] == null)
                {
                    Response.Redirect("Entrar.aspx");
                }

                tbxNome.Text = Session["usu_nome"].ToString();
                tbxEmail.Text = Session["usu_email"].ToString();
                tbxSenha.Text = Session["usu_senha"].ToString();
                tbxTelefone.Text = Session["usu_telefone"].ToString();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nome = tbxNome.Text;
            string email = tbxEmail.Text;
            string senha = tbxSenha.Text;
            string telefone = tbxTelefone.Text;

            UsuarioController usu = new UsuarioController();

            int resultado = usu.EditarDados(nome, email, telefone, senha);

            switch (resultado)
            {
                case 200:

                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Dados alterados com sucesso!');", true);

                    Session["usu_id"] = null;
                    Session["usu_nome"] = null;
                    Session["usu_email"] = null;
                    Session["usu_telefone"] = null;
                    Session["usu_senha"] = null;
                    Session["usu_imagem"] = null;

                    Response.Redirect("Entrar.aspx");

                    break;

                case 500:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                break;

                default:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                break;
            }
        }
    }
}