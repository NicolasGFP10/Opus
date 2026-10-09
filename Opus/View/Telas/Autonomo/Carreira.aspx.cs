using Opus.Controller;
using Opus.Model;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
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

                // =========================
                // PORTFÓLIO
                // =========================

                CarregarPortifolios();
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
            int id = Convert.ToInt32(gvRegiao.DataKeys[e.RowIndex].Value);

            AutonomoCidadeController controller = new AutonomoCidadeController();

            int resultado = controller.ExcluirCidade(id);

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

        // =====================================================
        // EXCLUIR CIDADE
        // =====================================================

        protected void AbrirModal(object sender, EventArgs e)
        {
            string script = @"
                var modalAvaliacao = new bootstrap.Modal(
                document.getElementById('exampleModalCenter')
                );

                modalAvaliacao.show();
                ";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "AbrirModalAvaliacao",
                script,
                true
            );
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensagem.Visible = false;

                // Verifica se o autônomo está logado
                if (Session["aut_ID"] == null)
                {
                    throw new Exception("Sua sessão expirou. Entre novamente.");
                }

                int autonomoID = Convert.ToInt32(Session["aut_ID"]);

                string descricao = tbxDescricao.Text.Trim();

                if (string.IsNullOrWhiteSpace(descricao))
                {
                    throw new Exception("Informe a descrição do portfólio.");
                }

                // Conta somente arquivos realmente selecionados
                var arquivos = fuFotos.PostedFiles
                    .Cast<HttpPostedFile>()
                    .Where(a => a.ContentLength > 0)
                    .ToList();

                if (arquivos.Count > 5)
                {
                    throw new Exception("Você pode enviar no máximo 5 imagens.");
                }

                // Valida antes de cadastrar
                foreach (HttpPostedFile arquivo in arquivos)
                {
                    ValidarImagem(arquivo);
                }

                PortifolioController controller = new PortifolioController();

                int portifolioID = controller.CadastrarPortifolio(
                    autonomoID,
                    descricao
                );

                switch (portifolioID)
                {
                    case -400:
                        throw new Exception("Preencha todos os dados corretamente.");

                    case -406:
                        throw new Exception("Este portfólio já foi cadastrado.");

                    case -500:
                        throw new Exception("Erro ao cadastrar o portfólio no banco.");

                    default:
                        if (portifolioID <= 0)
                        {
                            throw new Exception("ID do portfólio inválido.");
                        }

                        SalvarImagens(portifolioID);
                        CarregarPortifolios();

                        lblMensagem.Text = "Portfólio cadastrado com sucesso!";
                        lblMensagem.CssClass = "text-success";

                        tbxDescricao.Text = "";

                        break;
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = Server.HtmlEncode(ex.Message);

                AbrirModal(sender, e);
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
                throw new Exception(
                    "Formato inválido. Utilize JPG, JPEG, PNG ou WEBP."
                );
            }

            int tamanhoMaximo = 5 * 1024 * 1024;

            if (arquivo.ContentLength > tamanhoMaximo)
            {
                throw new Exception(
                    "Cada imagem deve possuir no máximo 5 MB."
                );
            }
        }

        private void SalvarImagens(int portifolioID)
        {
            FotoPortifolioController controller = new FotoPortifolioController();

            foreach (HttpPostedFile arquivo in fuFotos.PostedFiles)
            {
                if (arquivo.ContentLength <= 0)
                    continue;

                // Cria um nome único para a imagem
                string extensao =
                    Path.GetExtension(arquivo.FileName).ToLower();

                string nomeArquivo =
                    Guid.NewGuid().ToString() + extensao;

                // Caminho físico
                string pasta =
                    Server.MapPath("~/Uploads/Portifolio/");

                // Garante que a pasta exista
                if (!Directory.Exists(pasta))
                {
                    Directory.CreateDirectory(pasta);
                }

                string caminhoFisico = Path.Combine(pasta, nomeArquivo);

                // Salva a imagem
                arquivo.SaveAs(caminhoFisico);

                // Caminho que será armazenado no banco
                string caminhoBanco = "~/Uploads/Portifolio/" + nomeArquivo;

                FotoPortifolio foto = new FotoPortifolio();

                foto.Caminho = caminhoBanco;
                foto.PortifolioID = portifolioID;

                controller.Cadastrar(foto);
            }
        }

        private void CarregarPortifolios()
        {
            if (Session["aut_ID"] == null)
                return;

            int autonomoID = Convert.ToInt32(Session["aut_ID"]);

            PortifolioController controller = new PortifolioController();

            rptPortifolios.DataSource =
                controller.ListarPortifolios(autonomoID);

            rptPortifolios.DataBind();
        }

        protected void rptPortifolios_ItemCommand(
    object source,
    RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                if (Session["aut_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                    return;
                }

                int portifolioID = Convert.ToInt32(e.CommandArgument);
                int autonomoID = Convert.ToInt32(Session["aut_ID"]);

                PortifolioController controller = new PortifolioController();

                bool excluido = controller.ExcluirPortifolio(
                    portifolioID,
                    autonomoID
                );

                if (excluido)
                {
                    lblMensagem.Text = "Portfólio excluído com sucesso!";
                    lblMensagem.CssClass = "text-success";

                    CarregarPortifolios();
                }
                else
                {
                    lblMensagem.Text = "Não foi possível excluir o portfólio.";
                    lblMensagem.CssClass = "text-danger";
                }
            }
        }
    }
}