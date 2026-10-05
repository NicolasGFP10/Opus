using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;

namespace Opus.DAO
{
    public class AvaliacaoDAO
    {
        public int Cadastrar(Avaliacao avaliacao)
        {
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
                        cod_ID
                    )
                    VALUES
                    (
                        @titulo,
                        @descricao,
                        @nota,
                        @usuario,
                        @autonomo,
                        @codigo
                    );";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@titulo", avaliacao.Titulo);
                cmd.Parameters.AddWithValue("@descricao", avaliacao.Descricao);
                cmd.Parameters.AddWithValue("@nota", avaliacao.Nota);
                cmd.Parameters.AddWithValue("@usuario", avaliacao.UsuarioID);
                cmd.Parameters.AddWithValue("@autonomo", avaliacao.AutonomoID);
                cmd.Parameters.AddWithValue("@codigo", avaliacao.CodigoID);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }
    }
}