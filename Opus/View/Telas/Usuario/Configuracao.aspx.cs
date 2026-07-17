using Opus.DAO;
using Opus.View.Telas.Autonomo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Usuario
{
    public partial class Configuracao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Session["usu_ID"] == null)
                {
                    Response.Redirect("Entrar.aspx");
                }

                if (Session["aut_ID"] != null)
                {
                    btnAuto.Visible = false; 
                }
            }
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Session["usu_ID"] = null;
            Session["usu_nome"] = null;
            Session["usu_imagem"] = null;
            Session["usu_email"] = null;

            Session["aut_ID"] = null;
            Session["aut_email_corp"] = null;
            Session["aut_telefone_corp"] = null;
            Session["aut_descricao"] = null;

            Session["mod_ID"] = null;

            Response.Redirect("Default.aspx");
        }

        protected void btnAuto_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Autonomo/CadastrarAutonomo.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditarUsuario.aspx");
        }

        protected void btnDesativar_Click(object sender, EventArgs e)
        {
            AutonomoDAO aut = new AutonomoDAO();
            UsuarioDAO usu = new UsuarioDAO();

            int resultado = usu.DesativarUsuario(Session["usu_ID"].ToString());

            switch (resultado)
            {
                case 200:

                    if (Session["aut_ID"] != null)
                    {
                        aut.DesativarAutonomo(Session["aut_ID"].ToString());
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Usuário desativado');", true);

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