using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Usuario
{
    public partial class Entrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            UsuarioController usu = new UsuarioController();

            string email = tbxEmail.Text;
            string senha = tbxSenha.Text;

            int resultado = usu.ValidarLogin(email, senha);

            switch(resultado)
            {
                case 200:

                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Login realizado com sucesso');", true);

                    Response.Redirect("Servicos.aspx");

                break;

                case 404:

                    ClientScript.RegisterStartupScript(this.GetType(), "Acesso negado", "alert('Dados de usuário não encontrados');", true);

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