using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class CodigoAvaliacaoDAO
    {

        public int ValidarCodigoAvaliacao(int token)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                               FROM codigo_avaliacao
                               WHERE cod_token = @token
                                 AND cod_status = 1";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@token", token);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0 ? 1 : 0;
            }
        }

        public int GerarCodigoAvaliacao()
        {
            try
            {
                Random random = new Random();
                int token = random.Next(100000, 999999);

                if (ValidarCodigoAvaliacao(token) == 1)
                {
                    return GerarCodigoAvaliacao();
                }

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {

                    Usuario usuario = new Usuario();

                    conexao.Open();

                    string sql = @"INSERT INTO codigo_avaliacao (cod_token, cod_status, cod_data_criacao, aut_id) 
                        VALUES (@token, @status, @data, @id)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.Parameters.AddWithValue("@status", true);
                    cmd.Parameters.AddWithValue("@data", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", HttpContext.Current.Session["aut_id"]);

                    cmd.ExecuteNonQuery();

                    return token;
                }

            }
            catch
            {
                return 500;
            }
        }
    }
}