using Opus.Controller;
using Opus.View.Telas.Usuario;
using System;

namespace Opus.View.Telas.Autonomo
{
    public partial class Carreira : System.Web.UI.Page
    {
        AutonomoServicoController controller = new AutonomoServicoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["aut_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                }

                CarregarDropDownServico();
                CarregarGridServico();
            }
        }

        void CarregarDropDownServico()
        {
            ddlServico.DataSource = controller.ListarServicos();

            ddlServico.DataTextField = "Nome";
            ddlServico.DataValueField = "ID";

            ddlServico.DataBind();
        }

        void CarregarGridServico()
        {
            gvServicos.DataSource = controller.ListarServicosAutonomo();
            gvServicos.DataBind();
        }

        protected void btnAdicionarServico_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlServico.SelectedValue);

            int resultado = controller.AdicionarServico(id);

            if (resultado == 409)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Serviço já existe", "alert('Este serviço já está cadastrado!');", true);
            }
            CarregarGridServico();
        }

        protected void gvServicos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvServicos.DataKeys[e.RowIndex].Value);

            controller.ExcluirServico(id);

            CarregarGridServico();
        }
    }
}