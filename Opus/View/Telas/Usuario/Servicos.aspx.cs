using Opus.Controller;
using Opus.Model;
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
            if (Session["usu_ID"] == null)
            {
                Response.Redirect("Entrar.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CarregarServicos();
                CarregarEstados();
            }
            else
            {
                RecarregarPesquisa();
            }
        }

        AutonomoServicoController servicoController = new AutonomoServicoController();

        private void CarregarServicos()
        {

            ddlServico.DataSource = servicoController.ListarServicos();

            ddlServico.DataTextField = "Nome";
            ddlServico.DataValueField = "ID";

            ddlServico.DataBind();

            ddlServico.Items.Insert(0, new ListItem("Selecione um serviço", "0"));
        }

        private void CarregarEstados()
        {
            EstadoController controller = new EstadoController();

            ddlEstado.DataSource = controller.ListarEstados();

            ddlEstado.DataTextField = "Nome";
            ddlEstado.DataValueField = "ID";

            ddlEstado.DataBind();

            ddlEstado.Items.Insert(0, new ListItem("Selecione um Estado", "0"));

            ddlCidade.Items.Clear();

            ddlCidade.Items.Add(new ListItem("Escolha um Estado primeiro","0"));

            ddlCidade.Enabled = false;
        }

        // =====================================================
        // QUANDO O ESTADO FOR ALTERADO
        // =====================================================

        protected void ddlEstado_SelectedIndexChanged(object sender,EventArgs e)
        {
            ddlCidade.Items.Clear();

            if (ddlEstado.SelectedValue == "0")
            {
                ddlCidade.Items.Add(new ListItem("Escolha um Estado primeiro","0"));

                ddlCidade.Enabled = false;

                return;
            }

            int idEstado = Convert.ToInt32(ddlEstado.SelectedValue);

            CidadeController controller = new CidadeController();

            ddlCidade.DataSource = controller.ListarCidades(idEstado);

            ddlCidade.DataTextField = "Nome";
            ddlCidade.DataValueField = "ID";

            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Selecione uma Cidade", "0"));

            ddlCidade.Enabled = true;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            lblMensagemPesquisa.Text = "";

            if (ddlServico.SelectedValue == "0")
            {
                lblMensagemPesquisa.Text = "Selecione um serviço.";
                return;
            }

            if (ddlEstado.SelectedValue == "0")
            {
                lblMensagemPesquisa.Text = "Selecione um estado.";
                return;
            }

            if (ddlCidade.SelectedValue == "0")
            {
                lblMensagemPesquisa.Text = "Selecione uma cidade.";
                return;
            }

            int servicoID = Convert.ToInt32(ddlServico.SelectedValue);
            int cidadeID = Convert.ToInt32(ddlCidade.SelectedValue);

            AutonomoController controller = new AutonomoController();

            rptAutonomos.DataSource = controller.PesquisarAutonomos(servicoID, cidadeID);

            rptAutonomos.DataBind();

            ViewState["ServicoPesquisa"] = servicoID;
            ViewState["CidadePesquisa"] = cidadeID;
        }

        protected void rptAutonomos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Detalhes")
            {
                int autonomoID = Convert.ToInt32(e.CommandArgument);

                try
                {
                    CarregarPerfilAutonomo(autonomoID);

                    AbrirModalAutonomo();
                }
                catch (Exception ex)
                {
                    lblMensagemPesquisa.Text =
                        Server.HtmlEncode(ex.Message);
                }
            }
        }

        private void AbrirModalAutonomo()
        {
            string script = @"

                var modal = new bootstrap.Modal(
                document.getElementById('modalAutonomo')
                );

                modal.show();";

            ScriptManager.RegisterStartupScript(this,GetType(),"AbrirModalAutonomo",script,true);

        }

        private void CarregarPerfilAutonomo(int autonomoID)
        { 
            AutonomoController autonomoController = new AutonomoController();

            PortifolioController portifolioController = new PortifolioController();

            AvaliacaoController avaliacaoController = new AvaliacaoController();

            // Dados principais do profissional
            AutonomoPesquisaView autonomo = autonomoController.BuscarPerfil(autonomoID);

            if (autonomo == null)
            {
                throw new Exception("Profissional não encontrado.");
            }

            if (autonomo.QuantidadeAvaliacoes == 0)
            {
                lblNotaAutonomo.Text = "Ainda não possui avaliações.";
            }
            else
            {
                lblNotaAutonomo.Text =
                    $"Nota: {autonomo.NotaMedia:N1} / 5 " +
                    $"({autonomo.QuantidadeAvaliacoes} avaliações)";
            }

            lblNomeAutonomo.Text = autonomo.Nome;

            string telefoneLimpo = autonomo.Telefone;

            if (telefoneLimpo.Length == 11)
            {
                lblTelefone.Text = long.Parse(telefoneLimpo).ToString(@"(##) #####-####");
            }
            else if (telefoneLimpo.Length == 10)
            {
                lblTelefone.Text = long.Parse(telefoneLimpo).ToString(@"(##) ####-####");
            }
            else
            {
                lblTelefone.Text = telefoneLimpo; // Caso venha fora do padrão, exibe original
            }


            lblEmail.Text = autonomo.Email;

            lblDescricaoAutonomo.Text = Server.HtmlEncode(autonomo.Descricao);

            imgPerfil.ImageUrl = "~/Uploads/Usuario/" + autonomo.Foto;

            // Portfólios cadastrados
            rptPortifoliosPerfil.DataSource = portifolioController.ListarPortifolios(autonomoID);

            rptPortifoliosPerfil.DataBind();

            // Avaliações recebidas
            rptAvaliacoesPerfil.DataSource = avaliacaoController.ListarAvaliacoesAutonomo(autonomoID);

            rptAvaliacoesPerfil.DataBind();
        }

        private void RecarregarPesquisa()
        {
            if (ViewState["ServicoPesquisa"] == null ||
                ViewState["CidadePesquisa"] == null)
            {
                return;
            }

            int servicoID = Convert.ToInt32(ViewState["ServicoPesquisa"]);

            int cidadeID = Convert.ToInt32(ViewState["CidadePesquisa"]);

            AutonomoController controller = new AutonomoController();

            rptAutonomos.DataSource = controller.PesquisarAutonomos(servicoID, cidadeID);

            rptAutonomos.DataBind();
        }
    }
}