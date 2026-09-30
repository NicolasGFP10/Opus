using Opus.Controller;
using System;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Autonomo
{
    public partial class Carreira : System.Web.UI.Page
    {
        AutonomoServicoController servicoController =
            new AutonomoServicoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["aut_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                    return;
                }

                // =========================
                // SERVIÇOS
                // =========================

                CarregarServicos();
                CarregarGridServicos();

                // =========================
                // REGIÕES
                // =========================

                CarregarEstados();
                CarregarGridCidade();
            }
        }

        // =====================================================
        // SERVIÇOS
        // =====================================================

        private void CarregarServicos()
        {
            ddlServico.DataSource =
            servicoController.ListarServicos();

            ddlServico.DataTextField = "Nome";
            ddlServico.DataValueField = "ID";

            ddlServico.DataBind();

            ddlServico.Items.Insert(
                0,
                new ListItem("Selecione um serviço", "0"));
        }

        private void CarregarGridServicos()
        {
            gvServicos.DataSource =
                servicoController.ListarServicosAutonomo();

            gvServicos.DataBind();
        }

        protected void btnAdicionarServico_Click(
            object sender,
            EventArgs e)
        {
            if (ddlServico.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Erro",
                    "alert('Selecione um serviço.');",
                    true);

                return;
            }

            int id =
                Convert.ToInt32(ddlServico.SelectedValue);

            int resultado =
                servicoController.AdicionarServico(id);

            switch (resultado)
            {
                case 200:

                    CarregarGridServicos();

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Sucesso",
                        "alert('Serviço adicionado com sucesso!');",
                        true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Este serviço já está cadastrado!');",
                        true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Não foi possível adicionar o serviço.');",
                        true);

                    break;
            }
        }

        protected void gvServicos_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            int id =
                Convert.ToInt32(
                    gvServicos.DataKeys[e.RowIndex].Value);

            int resultado =
                servicoController.ExcluirServico(id);

            if (resultado == 200)
            {
                CarregarGridServicos();
            }
        }

        // =====================================================
        // ESTADOS
        // =====================================================

        private void CarregarEstados()
        {
            EstadoController controller =
                new EstadoController();

            ddlEstado.DataSource =
                controller.ListarEstados();

            ddlEstado.DataTextField = "Nome";
            ddlEstado.DataValueField = "ID";

            ddlEstado.DataBind();

            ddlEstado.Items.Insert(
                0,
                new ListItem("Selecione um Estado", "0"));

            ddlCidade.Items.Clear();

            ddlCidade.Items.Add(
                new ListItem(
                    "Escolha um Estado primeiro",
                    "0"));

            ddlCidade.Enabled = false;
        }

        // =====================================================
        // QUANDO O ESTADO FOR ALTERADO
        // =====================================================

        protected void ddlEstado_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ddlCidade.Items.Clear();

            if (ddlEstado.SelectedValue == "0")
            {
                ddlCidade.Items.Add(
                    new ListItem(
                        "Escolha um Estado primeiro",
                        "0"));

                ddlCidade.Enabled = false;

                return;
            }

            int idEstado =
                Convert.ToInt32(
                    ddlEstado.SelectedValue);

            CidadeController controller =
                new CidadeController();

            // IMPORTANTE:
            // O seu CidadeController usa ListarCidades()
            // e não ListarPorEstado().

            ddlCidade.DataSource =
                controller.ListarCidades(idEstado);

            ddlCidade.DataTextField = "Nome";
            ddlCidade.DataValueField = "ID";

            ddlCidade.DataBind();

            ddlCidade.Items.Insert(
                0,
                new ListItem(
                    "Selecione uma Cidade",
                    "0"));

            ddlCidade.Enabled = true;
        }

        // =====================================================
        // ADICIONAR CIDADE
        // =====================================================

        protected void btnSalvarCidade_Click(
            object sender,
            EventArgs e)
        {
            if (ddlCidade.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Erro",
                    "alert('Selecione uma cidade.');",
                    true);

                return;
            }

            int cidade =
                Convert.ToInt32(
                    ddlCidade.SelectedValue);

            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            int resultado =
                controller.CadastrarCidadeAutonomo(cidade);

            switch (resultado)
            {
                case 200:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Sucesso",
                        "alert('Cidade adicionada com sucesso!');",
                        true);

                    CarregarGridCidade();

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Essa cidade já foi adicionada.');",
                        true);

                    break;

                case 400:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Cidade inválida.');",
                        true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Erro ao adicionar cidade.');",
                        true);

                    break;
            }
        }

        // =====================================================
        // GRID DE CIDADES
        // =====================================================

        private void CarregarGridCidade()
        {
            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            gvRegiao.DataSource =
                controller.ListarCidadesAutonomo();

            gvRegiao.DataBind();
        }

        // =====================================================
        // EXCLUIR CIDADE
        // =====================================================

        protected void gvRegiao_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            int id =
                Convert.ToInt32(
                    gvRegiao.DataKeys[e.RowIndex].Value);

            AutonomoCidadeController controller =
                new AutonomoCidadeController();

            int resultado =
                controller.ExcluirCidade(id);

            if (resultado == 200)
            {
                CarregarGridCidade();
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Erro",
                    "alert('Não foi possível remover a cidade.');",
                    true);
            }
        }
    }
}