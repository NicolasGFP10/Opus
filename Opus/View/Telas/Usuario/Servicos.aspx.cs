using Opus.Controller;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

        private int autonomoID;

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

            ddlCidade.Items.Add(new ListItem("Escolha um Estado primeiro", "0"));

            ddlCidade.Enabled = false;
        }

        // =====================================================
        // QUANDO O ESTADO FOR ALTERADO
        // =====================================================

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCidade.Items.Clear();

            if (ddlEstado.SelectedValue == "0")
            {
                ddlCidade.Items.Add(new ListItem("Escolha um Estado primeiro", "0"));

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
                autonomoID = Convert.ToInt32(e.CommandArgument);

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
        var modal = bootstrap.Modal.getOrCreateInstance(
            document.getElementById('modalAutonomo')
        );

        modal.show();
    ";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "AbrirModalAutonomo",
                script,
                true
            );
        }

        private void AbrirModalDenunciaAutonomo()
        {
            string script = @"
        var elemento = document.getElementById('modalDenunciaAutonomo');

        if (elemento) {
            var modal = bootstrap.Modal.getOrCreateInstance(elemento);
            modal.show();
        } else {
            console.error('Modal de denúncia não encontrado!');
        }
    ";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "AbrirModalDenunciaAutonomo",
                script,
                true
            );
        }

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

            // Cidades atendidas
            rptRegioesPerfil.DataSource = cidadeController.ListarCidadesAutonomo(autonomoID);

            rptRegioesPerfil.DataBind();

            // Serviços a prestar
            rptServicosPerfil.DataSource = servicoController.ListarServicosAutonomo(autonomoID);

            rptServicosPerfil.DataBind();

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

        protected void btnDenunciar_Click(object sender, EventArgs e)
        {
            int usuarioDenunciadoID;

            if (!int.TryParse(hfUsuarioDenunciadoID.Value, out usuarioDenunciadoID) || usuarioDenunciadoID <= 0)
            {
                lblMensagemPesquisa.Text = "Não foi possível identificar o usuário denunciado.";

                return;
            }

            // Abrimos o modal de denúncia
            AbrirModalDenunciaAutonomo();
        }

        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {

            string titulo = tbxTitulo.Text;
            string descricao = tbxDescricao.Text;
            int categoria = Convert.ToInt32(ddlCategoria.SelectedValue);

            int denunciado = autonomoID;
            int denunciante = Convert.ToInt32(Session["usu_ID"]);

            var arquivos = fuFotos.PostedFiles
                    .Cast<HttpPostedFile>()
                    .Where(a => a.ContentLength > 0)
                    .ToList();

            if (arquivos.Count > 5)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Cadastre apenas 5 fotos');", true);

                return;
            }

            foreach (HttpPostedFile arquivo in arquivos)
            {
                ValidarImagem(arquivo);
            }

            DenunciaController controller = new DenunciaController();

            int resultado = controller.ValidarDenuncia(titulo, descricao, categoria, denunciante, denunciado);

            switch (resultado)
            {
                case -400:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Preencha os dados corretamente');", true);

                    break;

                case -500:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O sistema não está funcionando no momento, tente novamente mais tarde');", true);

                    break;

                default:

                    if (resultado <= 0)
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O sistema não está funcionando no momento, tente novamente mais tarde');", true);

                    }

                    if (fuFotos.PostedFile.ContentLength != 0)
                    {

                        SalvarImagens(resultado);

                    }

                break;
            }
        }

        private void ValidarImagem(HttpPostedFile arquivo)
        {
            string extensao = Path.GetExtension(arquivo.FileName).ToLowerInvariant();

            string[] extensoesPermitidas =
            {
                ".jpg", ".jpeg", ".png", ".webp"
            };

            if (!extensoesPermitidas.Contains(extensao))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Formato inválido. Utilize JPG, JPEG, PNG ou WEBP');", true);
            }

            int tamanhoMaximo = 5 * 1024 * 1024;

            if (arquivo.ContentLength > tamanhoMaximo)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Cada imagem deve possuir no máximo 5 MB.');", true);
            }
        }

        private void SalvarImagens(int resultado)
        {
            FotoDenunciaController controller = new FotoDenunciaController();

            foreach (HttpPostedFile arquivo in fuFotos.PostedFiles)
            {
                string extensao = Path.GetExtension(arquivo.FileName).ToLower();

                string nomeArquivo = Guid.NewGuid().ToString() + extensao;

                string pasta = Server.MapPath("~/Uploads/Denuncia/");

                if (!Directory.Exists(pasta))
                {
                    Directory.CreateDirectory(pasta);
                }

                string caminhoFisico = Path.Combine(pasta, nomeArquivo);

                arquivo.SaveAs(caminhoFisico);

                string caminhoBanco = "~/Uploads/Denuncia/" + nomeArquivo;

                FotoDenuncia foto = new FotoDenuncia();

                foto.CaminhoFoto = caminhoBanco;
                foto.DenunciaID = resultado;

                controller.Cadastrar(foto);
            }
        }
    }
}