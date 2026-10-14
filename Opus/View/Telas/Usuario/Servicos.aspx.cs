using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Usuario
{
    public partial class Servicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usu_ID"] == null)
                {
                    Response.Redirect("Entrar.aspx");
                }

                CarregarServicos();
                CarregarEstados();

            }
        }

        AutonomoServicoController servicoController =
            new AutonomoServicoController();

        private void CarregarServicos()
        {


            ddlServico.DataSource =
            servicoController.ListarServicos();

            ddlServico.DataTextField = "Nome";
            ddlServico.DataValueField = "ID";

            ddlServico.DataBind();

            ddlServico.Items.Insert(0, new ListItem("Selecione um serviço", "0"));
        }

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
    }
}