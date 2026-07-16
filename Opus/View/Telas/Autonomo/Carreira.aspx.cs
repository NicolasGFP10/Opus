using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Autonomo
{
    public partial class Carreira : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["aut_ID"] == null)
                {
                    Response.Redirect("../Usuario/Entrar.aspx");
                }
            }
        }
    }
}