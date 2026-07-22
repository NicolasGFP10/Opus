using Opus.Controller;
using System;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Autonomo
{
    public partial class Carreira : System.Web.UI.Page
    {
        AutonomoServicoController servicoController = new AutonomoServicoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["aut_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                    return;
                }

                CarregarServicos();
                CarregarGridServicos();

                CarregarEstados();
                CarregarGridCidade();
            }
        }

        private void CarregarEstados()
        {
            EstadoController controller = new EstadoController();

            ddlEstado.DataSource = controller.ListarEstados();

            ddlEstado.DataTextField = "Nome";
            ddlEstado.DataValueField = "ID";

            ddlEstado.DataBind();

            ddlEstado.Items.Insert(0,
                new ListItem("Selecione um estado", "0"));

            ddlCidade.Items.Clear();

            ddlCidade.Items.Add(
                new ListItem("Escolha um estado primeiro", "0"));

            ddlCidade.Enabled = false;
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCidade.Items.Clear();

            if (ddlEstado.SelectedValue == "0")
            {
                ddlCidade.Enabled = false;

                ddlCidade.Items.Add(
                    new ListItem("Escolha um estado primeiro", "0"));

                return;
            }

            CidadeController controller = new CidadeController();

            ddlCidade.DataSource =
                controller.ListarPorEstado(
                    Convert.ToInt32(ddlEstado.SelectedValue));

            ddlCidade.DataTextField = "Nome";
            ddlCidade.DataValueField = "ID";

            ddlCidade.DataBind();

            ddlCidade.Enabled = true;
        }

        protected void btnSalvarCidade_Click(object sender, EventArgs e)
        {
            if (ddlCidade.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "erro",
                    "alert('Escolha uma cidade.');",
                    true);

                return;
            }

            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            int resultado =
                controller.CadastrarCidadeAutonomo(
                    Convert.ToInt32(ddlCidade.SelectedValue));

            switch (resultado)
            {
                case 200:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "ok",
                        "alert('Cidade adicionada.');",
                        true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "erro",
                        "alert('Essa cidade já foi adicionada.');",
                        true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "erro",
                        "alert('Erro ao adicionar cidade.');",
                        true);

                    break;
            }

            CarregarGridCidade();
        }

        private void CarregarGridCidade()
        {
            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            gvRegiao.DataSource =
                controller.ListarCidades();

            gvRegiao.DataBind();
        }

        protected void gvRegiao_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(
                gvRegiao.DataKeys[e.RowIndex].Value);

            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            controller.ExcluirCidade(id);

            CarregarGridCidade();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            int cidade = Convert.ToInt32(ddlCidade.SelectedValue);

            AutonomoCidadeController controller = new AutonomoCidadeController();

            int resultado = controller.CadastrarCidade(cidade);

            switch (resultado)
            {
                case 200:

                    CarregarGrid();

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "Erro",
                        "alert('Essa cidade já foi cadastrada.');",
                        true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "Erro",
                        "alert('Erro ao cadastrar cidade.');",
                        true);

                    break;
            }
        }

        private void CarregarGrid()
        {
            AutonomoCidadeController controller = new AutonomoCidadeController();

            gvRegiao.DataSource = controller.ListarCidades();

            gvRegiao.DataBind();
        }

        protected void gvCidades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvRegiao.DataKeys[e.RowIndex].Value);

            AutonomoCidadeController controller = new AutonomoCidadeController();

            controller.ExcluirCidade(id);

            CarregarGrid();
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