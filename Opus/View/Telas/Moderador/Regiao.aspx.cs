using Opus.Controller;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Moderador
{
    public partial class Regiao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mod_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                    return;
                }

                CarregarEstados();
                CarregarGridEstados();
                CarregarGridCidades();
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
                new ListItem(
                    "Selecione um Estado",
                    "0"));
        }

        private void CarregarGridEstados()
        {
            EstadoController controller =
                new EstadoController();

            gvEstados.DataSource =
                controller.ListarEstados();

            gvEstados.DataBind();
        }

        protected void CadastrarEstado(
            object sender,
            EventArgs e)
        {
            string estado =
                tbxEstado.Text.Trim();

            EstadoController controller =
                new EstadoController();

            int resultado =
                controller.ValidarEstado(estado);

            switch (resultado)
            {
                case 200:

                    tbxEstado.Text = "";

                    CarregarEstados();
                    CarregarGridEstados();

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Sucesso",
                        "alert('Estado cadastrado com sucesso!');",
                        true);

                    break;

                case 400:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Preencha o nome do Estado!');",
                        true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Este Estado já está cadastrado!');",
                        true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Não foi possível cadastrar o Estado.');",
                        true);

                    break;
            }
        }

        protected void gvEstados_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            int id =
                Convert.ToInt32(
                    gvEstados.DataKeys[e.RowIndex].Value);

            EstadoController controller =
                new EstadoController();

            int resultado =
                controller.ExcluirEstado(id);

            if (resultado == 200)
            {
                CarregarEstados();
                CarregarGridEstados();
                CarregarGridCidades();

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Sucesso",
                    "alert('Estado excluído com sucesso!');",
                    true);
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Erro",
                    "alert('Não foi possível excluir o Estado. Verifique se existem cidades vinculadas a ele.');",
                    true);
            }
        }

        // =====================================================
        // CIDADES
        // =====================================================

        private void CarregarGridCidades()
        {
            CidadeController controller = new CidadeController();

            gvCidades.DataSource = controller.ListarTodasCidades();

            gvCidades.DataBind();
        }

        protected void CadastrarCidade(
            object sender,
            EventArgs e)
        {
            if (ddlEstado.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Erro",
                    "alert('Selecione um Estado.');",
                    true);

                return;
            }

            string cidade =
                tbxCidade.Text.Trim();

            int estado =
                Convert.ToInt32(
                    ddlEstado.SelectedValue);

            CidadeController controller =
                new CidadeController();

            int resultado =
                controller.ValidarCidade(
                    cidade,
                    estado);

            switch (resultado)
            {
                case 200:

                    tbxCidade.Text = "";

                    CarregarGridCidades();

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Sucesso",
                        "alert('Cidade cadastrada com sucesso!');",
                        true);

                    break;

                case 400:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Preencha todos os campos!');",
                        true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Essa cidade já está cadastrada neste Estado.');",
                        true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "Erro",
                        "alert('Não foi possível cadastrar a cidade.');",
                        true);

                    break;
            }
        }

        protected void gvCidades_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            int id =
                Convert.ToInt32(
                    gvCidades.DataKeys[e.RowIndex].Value);

            CidadeController controller = new CidadeController();

            int resultado = controller.ExcluirCidade(id);

            if (resultado == 200)
            {
                CarregarGridCidades();

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Sucesso",
                    "alert('Cidade excluída com sucesso!');",
                    true);
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "Erro",
                    "alert('Não foi possível excluir a cidade.');",
                    true);
            }
        }
    }
}