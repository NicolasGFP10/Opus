using MySqlConnector;
using Opus.Data;
using Opus.Model;

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
    }
}