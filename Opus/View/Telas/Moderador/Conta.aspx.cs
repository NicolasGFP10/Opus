using Opus.Controller;
using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Moderador
{
    public partial class Conta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mod_ID"] == null || Session["usu_ID"] == null)
                {
                    Response.Redirect("~/View/Telas/Usuario/Entrar.aspx");
                }
            }
        }

        private void CarregarDados()
        {
            try
            {
                lblMensagemPesquisa.Text = "";

                string pesquisa = tbxPesquisa.Text.Trim();

                int filtro = Convert.ToInt32(
                    ddlFiltro.SelectedValue
                );

                UsuarioController controller = new UsuarioController();

                List<Model.Usuario> usuarios = controller.PesquisarUsuario(pesquisa, filtro);

                rptDados.DataSource = usuarios;
                rptDados.DataBind();

                if (usuarios.Count == 0)
                {
                    lblMensagemPesquisa.Text = "Nenhuma conta encontrada.";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                lblMensagemPesquisa.Text = "Não foi possível realizar a pesquisa.";
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        protected void rptDados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Detalhes")
            {

                if (e.CommandName == "Detalhes")
                {
                    AutonomoController aut = new AutonomoController();

                    int id = Convert.ToInt32(e.CommandArgument);

                    try
                    {
                        CarregarPerfilUsuario(id);

                        int resultado = aut.ValidarConsultaAutonomo(id);

                        switch (resultado)
                        {
                            case -400:
                                lblMensagemPesquisa.Text = "ID inválido";
                                break;

                            case -500:
                                lblMensagemPesquisa.Text = "Erro interno do servidor";
                                break;

                            case -404:

                                lblRegioes.Visible = false;
                                lblServico.Visible = false;
                                lblPortifolio.Visible = false;
                                lblAvaliacoes.Visible = false;

                                break;

                            default:

                                lblRegioes.Visible = true;
                                lblServico.Visible = true;
                                lblPortifolio.Visible = true;
                                lblAvaliacoes.Visible = true;

                                CarregarPerfilAutonomo(resultado);

                                break;
                        }

                        AbrirModalDados();
                    }
                    catch (Exception ex)
                    {
                        lblMensagemPesquisa.Text = Server.HtmlEncode(ex.Message);
                    }
                }

            }
        }

        private void AbrirModalDados()
        {
            string script = @"
        var modal = bootstrap.Modal.getOrCreateInstance(
            document.getElementById('modalDado')
        );

        modal.show();
    ";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "AbrirModalDados",
                script,
                true
            );
        }

        // ===================================================================================================
        // CARREGAR USUÁRIO
        // ===================================================================================================

        private void CarregarPerfilUsuario(int id)
        {

            UsuarioController usuarioController = new UsuarioController();

            AvaliacaoController avaliacaoController = new AvaliacaoController();

            Model.Usuario usu = new Model.Usuario();

            usu = usuarioController.DadosUsuario(id);

            hfUsuarioDadoID.Value = id.ToString();

            imgPerfilUsuario.ImageUrl = "~/Uploads/Usuario/" + usu.Imagem;

            lblNomeUsuario.Text = usu.Nome;

            lblEmailUsuario.Text = usu.Email;

            string telefoneLimpo = usu.Telefone;

            if (telefoneLimpo.Length == 11)
            {
                lblTelefoneUsuario.Text = long.Parse(telefoneLimpo).ToString(@"(##) #####-####");
            }
            else if (telefoneLimpo.Length == 10)
            {
                lblTelefoneUsuario.Text = long.Parse(telefoneLimpo).ToString(@"(##) ####-####");
            }
            else
            {
                lblTelefoneUsuario.Text = telefoneLimpo; // Caso venha fora do padrão, exibe original
            }

            if (usu.Status == true)
            {
                lblStatusUsuario.Text = "Ativo";
            }
            else
            {
                lblStatusUsuario.Text = "Inativo";
            }

            // Limpar dados

            lblNotaAutonomo.Text = null;
            lblTelefoneCorp.Text = null;
            lblEmailCorp.Text = null;
            lblDescricaoAutonomo.Text = null;

            // Portifólio
            rptPortifoliosPerfil.DataSource = null;

            rptPortifoliosPerfil.DataBind();

            // Avaliações recebidas
            rptAvaliacoesPerfil.DataSource = null;

            rptAvaliacoesPerfil.DataBind();

            // Cidades atendidas
            rptRegioesPerfil.DataSource = null;

            rptRegioesPerfil.DataBind();

            // Serviços a prestar
            rptServicosPerfil.DataSource = null;

            rptServicosPerfil.DataBind();

        }

        // ===================================================================================================
        // CARREGAR AUTÔNOMO
        // ===================================================================================================

        private void CarregarPerfilAutonomo(int autonomoID)
        {
            AutonomoController autonomoController = new AutonomoController();

            PortifolioController portifolioController = new PortifolioController();

            AvaliacaoController avaliacaoController = new AvaliacaoController();

            AutonomoCidadeController cidadeController = new AutonomoCidadeController();

            AutonomoPesquisaView autonomo = autonomoController.BuscarPerfilCompleto(autonomoID);

            AutonomoServicoController servicoController = new AutonomoServicoController();

            if (autonomo.QuantidadeAvaliacoes == 0)
            {
                lblNotaAutonomo.Text = "Ainda não possui avaliações.";
            }
            else
            {
                lblNotaAutonomo.Text = $"Nota: {autonomo.NotaMedia:N1} / 5 " + $"({autonomo.QuantidadeAvaliacoes} avaliações)";
            }

            string telefoneLimpo = autonomo.TelefoneCorp;

            if (telefoneLimpo.Length == 11)
            {
                lblTelefoneCorp.Text = "Telefone corporativo: " + long.Parse(telefoneLimpo).ToString(@"(##) #####-####");
            }
            else if (telefoneLimpo.Length == 10)
            {
                lblTelefoneCorp.Text = "Telefone corporativo: " + long.Parse(telefoneLimpo).ToString(@"(##) ####-####");
            }
            else
            {
                lblTelefoneCorp.Text = "Telefone corporativo: " + telefoneLimpo; // Caso venha fora do padrão, exibe original
            }

            lblEmailCorp.Text = "E-mail corporativo: " + autonomo.Email;

            lblDescricaoAutonomo.Text = Server.HtmlEncode(autonomo.Descricao);

            // Portfólios cadastrados
            rptPortifoliosPerfil.DataSource = portifolioController.ListarPortifolios(autonomoID);

            rptPortifoliosPerfil.DataBind();

            // Avaliações recebidas
            rptAvaliacoesPerfil.DataSource = avaliacaoController.ListarAvaliacoesAutonomo(autonomoID);

            rptAvaliacoesPerfil.DataBind();

            // Cidades atendidas
            rptRegioesPerfil.DataSource = cidadeController.ListarCidadesAutonomo(autonomoID);

            rptRegioesPerfil.DataBind();

            // Serviços a prestar
            rptServicosPerfil.DataSource = servicoController.ListarServicosAutonomo(autonomoID);

            rptServicosPerfil.DataBind();

        }

        protected void btnGerenciar_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(hfUsuarioDadoID.Value);

            int status = lblStatusUsuario.Text == "Ativo" ? 1 : 0;

            UsuarioController controller = new UsuarioController();

            int resultado = controller.AlterarStatus(id, status);

            switch (resultado)
            {
                case 400:
                    lblMensagemPesquisa.Text = "ID não encontrado";
                    break;

                case 200:
                    lblStatusUsuario.Text = "OK";
                    CarregarDados();
                    break;

                case 500:
                    lblStatusUsuario.Text = "Erro interno no servidor";
                    break;
            }
        }
    }
}