using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class AvaliacaoDAO
    {
        public int ConsultarCodigoAutonomo(int token)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
                    SELECT aut_ID
                    FROM codigo_avaliacao
                    WHERE cod_token = @token;";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@token", token);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Cadastrar(Avaliacao avaliacao)
        {

            int codigoAutonomo = ConsultarCodigoAutonomo(avaliacao.Token);

            if (codigoAutonomo == 0)
            {
                throw new Exception("Erro na busca.");
            }

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
                    INSERT INTO avaliacao
                    (
                        ava_titulo,
                        ava_descricao,
                        ava_nota,
                        usu_ID,
                        aut_ID,
                        cod_token
                    )
                    VALUES
                    (
                        @titulo,
                        @descricao,
                        @nota,
                        @usuario,
                        @autonomo,
                        @token
                    );";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@titulo", avaliacao.Titulo);
                cmd.Parameters.AddWithValue("@descricao", avaliacao.Descricao);
                cmd.Parameters.AddWithValue("@nota", avaliacao.Nota);
                cmd.Parameters.AddWithValue("@usuario", avaliacao.UsuarioID);
                cmd.Parameters.AddWithValue("@autonomo", codigoAutonomo);
                cmd.Parameters.AddWithValue("@token", avaliacao.Token);

                cmd.ExecuteNonQuery();

                CodigoAvaliacaoDAO dao = new CodigoAvaliacaoDAO();

                dao.DesativarToken(avaliacao.Token);

                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        public List<AvaliacaoView> ListarAvaliacoesAutonomo(int autonomoID)
        {
            List<AvaliacaoView> lista = new List<AvaliacaoView>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            SELECT
                a.ava_ID,
                a.ava_titulo,
                a.ava_descricao,
                a.ava_nota,
                a.usu_ID,
                a.aut_ID,
                u.usu_nome
            FROM avaliacao a

            INNER JOIN usuario u
                ON a.usu_ID = u.usu_ID

            WHERE a.aut_ID = @autonomo

            ORDER BY a.ava_ID DESC;";

                MySqlCommand cmd =
                    new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue(
                    "@autonomo",
                    autonomoID
                );

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AvaliacaoView avaliacao =
                            new AvaliacaoView();

                        avaliacao.ID =
                            Convert.ToInt32(reader["ava_ID"]);

                        avaliacao.Titulo =
                            reader["ava_titulo"].ToString();

                        avaliacao.Descricao =
                            reader["ava_descricao"].ToString();

                        avaliacao.Nota =
                            Convert.ToInt32(reader["ava_nota"]);

                        avaliacao.UsuarioID =
                            Convert.ToInt32(reader["usu_ID"]);

                        avaliacao.NomeUsuario =
                            reader["usu_nome"].ToString();

                        avaliacao.AutonomoID =
                            Convert.ToInt32(reader["aut_ID"]);

                        lista.Add(avaliacao);
                    }
                }
            }

            return lista;
        }
    }
}