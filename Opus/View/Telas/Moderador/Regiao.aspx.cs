using Opus.Controller;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                }
                CarregarGrid();
            }
        }

        private void CarregarGrid()
        {
            RegiaoController controller = new RegiaoController();

            gvRegioes.DataSource = controller.ListarRegioes();

            gvRegioes.DataBind();
        }

        protected void gvRegioes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvRegioes.DataKeys[e.RowIndex]["ID"]);

            RegiaoController controller = new RegiaoController();

            controller.ExcluirRegiao(id);

            CarregarGrid();
        }

        protected void gvRegioes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRegioes.EditIndex = e.NewEditIndex;

            CarregarGrid();
        }

        protected void gvRegioes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRegioes.EditIndex = -1;

            CarregarGrid();
        }

        protected void gvRegioes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvRegioes.DataKeys[e.RowIndex].Value);

            GridViewRow linha = gvRegioes.Rows[e.RowIndex];

            string estado = ((TextBox)linha.Cells[1].Controls[0]).Text;

            string cidade = ((TextBox)linha.Cells[2].Controls[0]).Text;

            RegiaoController controller = new RegiaoController();

            int resultado = controller.EditarRegiao(id, estado, cidade);

            switch (resultado)
            {

                case 200:

                    ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Região editada com sucesso!');", true);

                    break;

                case 400:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Preencha todos os campos!');", true);

                    break;

                case 409:

                    ClientScript.RegisterStartupScript(this.GetType(), "Dados já cadastrados", "alert('Região já cadastrada');", true);

                    break;

                case 500:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro do sistema", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;

                default:

                    ClientScript.RegisterStartupScript(this.GetType(), "Erro do sistema", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;
            }

            gvRegioes.EditIndex = -1;

            CarregarGrid();
        }

        public void CadastrarRegiao(object sender, EventArgs e)
        {
            string estado = tbxEstado.Text;
            string cidade = tbxCidade.Text;

            RegiaoController reg = new RegiaoController();

            int resultado = reg.ValidarRegiao(estado, cidade);

            switch (resultado) 
            { 

            case 200:

                ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Região cadastrada com sucesso!');", true);
                Response.Redirect("Regiao.aspx");

                break;

            case 400:

                ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Preencha todos os campos!');", true);

                break;

            case 409:

                ClientScript.RegisterStartupScript(this.GetType(), "Dados já cadastrados", "alert('Região já cadastrada');", true);

                break;

            case 500:

                ClientScript.RegisterStartupScript(this.GetType(), "Erro do sistema", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                break;

            default:

                ClientScript.RegisterStartupScript(this.GetType(), "Erro do sistema", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                break;
            }
        }
    }
}