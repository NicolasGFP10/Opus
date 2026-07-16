using Opus.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opus.View.Telas.Usuario
{
    public partial class Cadastrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CadastrarUsuario(object sender, EventArgs e)
        {
            string telefone = Regex.Replace(tbxTelefone.Text, @"\D", "");

            string cpf = Regex.Replace(tbxCPF.Text, @"\D", "");

            UsuarioController usu = new UsuarioController();

            if (!fuImagem.HasFile)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('Cadastre uma imagem!');", true);

            }
            else
            {

                int resultado = usu.ValidarCadastro(tbxNome.Text, tbxEmail.Text, telefone, cpf, tbxSenha.Text, fuImagem.PostedFile);

                switch (resultado)
                {
                    case 200:

                        ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "alert('Cadastro realizado com sucesso!');", true);

                        Response.Redirect("Entrar.aspx");

                    break;

                    case 400:

                        ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('Preencha todos os campos corretamente!');", true);

                    break;

                    case 409:

                        ClientScript.RegisterStartupScript(this.GetType(), "Dados iguais", "alert('E-mail/Telefone/CPF já cadastrados!');", true);

                    break;

                    case 500:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro no Sistema", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;

                    default:

                        ClientScript.RegisterStartupScript(this.GetType(), "Erro inesperado", "alert('O Sistema não está respondendo no momento, tente novamente mais tarde');", true);

                    break;
                }
            }
        }
    }
}