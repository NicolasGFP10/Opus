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

                    labNome.Text = Session["usu_nome"].ToString();
                    imgUsuario.Visible = true;
                    imgUsuario.ImageUrl = "~/Uploads/Usuario/" + Session["usu_imagem"].ToString();
                    
                }
            }
        }

        protected void imgUsuario_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Configuracao.aspx");
        }
    }
}