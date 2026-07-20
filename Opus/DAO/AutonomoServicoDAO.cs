using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class AutonomoServicoDAO
    {

        public bool ServicoJaExiste(int autID, int serID)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                       FROM autonomo_servico
                       WHERE aut_ID=@aut
                       AND ser_ID=@ser";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@aut", autID);
                cmd.Parameters.AddWithValue("@ser", serID);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public List<Servico> ListarServicos()
        {
            List<Servico> lista = new List<Servico>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT ser_ID, ser_nome
                               FROM servico
                               ORDER BY ser_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Servico servico = new Servico();

                    servico.ID = reader.GetInt32("ser_ID");
                    servico.Nome = reader.GetString("ser_nome");

                    lista.Add(servico);
                }
            }

            return lista;
        }

        public int CadastrarServico(int autID, int serID)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO autonomo_servico
                                   (aut_ID, ser_ID)
                                   VALUES
                                   (@aut,@ser)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@aut", autID);
                    cmd.Parameters.AddWithValue("@ser", serID);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch
            {
                return 500;
            }
        }

        public List<AutonomoServico> ListarServicosAutonomo(int autID)
        {
            List<AutonomoServico> lista = new List<AutonomoServico>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT
                                aus_ID,
                                servico.ser_ID,
                                servico.ser_nome

                               FROM autonomo_servico

                               INNER JOIN servico
                               ON autonomo_servico.ser_ID = servico.ser_ID

                               WHERE aut_ID=@aut";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@aut", autID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AutonomoServico item = new AutonomoServico();

                    item.ID = reader.GetInt32("aus_ID");
                    item.ServicoID = reader.GetInt32("ser_ID");
                    item.NomeServico = reader.GetString("ser_nome");

                    lista.Add(item);
                }
            }

            return lista;
        }

        public int ExcluirServico(int ausID)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"DELETE
                                   FROM autonomo_servico
                                   WHERE aus_ID=@id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", ausID);

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