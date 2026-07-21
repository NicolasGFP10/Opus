using Opus.Controller;
using Opus.View.Telas.Usuario;
using System;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Autonomo
{
    public partial class Carreira : System.Web.UI.Page
    {
        AutonomoServicoController servicoController = new AutonomoServicoController();
        AutonomoRegiaoController regiaoController = new AutonomoRegiaoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["usu_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                }

                CarregarDropDownServico();
                CarregarGridServico();

                CarregarEstados();

                ddlCidade.Items.Clear();

                ddlCidade.Items.Add("Escolha um estado");

                ddlCidade.Enabled = false;
            }
        }

        // ===================================== SERVIÇO =====================================

        void CarregarDropDownServico()
        {
            ddlServico.DataSource = servicoController.ListarServicos();

            ddlServico.DataTextField = "Nome";
            ddlServico.DataValueField = "ID";

            ddlServico.DataBind();
        }

        void CarregarGridServico()
        {
            gvServicos.DataSource = servicoController.ListarServicosAutonomo();
            gvServicos.DataBind();
        }

        protected void btnAdicionarServico_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlServico.SelectedValue);

            int resultado = servicoController.AdicionarServico(id);

            if (resultado == 409)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Serviço já existe", "alert('Este serviço já está cadastrado!');", true);
            }
            CarregarGridServico();
        }

        protected void gvServicos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvServicos.DataKeys[e.RowIndex].Value);

            servicoController.ExcluirServico(id);

            CarregarGridServico();
        }

        // ===================================== REGIÃO =====================================

        private void CarregarEstados()
        {
            EstadoController controller = new EstadoController();

            ddlEstado.DataSource = controller.ListarEstados();

            ddlEstado.DataTextField = "Nome";

            ddlEstado.DataValueField = "ID";

            ddlEstado.DataBind();

            ddlEstado.Items.Insert(0,
                new ListItem("Escolha um estado", "0"));
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedValue == "0")
            {
                ddlCidade.Items.Clear();

                ddlCidade.Items.Add("Escolha um estado");

                ddlCidade.Enabled = false;

                return;
            }

            CidadeController controller = new CidadeController();

            ddlCidade.DataSource =
                controller.ListarCidades(Convert.ToInt32(ddlEstado.SelectedValue));

            ddlCidade.DataTextField = "Nome";

            ddlCidade.DataValueField = "ID";

            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0,
                new ListItem("Escolha uma cidade", "0"));

            ddlCidade.Enabled = true;
        }

        protected void btnSalvarCidade_Click(object sender, EventArgs e)
        {
            if (ddlCidade.SelectedValue == "0")
                return;

            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            controller.CadastrarCidade(
                Convert.ToInt32(ddlCidade.SelectedValue));

            Response.Redirect(Request.RawUrl);
        }
    }
}