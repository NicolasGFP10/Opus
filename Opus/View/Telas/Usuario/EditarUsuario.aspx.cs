using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

                if (Session["aut_ID"] != null)
                {
                    tbxEmailCorp.Visible = true;
                    tbxTelefoneCorp.Visible = true;
                    tbxDescricao.Visible = true;
                    Label1.Visible = true;
                    Label2.Visible = true;
                    Label3.Visible = true;

                    tbxEmailCorp.Text = Session["aut_email_corp"].ToString();
                    tbxTelefoneCorp.Text = Session["aut_telefone_corp"].ToString();
                    tbxDescricao.Text = Session["aut_descricao"].ToString();
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
            string telefone = Regex.Replace(tbxTelefone.Text, @"\D", "");


            string emailCorp = tbxEmailCorp.Text;
            string telefoneCorp = Regex.Replace(tbxTelefoneCorp.Text, @"\D", "");
            string descricao = tbxDescricao.Text;

            UsuarioController usu = new UsuarioController();
            AutonomoController aut = new AutonomoController();

            HttpPostedFile imagem = null;

            if (fuImagem.HasFile)
            {
                imagem = fuImagem.PostedFile;
            }

            int resultadoUsu = usu.EditarDados(nome, email, telefone, senha, imagem);

            if (Session["aut_ID"] != null)
            {

                switch (resultadoUsu)
                {
                    case 200:

                        ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Dados alterados com sucesso!');", true);

                        Session["usu_id"] = null;
                        Session["usu_nome"] = null;
                        Session["usu_email"] = null;
                        Session["usu_telefone"] = null;
                        Session["usu_senha"] = null;
                        Session["usu_imagem"] = null;

                        break;

                    case 413:

                        ClientScript.RegisterStartupScript(this.GetType(), "Imagem muito grande", "alert('Escolha um arquivo menor!');", true);

                        break;

                    case 415:

                        ClientScript.RegisterStartupScript(this.GetType(), "Arquivo sem suporte", "alert('Escolha um arquivo do tipo imagem!');", true);

                        break;

                    case 500:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                        break;

                    default:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                        break;
                }

                int resultadoAut = aut.EditarDados(telefoneCorp, emailCorp, descricao);
                
                switch (resultadoAut)
                {
                    case 200:

                        ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Dados de autônomo alterados com sucesso!');", true);

                        Session["aut_ID"] = null;
                        Session["aut_email_corp"] = null;
                        Session["aut_telefone_corp"] = null;
                        Session["aut_descricao"] = null;

                        break;

                    case 500:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                        break;

                    default:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                        break;
                }

                if(resultadoAut == 200 || resultadoUsu == 200) {
                
                    Response.Redirect("Entrar.aspx");

                }
            }
        }
    }
}