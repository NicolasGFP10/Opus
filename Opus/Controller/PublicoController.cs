using Opus.DAO;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Controller
{
    public class PublicoController
    {
        public int ValidarMensagem(string email, string texto)
        {
            try
            {
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(texto))
                {
                    return 400;
                }

                Mensagem mensagem = new Mensagem();

                mensagem.Email = email;
                mensagem.Texto = texto;
                mensagem.DataEnvio = DateTime.Now;

                PublicDAO pub = new PublicDAO();

                return pub.EnviarMensagem(mensagem);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 500;
            }
        }

        public List<Mensagem> ListarMensagens()
        {
            PublicDAO dao = new PublicDAO();

            return dao.ListarMensagens();
        }

        public int ExcluirMensagem(int id)
        {
            PublicDAO dao = new PublicDAO();

            return dao.ExcluirMensagem(id);
        }
    }
}