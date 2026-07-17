using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Moderador
{
    public partial class Mensagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mod_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                }

                CarregarGrid();
            }
        }

        private void CarregarGrid()
        {
            PublicoController controller = new PublicoController();

            gvMensagens.DataSource = controller.ListarMensagens();

            gvMensagens.DataBind();
        }

        protected void gvMensagens_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvMensagens.DataKeys[e.RowIndex].Value);

            PublicoController controller = new PublicoController();

            controller.ExcluirMensagem(id);

            CarregarGrid();
        }
    }
}