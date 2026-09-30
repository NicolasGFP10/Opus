using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}