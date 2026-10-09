using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Opus.DAO
{
    public class PortifolioDAO
    {
        public int PortifolioValido(Portifolio portifolio)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*) FROM portfolio WHERE por_descricao = @descricao AND aut_id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", portifolio.AutonomoID);
                cmd.Parameters.AddWithValue("@descricao", portifolio.Descricao);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    return -406;
                }

                return count;
            }
        }

        public int ConsultarID(Portifolio portifolio)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT por_id FROM portfolio WHERE por_descricao = @descricao AND aut_id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", portifolio.AutonomoID);
                cmd.Parameters.AddWithValue("@descricao", portifolio.Descricao);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count;
            }
        }

        public int CadastrarPortifolio(Portifolio portifolio)
        {
            try
            {
                if (PortifolioValido(portifolio) == -406)
                {
                    return -406;
                }

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"
                INSERT INTO portfolio
                (
                    por_descricao,
                    aut_id
                )
                VALUES
                (
                    @descricao,
                    @id
                );";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@descricao", portifolio.Descricao);
                        cmd.Parameters.AddWithValue("@id", portifolio.AutonomoID);

                        cmd.ExecuteNonQuery();

                        return Convert.ToInt32(cmd.LastInsertedId);
                    }
                }
            }
            catch
            {
                return -500;
            }
        }

        public List<PortifolioView> ListarPortifolios(int autonomoID)
        {
            List<PortifolioView> lista = new List<PortifolioView>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            SELECT
                por_id,
                por_descricao,
                aut_id
            FROM portfolio
            WHERE aut_id = @autonomo
            ORDER BY por_id DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@autonomo", autonomoID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PortifolioView portifolio = new PortifolioView();

                            portifolio.ID = Convert.ToInt32(reader["por_id"]);
                            portifolio.Descricao = reader["por_descricao"].ToString();
                            portifolio.AutonomoID = Convert.ToInt32(reader["aut_id"]);

                            lista.Add(portifolio);
                        }
                    }
                }
            }

            return lista;
        }

        public bool ExcluirPortifolio(int portifolioID, int autonomoID)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            DELETE FROM portfolio
            WHERE por_id = @portifolio
            AND aut_id = @autonomo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@portifolio", portifolioID);
                    cmd.Parameters.AddWithValue("@autonomo", autonomoID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}