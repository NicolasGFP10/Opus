using Opus.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Session["usu_ID"] != null)
                {
                    lbtServicos.Visible = true;
                    lbtAvaliacao.Visible = true;
                    lbtEntrar.Visible = false;
                    imgDeslogado.Visible = false;

                    lbtConfiguracao.Visible = true;
                    labNome.Text = Session["usu_nome"].ToString();
                    imgUsuario.ImageUrl = "~/Uploads/Usuario/" + Session["usu_imagem"].ToString();
                    imgUsuario.Visible = true;

                }

                if (Session["aut_ID"] != null && Session["usu_ID"] != null)
                {
                    lbtCarreira.Visible = true;
                }

                if (Session["usu_ID"] != null && Session["mod_ID"] != null)
                {
                    lbtSobre.Visible = false;
                    lbtSuporte.Visible = false;
                    lbtServicos.Visible = false;
                    lbtAvaliacao.Visible = false;
                    lbtMensagem.Visible = true;
                    lbtDenuncias.Visible = true;
                    lbtContas.Visible = true;
                    lbtTrabalhos.Visible = true;
                    lbtRegioes.Visible = true;
                }
            }
        }

        protected void imgUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Usuario/Configuracao.aspx");
        }
    }
}