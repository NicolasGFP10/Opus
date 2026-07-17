using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class PublicDAO
    {
        public int EnviarMensagem(Mensagem mensagem)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO mensagem
                                   (men_email,
                                    men_texto,
                                    men_data_envio)

                                   VALUES
                                   (@email,
                                    @texto,
                                    @data)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@email", mensagem.Email);
                    cmd.Parameters.AddWithValue("@texto", mensagem.Texto);
                    cmd.Parameters.AddWithValue("@data", mensagem.DataEnvio);

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

        public List<Mensagem> ListarMensagens()
        {
            List<Mensagem> lista = new List<Mensagem>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql =
                @"SELECT men_ID,
                men_email,
                men_texto,
                men_data_envio
                FROM mensagem
                ORDER BY men_data_envio DESC";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mensagem mensagem = new Mensagem();

                    mensagem.ID = Convert.ToInt32(reader["men_ID"]);
                    mensagem.Email = reader["men_email"].ToString();
                    mensagem.Texto = reader["men_texto"].ToString();
                    mensagem.DataEnvio = Convert.ToDateTime(reader["men_data_envio"]);

                    lista.Add(mensagem);
                }
            }

            return lista;
        }

        public int ExcluirMensagem(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql =
                    @"DELETE FROM mensagem
              WHERE men_ID = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch
            {
                return 500;
            }
        }
    }
}