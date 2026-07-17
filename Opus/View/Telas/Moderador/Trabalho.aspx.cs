using Opus.Controller;
using Org.BouncyCastle.Crypto.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Moderador
{
    public partial class Trabalho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["mod_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                }

                CarregarGrid();
            }
        }

        protected void EnviarServico(object sender, EventArgs e)
        {

            string servico = tbxServico.Text.ToString();

            ServicoController ser = new ServicoController();

            int resultado = ser.EnviarServico(servico);

            switch (resultado)
            {
                case 200:

                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Serviço cadastrado com sucesso!');", true);

                    break;

                case 400:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Preencha todos os campos!');", true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(this.GetType(), "Dados já cadastrados", "alert('Serviço já cadastrado');", true);

                    break;

                case 500:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro do sistema", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro do sistema", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;
            }
        }

        private void CarregarGrid()
        {
            ServicoController controller = new ServicoController();

            gvServico.DataSource = controller.ListarServicos();

            gvServico.DataBind();
        }

        protected void gvServico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvServico.DataKeys[e.RowIndex].Value);

            ServicoController controller = new ServicoController();

            controller.ExcluirServico(id);

            CarregarGrid();
        }

        protected void btnExcluir(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvServico.DataKeys[e.RowIndex].Value);

            ServicoController controller = new ServicoController();

            int resultado = controller.ExcluirServico(id);

            if (resultado == 200)
            {
                CarregarGrid();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "Sucesso",
                    "alert('Serviço excluído com sucesso!');",
                    true);
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "Erro",
                    "alert('Não foi possível excluir o serviço.');",
                    true);
            }
        }
    }
}