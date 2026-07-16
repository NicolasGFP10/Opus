using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class AutonomoDAO
    {

        public bool AutonomoExiste(string cnpj, string email, string telefone)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                       FROM autonomo
                       WHERE aut_cnpj = @cnpj
                          OR aut_email_corp = @email
                          OR aut_telefone_corp = @telefone;";
                          

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@cnpj", cnpj);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefone", telefone);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        public int CadastrarAutonomo(Autonomo autonomo)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO autonomo
                                   (aut_CNPJ,
                                    aut_email_corp,
                                    aut_telefone_corp,
                                    aut_descricao,
                                    aut_data_cadastro,
                                    aut_status,
                                    usu_ID)

                                   VALUES
                                   (@cnpj,
                                    @email,
                                    @telefone,
                                    @descricao,
                                    @data,
                                    @status,
                                    @ID)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@cnpj", autonomo.CNPJ);
                    cmd.Parameters.AddWithValue("@email", autonomo.EmailCorp);
                    cmd.Parameters.AddWithValue("@telefone", autonomo.TelefoneCorp);
                    cmd.Parameters.AddWithValue("@descricao", autonomo.Descricao);
                    cmd.Parameters.AddWithValue("@data", autonomo.DataCadastro);
                    cmd.Parameters.AddWithValue("@status", autonomo.Status);
                    cmd.Parameters.AddWithValue("@ID", autonomo.usu_ID);

                    cmd.ExecuteNonQuery();
                }

                return 200;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public void EntrarAutonomo(int id)
        {

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {

                conexao.Open();

                string sql = @"SELECT aut_ID, 
                               aut_email_corp, 
                               aut_telefone_corp,
                               aut_descricao
                               FROM autonomo WHERE
                               usu_ID = @id AND
                               aut_status = 1;";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int idAut = Convert.ToInt32(reader["aut_ID"]);
                    string emailCorp = reader["aut_email_corp"].ToString();
                    string telefoneCorp = reader["aut_telefone_corp"].ToString();
                    string descricao = reader["aut_descricao"].ToString();

                    HttpContext.Current.Session["aut_ID"] = idAut;
                    HttpContext.Current.Session["aut_email_corp"] = emailCorp;
                    HttpContext.Current.Session["aut_telefone_corp"] = telefoneCorp;
                    HttpContext.Current.Session["aut_descricao"] = descricao;

                }
            }
        }

        public int EditarAutonomo(Autonomo autonomo)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {

                    conexao.Open();

                    string sql = @"UPDATE autonomo
                                   SET aut_telefone_corp = @telefone,
                                       aut_email_corp = @email,
                                       aut_descricao = @descricao
                                       WHERE aut_ID = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@telefone", autonomo.TelefoneCorp);
                    cmd.Parameters.AddWithValue("@email", autonomo.EmailCorp);
                    cmd.Parameters.AddWithValue("@descricao", autonomo.Descricao);
                    cmd.Parameters.AddWithValue("@id", autonomo.ID);
                    cmd.ExecuteNonQuery();
                }
                return 200;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public void DesativarAutonomo(string id)
        {
            try
            {

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"UPDATE autonomo
                               SET aut_status = 0
                               WHERE aut_ID = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());

            }
        }
    }
}