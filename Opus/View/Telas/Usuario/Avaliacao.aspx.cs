using Opus.Controller;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Opus.View.Telas.Usuario
{
    public partial class Avaliacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usu_ID"] == null)
                {
                    Response.Redirect("Entrar.aspx");
                }

                if (Session["aut_ID"] != null)
                {
                    lblTexto.Visible = true;
                    lblCodigo.Visible = true;
                    btnGerar.Visible = true;

                    CarregarAvaliacoes();
                }
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            AvaliacaoController avaliacaoController = new AvaliacaoController();

            int codigoAvaliacao = avaliacaoController.GerarCodigoAvaliacao();

            switch (codigoAvaliacao)
            {
                case 500:
                    lblTexto.Text = "Erro ao gerar código de avaliação, tente novamente mais tarde.";
                    break;

                default:

                    lblTexto.Text = "Gerar código de Avaliação |";
                    lblCodigo.Text = "";
                    lblCodigo.Text = codigoAvaliacao.ToString();

                    break;
            }
        }

        protected void btnAvaliar_Click(object sender, EventArgs e)
        {

            if (tbxCodigoAvaliacao.Text.Trim() == "")
            {
                lblErroCodigo.Text = "Informe o código de avaliação.";
                lblErroCodigo.Visible = true;
                return;
            }

            int codigo = Convert.ToInt32(tbxCodigoAvaliacao.Text);

            AvaliacaoController avaliacaoController = new AvaliacaoController();

            bool codigoValido = avaliacaoController.ValidarCodigo(codigo);

            if (codigoValido)
            {
                lblErroCodigo.Visible = false;

                AbrirModalAvaliacao();
            }
            else
            {
                lblErroCodigo.Text = "Código inválido.";
                lblErroCodigo.Visible = true;
            }
        }

        private void AbrirModalAvaliacao()
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

                int nota = ObterNota();

                if (fuFotos.PostedFiles.Count > 5)
                {
                    throw new Exception("Você pode enviar no máximo 5 imagens.");
                }

                foreach (HttpPostedFile arquivo in fuFotos.PostedFiles)
                {
                    if (arquivo.ContentLength > 0)
                    {
                        ValidarImagem(arquivo);
                    }
                }

                int usuarioID = Convert.ToInt32(Session["usu_id"]);
                string titulo = tbxTitulo.Text.Trim();
                string descricao = tbxDescricao.Text.Trim();
                int usuario = usuarioID;
                int token = Convert.ToInt32(tbxCodigoAvaliacao.Text);

                AvaliacaoController controller = new AvaliacaoController();

                int avaliacaoID = controller.Cadastrar(titulo, descricao, nota, usuario, token);

                SalvarImagens(avaliacaoID);

            }
            catch (Exception ex)
            {
                // Exiba a mensagem da maneira que você já utiliza no projeto
                // Exemplo:
                lblErroCodigo.Text = ex.Message;
                lblErroCodigo.Visible = true;

                // Reabre o modal porque ocorreu PostBack
                AbrirModalAvaliacao();
            }
        }

        private int ObterNota()
        {
            if (rb1.Checked)
                return 1;

            if (rb2.Checked)
                return 2;

            if (rb3.Checked)
                return 3;

            if (rb4.Checked)
                return 4;

            if (rb5.Checked)
                return 5;

            return 0;
        }

        private void ValidarImagem(HttpPostedFile arquivo)
        {
            string extensao = Path.GetExtension(arquivo.FileName).ToLower();

            string[] extensoesPermitidas =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            if (!extensoesPermitidas.Contains(extensao))
            {
                throw new Exception(
                    "Formato de imagem inválido. Utilize JPG, JPEG, PNG ou WEBP."
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

        private void SalvarImagens(int avaliacaoID)
        {
            FotoAvaliacaoController controller =
                new FotoAvaliacaoController();

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
                    Server.MapPath("~/Uploads/Avaliacao/");

                // Garante que a pasta exista
                if (!Directory.Exists(pasta))
                {
                    Directory.CreateDirectory(pasta);
                }

                string caminhoFisico = Path.Combine(pasta, nomeArquivo);

                // Salva a imagem
                arquivo.SaveAs(caminhoFisico);

                // Caminho que será armazenado no banco
                string caminhoBanco =
                    "~/Uploads/Avaliacao/" + nomeArquivo;

                FotoAvaliacao foto = new FotoAvaliacao();

                foto.Imagem = caminhoBanco;
                foto.AvaliacaoID = avaliacaoID;

                controller.Cadastrar(foto);
            }
        }

        private void CarregarAvaliacoes()
        {
            if (Session["aut_ID"] == null)
                return;

            int autonomoID =
                Convert.ToInt32(Session["aut_ID"]);

            AvaliacaoController controller =
                new AvaliacaoController();

            rptAvaliacoes.DataSource =
                controller.ListarAvaliacoesAutonomo(
                    autonomoID
                );

            rptAvaliacoes.DataBind();
        }

        // DENUNCIA

        protected void DenunciarUsuario_Click(object sender, EventArgs e)
        {
            LinkButton botao = (LinkButton)sender;

            if (!int.TryParse(botao.CommandArgument, out int usuarioID) || usuarioID <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Usuário inválido.');", true);

                return;
            }

            hfDenunciadoSelecionado.Value = usuarioID.ToString();

            AbrirModalDenunciaAutonomo();
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

        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {

            string titulo = tbxTituloDenuncia.Text;
            string descricao = tbxDescricaoDenuncia.Text;

            int categoria = Convert.ToInt32(ddlCategoria.SelectedValue);

            if (!int.TryParse(hfDenunciadoSelecionado.Value, out int denunciado) || denunciado <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Não foi possível identificar o usuário denunciado.');", true);

                return;
            }

            int denunciante = Convert.ToInt32(Session["usu_ID"]);

            var arquivos = fuDenuncia.PostedFiles
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
                ValidarImagemDenuncia(arquivo);
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

                        return;
                    }

                    if (arquivos.Count > 0)
                    {
                        SalvarImagensDenuncia(resultado);
                    }

                    if (arquivos.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Denúncia enviada com sucesso');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Denúncia enviada com sucesso, as fotos foram anexadas');", true);
                    }

                    tbxTitulo.Text = "";
                    tbxDescricao.Text = "";
                    ddlCategoria.SelectedIndex = 0;
                    fuDenuncia.Attributes.Clear();

                    break;
            }
        }

        private void ValidarImagemDenuncia(HttpPostedFile arquivo)
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

        private void SalvarImagensDenuncia(int resultado)
        {
            FotoDenunciaController controller = new FotoDenunciaController();

            foreach (HttpPostedFile arquivo in fuDenuncia.PostedFiles)
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