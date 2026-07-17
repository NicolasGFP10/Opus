using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Usuario
{
    public partial class Suporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usu_email"] != null)
            {
                tbxEmail.Visible = false;
                lblEmail.Text = "- " + Session["usu_email"].ToString(); ;
            }
        }

        protected void EnviarMensagem(object sender, EventArgs e)
        {

            string email;
            string texto = tbxMensagem.Text;
            
            if (Session["usu_email"] != null)
            {
                email = Session["usu_email"].ToString();
            }
            else
            {
                email = tbxEmail.Text;
            }

            Controller.PublicoController pub = new Controller.PublicoController();

            int resultado = pub.ValidarMensagem(email, texto);

            switch (resultado)
            {
                case 200:
                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Mensagem enviada com sucesso');", true);
                    break;
                case 400:
                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Preencha todos os campos');", true);
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