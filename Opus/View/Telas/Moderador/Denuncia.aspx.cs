using Opus.Controller;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Moderador
{
    public partial class Denuncia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usu_ID"] == null)
            {
                Response.Redirect("~/View/Telas/Usuario/Entrar.aspx");
                return;
            }

            if (Session["mod_ID"] == null)
            {
                Response.Redirect("~/View/Telas/Usuario/Default.aspx");
            }

            if (!IsPostBack)
            {
                CarregarDenuncias(ddlFiltro.SelectedIndex);
            }
        }

        protected void rptDenuncias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Gerenciar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Label status = (Label)e.Item.FindControl("lblStatus");

                DenunciaController controller = new DenunciaController();

                int resultado = controller.AlterarStatus(id, status.Text);

                switch (resultado)
                {
                    case 200:

                        CarregarDenuncias(ddlFiltro.SelectedIndex);
                        
                        ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Status alterados com sucesso');", true);

                        break;

                    case 400:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('Dados incorretos');", true);

                        break;

                    case 500:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro", "alert('O sistema não está respondendo no momento, tente novamente mais tarde');", true);

                        break;
                }
            }
        }

        private void CarregarDenuncias(int filtro)
        {
            try
            {
                DenunciaController controller = new DenunciaController();

                List<DenunciaView> denuncias = controller.ListarDenuncias(filtro);

                rptDenuncias.DataSource = denuncias;

                rptDenuncias.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected void Selection_Change(Object sender, EventArgs e)
        {

            CarregarDenuncias(ddlFiltro.SelectedIndex);

        }
    }
}