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

        public int EntrarUsuario(string email, string senha)
        {
            try
            {

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {

                    Usuario usuario = new Usuario();

                    conexao.Open();

                    string sql = @"SELECT usu_ID, 
                               usu_nome, 
                               usu_imagem,
                               usu_email,
                               usu_telefone,
                               usu_senha
                               FROM usuario WHERE
                               usu_email = @email AND
                               usu_senha = @senha AND
                               usu_status = 1";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["usu_ID"]);
                        string nome = reader["usu_nome"].ToString();
                        string imagem = reader["usu_imagem"].ToString();
                        string telefone = reader["usu_telefone"].ToString();
                        string emailUsuario = reader["usu_email"].ToString();
                        string senhaUsuario = reader["usu_senha"].ToString();


                        HttpContext.Current.Session["usu_ID"] = id;
                        HttpContext.Current.Session["usu_nome"] = nome;
                        HttpContext.Current.Session["usu_imagem"] = imagem;
                        HttpContext.Current.Session["usu_email"] = emailUsuario;     
                        HttpContext.Current.Session["usu_telefone"] = telefone;
                        HttpContext.Current.Session["usu_senha"] = senha;

                        return 200;
                    }
                    else
                    {
                        return 404;
                    }
                }

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 500;

            }
        }

        public int EditarUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    string sql = @"UPDATE usuario SET
                                   usu_nome = @nome,
                                   usu_email = @email,
                                   usu_telefone = @telefone,
                                   usu_senha = @senha
                                   WHERE usu_ID = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
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

        public int DesativarUsuario(string id)
        {
            try
            {

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"UPDATE usuario
                               SET usu_status = 0
                               WHERE usu_id = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return 200;

            } catch (Exception ex) {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 500;

            }
        }
    }
}