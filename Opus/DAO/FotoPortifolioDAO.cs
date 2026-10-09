using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class FotoPortifolioDAO
    {
        public void Cadastrar(FotoPortifolio foto)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
                    INSERT INTO foto_portfolio
                    (
                        fot_imagem,
                        por_id
                    )
                    VALUES
                    (
                        @imagem,
                        @id
                    );";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@imagem", foto.Caminho);
                cmd.Parameters.AddWithValue("@id", foto.PortifolioID);

                cmd.ExecuteNonQuery();
            }
        }

        public List<FotoPortifolio> ListarFotos(int portifolioID)
        {
            List<FotoPortifolio> lista = new List<FotoPortifolio>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            SELECT
                fot_imagem,
                por_id
            FROM foto_portfolio
            WHERE por_id = @portifolio;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@portifolio", portifolioID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FotoPortifolio foto = new FotoPortifolio();

                            foto.Caminho = reader["fot_imagem"].ToString();
                            foto.PortifolioID = Convert.ToInt32(reader["por_id"]);

                            lista.Add(foto);
                        }
                    }
                }
            }

            return lista;
        }
    }
}