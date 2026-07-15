using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.IO;
using System.Web;

namespace Opus.DAO
{
    public class UsuarioDAO
    {

        public bool UsuarioExiste(string email, string telefone, string cpf)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                       FROM usuario
                       WHERE usu_email = @email
                          OR usu_telefone = @telefone
                          OR usu_CPF = @cpf";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.Parameters.AddWithValue("@cpf", cpf);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        public int CadastrarUsuario(Usuario usuario, HttpPostedFile imagem)
        {
            try
            {
                string nomeImagem = Guid.NewGuid().ToString() +
                                    Path.GetExtension(imagem.FileName);

                string pasta = HttpContext.Current.Server.MapPath("~/Uploads/Usuario/");

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                imagem.SaveAs(Path.Combine(pasta, nomeImagem));

                usuario.Imagem = nomeImagem;

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO usuario
                                   (usu_nome,
                                    usu_email,
                                    usu_telefone,
                                    usu_CPF,
                                    usu_senha,
                                    usu_imagem,
                                    usu_data_cadastro,
                                    usu_status)

                                   VALUES
                                   (@nome,
                                    @email,
                                    @telefone,
                                    @cpf,
                                    @senha,
                                    @imagem,
                                    @data,
                                    @status)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
                    cmd.Parameters.AddWithValue("@cpf", usuario.CPF);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@imagem", usuario.Imagem);
                    cmd.Parameters.AddWithValue("@data", usuario.DataCadastro);
                    cmd.Parameters.AddWithValue("@status", usuario.Status);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 500;

            }
        }
    }
}