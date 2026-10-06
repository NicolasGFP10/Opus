using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class FotoAvaliacaoDAO
    {
        public void Cadastrar(FotoAvaliacao foto)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
                    INSERT INTO foto_avaliacao
                    (
                        fot_imagem,
                        ava_ID
                    )
                    VALUES
                    (
                        @imagem,
                        @avaliacao
                    );";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@imagem", foto.Imagem);
                cmd.Parameters.AddWithValue("@avaliacao", foto.AvaliacaoID);

                cmd.ExecuteNonQuery();
            }
        }

        public List<FotoAvaliacao> ListarPorAvaliacao(int avaliacaoID)
        {
            List<FotoAvaliacao> lista =
                new List<FotoAvaliacao>();

            using (MySqlConnection conexao =
                Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            SELECT
                fot_ID,
                fot_imagem,
                ava_ID
            FROM foto_avaliacao
            WHERE ava_ID = @avaliacao
            ORDER BY fot_ID;";

                MySqlCommand cmd =
                    new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue(
                    "@avaliacao",
                    avaliacaoID
                );

                using (MySqlDataReader reader =
                    cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FotoAvaliacao foto =
                            new FotoAvaliacao();

                        foto.ID =
                            Convert.ToInt32(reader["fot_ID"]);

                        foto.Imagem =
                            reader["fot_imagem"].ToString();

                        foto.AvaliacaoID =
                            Convert.ToInt32(reader["ava_ID"]);

                        lista.Add(foto);
                    }
                }
            }

            return lista;
        }
    }
}