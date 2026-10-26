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
                    Response.Redirect("~/View/Telas/Login.aspx");
                }
            }
        }

        protected void rptDados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Detalhes")
            {
                AutonomoController aut = new AutonomoController();

                int id = Convert.ToInt32(e.CommandArgument);

                try
                {
                    //CarregarPerfilUsuario(id);

                    int resultado = aut.ValidarConsultaAutonomo(id);

                    if (resultado > 0)
                    {
                        CarregarPerfilAutonomo(resultado);
                    }

                    AbrirModalDados();
                }
                catch (Exception ex)
                {
                    lblMensagemPesquisa.Text = Server.HtmlEncode(ex.Message);
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

        /*private void CarregarPerfilUsuario(int usuarioID)
        {
            UsuarioController usuarioController = new UsuarioController();

            AvaliacaoController avaliacaoController = new AvaliacaoController();

            Usuario usuario = usuarioController.BuscarUsuario(usuarioID);

            if (usuario == null)
            {
                lblMensagemPesquisa.Text = "Usuário não encontrado.";
            }
        }*/

        // ===================================================================================================
        // CARREGAR AUTÔNOMO
        // ===================================================================================================

        private void CarregarPerfilAutonomo(int autonomoID)
        {
            AutonomoController autonomoController = new AutonomoController();

            PortifolioController portifolioController = new PortifolioController();

            AvaliacaoController avaliacaoController = new AvaliacaoController();

            AutonomoCidadeController cidadeController = new AutonomoCidadeController();

            AutonomoPesquisaView autonomo = autonomoController.BuscarPerfil(autonomoID);

            AutonomoServicoController servicoController = new AutonomoServicoController();

            if (autonomo == null)
            {
                throw new Exception("Profissional não encontrado.");
            }

            hfUsuarioDenunciadoID.Value = autonomo.UsuarioID.ToString();

            if (autonomo.QuantidadeAvaliacoes == 0)
            {
                lblNotaAutonomo.Text = "Ainda não possui avaliações.";
            }
            else
            {
                lblNotaAutonomo.Text = $"Nota: {autonomo.NotaMedia:N1} / 5 " + $"({autonomo.QuantidadeAvaliacoes} avaliações)";
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

            // Cidades atendidas
            rptRegioesPerfil.DataSource = cidadeController.ListarCidadesAutonomo(autonomoID);

            rptRegioesPerfil.DataBind();

            // Serviços a prestar
            rptServicosPerfil.DataSource = servicoController.ListarServicosAutonomo(autonomoID);

            rptServicosPerfil.DataBind();

        }
    }
}